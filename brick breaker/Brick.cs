using System.Drawing;
using System.Windows.Forms;

namespace Brick_game
{
    // Class representing a brick in the game
    public class Brick : PictureBox
    {
        // Constructor to initialize a brick with specified position and color
        public Brick(int left, int top, int width, int height, Color color)
        {
            this.Left = left;
            this.Top = top;
            this.Width = width;
            this.Height = height;
            this.BackColor = color;
        }
    }
}
