using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        int scaredTimer = 0;
        Ghost debugGhost;

        public SeeNoEvilGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            debugGhost = null;
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

            if(scaredTimer == 0) {
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
                if(!player.Moving) {
                    scared = enemyController.AreGhostsHere(player.Position, player.GetSight(), player.Facing);
                }
                if(scared) {
                    player.Scared();
                    scaredTimer = 60;
                }
            } else if(scaredTimer == 1) {
                scared = false;
                player.Reset();
                player.ChooseAnimation(player.Facing);
                scaredTimer = 0;
            } else scaredTimer--;
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
