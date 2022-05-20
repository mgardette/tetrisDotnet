﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace tetrisDotnet.model
{
    public class TBlock : Block
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new Position(0,1), new Position(1, 0), new Position(1, 1), new Position(1, 2) },
            new Position[] { new Position(0,1), new Position(1, 1), new Position(1, 2), new Position(2, 1) },
            new Position[] { new Position(1,0), new Position(1, 1), new Position(1, 2), new Position(2, 1) },
            new Position[] { new Position(0,1), new Position(1, 0), new Position(1, 1), new Position(2, 1) },
        };

        private readonly Color color = Colors.Purple;
        protected override Color Color => color;

        public override int Id => 6;
        protected override Position StartOffset => new Position(0, 3);
        protected override Position[][] Tiles => tiles;
    }
}
