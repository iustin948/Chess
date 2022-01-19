using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess
{
    public class Patrat
    {
        public string state;
        public int attacked;
        public int color;
        public PictureBox box;
        public int x, y;
        public List<Patrat> Take;
        public Pion pion;
        public Tura tura;
        public Patrat()
        {
            state = "empty";
            box = new PictureBox();
            Take = new List<Patrat>();
            pion = new Pion();
            tura = new Tura();
        }

    }
}
