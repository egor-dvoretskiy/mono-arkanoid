using Arkanoid.Source.Abstract;
using Arkanoid.Source.Models;
using Microsoft.Xna.Framework;
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
        public const int ButtonYOffset = 6;

        private readonly SpriteFont _buttonsFont;

        private readonly Button _buttonStartGame;
        private readonly Button _buttonQuitGame;

        #region events

        public event EventHandler OnStartGame;
        public event EventHandler OnQuitGame;

        #endregion

        public MainMenu(Texture2D texture, Vector2 position, ContentManager contentManager) 
            : base(texture, position)
        {
            _buttonsFont = contentManager.Load<SpriteFont>("Fonts/ButtonFont");

            Texture2DPackStates texturePack = new Texture2DPackStates()
            {
                Simple = contentManager.Load<Texture2D>("GUI/Button"),
                Hover = contentManager.Load<Texture2D>("GUI/ButtonHover"),
                Pressed = contentManager.Load<Texture2D>("GUI/ButtonPressed"),
            };

            _buttonStartGame = new Button(
                texturePack.Simple,
                new Vector2(
                    position.X + Width / 2 - texturePack.Simple.Width / 2,
                    position.Y + Height / 2 - texturePack.Simple.Height - ButtonYOffset
                ),
                texturePack,
                _buttonsFont,
                "START"
            );
            _buttonStartGame.Click += ButtonStartGame_Click;

            _buttonQuitGame = new Button(
                texturePack.Simple,
                new Vector2(
                    position.X + Width / 2 - texturePack.Simple.Width / 2,
                    position.Y + Height / 2 + ButtonYOffset
                ),
                texturePack,
                _buttonsFont,
                "QUIT"
            );
            _buttonQuitGame.Click += ButtonQuitGame_Click;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                Position,
                Color.White
            );

            _buttonStartGame.Draw(spriteBatch);
            _buttonQuitGame.Draw(spriteBatch);
        }

        public override void Update()
        {
            _buttonStartGame.Update();
            _buttonQuitGame.Update();
        }

        private void ButtonQuitGame_Click(object sender, EventArgs e)
        {
            OnQuitGame?.Invoke(this, new EventArgs());
        }

        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            OnStartGame?.Invoke(this, new EventArgs());
        }
    }
}
