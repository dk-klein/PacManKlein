using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacManKlein
{
    class Game
    {
        //rychlosti
        public int pacmanspeed = 8;
        public int redGhostSpeed = 10;
        public int yellowGhostSpeed = 10;
        public int pinkGhostXSpeed = 5;
        public int pinkGhostYSpeed = 5;
        //herni promenne
        public int score;
        public bool isGameOver;
        public int winningScore = 84; //vyherni score je dvojnasobek odpovedi na zakladni otazku zivota, vesmiru a vubec
        public void SetGameOverTrue()
        {
            isGameOver = true;
        }
        public void SetGameOverFalse()
        {
            isGameOver = false;
        }
        public bool GetGameOver()
        {
            return isGameOver;
        }
        public void GameOver(string message, Timer gameTimer, Label resultLabel)
        {
            gameTimer.Stop();
            resultLabel.Text = "Score: " + score + Environment.NewLine + message;
            resultLabel.Visible = true;
        }
        public void ResetGame()
        {
            score = 0;
        }

        public int GetScore()
        {
            return score;
        }
        public void IncrementScore()
        {
            score++;
        }
    }
}
