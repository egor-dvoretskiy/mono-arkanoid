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
                    /*block.Position = new Vector2(
                        _blocksDrawOffset.X + j * (block.Width + BlockSpacing.X),
                        _blocksDrawOffset.Y + i * (block.Height + BlockSpacing.Y)
                    );*/
                    var position = new Vector2(
                        _blocksDrawOffset.X + j * (block.Width + BlockSpacing.X),
                        _blocksDrawOffset.Y + i * (block.Height + BlockSpacing.Y)
                    );

                    //blocks.Add(block);
                    blocks.Add(new Block(blockTexture, position));
                }
            }
        }

        private int CalculateOffset(int amount, int blockSize, float spacing, int limitSize)
        {
            return (limitSize - (amount * blockSize + (int)spacing * (amount - 1))) / 2; 
        }
    }
}
