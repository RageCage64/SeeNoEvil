using System.Collections.Generic;
using System.Linq;
using SeeNoEvil.Level;

namespace SeeNoEvil.Tiled {
    public class MapLayer {

		public string Type {get; private set;}
		public string Name {get; private set;}
		public readonly List<DataCoordinate> DataCoordinates = new List<DataCoordinate>();

		private int Width;
		private int Height;
		private List<uint> Data;

        public MapLayer(MapLayerModel model) {
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

		// TODO Remember to delete this if it's garbo
        // public IEnumerable<DataCoordinate> GetVisibleTiles(Camera viewport, int tileHeight, int tileWidth) {
		// 	if(!BuiltCoordinates) BuildCoordinates();

		// 	int startColumn = viewport.x / tileWidth;
		// 	int endColumn = startColumn + (viewport.width / tileWidth);
		// 	int startRow = viewport.y / tileHeight;
		// 	int endRow = startRow + (viewport.height / tileHeight);

		// 	return DataCoordinates.Where(coord =>
		// 		startRow <= coord.y &&
		// 		endRow >= coord.y &&
		// 		startColumn <= coord.x &&
		// 		endColumn >= coord.x
		// 	);
		// }

    }    
}
