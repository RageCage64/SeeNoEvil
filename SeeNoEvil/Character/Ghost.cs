using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SeeNoEvil.Level;
using SeeNoEvil.Tiled;

namespace SeeNoEvil.Character {
    public class GhostController {
        public List<Ghost> Ghosts {get; set;}
        public GhostController(IEnumerable<ObjectCoordinate> coordinates) {
            Ghosts = coordinates.Select(
                coord => new Ghost(new Vector2(coord.X, coord.Y))
            ).ToList();
        }

        public void LoadAll(ContentManager content, PlayField playField) =>
            Ghosts.ForEach(ghost => ghost.Load(content, playField));

        public void DrawAll(SpriteBatch spriteBatch) => 
            Ghosts.ForEach(ghost => ghost.Draw(spriteBatch));

        public void UpdateAll() => 
            Ghosts.ForEach(ghost => ghost.Update());

        public void MoveGhosts() => 
            Ghosts.ForEach(ghost => ghost.DecideMove());

        public bool AreGhostsHere(IEnumerable<Vector2> lineOfSight) =>
            Ghosts.Any(ghost => lineOfSight.Contains(ghost.Position));
    }

    public class Ghost : Character {
        public Ghost(Vector2 position) : base(position) {
            AnimationController = new AnimationController(AnimationParser.ReadAnimationJson("SeeNoEvil/Animation/ghost.json"));
            Width = AnimationController.Width;
            Height = AnimationController.Height;
            Facing = Direction.Down;
        }

        public void DecideMove() {
            Array values = Enum.GetValues(typeof(Direction));
            Random random = new Random();
            Direction randomDirection = (Direction)values.GetValue(random.Next(values.Length));
            switch(randomDirection) {
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

            base.Move(randomDirection);
        }
    }
}