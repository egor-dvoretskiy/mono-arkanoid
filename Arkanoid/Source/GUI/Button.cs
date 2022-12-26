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

        public Button(Texture2D texture, Texture2DPackStates textureStates, SpriteFont spriteFont, string innerContent)
            : base(texture)
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
            spriteBatch.Draw(
                texture, 
                Position,
                Color.White
            );

            spriteBatch.DrawString(
                _font,
                InnerContent,
                new Vector2(
                    Position.X + (Width - InnerContentBoxSize.X / 2),
                    Position.Y + (Height - InnerContentBoxSize.Y / 2)
                ),
                Color.Black
            );
        }
    }
}
