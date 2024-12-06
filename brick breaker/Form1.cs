//
// =======================================================
// This is the break breaker Game that is created by Paras.
// =======================================================
//Programming 2 (Block 4)
// =======================================================
//

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Brick_game
{
    public partial class Form1 : Form
    {
        // Boolean flags for keyboard-based movement
        bool goLeft;
        bool goRight;
        GameManager gameManager;

        // Property to track the mouse-down location for paddle control
        public Point MouseDownLocation { get; set; }

        public Form1()
        {
            InitializeComponent();

            // Set the form size and behavior
            this.ClientSize = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Initialize the game manager and set up the game
            gameManager = new GameManager(this);
            gameManager.SetupGame();

            // Subscribe to keyboard and mouse events
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += keyisup;
            this.KeyDown += keyisdown;

            // Attach mouse events for paddle control
            //gameManager.Paddle.MouseDown += Paddle_MouseDown;
            //gameManager.Paddle.MouseMove += Paddle_MouseMove;

        }

        // Event handler for KeyDown event
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Restart the game if Enter key is pressed and the game is over
            if (e.KeyCode == Keys.Enter && gameManager.IsGameOver())
            {
                gameManager.RemoveBricks();
                gameManager.SetupGame();
            }
        }

        // Main game loop event
        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            // Update the score display
            txtScore.Text = "Score: " + gameManager.GetScore();

            // Handle paddle and ball movements
            gameManager.MovePaddle(goLeft, goRight);
            gameManager.MoveBall();
        }

        // Event handler for KeyDown event to track arrow key presses
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = true;
            if (e.KeyCode == Keys.Right) goRight = true;
        }

        // Event handler for KeyUp event to track arrow key releases
        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = false;
            if (e.KeyCode == Keys.Right) goRight = false;
        }

        // Event handler for Paddle MouseDown
        private void Paddle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                MouseDownLocation = e.Location; // Save the mouse click location
            }
        }

        // Event handler for Paddle MouseMove
        private void Paddle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                // Calculate the new paddle position
                int newPaddleX = e.X + gameManager.Paddle.Left - MouseDownLocation.X;

                // Ensure the paddle stays within the form's horizontal boundaries
                if (newPaddleX < 0)
                    newPaddleX = 0;
                if (newPaddleX + gameManager.Paddle.Width > this.ClientSize.Width)
                    newPaddleX = this.ClientSize.Width - gameManager.Paddle.Width;

                // Update the paddle's position
                gameManager.Paddle.Left = newPaddleX;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true; // Ensures the form captures key events
        }

        private void txtScore_Click(object sender, EventArgs e)
        {

        }


        private void ball_Click_1(object sender, EventArgs e)
        {

        }
    }
}
