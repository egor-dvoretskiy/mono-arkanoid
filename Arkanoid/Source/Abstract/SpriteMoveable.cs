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
            : base(texture)
        {
            this.StartPosition = startPosition;
            this.Velocity = velocity;
            this.ResetPosition();
        }

        public Vector2 StartPosition { get; init; }

        public Vector2 Velocity { get; init; }

        public void ResetPosition()
        {
            this.Position = this.StartPosition;
        }
    }
}
