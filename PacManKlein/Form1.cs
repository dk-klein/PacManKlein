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
        //pohybove promenne
        bool goup;
        bool godown;
        bool goleft;
        bool goright;

        Game game = new Game();
        public GameBoard()
        {
            InitializeComponent();
            ResetGame();
        }
        private void mainGameTimer_Tick(object sender, EventArgs e)
        {
            scoreLabel.Text = "Score: " + game.GetScore(); // ukazovani score 
            Movement();         //pohyb hrace
            TeleportPacman();   //teleport pacmana na stranach
            CheckCollision();   //kolize
            BasicGhostMovementChange(); //pohyb duchu
            PinkGhostMovementChange(); //pohyb ruzoveho ducha
        }
        private void ResetGame()
        {
            scoreLabel.Text = "Score: 0";
            game.SetGameOverFalse();
            //urcovani zakladnich pozic duchu a pacmana
            pacman.Left = 30;
            pacman.Top = 45;
            redGhost.Left = 180;
            redGhost.Top = 80;
            pinkGhost.Left = 215;
            pinkGhost.Top = 400;
            yellowGhost.Left = 85;
            yellowGhost.Top = 400;
            //konec zakladnich pozic
            foreach (Control x in this.Controls)    //zobrazeni zpet vsech minci 
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }
            resultLabel.Visible = false;
            gameTimer.Start();
            game.ResetGame(); 

        }
        private void GameOver(string message)
        {
            game.SetGameOverTrue();
            game.GameOver(message, gameTimer, resultLabel);
        }
        private void TeleportPacman() //teleport pacmana ze strany na stranu a ze shoru dolu
        {
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
        }
        private void Movement()         //pohyb hrace
        {
            if (goleft)
            {
                pacman.Left -= game.pacmanspeed;
            }
            if (goright)
            {
                pacman.Left += game.pacmanspeed;
            }
            if (goup)
            {
                pacman.Top -= game.pacmanspeed;
            }
            if (godown)
            {
                pacman.Top += game.pacmanspeed;
            }
        }
        private void PinkGhostMovementChange()
        {
            pinkGhost.Left -= game.pinkGhostXSpeed;
            pinkGhost.Top -= game.pinkGhostYSpeed;

            //pozice ruzoveho ducha vs steny obrazovky
            if (pinkGhost.Top < 0 || pinkGhost.Top > 500)
            {
                game.pinkGhostYSpeed = -game.pinkGhostYSpeed;
            }

            if (pinkGhost.Left < 0 || pinkGhost.Left > 555)
            {
                game.pinkGhostXSpeed = -game.pinkGhostXSpeed;
            }
        }
        private void BasicGhostMovementChange()
        {
            redGhost.Left += game.redGhostSpeed;
            //kolize zakladnich duchu se stenami
            if (redGhost.Bounds.IntersectsWith(wallRed1.Bounds) || redGhost.Bounds.IntersectsWith(wallRed2.Bounds))
            {
                game.redGhostSpeed = -game.redGhostSpeed;
            }
            yellowGhost.Left -= game.yellowGhostSpeed;
            if (yellowGhost.Bounds.IntersectsWith(wallYellow1.Bounds) || yellowGhost.Bounds.IntersectsWith(wallYellow2.Bounds))
            {
                game.yellowGhostSpeed = -game.yellowGhostSpeed;
            }
        }
        private void CheckCollision()    //kolize
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds)) //kolize pacmana s minci
                        {
                            game.IncrementScore(); //pricteni score
                            x.Visible = false; //skryti mince
                        }
                    }
                    if ((string)x.Tag == "wall")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds)) //kolize pacmana se stenou
                        {
                            GameOver("YOU LOSE!");
                        }
                        if (pinkGhost.Bounds.IntersectsWith(x.Bounds))  //kolize se stenou pro ruzoveho ducha
                        {
                            game.pinkGhostXSpeed = -game.pinkGhostXSpeed;
                        }
                    }
                    if ((string)x.Tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds)) //kolize pacmana s duchem
                        {
                            GameOver("YOU LOSE!");
                        }
                    }
                    if (game.GetScore() == game.winningScore) //vyherni score je dvojnasobek odpovedi na zakladni otazku zivota, vesmiru a vubec
                    {
                        GameOver("YOU WIN!");
                    }
                }
            }
        }
        private void KeyIsDown(object sender, KeyEventArgs e) //metoda pro pohyb, zmacknuti klavesy
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

        private void KeyIsUp(object sender, KeyEventArgs e)    //metoda pro pohyb, pusteni klavesy
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
            if (e.KeyCode == Keys.Enter && game.GetGameOver() == true)
            {
                ResetGame();
            }
        }

    }
}
