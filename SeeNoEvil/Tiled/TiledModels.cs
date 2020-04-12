using System.Collections.Generic;

namespace SeeNoEvil.Tiled {
    public class TiledModel {
        public TiledModel() {}

        public bool Infinite {get; set;}
        public IEnumerable<MapLayerModel> Layers {get; set;}
		public IEnumerable<TileSetModel> TileSets {get; set;}
		public int TileHeight {get; set;}
		public int TileWidth {get; set;}
		public int Height {get; set;}
		public int Width {get; set;}
    }

	public class TileSetModel {
		public TileSetModel() {}

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
    }


	public class MapLayerModel {
        public MapLayerModel() { }

		public string Name {get; set;}
		public int Width {get; set;}
		public int Height {get; set;}
		public IEnumerable<uint> Data {get; set;}
		public string Type {get; set;}
    }
}