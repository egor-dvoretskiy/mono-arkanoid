using Arkanoid.Source.Abstract;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.GUI
{
    public class MainMenu : Sprite
    {
        private readonly SpriteFont _buttonsFont;

        public MainMenu(Texture2D texture, ContentManager contentManager) 
            : base(texture)
        {
            _buttonsFont = contentManager.Load<SpriteFont>("Fonts/ButtonFont");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
