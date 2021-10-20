using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout_room
{
    public partial class Form1 : Form
    {

        bool goLeft;
        bool goRight;
        bool isGameOver;


        int score;
        int ballx;
        int bally;
        int playerSpeed;

        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            setupGame();
        }

        private void setupGame()
        {
            score = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 12;
            txtScore.Text = "Score: " + score;
            GameTimer.Start();
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "Blocks")
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }
        }


        private void gameOver(string message)
        {
            isGameOver = true;
            GameTimer.Stop();

            txtScore.Text = "Score: " + score + " " + message;
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            if(goLeft == true && Player.Left > 0)
            {
                Player.Left -= playerSpeed;
            }

            if(goRight == true && Player.Left < 1591)
            {
                Player.Left += playerSpeed;
            }

            Ball.Left += ballx;
            Ball.Top += bally;
            if (Ball.Left < 0 || Ball.Left > 1671)
            {
                ballx = -ballx;

            }
            if (Ball.Top < 0)
            {
                bally = -bally;
            }
            if (Ball.Bounds.IntersectsWith(Player.Bounds))
            {
                bally = rnd.Next(5, 12) * -1;
                if(ballx < 0)
                {
                    ballx = rnd.Next(5, 12) * -1;

                }
                else
                {
                    ballx = rnd.Next(5, 12);
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Blocks")
                {
                    if(Ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        bally = -bally;
                        this.Controls.Remove(x);
                    }

                }
            }

            if(score == 180)
            {
                //end game message here
                gameOver("you Win!!!");
            }

            if(Ball.Top > 1223)
            {
                //end game message here
                gameOver("You Lose!!!");
            }
        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;

            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

            

        }

        private void KeyisUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;

            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }
    }
}
