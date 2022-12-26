using Arkanoid.Source.Abstract;
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
        public Block(Texture2D texture) 
            : base(texture)
        {
        }
    }
}
