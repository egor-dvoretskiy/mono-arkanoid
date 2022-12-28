using Arkanoid.Source.Abstract;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.Models
{
    public class BlockField : Field
    {
        private readonly List<Block> blocks;
        private readonly Vector2 _blocksDrawOffset;
        private readonly Texture2D blockTexture;

        private Vector2 blockSpacing = new Vector2(16, 10);

        public BlockField(Rectangle outsideBox, Block block, Texture2D blockTexture) 
            : base(outsideBox)
        {
            this.blockTexture = blockTexture;
            Box = outsideBox;
            blocks = new List<Block>();

            int amountOfBlocksColumns = ClarifyBlockAmount(outsideBox.Width, block.Width, this.BlockSpacing.X);
            int amountOfBlocksRows = ClarifyBlockAmount(outsideBox.Height / 2, block.Height, this.BlockSpacing.Y);

            _blocksDrawOffset = new Vector2(
                CalculateOffset(amountOfBlocksColumns, block.Width, BlockSpacing.X, outsideBox.Width),
                CalculateOffset(amountOfBlocksRows, block.Height, BlockSpacing.Y, outsideBox.Height / 2)
            );

            FillBlocksList(amountOfBlocksRows, amountOfBlocksColumns, block);
        }

        public Vector2 BlockSpacing
        {
            get => blockSpacing;
            set
            {
                blockSpacing = value;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Draw(spriteBatch);
            }
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public bool CollisionCheck(Ball ball)
        {
            var ballRectangle = new Rectangle(
                (int)ball.Position.X,
                (int)ball.Position.Y,
                ball.Width,
                ball.Height
            );

            for (int i = 0; i < blocks.Count; i++)
            {
                //if (HasIntersection(ballRectangle, blocks[i].Box))
                if (blocks[i].Box.Intersects(ballRectangle))
                {
                    blocks.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        private int ClarifyBlockAmount(int limit, int blockSize, float spacing)
        {
            int amount = limit / blockSize;

            while (amount * blockSize + spacing * (amount - 1) >= limit)
                if (--amount < 0)
                    return 0;

            return amount;
        }

        private void FillBlocksList(int yamount, int xamount, Block block)
        {
            if (blocks is null) 
                return;

            for (int i = 0; i < yamount; i++)
            {
                for (int j = 0; j < xamount; j++)
                {
                    var position = new Vector2(
                        _blocksDrawOffset.X + j * (block.Width + BlockSpacing.X),
                        _blocksDrawOffset.Y + i * (block.Height + BlockSpacing.Y)
                    );

                    blocks.Add(new Block(blockTexture, position));
                }
            }
        }

        private int CalculateOffset(int amount, int blockSize, float spacing, int limitSize)
        {
            return (limitSize - (amount * blockSize + (int)spacing * (amount - 1))) / 2; 
        }

        private bool HasIntersection(Rectangle rectangle1, Rectangle rectangle2)
        {
            return
                IsPointPreceedsBorders(
                    new Point(
                        rectangle1.X,
                        rectangle1.Y
                    ),
                    rectangle2
                ) ||
                IsPointPreceedsBorders(
                    new Point(
                        rectangle1.X + rectangle1.Width,
                        rectangle1.Y
                    ),
                    rectangle2
                ) ||
                IsPointPreceedsBorders(
                    new Point(
                        rectangle1.X,
                        rectangle1.Y + rectangle1.Height
                    ),
                    rectangle2
                ) ||
                IsPointPreceedsBorders(
                    new Point(
                        rectangle1.X + rectangle1.Width,
                        rectangle1.Y + rectangle1.Height
                    ),
                    rectangle2
                );
        }

        private bool IsPointPreceedsBorders(Point point, Rectangle rectangle)
        {
            return point.X > rectangle.X &&
                point.X < rectangle.X + rectangle.Width &&
                point.Y > rectangle.Y &&
                point.Y < rectangle.Y + rectangle.Height;
        }
    }
}
