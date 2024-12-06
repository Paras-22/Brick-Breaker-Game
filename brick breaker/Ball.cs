using System.Drawing;
using System.Windows.Forms;

namespace Brick_game
{
    //This is the class that will control the ball movement.
    public class Ball : PictureBox
    {
       
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }

   
        public Ball()
        {
         
            this.Width = 20;
            this.Height = 20; 
            this.BackColor = Color.Red; 
            this.SpeedX = 5; 
            this.SpeedY = 5; 
        }

        // Method to move the ball
        public new void Move()
        {
           
            this.Left += SpeedX;
            this.Top += SpeedY;
        }

        // Method to reverse the horizontal direction of the ball
        public void ReverseX()
        {
           
            this.SpeedX = -this.SpeedX;
        }

        // Method to reverse the vertical direction of the ball
        public void ReverseY()
        {
           
            this.SpeedY = -this.SpeedY;
        }
    }
}
