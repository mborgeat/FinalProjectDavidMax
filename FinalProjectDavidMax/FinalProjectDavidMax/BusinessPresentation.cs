using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectDavidMax
{
    class BusinessPresentation : IDisplay
    {
        private static BusinessPresentation instance;
        private static OutputInput outputInput;
        private static Bitmap originalBitmap = null;
        private BusinessPresentation()
        {
            outputInput = new OutputInput();
        }

        public static BusinessPresentation getInstance()
        {
            if (instance == null)
                instance = new BusinessPresentation();

            return instance;
        }
        public void ClickFilter(PictureBox picPreview)
        {
            Bitmap workImage = getImage(picPreview);
            workImage = ExtBitmap.Laplacian3x3Filter(workImage, false);
            putImage(picPreview, workImage);

            
        }

        public void ClickLoad(PictureBox picPreview)
        {
            Bitmap workImage = outputInput.LoadImage();
            if (workImage != null)
            {
                putImage(picPreview, workImage);
            }
            originalBitmap = workImage;
        }

        public void ClickSave(PictureBox picPreview)
        {
            outputInput.SaveImage(getImage(picPreview));
        }

        public Bitmap getImage(PictureBox pictureBox)
        {
            return (Bitmap) pictureBox.Image;
        }

        public void putImage(PictureBox pictureBox, Bitmap image)
        {
            pictureBox.Image = image;
        }
    }
}
