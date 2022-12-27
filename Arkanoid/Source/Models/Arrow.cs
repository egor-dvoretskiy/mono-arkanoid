using Arkanoid.Source.Abstract;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.Models
{
    public class Arrow : Sprite
    {
        private readonly Rectangle _outsideBox;

        public Arrow(Texture2D texture, Vector2 position, Rectangle outsideBox) 
            : base(texture, position)
        {
            this._outsideBox = outsideBox;
        }

        public float Angle { get; set; }

        public void DrawString(SpriteFont font, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                font,
                Angle.ToString("f0"),
                new Vector2(220, 0),
                Color.Pink
            );
        }

        /*public override void Draw(SpriteBatch spriteBatch)
        {
            *//*spriteBatch.Draw(
                texture,
                new Rectangle(0, 0, 0, 0),
                null,
                Color.White,
                Angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0
            );*//*
        }*/

        public override void Update()
        {
            EstimateAngle();            
        }

        private void EstimateAngle()
        {
            var mouseState = Mouse.GetState();

            var numerator = Position.X * mouseState.X + Position.Y * mouseState.Y;
            var denominator = Math.Sqrt(Math.Pow(Position.X, 2) + Math.Pow(Position.X, 2)) * Math.Sqrt(Math.Pow(mouseState.X, 2) + Math.Pow(mouseState.X, 2));

            var angle = Math.Acos(numerator / denominator);

            Angle = (float)angle;
        }
    }
}
