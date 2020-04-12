using System.Collections.Generic;
using System.Linq;
using SeeNoEvil.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SeeNoEvil.Level {
    public class TilemapLevel {
        private readonly string MapName;
        private TiledMap Map;
		private Dictionary<string, Texture2D> tilesetTextures;

        public TilemapLevel(string tilemapName) {
            MapName = tilemapName;
        }

        public void LoadMap(ContentManager Content) {
			Map = new TiledMap(TiledParser.ReadMapJson(MapName));
			Map.LoadView();
			tilesetTextures = Map.GetTilesetNames().Aggregate(new Dictionary<string, Texture2D>(),
				(content, contentName) => {
					content.Add(contentName, Content.Load<Texture2D>(contentName));
					return content;
				});
		} 

		public IEnumerable<string> GetTilesetNames() {
			return Map.GetTilesetNames();
		}

		public void Draw(SpriteBatch spritebatch, string layer) {
			List<TileLocation> locations;
			if(Map.View.TryGetValue(layer, out locations)) 
				locations.ForEach(tile => {
					Texture2D layerTexture;
					if(tile.tile.gid > 0 && tilesetTextures.TryGetValue(tile.tile.setName, out layerTexture)) {
						spritebatch.Draw(layerTexture, tile.location, tile.tile.srcRectangle, Color.White);
					}	
				});
		}
    }
}