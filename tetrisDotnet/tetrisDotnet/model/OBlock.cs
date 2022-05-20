using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace tetrisDotnet.model
{
    public class OBlock : Block
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new Position(0, 0), new Position(0, 1), new Position(1, 0), new Position(1, 1) }
        };

        private readonly Color color = Colors.Yellow;
        protected override Color Color => color;

        public override int Id => 4;
        protected override Position StartOffset => new Position(0, 4);
        protected override Position[][] Tiles => tiles;
    }
}
