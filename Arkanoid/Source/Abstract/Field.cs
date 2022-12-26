using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.Abstract
{
    public abstract class Field
    {
        private readonly Rectangle _box;

        public Field(Rectangle box)
        {
            this.Box = box;
        }

        public Rectangle Box { get; init; }

        public abstract void Update();

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
