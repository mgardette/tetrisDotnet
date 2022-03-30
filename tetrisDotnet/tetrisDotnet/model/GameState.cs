using System;
using System.Collections.Generic;
using System.Text;

namespace tetrisDotnet.model
{
    public class GameState
    {
        public Block CurrentBlock
        {
            get => CurrentBlock;
            private set
            {
                CurrentBlock = value;
                CurrentBlock.Reset();
            }
        }

        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid();
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.UpdateBlock();
        }
    }
}
