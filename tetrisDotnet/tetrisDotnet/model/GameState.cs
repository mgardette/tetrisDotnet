using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace tetrisDotnet.model
{
    public class GameState
    {
        private Block currentBlock;

        //public Block currentBlock;
        private static System.Timers.Timer timer;

        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();

                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);

                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid();
            SetTimer();
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.UpdateBlock();
        }

        public async Task Start()
        {
            var rand = new Random();
            while (true)
            {
                MoveBlockDown();
                await Task.Delay(1000);
            }
        }

        private bool BlockFits()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if (!GameGrid.isEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }

        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public bool IsGameOver()
        {
            return !(GameGrid.isRowEmpty(0) && GameGrid.isRowEmpty(1));
        }

        private void PlaceBlock()
        {
            foreach(Position p in CurrentBlock.TilePositions())
            {
                GameGrid.Grid[p.Row, p.Column] = CurrentBlock.Id;
            }
            GameGrid.ClearFullRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.UpdateBlock();      
            }
        }

        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            timer = new Timer(1500);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            synchronize(() =>
            {
                MoveBlockDown();
            });
        }

        public void MoveBlockSide(Key side)
        {
            switch (side)
            {
                case Key.Right:
                    MoveBlockRight();
                    break;

                case Key.Left:
                    MoveBlockLeft();
                    break;
            }
        }

        private static void synchronize(Action a)
        {
            Application app = Application.Current;
            if (app != null && app.Dispatcher != null)
            {
                Application.Current.Dispatcher.Invoke(a);
            }
        }
    }
}
