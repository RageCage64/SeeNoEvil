using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SeeNoEvil.Character {
    public class Cat : Character {
        public Cat(Vector2 position, Direction facing) : base(position) {
            AnimationController = new AnimationController(AnimationParser.ReadAnimationJson("SeeNoEvil/Animation/cat.json"));
            Width = AnimationController.Width;
            Height = AnimationController.Height;
            Facing = facing;
            ChooseAnimation(Facing);
        }

        public override void Move(Direction direction) {
            if(!Moving) {
                ChooseAnimation(direction);
                base.Move(direction);
            }
        }

        private void ChooseAnimation(Direction direction) {
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
        }

        public void Scared() {
            switch(Facing) {
            case Direction.Up:
                AnimationController.ChangeAnimation(7);
                break;
            case Direction.Down:
                AnimationController.ChangeAnimation(8);
                break;
            case Direction.Left:
                AnimationController.ChangeAnimation(5);
                break;
            case Direction.Right:
                AnimationController.ChangeAnimation(6);
                break;
            }
        }
    }
}