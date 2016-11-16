using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectDavidMax
{
    public class BusinessPresentation : IDisplay
    {
        private static BusinessPresentation instance;
        private static OutputInput outputInput;
        private static Bitmap originalBitmap = null;
        private Bitmap filteredBitmap = null;
        private IExtBitmap extBitmap;

       

        // Singleton
        private BusinessPresentation()
        {
            outputInput = new OutputInput();
            extBitmap = new ExtBitmap();
        }

        public static BusinessPresentation getInstance()
        {
            if (instance == null)
                instance = new BusinessPresentation();

            return instance;
        }

        // Getters and setters
        public Bitmap FilteredBitmap
        {
            get
            {
                return filteredBitmap;
            }

            set
            {
                filteredBitmap = value;
            }
        }

        public void setExtBitmap(IExtBitmap extBitmap)
        {
            this.extBitmap = extBitmap;
        }

        //  Click methods

        // Click on Filter
        public void ClickFilter(PictureBox picPreview)
        {
            // Get the image from the form
            Bitmap workImage = getImage(picPreview);

            // If there is no image in the PictureBox, do nothing
            if (workImage == null)
            {
                return;
            }

            try
            {
                // Apply the filter
                workImage = extBitmap.Laplacian3x3Filter(workImage, false);
            }
            catch (Exception)
            {
                // If there is an exception in the filter, do nothing in the form; filtered image remains null
                return;
            }
           
            // Put the filtered image in the form
            putImage(picPreview, workImage);

            // Keep the filtered image in this
            FilteredBitmap = workImage;
        }

        // Click on Load image
        public void ClickLoad(PictureBox picPreview)
        {
            // Get the image from the disk
            Bitmap workImage = outputInput.LoadImage();

            // If image is not null, put it in the form
            if (workImage != null)
            {
                putImage(picPreview, workImage);
            }

            // Save the non filtered image - just in case we need it in a next release
            originalBitmap = workImage;
        }

        public void ClickSave(PictureBox picPreview)
        {
            ISave output = new OutputInput();
            // Get the image from the form
            Bitmap workImage = getImage(picPreview);

            // todo : if the image is null

            // Save the image on the disk
            output.SaveImage(workImage);
        }

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
