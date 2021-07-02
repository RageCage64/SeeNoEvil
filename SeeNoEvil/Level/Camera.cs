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
                        1);

        public Camera(Viewport _viewport) {
            Viewport = _viewport;
            //TODO This is to experiment
            Centre = new Vector2(Viewport.Width / 2, Viewport.Height / 2);
        }

        //FIXME Don't need velocity anymore?
        public void Update(Vector2 position) {
            Centre = position;
            Transform = Matrix.CreateTranslation(TranslationVector);
        }
    }
}