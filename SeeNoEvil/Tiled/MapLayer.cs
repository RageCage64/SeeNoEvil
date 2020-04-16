// TODO Handle flipped tiles!
using System.Collections.Generic;
using System.Linq;
using SeeNoEvil.Level;

namespace SeeNoEvil.Tiled {
    public class TileLayer {

		public string Type {get; private set;}
		public string Name {get; private set;}
		public readonly List<DataCoordinate> DataCoordinates = new List<DataCoordinate>();

		private int Width;
		private int Height;
		private List<uint> Data;

        public TileLayer(MapLayerModel model) {
			Name = model.Name;
			Width = model.Width;
			Height = model.Height;
			Type = model.Type;
			if(model.Type == "tilelayer") {
				Data = model.Data.ToList();
				BuildCoordinates();
			}
		}

		private void BuildCoordinates() {
			int row = 0;
			int column = 0;
			Data.ForEach(gid => {
				DataCoordinates.Add(new DataCoordinate(column, row, gid));
				if(column == Width-1) {
					row++;
					column = 0;
				} else column++;
			});
		}
    }    
}
