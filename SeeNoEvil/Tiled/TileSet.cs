using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SeeNoEvil.Tiled {
    public class TileSet {

        private string Image {get; set;}
		private string Name {get; set;}
		private int ImageHeight {get; set;}
		private int ImageWidth {get; set;}
		private int TileHeight {get; set;}
		private int TileWidth {get; set;}
		private int Spacing {get; set;}
		private int Columns {get; set;}
		private int FirstGid {get; set;}
		private int TileCount {get; set;}
		private Dictionary<uint, Tile> Tiles {get; set;}
		private bool TilesLoaded {get; set;}

		public TileSet(TileSetModel model) {
			Image = model.Image;
			Name = model.Name;
			ImageHeight = model.ImageHeight;
			ImageWidth = model.ImageWidth;
			TileHeight = model.TileHeight;
			TileWidth = model.TileWidth;
			Spacing = model.Spacing;
			Columns = model.Columns;
			FirstGid = model.FirstGid;
			TileCount = model.TileCount;
			Tiles = new Dictionary<uint, Tile>();
			TilesLoaded = false;
		}

        public bool ContainsTile(uint gid) => 
			!(gid < FirstGid || 
			gid > (FirstGid + TileCount - 1));

		public Tile GetTile(uint gid) {
			if(!TilesLoaded) LoadTiles();
			Tile result;
			Tiles.TryGetValue(gid, out result);
			return result;
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
}
