using Microsoft.Xna.Framework;

namespace SeeNoEvil.Tiled {
    public struct Tile {
        public Tile(long _gid, Rectangle _srcRectangle, string _setName) {
            gid = (uint)_gid;
            srcRectangle = _srcRectangle;
            setName = _setName;
        }
        public uint gid;
        public Rectangle srcRectangle;
        public string setName;
    }

    public struct TileLocation {
        public TileLocation(Tile tile, Vector2 location) {
            this.tile = tile;
            this.location = location;
        }

        public Tile tile;
        public Vector2 location;
    }

    public struct DataCoordinate {
        public DataCoordinate(int _x, int _y, uint _gid) {
            x = _x;
            y = _y;
            gid = _gid;
        }

        public int x;
        public int y;
        public uint gid;
    }
}