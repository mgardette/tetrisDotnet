using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace tetrisDotnet.model
{
    class GameState
    {
        private Block currentBlock;

        //public Block currentBlock;
        private static System.Timers.Timer timer;

        private readonly Subject<CellChanges> changesToPush = new Subject<CellChanges>();

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
        public bool GameOver { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid();
            BlockQueue = new BlockQueue();
            CellChanges = changesToPush.AsObservable();
        }

        public BlockQueue BlockQueue { get; }

        public void Start()
        {
            //SetTimer();
            var rand = new Random();
            while (true)
            {
                bool verif = true;
                CurrentBlock = BlockQueue.UpdateBlock();
                do
                {
                    MoveBlockDown();
                    foreach (Position p in CurrentBlock.TilePositions())
                    {
                        verif = false;
                    }

                    Thread.Sleep(1000);
                } while (verif);
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

        private bool BlockFits()
        {
            bool result = true;
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if (!GameGrid.isEmpty(p.Row, p.Column))
                {
                    result = false;
                }
            }

            return result;
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
            List<CellChange> changes = new List<CellChange>();
            foreach(Position p in CurrentBlock.TilePositions())
            {
                GameGrid.Grid[p.Row, p.Column] = CurrentBlock.Id;
                changes.Add(SetCell(p.Row, p.Column, CurrentBlock.getColor()));
            }

            changesToPush.OnNext(new model.CellChanges(changes.ToArray()));
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

        public CellChange SetCell(int x, int y, Color? color)
        {
            if (x < 0 || x >= GameGrid.RowNum || y < 0 || y >= GameGrid.ColNum) return null;

            if (GameGrid.Cells[x, y] != color)
            {
                CellChange change = new CellChange(x, y, color);
                GameGrid.Cells[x, y] = color;
                return change;
            }

            return null;
        }

        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
            }
            PlaceBlock();
        }

        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }

        private int TileDropDistance(Position p)
        {
            int drop = 0;

            while (GameGrid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }

            return drop;
        }

        public int BlockDropDistance()
        {
            int drop = GameGrid.RowNum;

            foreach (Position p in CurrentBlock.TilePositions())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }

            return drop;
        }
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            //timer = new Timer(1500);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //synchronize(() =>
            //{
                MoveBlockDown();
            //});
        }

        public void MoveBlockSide(Key side)
        {
            switch (side)
            {
                case Key.Left:
                    MessageBox.Show("e");
                    MoveBlockLeft();
                    break;
                case Key.Right:
                    MoveBlockRight();
                    break;
                case Key.Down:
                    MoveBlockDown();
                    break;
                /* TODO finir les touches du jeu 
                case Key.Up:
                    RotateBlockCW();
                    break;
                case Key.Z:
                    RotateBlockCCW();
                    break;
                case Key.C:
                    HoldBlock();
                    break;
                case Key.Space:
                    DropBlock();
                    break;*/
                default:
                    return;
            }
        }
        public IObservable<CellChanges> CellChanges { get; }
    }

    internal class CellChanges
    {
        public CellChanges(CellChange[] changes)
        {
            Changes = changes;
        }

        public CellChange[] Changes { get; }
    }

    internal class CellChange
    {
        public CellChange(int x, int y, Color? color)
        {
            this.CellX = x;
            this.CellY = y;
            Color = color;
        }

        public int CellX { get; set; }
        public int CellY { get; set; }

        public Color? Color { get; }
    }
}
