using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SeeNoEvil.Tiled;
using SeeNoEvil.Level;

namespace SeeNoEvil
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TilemapLevel level;
        Dictionary<string, Texture2D> tilesets;
        Camera viewport;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
			viewport.height = 720;
			viewport.width = 1280;
			viewport.x = 0;
			viewport.y = 0;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            level = new TilemapLevel("./Maps/MagicLandCsv.json");
            level.LoadMap();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tilesets = new Dictionary<string, Texture2D>();
            tilesets = level.GetTilesetNames().Aggregate(new Dictionary<string, Texture2D>(),
                (content, contentName) => { 
                        content.Add(contentName, Content.Load<Texture2D>(contentName));
                        return content;
                });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Keyboard.GetState().IsKeyDown(Keys.Down))
                viewport.y += 16;
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
                viewport.y -= 16;
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
                viewport.x -= 16;
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
                viewport.x += 16;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            level.Draw(viewport, spriteBatch, tilesets);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
