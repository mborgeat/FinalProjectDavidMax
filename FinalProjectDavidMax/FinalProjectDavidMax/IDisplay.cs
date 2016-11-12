using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectDavidMax
{
    interface IDisplay
    {
        Bitmap getImage(PictureBox pictureBox);

        void putImage(PictureBox pictureBox, Bitmap image);
    }
}
