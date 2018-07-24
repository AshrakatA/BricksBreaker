using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using System.Net.Sockets;
using System.IO;

namespace WindowsFormsApplication2
{
    class BricksPanel : Panel
    {
        #region game elements
        public int score = 0;
        public int score1 = 0;
        public int score2 = 0;
        public int player = 400;
        public int player2 = 200;
        private bool p2=true;
        public int ballx = 400;
        public int bally = 250;
        public int ballrx = 2;
        public int ballry = 2;
        public int ballx2 = 200;
        public int bally2 = 250;
        public int ballrx2 = -2;
        public int ballry2 = -2;
        public bool start = true;
        private bricksgen b;
        public int r = 1;
        public int c = 2;
        public int lvl = 2;
        public int nbricks;
        System.Windows.Media.MediaPlayer mp = new System.Windows.Media.MediaPlayer();
        Image bat1img = Image.FromFile(@"dependencies/Dragon-scroll.png");
        Image ball1img = Image.FromFile(@"dependencies/ball1.png");

        #endregion

        public BricksPanel()
        {
            nbricks = r * c;
            this.DoubleBuffered = true;
            b = new bricksgen(r, c);
        }

        public void CloseGame()
        { Environment.Exit(0); }

      /*  public void move()
        {
            bool pause = false;
      //      if (KeyInputManager.getKeyState(Keys.Escape)) { Application.Exit(); }
            if (KeyInputManager.getKeyState(Keys.ControlKey)) { if (lvl < 10) nextlvl(); }
            if (KeyInputManager.getKeyState(Keys.Enter) && lvl < 10)
            {
                start = true;
                if (nbricks == 0)
                    nextlvl();
            }
            if (KeyInputManager.getKeyState(Keys.S))
            {
                if (nbricks == r * c || pause == true)
                    start = true;
            }
            if (KeyInputManager.getKeyState(Keys.P))
            {
                start = false;
            }
            if (KeyInputManager.getKeyState(Keys.Space) && lvl < 10) { restart(); }
            if (KeyInputManager.getKeyState(Keys.Right))
            {

                if (player < 561)
                    player += 20;
            }
            if (KeyInputManager.getKeyState(Keys.Left))
            {
                if (player > 0)
                    player -= 20;
            }
            if (KeyInputManager.getKeyState(Keys.D))
            {

                if (player2 < 561)
                    player2 += 20;
            }
            if (KeyInputManager.getKeyState(Keys.A))
            {
                if (player2 > 0)
                    player2 -= 20;
            }

        }*/

        public void nextlvl()
        {
            ballx = 400;
            bally = 250;
            ballrx = 2;
            ballry = 2;
            if (p2)
            {
                ballx2 = 200;
                bally2 = 250;
                ballrx2 = -2;
                ballry2 = -2;
                player2 = 200;
                score1 = 0;
                score2 = 0;
            }
            score = 0;
            r += 1;
            c += 1;
            player = 400;
            lvl++;
            nbricks = r * c;
            b = new bricksgen(r, c);
        }
        public void restart()
        {
            start = true;
            ballx = 400;
            bally = 250;
            ballrx = 2;
            ballry = 2;
            score = 0;
            if (p2)
            {
                ballx2 = 300;
                bally2 = 250;
                ballrx2 = -2;
                ballry2 = -2;
                player2 = 200;
                score1 = 0;
                score2 = 0;
            }
            score = 0;
            nbricks = r * c;
            player = 400;
            b = new bricksgen(r, c);
        }

        
        public void updateGame()
        {
            //move();
            if (lvl < 10)
            {
                if (start)
                {

                    if (new Rectangle(ballx, bally, 20, 20).IntersectsWith(new Rectangle(player, 470, 100, 10)))
                    {
                        ballry = -ballry;
                        
                    }

                    if (new Rectangle(ballx2, bally2, 20, 20).IntersectsWith(new Rectangle(player2, 470, 100, 10)))
                    {
                        ballry2 = -ballry2;
                       
                    }

                    for (int i = 0; i < b.getB().GetLength(0); i++)
                    {
                        for (int j = 0; j < b.getB().GetLength(1); j++)
                        {
                            if (b.getB()[i, j] > 0)
                            {
                                {
                                    int brickx = j * b.getBrickw() + 90;
                                    int bricky = i * b.getBrickh() + 70;
                                    int brickw = b.getBrickw();
                                    int brickh = b.getBrickh();
                                    if (new Rectangle(ballx, bally, 20, 20).IntersectsWith(new Rectangle(brickx, bricky, brickw, brickh)))
                                    {
                                        b.setValue(i, j);
                                        nbricks--;
                                        score += 10;
                                        score1 += 10;
                                        ballry = -ballry;
                                        ballrx = -ballrx;
                                        mp.Open(new Uri(@"dependencies/HitSoundEffect.wav", UriKind.Relative));

                                        mp.Play();

                                    } if (p2)
                                    {
                                        if (new Rectangle(ballx2, bally2, 20, 20).IntersectsWith(new Rectangle(brickx, bricky, brickw, brickh)))
                                        {
                                            b.setValue(i, j);
                                            nbricks--;
                                            score += 10;
                                            score2 += 10;
                                            ballry2 = -ballry2;
                                            ballrx2 = -ballrx2;
                                            mp.Open(new Uri(@"dependencies/HitSoundEffect.wav", UriKind.Relative));

                                            mp.Play();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    ballx += ballrx;
                    bally += ballry;
                    if (p2)
                    {
                        ballx2 += ballrx2;
                        bally2 += ballry2;
                    }
                    if (ballx < 0)
                    {
                        ballrx = -ballrx;
                    }
                    if (bally < 0)
                    {
                        ballry = -ballry;
                    }
                    if (ballx > 670)//700(x)-30(ballsize)
                    {
                        ballrx = -ballrx;
                    }
                    if (bally > 510) { start = false; }
                    if (p2)
                    {
                        if (ballx2 < 0)
                        {
                            ballrx2 = -ballrx2;
                        }
                        if (bally2 < 0)
                        {
                            ballry2 = -ballry2;
                        }
                        if (ballx2 > 670)//700(x)-30(ballsize)
                        {
                            ballrx2 = -ballrx2;
                        }
                        if (bally2 > 510) { start = false; }
                    }
                    if (nbricks == 0) start = false;

                }

            }
        }
        public void drawGame(Graphics g)
        {

            g.DrawImage(bat1img, player, 473, 120, 25);//bat of player 1

            //draw blocks
            b.draw(g);

            g.DrawImage(ball1img, ballx, bally, 25, 25);//ball

            //start
            if (start == false)
            {
                if (!(bally > 510 || bally2 > 510))
                    g.DrawString("ENTER to next level,Space to restart or ESC to exit", new Font("Algerian", 18, FontStyle.Regular), new SolidBrush(Color.Black), 20, 30);
                else g.DrawString("Space to restart or ESC to exit", new Font("Algerian", 20, FontStyle.Regular), new SolidBrush(Color.Black), 120, 20);
            }

            //score 
            if (p2 == false)
            {
                Color c1 = new Color();
                c1 = Color.FromArgb(58, 110, 74);
                g.DrawString("Score : " + score, new Font("Algerian", 20, FontStyle.Regular), new SolidBrush(c1), 5, 5);
            }

            //restart
            if (p2 == false)
            {
                if (bally > 510)
                {
                    g.DrawString("Game Over !", new Font("Algerian", 50, FontStyle.Regular), new SolidBrush(Color.Black), 150, 250);
                }
            }
            if (p2 == true)
            {
                if (bally > 510)
                {
                    g.DrawString("Player 2 Won!", new Font("Algerian", 50, FontStyle.Regular), new SolidBrush(Color.Black), 105, 240);
                    g.DrawString("Space to restart or ESC to exit", new Font("Algerian", 20, FontStyle.Regular), new SolidBrush(Color.Black), 120, 20);
                }
                if (bally2 > 510)
                {
                    g.DrawString("Player 1 Won!", new Font("Algerian", 50, FontStyle.Regular), new SolidBrush(Color.Black), 105, 240);
                    g.DrawString("Space to restart or ESC to exit", new Font("Algerian", 20, FontStyle.Regular), new SolidBrush(Color.Black), 120, 20);
                }
            }
            if (nbricks == 0)
            {
                if (p2)
                {
                    if (score1 > score2)
                    {
                        g.DrawString("Player 1 Won!", new Font("Algerian", 50, FontStyle.Regular), new SolidBrush(Color.Black), 105, 240);
                        g.DrawString("Enter to Go,Space to restart or ESC to exit", new Font("Algerian", 20, FontStyle.Regular), new SolidBrush(Color.Black), 120, 20);
                    }
                    else if (score1 < score2)
                    {
                        g.DrawString("Player 2 Won!", new Font("Algerian", 50, FontStyle.Regular), new SolidBrush(Color.Black), 105, 240);
                        g.DrawString("Enter to Go,Space to restart or ESC to exit", new Font("Algerian", 20, FontStyle.Regular), new SolidBrush(Color.Black), 120, 20);
                    }
                    else
                    {
                        g.DrawString("Tie!", new Font("Algerian", 50, FontStyle.Regular), new SolidBrush(Color.Black), 280, 200);
                    }
                }

                if (false == p2)
                {
                    g.DrawString("You Won!,Go to level : " + (lvl) + "?", new Font("Algerian", 35, FontStyle.Regular), new SolidBrush(Color.Black), 60, 250);
                }
            }

            if (p2)
            { b.paint_p2(g, player2, ballx2, bally2, score1, score2); }

            if (lvl == 10)
            {
                {
                    g.DrawString("You reached max level. ", new Font("Algerian", 35, FontStyle.Regular), new SolidBrush(Color.Black), 60, 250);
                    g.DrawString("Press Escape to exit", new Font("Algerian", 20, FontStyle.Regular), new SolidBrush(Color.Black), 200, 300);
                }
            }
        }

    }


}
