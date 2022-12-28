using Arkanoid.Source.Abstract;
using Arkanoid.Source.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.Models
{
    public class GameField : Field
    {
        private readonly ContentManager _contentManager;

        private readonly Ball _ball;
        private readonly Paddle _paddle;
        private readonly BlockField _blockField;
        private readonly Arrow _arrow;
        private readonly ScoreManager _scoreManager;
        private readonly StateMachine _stateMachine;

        private MouseState previouseMouseState;
        private MouseState currentMouseState;

        #region velocities

        //private readonly Vector2 _ballVelocity = new Vector2(3.0f, 3.0f);
        private readonly Vector2 _paddleVelocity = new Vector2(6.0f, 6.0f);

        #endregion

        public GameField(Rectangle box, ContentManager contentManager, StateMachine stateMachine) 
            : base(box)
        {
            _scoreManager = new ScoreManager();
            _stateMachine = stateMachine;
            _contentManager = contentManager;

            var paddleTexture = contentManager.Load<Texture2D>("Models/Paddle");
            _paddle = new Paddle(
                paddleTexture,
                new Vector2(
                    Box.Width / 2 - paddleTexture.Width / 2,
                    Box.Height - paddleTexture.Height
                ),
                _paddleVelocity,
                Box
            );

            var ballTexture = contentManager.Load<Texture2D>("Models/Ball");
            _ball = new Ball(
                ballTexture,
                new Vector2(
                    Box.Width / 2 - ballTexture.Width / 2,
                    Box.Height - ballTexture.Height - paddleTexture.Height
                ),
                new Vector2(0f, 0f),
                Box
            );

            var blockTexture = contentManager.Load<Texture2D>("Models/Block");
            _blockField = new BlockField(
                new Rectangle(
                    Box.X,
                    Box.Y,
                    Box.Width,
                    Box.Height * 2 / 3
                ),
                new Block(blockTexture, new System.Numerics.Vector2(0, 0)),
                blockTexture
            );

            var arrowTexture = contentManager.Load<Texture2D>("Models/Arrow");
            _arrow = new Arrow(
                arrowTexture,
                new Vector2(
                    Box.Width / 2 - arrowTexture.Width / 2,
                    Box.Height - arrowTexture.Height - paddleTexture.Height - ballTexture.Height / 2
                )
            );
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch(_stateMachine.CurrentState)
            {
                case GameState.ChooseDirection:
                    {
                        _ball.Draw(spriteBatch);
                        _paddle.Draw(spriteBatch);
                        _blockField.Draw(spriteBatch);
                        _arrow.Draw(spriteBatch);
                    }
                    break;
                case GameState.Play:
                    {
                        _ball.Draw(spriteBatch);
                        _paddle.Draw(spriteBatch);
                        _blockField.Draw(spriteBatch);
                    }
                    break;
                default:
                    break;
            }
        }

        public override void Update()
        {
            ProceedMouse();
            PaddleMove();

            _arrow.Update();
            _ball.DirectionAngle = _arrow.Angle;
            _ball.Update();
            _paddle.CollisionCheck(_ball);
        }

        private void ProceedMouse()
        {
            previouseMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (currentMouseState.LeftButton == ButtonState.Released && previouseMouseState.LeftButton == ButtonState.Pressed)
            {
                _ball.IsVelocityRequiresEstimation = true;
                _stateMachine.ProceedNextStage();
            }
        }

        private void PaddleMove()
        {
            _paddle.Move(currentMouseState.X - previouseMouseState.X);
        }
    }
}
