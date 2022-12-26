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

        private Vector2 blockSpacing = new Vector2(16, 10);

        public BlockField(Rectangle outsideBox, Block block) 
            : base(outsideBox)
        {
            this.Box = outsideBox;
            this.blocks = new List<Block>();

            int amountOfBlocksColumns = this.ClarifyBlockAmount(outsideBox.Width, block.Width, this.BlockSpacing.X);
            int amountOfBlocksRows = this.ClarifyBlockAmount(outsideBox.Height / 2, block.Height, this.BlockSpacing.Y);

            this._blocksDrawOffset = new Vector2(
                this.CalculateOffset(amountOfBlocksColumns, block.Width, this.BlockSpacing.X, outsideBox.Width),
                this.CalculateOffset(amountOfBlocksRows, block.Height, this.BlockSpacing.Y, outsideBox.Height / 2)
            );

            this.FillBlocksList(amountOfBlocksRows, amountOfBlocksColumns, block);
        }

        public Vector2 BlockSpacing
        {
            get => this.blockSpacing;
            set
            {
                this.blockSpacing = value;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < this.blocks.Count; i++)
            {
                this.blocks[i].Draw(spriteBatch);
            }
        }

        public override void Update()
        {
            throw new NotImplementedException();
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
            if (this.blocks is null || this.blocks.Count == 0) 
                return;

            for (int i = 0; i < yamount; i++)
            {
                for (int j = 0; j < xamount; j++)
                {
                    block.Position = new Vector2(
                        this._blocksDrawOffset.X + j * (block.Width + this.BlockSpacing.X),
                        this._blocksDrawOffset.Y + i * (block.Height + this.BlockSpacing.Y)
                    );

                    this.blocks.Add(block);
                }
            }
        }

        private int CalculateOffset(int amount, int blockSize, float spacing, int limitSize)
        {
            return (limitSize - (amount * blockSize + (int)spacing * (amount - 1))) / 2; 
        }
    }
}
