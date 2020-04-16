using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

using SeeNoEvil.Tiled;

namespace SeeNoEvil.Level {
    public class PlayField {
		private IEnumerable<TileLocation> Tiles {get; set;}
		
		public PlayField(IEnumerable<TileLocation> tiles) {
			Tiles = tiles;
		}

		public bool TryWalk(Vector2 newLocation) =>
			Tiles.Any(tile => tile.location.Equals(newLocation));
	}
}