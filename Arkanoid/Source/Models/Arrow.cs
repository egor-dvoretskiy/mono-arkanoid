using Arkanoid.Source.Abstract;
using Arkanoid.Source.Enums;
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
        private readonly float _angleBound = 80;
        private readonly Point _vectorDirectionOrigin;

        private DirectionSpread directionSpread = DirectionSpread.Uprise;
        private float angle;

        public Arrow(Texture2D texture, Vector2 position) 
            : base(texture, position)
        {
            angle = -_angleBound;

            _vectorDirectionOrigin = new Point(
                (int)position.X,
                (int)position.Y + Height
            );
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                new Rectangle(
                    (int)Position.X + Width / 2,
                    (int)Position.Y + Height, 
                    Width, 
                    Height
                ),
                null,
                Color.White,
                MathHelper.ToRadians(angle),
                new Vector2(Width / 2f, Height),
                SpriteEffects.None,
                0
            );
        }

        public override void Update()
        {          
            UpdateDirectionSpread();
            UpdateAngle();
        }

        private void UpdateAngle()
        {
            if (directionSpread == DirectionSpread.Uprise)
            {
                angle++;
                return;
            }

            if (directionSpread == DirectionSpread.Downrise)
            {
                angle--;
                return;
            }
        }

        private void UpdateDirectionSpread()
        {
            if (angle >= _angleBound)
            {
                directionSpread = DirectionSpread.Downrise;
                return;
            }

            if (angle <= -_angleBound)
            {
                directionSpread = DirectionSpread.Uprise;
                return;
            }
        }
    }
}
