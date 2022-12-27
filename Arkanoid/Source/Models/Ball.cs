using Arkanoid.Source.Abstract;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.Models
{
    public class Ball : SpriteMoveable
    {
        private bool isVelocityRequiresEstimation = false;

        public Ball(Texture2D texture, Vector2 startPosition, Vector2 velocity) 
            : base(texture, startPosition, velocity)
        {
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

            Position += Velocity;
        }

        private void EstimateVelocity()
        {
            var alpha = 90 - Math.Abs(DirectionAngle);
            var xvelocity = Height * Math.Cos(alpha);
            var yvelocity = Height * Math.Cos(90 - alpha);

            base.Velocity = new Vector2((float)xvelocity, (float)yvelocity);

            isVelocityRequiresEstimation = false;
        }
    }
}
