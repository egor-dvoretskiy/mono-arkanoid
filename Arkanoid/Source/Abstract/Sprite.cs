using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid.Source.Abstract
{
    public abstract class Sprite
    {
        protected readonly Texture2D _texture;

        public Sprite(Texture2D texture)
        {
            this._texture = texture;
        }

        public Vector2 Position { get; set; }

        public int Width
        {
            get => this._texture.Width;
        }

        public int Height
        {
            get => this._texture.Height;
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
            spriteBatch.Draw(this._texture, Position, Color.White);
        }
    }
}
