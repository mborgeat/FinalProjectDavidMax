using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectDavidMax
{
    class Display : IDisplay
    {

        // Methods overrided from IDisplay
        // Method for getting the image from the form
        public Bitmap getImage(PictureBox pictureBox)
        {
            return (Bitmap)pictureBox.Image;
        }

        // Method for sending the image to the form
        public void putImage(PictureBox pictureBox, Bitmap image)
        {
            pictureBox.Image = image;
        }
    }
}
