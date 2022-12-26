using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.Models
{
    public struct Texture2DPackStates
    {
        public Texture2D Simple { get; set; }

        public Texture2D Hover { get; set; }

        public Texture2D Pressed { get; set; }
    }
}
