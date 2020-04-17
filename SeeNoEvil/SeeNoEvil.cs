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
        Cat player;
        GhostController enemyController;
        PlayField playField;
        bool scared;

        public SeeNoEvilGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // level = new TilemapLevel("./Maps/MagicLandCsv.json");
            // level = new TilemapLevel("./Maps/test.json");
            level = new TilemapLevel("./Maps/level.json");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            level.LoadMap(Content);
            playField = level.GetPlayField();
            player = new Cat(level.GetPlayerPosition(), Direction.Right);
            player.Load(Content, playField);
            enemyController = new GhostController(level.GetGhostCoordinates());
            enemyController.LoadAll(Content, playField);
            camera = new Camera(GraphicsDevice.Viewport); 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Keyboard.GetState().IsKeyDown(Keys.Down)) {
                player.Move(Direction.Down);
                enemyController.MoveGhosts();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Up)) {
                player.Move(Direction.Up);
                enemyController.MoveGhosts();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Left)) {
                player.Move(Direction.Left);
                enemyController.MoveGhosts();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Right)) {
                player.Move(Direction.Right);
                enemyController.MoveGhosts();
            }
            player.Update();
            enemyController.UpdateAll();
            camera.Update(player.Position);
            if(!player.Moving) 
                scared = enemyController.AreGhostsHere(playField.GetLineOfSight(player.Facing, player.Position));
            if(scared)
                player.Scared();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(
                transformMatrix: camera.Transform
            );
            level.Draw(spriteBatch, "Sky");
            level.Draw(spriteBatch, "Ground");
            level.Draw(spriteBatch, "Bushes");
            player.Draw(spriteBatch);
            enemyController.DrawAll(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
