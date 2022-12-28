using Arkanoid.Source.Abstract;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.Models
{
    public class Ball : SpriteMoveable
    {
        private bool isVelocityRequiresEstimation = false;

        private readonly Rectangle _outerBox;

        public Ball(Texture2D texture, Vector2 startPosition, Vector2 velocity, Rectangle outerBox) 
            : base(texture, startPosition, velocity)
        {
            _outerBox = outerBox;
        }

        public float DirectionAngle { get; set; }

        public bool IsVelocityRequiresEstimation
        {
            get => isVelocityRequiresEstimation;
            set
            {
                isVelocityRequiresEstimation = value;
            }
        }

        public override void Update()
        {
            if (isVelocityRequiresEstimation)
                EstimateVelocity();

            ProceedBallBordersCollision();
            UpdateVelocity();
        }

        public void ReverseVelocity(bool x = false, bool y = false)
        {
            var vel = Velocity;

            if (x)
                vel.X = -vel.X;

            if (y)
                vel.Y = -vel.Y;

            Velocity = vel;
        }

        private void EstimateVelocity()
        {
            var alpha = 90 - Math.Abs(DirectionAngle);
            var xvelocity = Height * Math.Cos(alpha);
            var yvelocity = Height * Math.Cos(90 - alpha);

            Velocity = new Vector2((float)xvelocity, (float)yvelocity);

            Debug.Print($"x: {Velocity.X}, y: {Velocity.Y}");

            isVelocityRequiresEstimation = false;
        }

        private void UpdateVelocity()
        {
            Position += Velocity;
        }

        private void SetPosition()
        {

        }

        private void ProceedBallBordersCollision()
        {
            if (IsBallHasCollisionWithLeftBorder())
            {
                ReverseVelocity(x: true);
                return;
            }

            if (IsBallHasCollisionWithUpperBorder())
            {
                ReverseVelocity(y: true);
                return;
            }

            if (IsBallHasCollisionWithRightBorder())
            {
                ReverseVelocity(x: true);
                return;
            }

            if (IsBallHasCollisionWithBottomBorder())
            {
                // game over
                return;
            }
        }

        private bool IsBallHasCollisionWithLeftBorder()
        {
            return Velocity.X < 0 &&
                Position.X + Velocity.X < _outerBox.X;
        }

        private bool IsBallHasCollisionWithUpperBorder()
        {
            return Velocity.Y < 0 &&
                Position.Y + Velocity.Y < _outerBox.Y;
        }

        private bool IsBallHasCollisionWithRightBorder()
        {
            return Velocity.X > 0 &&
                Position.X + Velocity.X > _outerBox.X + _outerBox.Width;
        }

        private bool IsBallHasCollisionWithBottomBorder()
        {
            return Velocity.Y > 0 &&
                Position.Y + Velocity.Y > _outerBox.Y + _outerBox.Height;
        }
    }
}
