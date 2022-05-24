using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace tetrisDotnet.model
{
    public class GameGrid
    {
        public int[,] Grid { get; set; }

        public int ColNum = 10;

        public int RowNum = 20;

        public int CellSize = 20;

        public Color?[,] Cells { get; } = new Color?[20, 10];

        public GameGrid()
        {
            this.Grid = new int[RowNum, ColNum];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    this.Grid[i, j] = 0;
                }
            }
        }

        public bool isEmpty(int r, int c)
        {
            return Grid[r, c] == 0;
        }

        public bool isRowEmpty(int r)
        {
            bool res = true;
            for (int c = 0; c < ColNum; c++)
            {
                if (!isEmpty(r, c))
                {
                    res = false;
                }
            }

            return res;
        }

        public bool isRowFull(int r)
        {
            bool res = true;
            for (int c = 0; c < ColNum; c++)
            {
                if (isEmpty(r, c))
                {
                    res = false;
                }
            }

            return res;
        }

        public void ClearRow(int r)
        {
            for (int c = 0; c < ColNum; c++)
            {
                this.Grid[r, c] = 0;
            }
        }

        public void MoveRow(int r, int incr)
        {
            for (int c = 0; c < ColNum; c++)
            {
                this.Grid[r + incr, c] = this.Grid[r, c];
                this.Grid[r, c] = 0;
            }
        }

        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && Grid[r, c] == 0;
        }

        public bool IsInside(int r, int c)
        {
            return r >= 0 && r < RowNum && c >= 0 && c < ColNum;
        }

        public int ClearFullRows()
        {
            int cleared = 0;

            for(int r = RowNum - 1; r >= 0; r--)
            {
                if (isRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRow(r, cleared);
                }
            }

            return cleared;
        }
    }
}
