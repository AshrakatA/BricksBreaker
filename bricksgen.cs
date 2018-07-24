using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication2
{
    class bricksgen
    {
        private int[,] b;
        private int brickw;
        private int brickh;
        Image brickimg = Image.FromFile(@"dependencies/brick.png");
        Image bat2img = Image.FromFile(@"dependencies/Dragon-scroll2.png");
        Image ball2img = Image.FromFile(@"dependencies/ball2.png");
        
        public bricksgen(int r, int c)
        {
            
            b = new int[r, c];
            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                { b[i, j] = 1; }
            } brickw = 520 / c; brickh = 100 / r;
        }
        public void draw(Graphics g)
        {
            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    if (b[i, j] > 0)
                    {
                        if ((i + j) % 2 == 0)
                        {
                            g.DrawImage(brickimg, j * brickw + 90, i * brickh + 70, brickw, brickh);
                        }
                        else
                        {
                            g.DrawImage(brickimg, j * brickw + 90, i * brickh + 70, brickw, brickh);
                        }
                    }
                }
            }
        }
        public void paint_p2(Graphics g, int player2, int ballx2, int bally2, int score1, int score2)
        {
            Color c1 = new Color();
            c1 = Color.FromArgb(58, 110, 74);

            g.DrawImage(bat2img, player2, 473, 120, 25);//bat2
            g.DrawImage(ball2img, ballx2, bally2, 27, 27);//ball2

            //score1
            g.DrawString("Score1 : " + score1, new Font("Algerian", 20, FontStyle.Regular), new SolidBrush(c1), 5, 5);
            //score2
            g.DrawString("Score2 : " + score2, new Font("Algerian", 20, FontStyle.Regular), new SolidBrush(c1), 535, 5);

        }
        public void setValue(int r, int c)
        {
            b[r, c] = 0;
        }
        public int[,] getB()
        {
            return b;
        }
        public int getBrickw()
        {
            return brickw;
        }
        public int getBrickh()
        {
            return brickh;
        }
    }
}

