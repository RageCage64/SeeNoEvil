using System.Collections.Generic;
using System.Linq;
using SeeNoEvil.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeeNoEvil.Level {
    public class TilemapLevel {
        private readonly string MapName;
        private TiledMap Map;

        public TilemapLevel(string tilemapName) {
            MapName = tilemapName;
        }

        public void LoadMap() {
			Map = TiledParser.ReadMapJson(MapName);
		} 

		public IEnumerable<string> GetTilesetNames() {
			return Map.GetTilesetNames();
		}

		public void Draw(Camera viewport, SpriteBatch spritebatch, IDictionary<string, Texture2D> this_sucks) {
			IEnumerable<TileLocation> locations = Map.DrawView(viewport);
			locations.ToList().ForEach(tile => {
				Texture2D this_one;
				if(tile.tile.gid > 0 && this_sucks.TryGetValue(tile.tile.setName, out this_one)) {
					spritebatch.Draw(this_one, tile.location, tile.tile.srcRectangle, Color.White);
				}
			});
		}
    }
}