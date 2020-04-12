using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeeNoEvil.Level {
	public class Camera {
		public Matrix Transform {get; private set;}
		public Viewport Viewport {get; private set;}
		public Vector2 Centre {get; set;}

		private Vector3 TranslationVector => 
			new Vector3(-Centre.X + (Viewport.Width / 2),
						-Centre.Y + (Viewport.Height / 2),
						0);

		public Camera(Viewport _viewport) {
			Viewport = _viewport;
			//TODO This is to experiment
			Centre = new Vector2(Viewport.Width / 2, Viewport.Height / 2);
		}

        public void Update(Vector2 position, Vector2 velocity) {
			Centre = Vector2.Add(Centre, velocity);
			Transform = Matrix.CreateTranslation(TranslationVector);
		}
    }
}