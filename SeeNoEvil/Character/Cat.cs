using Microsoft.Xna.Framework;

namespace SeeNoEvil.Character {
    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }
    public class Cat : Character {
        private Direction Facing;
        public Cat(Vector2 position, Direction facing) : base(position) {
            AnimationController = new AnimationController(AnimationParser.ReadAnimationJson("SeeNoEvil/Animation/cat.json"));
            Width = AnimationController.Width;
            Height = AnimationController.Height;
            Facing = facing;
        }

        public void Move(Direction direction) {
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
            Destination = Vector2.Add(Position, new Vector2(Width*x, Height*y));
            Velocity = new Vector2(x, y);
        }
    }
}