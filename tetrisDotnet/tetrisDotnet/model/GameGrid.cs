using System;
using System.Collections.Generic;
using System.Text;

namespace tetrisDotnet.model
{
    public class GameGrid
    {
        public int[,] Grid { get; set; }

        private const int ColNum = 10;

        private const int RowNum = 20;

        public GameGrid()
        {
            this.Grid = new int[RowNum, ColNum];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 10; i++)
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
