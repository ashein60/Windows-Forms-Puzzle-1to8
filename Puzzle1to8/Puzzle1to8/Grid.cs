using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Puzzle1to8
{
    public class Grid
    {
        private static int gridSize = 3;
        public Square[,] Squares = new Square[gridSize, gridSize];

        public static int GridSize
        {
            get { return gridSize; }
        }

        public Grid()
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    Squares[y, x] = new Square(x * Square.SquareSize + Square.GapSize, y * Square.SquareSize + Square.GapSize);
                }
            }
        }

        public void Click_Grid(Grid Grid1, int mouseX, int mouseY)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    Squares[y, x].Click_Move(Grid1, mouseX, mouseY, x, y);
                }
            }
        }

        public void Paint_Grid(PaintEventArgs e)
        {
            foreach (Square i in Squares)
            {
                i.Paint_Square(e);
            }
        }
    }
}
