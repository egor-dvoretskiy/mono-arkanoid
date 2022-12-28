using Arkanoid.Source.Abstract;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arkanoid.Source.Models
{
    public class Paddle : SpriteMoveable
    {
        private readonly Rectangle _outerBox;

        public Paddle(Texture2D texture, Vector2 startPosition, Vector2 velocity, Rectangle outerBox) 
            : base(texture, startPosition, velocity)
        {
            _outerBox = outerBox;
        }

        public bool CollisionCheck(Ball ball)
        {
            if (!IsBallAbleToBeHit(ball))
                return false;

            (float delta, bool wayPastPaddle) = FindDeltaInBallMovement(ball);

            if (wayPastPaddle)
                return false;

            float deltaTime = delta / ball.Velocity.Y;
            int collY = (int)(ball.Position.Y - ball.Velocity.Y * deltaTime);
            int collX = (int)(ball.Position.X - ball.Velocity.X * deltaTime);

            if (PaddleCheck(collX, collY, ball))
            {
                ball.SetPosition(new Vector2(collX, collY));
                ball.ReverseVelocity(y: true);

                return true;
            }
            return false;
        }

        public void Move(int diff)
        {
            var pos = Position;
            pos.X += diff;
            this.FixBounds(pos);
        }

        private void FixBounds(Vector2 pos)
        {
            if (pos.X < _outerBox.X)
                pos.X = _outerBox.X;
            if (pos.X + Width > _outerBox.Width)
                pos.X = _outerBox.Width - Width;

            Position = pos;
        }

        private bool IsBallAbleToBeHit(Ball ball)
        {
            bool directionCheck = ball.Velocity.Y > 0;
            bool distanceCheck = ball.Position.Y + ball.Height > Position.Y;

            return directionCheck && distanceCheck;
        }

        private (float, bool) FindDeltaInBallMovement(Ball ball)
        {
            float delta = ball.Position.Y + ball.Height - this.Position.Y;
            bool wayPastPaddle = delta > ball.Velocity.Y + ball.Height;

            return (delta, wayPastPaddle);
        }

        private bool PaddleCheck(int x, int y, Ball ball)
        {
            return y <= Position.Y + Height &&
                y + ball.Height >= Position.Y &&
                x <= Position.X + Width &&
                x + ball.Width >= Position.X;
        }
    }
}
