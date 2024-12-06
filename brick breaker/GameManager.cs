using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Brick_game
{
    // This Class is responsible for managing the game logic.
    public class GameManager
    {
        private Form1 form;
        private Ball ball;
        private Paddle paddle;
        
        private List<Brick> bricks;
        private Random rnd = new Random();
        private bool isGameOver;
        private bool isGameWon;
        private int score;
        public Paddle Paddle
        {
            get { return paddle; }
        }

        // Constructor to initialize the game manager with the main form
        public GameManager(Form1 form)
        {
            this.form = form;
            this.ball = new Ball();
            this.paddle = new Paddle(form); 
            this.bricks = new List<Brick>();
            this.isGameOver = false;
            this.score = 0;

            // Add the ball and paddle to the form's controls
            this.form.Controls.Add(this.ball);
            this.form.Controls.Add(this.paddle);
        }

        // Method to set up the initial game state
        public void SetupGame()
        {
            isGameWon = false;
            isGameOver = false;
            score = 0;
            ball.SpeedX = 5;
            ball.SpeedY = 5;
            paddle.Speed = 12;
            form.txtScore.Text = "Score: " + score;

            // Set initial positions of the ball and paddle
            ball.Left = (form.ClientSize.Width - ball.Width) / 2;
            ball.Top = form.ClientSize.Height - paddle.Height - ball.Height - 30;
            paddle.Left = (form.ClientSize.Width - paddle.Width) / 2;
            paddle.Top = form.ClientSize.Height - paddle.Height - 30;

            // Start the game timer
            form.GameTimer.Interval = 30;
            form.GameTimer.Start();

           
            PlaceBricks();
        }

        // Method to handle the movement of the paddle
        public void MovePaddle(bool goLeft, bool goRight)
        {
            if (goLeft) paddle.MoveLeft();
            if (goRight) paddle.MoveRight();
        }

        // Method to handle the movement of the ball and collision detection
        public void MoveBall()
        {
            ball.Move();

            // Handle ball collision with walls
            if (ball.Left < 0 || ball.Right > form.ClientSize.Width)
            {
                ball.ReverseX();
            }
            if (ball.Top < 0)
            {
                ball.ReverseY();
            }

            // Handle ball collision with paddle
            if (ball.Bounds.IntersectsWith(paddle.Bounds))
            {
                ball.Top = paddle.Top - ball.Height;
                ball.ReverseY();
                // Adjust ball speed based on paddle movement
                if (ball.SpeedX < 0)
                {
                    ball.SpeedX = -rnd.Next(5, 12);
                }
                else
                {
                    ball.SpeedX = rnd.Next(5, 12);
                }
            }

            // Handle ball collision with bricks
            foreach (Brick brick in bricks.ToList())
            {
                if (ball.Bounds.IntersectsWith(brick.Bounds))
                {
                    score += 1;
                    ball.ReverseY();
                    form.Controls.Remove(brick);
                    bricks.Remove(brick);
                }
            }

            // Check if the ball is below the game board
            if (ball.Top > form.ClientSize.Height)
            {
                GameOver("You Lose!! Press Enter to try again");
            }

            // Check if all bricks are destroyed
            if (isGameOver || isGameWon) return; 
            if (bricks.Count == 0)
            {
                GameOver("You Win!! Press Enter to play again");
                return;  
            }
        }

        // Method to display the game over message
        private void GameOver(string message)
        {
            isGameOver = true;
            form.GameTimer.Stop();
            form.txtScore.Text = "Score: " + score  + " " + message;
          
            form.txtScore.TextAlign = ContentAlignment.MiddleLeft;
            form.txtScore.Size = new Size(form.ClientSize.Width - 20, 40); 
            form.txtScore.Location = new Point(10, 10);
        }

        // Method to place bricks on the game board
        private void PlaceBricks()
        {
            int rows = 5, cols = 10; 
            int brickWidth = 70, brickHeight = 30, padding = 5;
            int startX = (form.ClientSize.Width - (cols * (brickWidth + padding))) / 2; 
            int startY = 50; 

            Random rnd = new Random(); 

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // Create a new brick with a random color
                    Color randomColor = Color.FromArgb(100, 100, 100);

                    Brick brick = new Brick(
                        startX + col * (brickWidth + padding), 
                        startY + row * (brickHeight + padding), 
                        brickWidth, brickHeight, randomColor 
                    );

                    form.Controls.Add(brick); 
                    bricks.Add(brick); 
                }
            }
        }


        // Method to get the current score
        public int GetScore()
        {
            return score;
        }

        // Method to check if the game is over
        public bool IsGameOver()
        {
            return isGameOver;
        }

        // Method to remove all bricks from the game board
        public void RemoveBricks()
        {
            foreach (Brick brick in bricks)
            {
                form.Controls.Remove(brick);
            }
            bricks.Clear();
        }
    }
}
