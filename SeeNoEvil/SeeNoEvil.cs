using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SeeNoEvil.Tiled;
using SeeNoEvil.Level;
using SeeNoEvil.Character;

namespace SeeNoEvil
{
    public class SeeNoEvilGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TilemapLevel level;
        Camera camera;
        Cat cat;

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
            cat = new Cat(Vector2.Zero, Direction.Right);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            level.LoadMap(Content);
            cat.Load(Content);
            camera = new Camera(GraphicsDevice.Viewport); 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int xVelocity = 0, yVelocity = 0;
            // if(Keyboard.GetState().IsKeyDown(Keys.Down))
            //     yVelocity = 1;
            // if(Keyboard.GetState().IsKeyDown(Keys.Up))
            //     yVelocity = -1;
            // if(Keyboard.GetState().IsKeyDown(Keys.Left))
            //     xVelocity = -1;
            // if(Keyboard.GetState().IsKeyDown(Keys.Right))
            //     xVelocity = 1;
            camera.Update(Vector2.Zero, new Vector2(xVelocity, yVelocity));

            if(Keyboard.GetState().IsKeyDown(Keys.Down))
                cat.Move(Direction.Down);
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
                cat.Move(Direction.Up);
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
                cat.Move(Direction.Left);
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
                cat.Move(Direction.Right);
            cat.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(
                transformMatrix: camera.Transform
            );
            // level.Draw(spriteBatch, "background");
            cat.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
