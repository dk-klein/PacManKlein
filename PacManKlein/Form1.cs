using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacManKlein
{
    public partial class GameBoard : Form
    {
        // start the variables

        bool goup;
        bool godown;
        bool goleft;
        bool goright;
        bool isGameOver;

        int speed = 10;

        //ghost 1 and 2 variables. These guys are sane well sort of
        int redGhostSpeed = 12;
        int yellowGhostSpeed = 12;

        //ghost 3 crazy variables
        int pinkGhostX;
        int pinkGhostY;

        int score;

        // end of listing variables
        public GameBoard()
        {
            InitializeComponent();
            resetGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
                pacman.Image = Properties.Resources.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = true;

                pacman.Image = Properties.Resources.right;
            }
            if (e.KeyCode == Keys.Up)
            {

                goup = true;
                pacman.Image = Properties.Resources.Up;
            }
            if (e.KeyCode == Keys.Down)
            {

                godown = true;
                pacman.Image = Properties.Resources.down;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
            }
        }
        private void resetGame()
        {
            scoreLabel.Text = "Score: 0";
            score = 0;

            isGameOver = false;

            pacman.Left = 31;
            pacman.Top = 46;
            pinkGhostX = 5;
            pinkGhostY = 5;

            redGhost.Left = 193;
            redGhost.Top = 66;

            pinkGhost.Left = 396;
            pinkGhost.Top = 237;

            yellowGhost.Left = 104;
            yellowGhost.Top = 399;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }
            resultLabel.Visible = false;
            gameTimer.Start();

        }
        private void gameOver(string message)
        {

            isGameOver = true;

            gameTimer.Stop();

            resultLabel.Text = "Score: " + score + Environment.NewLine + message;
            resultLabel.Visible = true;
        }
        private void mainGameTimer_Tick(object sender, EventArgs e)
        {
            scoreLabel.Text = "Score: " + score; // show the score on the board

            //player movement codes start
            if (goleft)
            {
                pacman.Left -= speed;
                //moving player to the left. 
            }
            if (goright)
            {
                pacman.Left += speed;
                //moving player to the right
            }
            if (goup)
            {
                pacman.Top -= speed;
                //moving to the top
            }

            if (godown)
            {
                pacman.Top += speed;
                //moving down
            }
            //player movements code end

            if (pacman.Left < -10)
            {
                pacman.Left = 625;
            }
            if (pacman.Left > 625)
            {
                pacman.Left = -10;
            }

            if (pacman.Top < -10)
            {
                pacman.Top = 570;
            }
            if (pacman.Top > 570)
            {
                pacman.Top = 0;
            }

            //for loop to check walls, ghosts and points
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false;
                        }
                    }

                    if ((string)x.Tag == "wall")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("YOU LOSE!");
                        }


                        if (pinkGhost.Bounds.IntersectsWith(x.Bounds))
                        {
                            pinkGhostX = -pinkGhostX;
                        }
                    }


                    if ((string)x.Tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("YOU LOSE!");
                        }

                    }

                    if (score == 42)
                    {
                        gameOver("YOU WIN!");
                    }
                }
            }

            // end of for loop checking walls, points and ghosts. 

            redGhost.Left += redGhostSpeed;

            if (redGhost.Bounds.IntersectsWith(wallRed1.Bounds) || redGhost.Bounds.IntersectsWith(wallRed2.Bounds))
            {
                redGhostSpeed = -redGhostSpeed;
            }

            yellowGhost.Left -= yellowGhostSpeed;

            if (yellowGhost.Bounds.IntersectsWith(wallYellow1.Bounds) || yellowGhost.Bounds.IntersectsWith(wallYellow2.Bounds))
            {
                yellowGhostSpeed = -yellowGhostSpeed;
            }


            pinkGhost.Left -= pinkGhostX;
            pinkGhost.Top -= pinkGhostY;


            if (pinkGhost.Top < 0 || pinkGhost.Top > 500)
            {
                pinkGhostY = -pinkGhostY;
            }

            if (pinkGhost.Left < 0 || pinkGhost.Left > 555)
            {
                pinkGhostX = -pinkGhostX;
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox49_Click(object sender, EventArgs e)
        {

        }

        private void redGhost_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox109_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox110_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox111_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox112_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox113_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox114_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox115_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox116_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox117_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox62_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox63_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox64_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox65_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox66_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox67_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox68_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox69_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox70_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox33_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox41_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox42_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox43_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox44_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox45_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox46_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox48_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox49_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox38_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox39_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox34_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox35_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox36_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox37_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox40_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox47_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox50_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox62_Click_1(object sender, EventArgs e)
        {

        }
    }
}
