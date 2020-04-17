using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeeNoEvil.Character;
using SeeNoEvil.Tiled;

namespace SeeNoEvil.Level {
    public class PlayField {
		private IEnumerable<TileLocation> Tiles {get; set;}
		
		public PlayField(IEnumerable<TileLocation> tiles) {
			Tiles = tiles;
		}

		public bool TryWalk(Vector2 newLocation) =>
			Tiles.Any(tile => tile.location.Equals(newLocation) && tile.tile.gid != 0);

        public IEnumerable<Vector2> GetLineOfSight(Direction facing, Vector2 position) {
			return new List<Vector2>();
		}

		private bool Between(float pos1, float pos2, float bound) =>
			(bound - pos1) >= (pos2 - pos1);
    }
}