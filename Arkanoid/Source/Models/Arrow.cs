using Arkanoid.Source.Abstract;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.Models
{
    public class Arrow : Sprite
    {
        public Arrow(Texture2D texture, Vector2 position) 
            : base(texture, position)
        {
        }

        public float Angle { get; set; }

        /*public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                new Rectangle(0, 0, 0, 0),
                null,
                Color.White,
                Angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0
            );
        }*/
    }
}
