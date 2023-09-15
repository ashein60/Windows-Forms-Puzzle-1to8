using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Puzzle1to8
{
    public partial class puzzle1To8 : Form
    {
        Grid Grid1 = new Grid();

        private int mouseX;
        private int mouseY;

        public puzzle1To8()
        {
            InitializeComponent();

            Square.SetLastInactive(Grid1);
            RandomizePieces(20);
            this.Invalidate();
        }

        public void RandomizePieces(int repeatNum)
        {
            for (int i = 0; i < repeatNum; i++) //does this as many times as repeatNum
            {
                Square.RandomizeNumbers(Grid1, ref i); //ref i to change it if randomizer undos itself
                this.Invalidate();
            }
        }

        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            mouseX = this.PointToClient(Cursor.Position).X;
            mouseY = this.PointToClient(Cursor.Position).Y;

            Grid1.Click_Grid(Grid1, mouseX, mouseY);
            this.Invalidate();
        }

        private void Paint_Form(object sender, PaintEventArgs e)
        {
            Grid1.Paint_Grid(e);
        }
    }
}
