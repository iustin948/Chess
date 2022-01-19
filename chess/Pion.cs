using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public class Pion
    {

        public int FirstMove;
        public int EnPassant;
        public Pion()
        {
            FirstMove = 1;
            EnPassant = 0;
        }
    }

    public class Tura
    {
        public int Rocada = 1;
    }

    public class Rege
    {
        public int Rocada = 1;
        public int color;
        public int x, y;
        public Rege(int i, int j, int x)
        {
            color = x;
            x = i;
            y = j;
        }
    }
}
