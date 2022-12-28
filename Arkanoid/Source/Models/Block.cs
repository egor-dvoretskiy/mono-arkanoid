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
    public class Block : Sprite
    {
        public Block(Texture2D texture, Vector2 position) 
            : base(texture, position)
        {
            Box = new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width,
                texture.Height
            );
        }

        public Rectangle Box { get; private set; }
    }
}
