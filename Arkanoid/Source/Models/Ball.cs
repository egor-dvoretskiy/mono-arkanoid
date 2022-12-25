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
    public class Ball : Sprite
    {
        public Ball(Texture2D texture, Vector2 startPosition) 
            : base(texture, startPosition)
        {

        }

        public Vector2 Velocity { get; set; }
    }
}
