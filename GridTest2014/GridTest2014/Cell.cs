using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridTest2014
{
   public class Cell
    {
       public int count;
        public int type;
        public bool north;
        public bool south;
        public bool east;
        public bool west;

        public Cell(int count, int type, bool n, bool s, bool e, bool w)
        {
            this.count = count;
            this.type = type;
            this.north = n;
            this.south = s;
            this.east = e;
            this.west = w;
        }

    }
}
