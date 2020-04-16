using System.Collections.Generic;

namespace SeeNoEvil.Tiled {
    public struct TiledModel {
        public bool Infinite {get; set;}
        public IEnumerable<MapLayerModel> Layers {get; set;}
		public IEnumerable<TileSetModel> TileSets {get; set;}
		public int TileHeight {get; set;}
		public int TileWidth {get; set;}
		public int Height {get; set;}
		public int Width {get; set;}
    }

	public struct TileSetModel {
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

	public struct MapLayerModel {
		public string Name {get; set;}
		public int Width {get; set;}
		public int Height {get; set;}
		public IEnumerable<uint> Data {get; set;}
		public string Type {get; set;}
		public IEnumerable<TiledObjectModel> Objects {get; set;}
    }

    public struct TiledObjectModel {
		public int Id {get; set;}
		public float X {get; set;}
		public float Y {get; set;}
    }
}