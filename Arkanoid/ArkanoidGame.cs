using Arkanoid.Source.GUI;
using Arkanoid.Source.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arkanoid
{
    public class ArkanoidGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private StateMachine stateMachine;
        private MainMenu mainMenu;
        private GameField gameField;

        private Color backgroundColor = Color.CornflowerBlue;

        public ArkanoidGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = false;

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1 / 60.0f);
        }

        protected override void Initialize()
        {
            stateMachine = new StateMachine();

            #region main menu

            var mainMenuTexture = Content.Load<Texture2D>("GUI/MainMenuBackground");
            mainMenu = new MainMenu(
                mainMenuTexture, 
                new Vector2(
                    Window.ClientBounds.Width / 2 - mainMenuTexture.Width / 2,
                    Window.ClientBounds.Height / 2 - mainMenuTexture.Height / 2
                ),
                Content
            );
            mainMenu.OnStartGame += MainMenu_OnStartGame;
            mainMenu.OnQuitGame += MainMenu_OnQuitGame;

            #endregion

            #region game field

            gameField = new GameField(Window.ClientBounds, Content);

            #endregion

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (stateMachine.CurrentState)
            {
                case Source.Enums.GameState.MainMenu:
                    {
                        mainMenu.Update();
                    }
                    break;
                case Source.Enums.GameState.ChooseDirection:
                    {
                        backgroundColor = Color.Black;
                        gameField.Update();
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin();

            switch(stateMachine.CurrentState)
            {
                case Source.Enums.GameState.MainMenu:
                    {
                        mainMenu.Draw(spriteBatch);
                    }
                    break;
                case Source.Enums.GameState.ChooseDirection:
                    {
                        gameField.Draw(spriteBatch);
                    }
                    break;
                default:
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void MainMenu_OnQuitGame(object sender, System.EventArgs e)
        {
            Exit();
        }

        private void MainMenu_OnStartGame(object sender, System.EventArgs e)
        {
            stateMachine.ProceedNextStage();
        }
    }
}