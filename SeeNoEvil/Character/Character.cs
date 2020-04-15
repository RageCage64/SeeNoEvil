using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SeeNoEvil.Character {
    public class Character {
        protected Vector2 Destination {get; set;}
        protected Vector2 Velocity {get; set;}
        protected Vector2 Position {get; private set;}
        protected bool Transform => 
            !Destination.Equals(Vector2.Zero) && 
            !Velocity.Equals(Vector2.Zero) &&
            !Position.Equals(Destination);

        protected AnimationController AnimationController; 
        protected Texture2D SpriteSheet;
        protected int Width;
        protected int Height;

        public Character(Vector2 position) {
            Position = position;
            Destination = Vector2.Zero; 
            Velocity = Vector2.Zero;
        }

        public void Load(ContentManager content) {
            SpriteSheet = content.Load<Texture2D>(AnimationController.Image);
        }

        // TODO Do I want to move every frame?
        public void Update() {
            if(Transform) 
                Position = Vector2.Add(Position, Velocity);
        }

        public void Draw(SpriteBatch spriteBatch) {
            Frame currentFrame = AnimationController.CurrentFrame;
            Rectangle srcRectangle = new Rectangle(currentFrame.X,
                                                     currentFrame.Y,
                                                     AnimationController.Width,
                                                     AnimationController.Height);
            spriteBatch.Draw(SpriteSheet, Position, srcRectangle, Color.White);
            // spriteBatch.Draw(SpriteSheet, Position, Color.White);
        }
    }
}