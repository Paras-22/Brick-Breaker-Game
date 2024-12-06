using System.Drawing;
using System.Windows.Forms;

namespace Brick_game
{
    //This Class will handle the paddle.
    public class Paddle : PictureBox
    {

       
        public int Speed { get; set; }

        // Constructor to initialize the paddle
        public Paddle(Control parent)
        {
            // Set paddle properties
            this.Width = 100;
            this.Height = 20;
            this.BackColor = Color.Blue;
            this.Speed = 8; // Set paddle speed
            this.Parent = parent;

            // Set initial position of paddle
            this.Left = (parent.ClientSize.Width - this.Width) / 2;
            this.Top = parent.ClientSize.Height - this.Height - 30; 
        }
        private Point MouseDownLocation;
        // Method to move paddle to the left
        public void MoveLeft()
        {
            if (this.Left > 0)
            {
                this.Left -= Speed;
            }
        }

        // Method to move paddle to the right
        public void MoveRight()
        {
            if (this.Right < this.Parent.ClientSize.Width)
            {
                this.Left += Speed;
            }
        }



        // MouseDown Event: Save the mouse position when the button is pressed
        public new void MouseDown(object sender, MouseEventArgs e)
        {
            // Custom logic for handling mouse down
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                MouseDownLocation = e.Location; // Save mouse position
            }
        }

        public new void MouseMove(object sender, MouseEventArgs e)
        {
            // Custom logic for handling mouse movement
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                int newPaddleX = e.X + this.Left - MouseDownLocation.X;

                // Constrain the paddle to the boundaries of its parent container
                if (newPaddleX < 0) newPaddleX = 0;
                if (newPaddleX + this.Width > this.Parent.ClientSize.Width)
                    newPaddleX = this.Parent.ClientSize.Width - this.Width;

                this.Left = newPaddleX; // Update paddle position
            }
        }



    }
}

