using Arkanoid.Source.Abstract;
using Arkanoid.Source.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.GUI
{
    public class Button : Sprite
    {
        private readonly Texture2DPackStates _textureStates;
        private readonly SpriteFont _font;

        private MouseState currentMouseState;
        private MouseState previousMouseState;

        private bool isHovering;
        private bool isPressed;

        public event EventHandler Click;

        public Button(Texture2D texture, Vector2 position, Texture2DPackStates textureStates, SpriteFont spriteFont, string innerContent)
            : base(texture, position)
        {
            _textureStates = textureStates;
            _font = spriteFont;

            InnerContent = innerContent;
            InnerContentBoxSize = spriteFont.MeasureString(InnerContent);
        }

        public string InnerContent { get; init; }

        public Vector2 InnerContentBoxSize { get; init; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            texture = GetTextureDueToMouseState();

            spriteBatch.Draw(
                texture, 
                Position,
                Color.White
            );

            spriteBatch.DrawString(
                _font,
                InnerContent,
                new Vector2(
                    Position.X + (Width - InnerContentBoxSize.X) / 2,
                    Position.Y + (Height - InnerContentBoxSize.Y) / 2
                ),
                Color.Black
            );
        }

        public override void Update()
        {
            previousMouseState = this.currentMouseState;
            currentMouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(
                this.currentMouseState.X,
                this.currentMouseState.Y,
                1,
                1
            );

            if (this.IsBoundsCrossed(mouseRectangle, Position))
            {
                this.isHovering = true;

                if (this.currentMouseState.LeftButton == ButtonState.Pressed)
                    this.isPressed = true;

                if (this.currentMouseState.LeftButton == ButtonState.Released && this.previousMouseState.LeftButton == ButtonState.Pressed)
                {
                    this.Click?.Invoke(this, new EventArgs());
                    this.isPressed = false;
                }
            }
            else
            {
                this.isHovering = false;
                this.isPressed = false;
            }
        }

        private bool IsBoundsCrossed(Rectangle mouseRectangle, Vector2 position)
        {
            return mouseRectangle.X >= position.X &&
                mouseRectangle.X <= position.X + Width &&
                mouseRectangle.Y >= position.Y &&
                mouseRectangle.Y <= position.Y + Height;
        }

        private Texture2D GetTextureDueToMouseState()
        {
            if (isPressed)
                return _textureStates.Pressed;

            if (isHovering)
                return _textureStates.Hover;

            return _textureStates.Simple;
        }
    }
}
