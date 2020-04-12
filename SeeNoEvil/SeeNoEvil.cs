using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SeeNoEvil.Tiled;
using SeeNoEvil.Level;

namespace SeeNoEvil
{
    public class SeeNoEvilGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TilemapLevel level;
        Camera camera;

        public SeeNoEvilGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            level = new TilemapLevel("./Maps/MagicLandCsv.json");
            // level = new TilemapLevel("./Maps/test.json");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            level.LoadMap(Content);
            camera = new Camera(GraphicsDevice.Viewport); 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int xVelocity = 0, yVelocity = 0;
            if(Keyboard.GetState().IsKeyDown(Keys.Down))
                yVelocity = 1;
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
                yVelocity = -1;
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
                xVelocity = -1;
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
                xVelocity = 1;
            camera.Update(Vector2.Zero, new Vector2(xVelocity, yVelocity));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(
                transformMatrix: camera.Transform
            );
            level.Draw(spriteBatch, "background");
            // level.Draw(spriteBatch, "Tile Layer 1");
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
