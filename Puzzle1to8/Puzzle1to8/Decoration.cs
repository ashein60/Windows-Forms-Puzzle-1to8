using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Puzzle1to8
{
    class Decoration
    {


        public static void FillPiece(PaintEventArgs e, Brush activeBrush, int positionX, int positionY, int squareSize)
        {
            LayerSquare(e, activeBrush, positionX, positionY, squareSize);
        }

        public static void RoundSquare(PaintEventArgs e, Brush activeBrush, int positionX, int positionY, int squareSize)
        {
            int circleSize = 8; //to round edges

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None; //standard
            e.Graphics.FillRectangle(activeBrush, positionX + (circleSize / 2), positionY, squareSize - circleSize, squareSize);
            e.Graphics.FillRectangle(activeBrush, positionX, positionY + (circleSize / 2), squareSize, squareSize - circleSize);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //smoothes
            e.Graphics.FillEllipse(activeBrush, positionX, positionY, circleSize, circleSize);
            e.Graphics.FillEllipse(activeBrush, positionX + squareSize - circleSize, positionY, circleSize, circleSize);
            e.Graphics.FillEllipse(activeBrush, positionX, positionY + squareSize - circleSize, circleSize, circleSize);
            e.Graphics.FillEllipse(activeBrush, positionX + squareSize - circleSize, positionY + squareSize - circleSize, circleSize, circleSize);
        }

        public static void LayerSquare(PaintEventArgs e, Brush activeBrush, int positionX, int positionY, int squareSize)
        {
            Brush LayeredBrush = new SolidBrush(Color.FromArgb(80, 80, 80));

            int overLap = 3;

            RoundSquare(e, LayeredBrush, positionX, positionY, squareSize);
            RoundSquare(e, activeBrush, positionX + overLap, positionY + overLap, squareSize - (overLap * 2));

            LayeredBrush.Dispose();
        }
    }
}
