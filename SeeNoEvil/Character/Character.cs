using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SeeNoEvil.Level;

namespace SeeNoEvil.Character {
    public class Character {
        private Vector2 Destination {get; set;}
        private Vector2 Velocity {get; set;}
        public Vector2 Position {get; private set;}
        private Texture2D SpriteSheet;

        protected Vector2 StartingPosition;
        protected AnimationController AnimationController; 
        protected int Width;
        protected int Height;
        protected PlayField Field;

        public Direction Facing;
        public bool Moving => 
            !Destination.Equals(Vector2.Zero) && 
            !Velocity.Equals(Vector2.Zero) &&
            !Position.Equals(Destination);

        public Character(Vector2 position) {
            Position = position;
            StartingPosition = position;
            Destination = Vector2.Zero; 
            Velocity = Vector2.Zero;
        }

        public void Load(ContentManager content, PlayField playField) {
            SpriteSheet = content.Load<Texture2D>(AnimationController.Image);
            Field = playField;
        }

        // TODO Do I want to move every frame?
        public void Update() {
            if(Moving) {
                Position = Vector2.Add(Position, Velocity);
                AnimationController.Idle = false;
            }
            else AnimationController.Idle = true;
        }

        public void Draw(SpriteBatch spriteBatch) {
            Frame currentFrame = AnimationController.CurrentFrame;
            Rectangle srcRectangle = new Rectangle(currentFrame.X,
                                                     currentFrame.Y,
                                                     AnimationController.Width,
                                                     AnimationController.Height);
            spriteBatch.Draw(SpriteSheet, Position, srcRectangle, Color.White);
        }

        public virtual void Move(Direction direction) {
            if(!Moving) {
                int velocity = 16;
                int x = 0, y = 0;
                switch(direction) {
                case Direction.Up:
                    y = -1; 
                    break;
                case Direction.Down:
                    y = 1;
                    break;
                case Direction.Left:
                    x = -1;
                    break;
                case Direction.Right:
                    x = 1;
                    break;
                }
                var tryPosition = Vector2.Add(Position, new Vector2(Width*x, Height*y));
                if(Field.TryWalk(tryPosition)) {
                    Destination = Vector2.Add(Position, new Vector2(Width*x, Height*y));
                    Velocity = new Vector2(x*velocity, y*velocity);
                    Facing = direction;
                }
            }
        }

        public void Reset() {
            Position = StartingPosition;
            Destination = Vector2.Zero;
            Velocity = Vector2.Zero;
        }
    }
    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }
}