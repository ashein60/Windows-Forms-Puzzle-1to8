using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Puzzle1to8
{
    public class Square
    {
        private static int squareSize = 450/Grid.GridSize;
        private static int gapSize = 1;

        private static Point inActive; //holds the coordinates of the inactive space
        private static Point previousInactive = new Point(-1, -1); //in set up, removes the possibility of undoing an action

        private static int numberAssigner = 1; 
        private string number; //what number is located in this square, -1 if inactive
        private bool active; //piece holds a number

        private int positionX;
        private int positionY;

        public static int SquareSize
        {
            get { return squareSize; }
        }

        public static int GapSize
        {
            get { return gapSize; }
        }

        public Square(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;

            number = Convert.ToString(numberAssigner);
            numberAssigner++;
            active = true;
        }

        public static void SetLastInactive(Grid Grid1) //first step in starting the game
        {
            Grid1.Squares[Grid.GridSize - 1, Grid.GridSize - 1].active = false;
            Grid1.Squares[Grid.GridSize - 1, Grid.GridSize - 1].number = "-1";
            inActive = new Point(Grid.GridSize - 1, Grid.GridSize - 1);
        }

        public void Click_Move(Grid Grid1, int mouseX, int mouseY, int x, int y)
        {
            if (mouseX >= positionX && mouseX <= positionX + squareSize)
            {
                if (mouseY >= positionY && mouseY <= positionY + squareSize)
                {
                    MoveEvent(Grid1, x, y);
                }
            }
        }

        public static void MoveEvent(Grid Grid1, int x, int y) //when an object is clicked, do this
        {
            if (x == inActive.X) //tests for the same row or column
            {
                if (y + 1 == inActive.Y || y - 1 == inActive.Y) //tests if touching 
                {
                    SwitchActives(Grid1, x, y);
                }
            }
            else if (y == inActive.Y)
            {
                if (x + 1 == inActive.X || x - 1 == inActive.X)
                {
                    SwitchActives(Grid1, x, y);
                }
            }
        }

        public static void SwitchActives(Grid Grid1, int x, int y) //switches the inactive and active numbers and active bool
        {
            Grid1.Squares[inActive.Y, inActive.X].number = Grid1.Squares[y, x].number; //switch numbers
            Grid1.Squares[y, x].number = "-1";

            Grid1.Squares[inActive.Y, inActive.X].active = true; //switch actives
            Grid1.Squares[y, x].active = false;

            previousInactive.X = inActive.X; //sets the previous inactive for game setup randomization
            previousInactive.Y = inActive.Y;

            inActive.X = x; //replace values
            inActive.Y = y;
        }

        public static void RandomizeNumbers(Grid Grid1, ref int i)
        {
            Random rand = new Random();
            int x, y;

            if (rand.Next(0, 2) == 0) // decides if x or y is the same as the inactive x or y
            {
                x = inActive.X;

                if (inActive.Y < Grid.GridSize - 1 && inActive.Y > 0) //checks if not on the edge
                {
                    if (rand.Next(0, 2) == 0)
                    {
                        y = inActive.Y + 1;
                    }
                    else
                    {
                        y = inActive.Y - 1;
                    }
                }
                else if (inActive.Y < Grid.GridSize - 1) //if not on left edge
                {
                    y = inActive.Y + 1;
                }
                else //if not on right edge
                {
                    y = inActive.Y - 1;
                }
            }
            else //and repeat but for x
            {
                y = inActive.Y;

                if (inActive.X < Grid.GridSize - 1 && inActive.X > 0)
                {
                    if (rand.Next(0, 2) == 0)
                    {
                        x = inActive.X + 1;
                    }
                    else
                    {
                        x = inActive.X - 1;
                    }
                }
                else if (inActive.X < Grid.GridSize - 1)
                {
                    x = inActive.X + 1;
                }
                else
                {
                    x = inActive.X - 1;
                }
            }

            if (!(previousInactive.X == x && previousInactive.Y == y))
            {
                SwitchActives(Grid1, x, y);
            }
            else
            {
                i--; //redo to accurately move correct number of times without undoing movement
            }
        }

        public void Paint_Square(PaintEventArgs e)
        {
            Brush activeBrush = new SolidBrush(Color.LightGray);
            Brush Black = new SolidBrush(Color.Black);
            Font numberFont = new Font("Ariel", SquareSize/4);

            if (active == true)
            {
                Decoration.FillPiece(e, activeBrush, positionX, positionY, squareSize - gapSize);
                //e.Graphics.FillRectangle(activeBrush, positionX, positionY, squareSize - gapSize, squareSize - gapSize);
                e.Graphics.DrawString(number, numberFont, Black, positionX + (squareSize / 3), positionY + (squareSize / 3));
            }

            activeBrush.Dispose();
            Black.Dispose();
            numberFont.Dispose();
        }
    }
}
