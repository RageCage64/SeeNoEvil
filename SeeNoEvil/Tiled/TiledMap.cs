using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using SeeNoEvil.Level;
using System.Text.Json.Serialization;

namespace SeeNoEvil.Tiled {
    public class TiledMap {
        public TiledMap() => TileCache = new Dictionary<uint, Tile>();

        public bool Infinite {get; set;}
        public IEnumerable<MapLayer> Layers {get; set;}
		public IEnumerable<TileSet> TileSets {get; set;}
		public int TileHeight {get; set;}
		public int TileWidth {get; set;}
		private Dictionary<uint, Tile> TileCache {get; set;}

		public IEnumerable<TileLocation> DrawView(Camera viewport) {
			Dictionary<string, IEnumerable<DataCoordinate>> layerData = new Dictionary<string, IEnumerable<DataCoordinate>>();
			Layers.Where(layer => layer.Type == "tilelayer").ToList()
				.ForEach(layer => {
					layerData.Add(layer.Name, layer.GetVisibleTiles(viewport, TileHeight, TileWidth));
				});
			HashSet<uint> gids = layerData.Aggregate(new HashSet<uint>(), 
				(tiles, layer) => {
					layer.Value.ToList().ForEach(tile => tiles.Add(tile.gid));
					return tiles;
				});
			IDictionary<uint, Tile> tiles = GetTilesetTiles(gids);
			// return layerData.Aggregate(new List<TileLocation>(),
			// 	(locations, layer) => {
			// 		layer.Value.ToList().ForEach(coord => {
			// 			Tile currTile;
			// 			if(!tiles.TryGetValue(coord.gid, out currTile)) {
			// 				currTile = new Tile();
			// 			}
			// 			Vector2 pxLocation = new Vector2(coord.x*TileWidth, coord.y*TileHeight);
			// 			locations.Add(new TileLocation(currTile, pxLocation));
			// 		});
			// 		return locations;
			// 	});
			int column = 0, row = 0, columnPx = 0, rowPx = 0;
			int viewportRows = viewport.height / TileHeight;
			int viewportColumns = viewport.width / TileWidth;
			return layerData.Aggregate(new List<TileLocation>(),
				(locations, layer) => {
					layer.Value.ToList().ForEach(coord => {
						Tile currTile;
						if(!tiles.TryGetValue(coord.gid, out currTile)) {
							currTile = new Tile();
						}
						Vector2 pxLocation = new Vector2(columnPx, rowPx);
						locations.Add(new TileLocation(currTile, pxLocation));
						if(column == viewportColumns) {
							row++;
							rowPx += TileHeight;
							column = columnPx = 0;
						} else {
							column++;
							columnPx += TileWidth;
						}
					});
					return locations;
				});
		}

		public IEnumerable<string> GetTilesetNames() =>
			TileSets.Aggregate(new List<string>(), 
				(names, tileset) => {
					names.Add(tileset.GetContentName());
					return names;
				});

		private IDictionary<uint, Tile> GetTilesetTiles(HashSet<uint> tiles) {
			Dictionary<uint, Tile> loadedTiles = new Dictionary<uint, Tile>();
            tiles.ToList().ForEach(gid => {
				TileSet tileset = TileSets.FirstOrDefault(set => set.ContainsTile(gid));
				Tile tile = tileset == null ? new Tile() : tileset.GetTile(gid);
				loadedTiles.Add(gid, tile);
			});
			return loadedTiles;
		}
    }

	public struct TileLocation {
        public TileLocation(Tile tile, Vector2 location) {
            this.tile = tile;
            this.location = location;
        }

		public Tile tile;
		public Vector2 location;
    }

	public struct Tile {
		public Tile(long _gid, Rectangle _srcRectangle, string _setName) {
			gid = (uint)_gid;
			srcRectangle = _srcRectangle;
			setName = _setName;
		}
		public uint gid;
		public Rectangle srcRectangle;
		public string setName;
	}

	public class TileSet {
		public TileSet() {
			TilesLoaded = false;
		}

        public string Image {get; set;}
		public string Name {get; set;}
		public int ImageHeight {get; set;}
		public int ImageWidth {get; set;}
		public int TileHeight {get; set;}
		public int TileWidth {get; set;}
		public int Spacing {get; set;}
		public int Columns {get; set;}
		public int FirstGid {get; set;}
		public int TileCount {get; set;}
		private IDictionary<uint, Tile> Tiles {get; set;}
		private bool TilesLoaded {get; set;}

		public Tile GetTile(uint gid) {
			if(!TilesLoaded) {
				LoadTiles();
			}
			Tile result;
			Tiles.TryGetValue(gid, out result);
			return result;
		}

		public bool ContainsTile(uint gid) {
			return !(gid < FirstGid || 
					gid > (FirstGid + TileCount - 1));
		}

		public string GetContentName() {
			return Name;
		}

		private void LoadTiles() {
			Tiles = new Dictionary<uint, Tile>();
			int row = 0;
			int rowPx = 0;
			int column = 0;
			int columnPx = 0;
			for(int i = 0; i < TileCount; i++) {
				Rectangle rectangle = new Rectangle(
					columnPx,
					rowPx,
                    TileWidth,
                    TileHeight
				);
				uint gid = (uint)(i + FirstGid);
				Tiles.Add(gid, new Tile(gid, rectangle, Name));
				if(column == Columns-1) {
					row++;
					rowPx += Spacing + TileHeight;
					column = 0;
					columnPx = 0;
				} else {
					column++;
					columnPx += Spacing + TileWidth;
				}
			}

			TilesLoaded = true;
		}
	}

	public struct DataCoordinate {
		public DataCoordinate(int _x, int _y, uint _gid) {
			x = _x;
			y = _y;
			gid = _gid;
		}

		public int x;
		public int y;
		public uint gid;
	}

	public class MapLayer {
        public MapLayer() { }

		public string Name {get; set;}
		public int Width {get; set;}
		public int Height {get; set;}
		public IEnumerable<uint> Data {get; set;}
		public string Type {get; set;}
		public IEnumerable<DataCoordinate> DataCoordinates {get; set;}
		public bool BuiltCoordinates {get; private set;}

		public IEnumerable<DataCoordinate> GetVisibleTiles(Camera viewport, int tileHeight, int tileWidth) {
			if(!BuiltCoordinates) {
				BuildCoordinates();
			}
			int startColumn = viewport.x / tileWidth;
			int endColumn = startColumn + (viewport.width / tileWidth);
			int startRow = viewport.y / tileHeight;
			int endRow = startRow + (viewport.height / tileHeight);

			var coords = DataCoordinates.Where(coord =>
				startRow <= coord.y &&
				endRow >= coord.y &&
				startColumn <= coord.x &&
				endColumn >= coord.x
			).ToList();

			return DataCoordinates.Where(coord =>
				startRow <= coord.y &&
				endRow >= coord.y &&
				startColumn <= coord.x &&
				endColumn >= coord.x
			);
		}

		private void BuildCoordinates() {
			int row = 0;
			int column = 0;
			List<DataCoordinate> coordinates = new List<DataCoordinate>();
			Data.ToList().ForEach(gid => {
				coordinates.Add(new DataCoordinate(column, row, gid));
				if(column == Width-1) {
					row++;
					column = 0;
				} else column++;
			});
			DataCoordinates = coordinates;
			BuiltCoordinates = true;
		}
    }    
}
