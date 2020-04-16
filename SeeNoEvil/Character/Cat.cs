using Microsoft.Xna.Framework;

namespace SeeNoEvil.Character {
    public class Cat : Character {
        public Cat(Vector2 position, Direction facing) : base(position) {
            AnimationController = new AnimationController(AnimationParser.ReadAnimationJson("SeeNoEvil/Animation/cat.json"));
            Width = AnimationController.Width;
            Height = AnimationController.Height;
            Facing = facing;
            //FIXME Can probably share this code
            switch(Facing) {
            case Direction.Up:
                AnimationController.ChangeAnimation(3);
                break;
            case Direction.Down:
                AnimationController.ChangeAnimation(4);
                break;
            case Direction.Left:
                AnimationController.ChangeAnimation(1);
                break;
            case Direction.Right:
                AnimationController.ChangeAnimation(2);
                break;
            }
        }

        public override void Move(Direction direction) {
            if(!Moving) {
                switch(direction) {
                case Direction.Up:
                    AnimationController.ChangeAnimation(3);
                    break;
                case Direction.Down:
                    AnimationController.ChangeAnimation(4);
                    break;
                case Direction.Left:
                    AnimationController.ChangeAnimation(1);
                    break;
                case Direction.Right:
                    AnimationController.ChangeAnimation(2);
                    break;
                }
                base.Move(direction);
            }
        }
    }
}