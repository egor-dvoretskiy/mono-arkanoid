using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.Abstract
{
    public abstract class SpriteMoveable : Sprite
    {
        protected SpriteMoveable(Texture2D texture, Vector2 startPosition, Vector2 velocity) 
            : base(texture, startPosition)
        {
            StartPosition = startPosition;
            Velocity = velocity;
            ResetPosition();
        }

        public Vector2 StartPosition { get; init; }

        public Vector2 Velocity { get; set; }

        public void ResetPosition()
        {
            Position = StartPosition;
        }
    }
}
