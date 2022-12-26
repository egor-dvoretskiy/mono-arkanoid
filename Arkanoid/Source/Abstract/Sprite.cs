using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid.Source.Abstract
{
    public abstract class Sprite
    {
        protected Texture2D texture;

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
        }

        public Vector2 Position { get; set; }

        public int Width
        {
            get => texture.Width;
        }

        public int Height
        {
            get => texture.Height;
        }

        public virtual void Update()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {

            }
            if (keyboardState.IsKeyDown(Keys.A))
            {

            }
            if (keyboardState.IsKeyDown(Keys.S))
            {

            }
            if (keyboardState.IsKeyDown(Keys.D))
            {

            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
