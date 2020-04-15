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
		private Dictionary<string, Texture2D> TilesetTextures;

        public TilemapLevel(string tilemapName) {
            MapName = tilemapName;
        }

        public void LoadMap(ContentManager content) {
			Map = new TiledMap(TiledParser.ReadMapJson(MapName));
			Map.LoadView();
			TilesetTextures = Map.GetTilesetNames().Aggregate(new Dictionary<string, Texture2D>(),
				(textures, contentName) => {
					textures.Add(contentName, content.Load<Texture2D>(contentName));
					return textures;
				});
		} 

		private IEnumerable<string> GetTilesetNames() {
			return Map.GetTilesetNames();
		}

		public void Draw(SpriteBatch spriteBatch, string layer) {
			List<TileLocation> locations;
			if(Map.View.TryGetValue(layer, out locations)) 
				locations.ForEach(tile => {
					Texture2D layerTexture;
					if(tile.tile.gid > 0 && TilesetTextures.TryGetValue(tile.tile.setName, out layerTexture)) {
						spriteBatch.Draw(layerTexture, tile.location, tile.tile.srcRectangle, Color.White);
					}	
				});
		}
    }
}