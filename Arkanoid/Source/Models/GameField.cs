﻿using Arkanoid.Source.Abstract;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        private readonly Ball _ball;
        private readonly Paddle _paddle;
        private readonly BlockField _blockField;
        private readonly ScoreManager _scoreManager;

        #region velocities

        private readonly Vector2 _ballVelocity = new Vector2(3.0f, 3.0f);
        private readonly Vector2 _paddleVelocity = new Vector2(6.0f, 6.0f);

        #endregion

        public GameField(Rectangle box, ContentManager contentManager) 
            : base(box)
        {
            _scoreManager = new ScoreManager();

            var paddleTexture = contentManager.Load<Texture2D>("Models/Paddle");
            _paddle = new Paddle(
                paddleTexture,
                new Vector2(
                    Box.Width / 2 - paddleTexture.Width / 2,
                    Box.Height - paddleTexture.Height
                ),
                _paddleVelocity
            );

            var ballTexture = contentManager.Load<Texture2D>("Models/Ball");
            _ball = new Ball(
                ballTexture,
                new Vector2(
                    Box.Width / 2 - ballTexture.Width / 2,
                    Box.Height - ballTexture.Height - paddleTexture.Height
                ),
                _ballVelocity
            );

            var blockTexture = contentManager.Load<Texture2D>("Models/Block");
            _blockField = new BlockField(
                new Rectangle(
                    Box.X,
                    Box.Y,
                    Box.Width,
                    Box.Height * 2 / 3
                ),
                new Block(blockTexture)
            );
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _ball.Draw(spriteBatch);
            _paddle.Draw(spriteBatch);
            _blockField.Draw(spriteBatch);
        }

        public override void Update()
        {

        }
    }
}
