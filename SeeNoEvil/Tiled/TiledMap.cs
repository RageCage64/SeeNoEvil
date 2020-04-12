using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using SeeNoEvil.Level;

namespace SeeNoEvil.Tiled {
    public class TiledMap {
		public Dictionary<string, List<TileLocation>> View {get; private set;}
        private bool Infinite;
        private List<MapLayer> Layers;
		private List<TileSet> TileSets;
		private int TileHeight;
		private int TileWidth;
		private int Rows;
		private int Columns;
		// TODO Do I really need this?
		private Dictionary<uint, Tile> TileCache;

        public TiledMap(TiledModel model) {
			Infinite = model.Infinite;	
			Layers = model.Layers.Select(model => new MapLayer(model)).ToList();
			TileSets = model.TileSets.Select(model => new TileSet(model)).ToList();
			TileHeight = model.TileHeight;
			TileWidth = model.TileWidth;
			Rows = model.Height;
			Columns = model.Width;
			TileCache = new Dictionary<uint, Tile>();
			View = new Dictionary<string, List<TileLocation>>();
		}

		public IEnumerable<string> GetTilesetNames() =>
			TileSets.Select(tileset => tileset.GetContentName());

		// Load Tile Locations for each layer
		public void LoadView() {
			// Get all tilelayers
			Dictionary<string, List<DataCoordinate>> layerData = new Dictionary<string, List<DataCoordinate>>();
			Layers.Where(layer => layer.Type == "tilelayer").ToList()
				.ForEach(layer => {
					layerData.Add(layer.Name, layer.DataCoordinates);
				});

			// Get all required unique gids and load to cache
			LoadTiles(layerData.Aggregate(new HashSet<uint>(), 
				(tiles, layer) => {
					foreach(var tile in layer.Value) {
						tiles.Add(tile.gid);
					}
					return tiles;
				})
			);

			// build resulting tile location collection for each layer
			View = layerData.Aggregate(new Dictionary<string, List<TileLocation>>(),
				(locations, layer) => {
					int column = 0, row = 0, columnPx = 0, rowPx = 0;
					var layerLocations = new List<TileLocation>();
					layer.Value.ForEach(coord => {
						Tile currTile = new Tile();
						if(coord.gid > 0) 
							TileCache.TryGetValue(coord.gid, out currTile);
						Vector2 pxLocation = new Vector2(columnPx, rowPx);
						layerLocations.Add(new TileLocation(currTile, pxLocation));
						if(column == (Columns-1)) {
							row++;
							rowPx += TileHeight;
							column = columnPx = 0;
						} else {
							column++;
							columnPx += TileWidth;
						}
					});
					locations.Add(layer.Key, layerLocations);
					return locations;
				});
		}

		// Load tiles into cache if they don't exist
        private void LoadTiles(HashSet<uint> tiles) {
			foreach(uint gid in tiles) {
				if(!TileCache.ContainsKey(gid)) {
					TileSet tileset = TileSets.FirstOrDefault(set => set.ContainsTile(gid));
					Tile tile = tileset == null ? new Tile() : tileset.GetTile(gid);
					TileCache.Add(gid, tile);
				}
			}
		}
    }
}
