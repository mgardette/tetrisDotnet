using System;
using System.Collections.Generic;
using System.Text;

namespace tetrisDotnet.model
{
    public class BlockQueue
    {
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock(),
        };

        private readonly Random random = new Random();

        public Block NextBlock { get; private set; }

        public BlockQueue()
        {
            NextBlock = getRandomBlock();
        }

        public Block getRandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block UpdateBlock()
        {
            Block block = NextBlock;

            do
            {
                NextBlock = getRandomBlock();
            }
            while (block.Id == NextBlock.Id);

            return block;
        }
    }
}
