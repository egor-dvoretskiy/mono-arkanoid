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
            IsBelowBottom = false;
        }

        public int DirectionAngle { get; set; }

        public bool IsBelowBottom { get; private set; }

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

            if (!IsProceedBallBordersCollision())
            {
                UpdateVelocity();
            }
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

        public void SetPosition(Vector2 updPosition)
        {
            Position = updPosition;
        }

        private void EstimateVelocity()
        {
            float absAngle = Math.Abs(DirectionAngle);

            var cosx = DirectionAngle < 0 ? Math.Cos(90 - absAngle) : Math.Cos(absAngle);
            var cosy = DirectionAngle < 0 ? Math.Cos(absAngle) : Math.Cos(90 - absAngle);
            
            var xvelocity = Math.Abs(Height * cosx) * (DirectionAngle < 0 ? -1 : 1);
            var yvelocity = -Math.Abs(Height * cosy);

            Velocity = new Vector2((float)xvelocity * 0.4f, (float)yvelocity * 0.4f);

            Debug.Print($"x: {Velocity.X}, y: {Velocity.Y}");

            isVelocityRequiresEstimation = false;
        }

        private void UpdateVelocity()
        {
            Position += Velocity;
        }

        private bool IsProceedBallBordersCollision()
        {
            if (IsBallHasCollisionWithLeftBorder())
            {
                //Position -= FindDeltaLeft();
                ReverseVelocity(x: true);
                return true;
            }

            if (IsBallHasCollisionWithUpperBorder())
            {
                //Position -= FindDeltaTop();
                ReverseVelocity(y: true);
                return true;
            }

            if (IsBallHasCollisionWithRightBorder())
            {
                //Position -= FindDeltaRight();
                ReverseVelocity(x: true);
                return true;
            }

            if (IsBallHasCollisionWithBottomBorder())
            {
                IsBelowBottom = true;
                return true;
            }

            return false;
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
                Position.X + Width + Velocity.X > _outerBox.X + _outerBox.Width;
        }

        private bool IsBallHasCollisionWithBottomBorder()
        {
            return Velocity.Y > 0 &&
                Position.Y + Velocity.Y > _outerBox.Y + _outerBox.Height;
        }

        private Vector2 FindDeltaLeft()
        {
            float delta = Position.X - _outerBox.X;

            return new Vector2(delta, delta);
        }

        private Vector2 FindDeltaTop()
        {
            float delta =  _outerBox.Y - Position.Y;

            return new Vector2(delta, delta);
        }

        private Vector2 FindDeltaRight()
        {
            float delta = _outerBox.Width - Position.X;

            return new Vector2(delta, delta);
        }
    }
}
