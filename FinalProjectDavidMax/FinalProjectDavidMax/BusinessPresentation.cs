using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectDavidMax
{
    public class BusinessPresentation
    {
        private static Bitmap originalBitmap = null;
        private Bitmap filteredBitmap = null;
        private IExtBitmap extBitmap;
        private IDisplay display;
        private ILoad iLoad;
        private ISave iSave;
        public Boolean iHaveException = false;



        // Singleton
        public BusinessPresentation(ILoad iLoad, ISave iSave, IExtBitmap extBitmap, IDisplay display)
        {
            this.display = display;
            this.extBitmap = extBitmap;
            this.iLoad = iLoad;
            this.iSave = iSave;
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
        public string ClickFilter(PictureBox picPreview)
        {
            // If picPreview is null, do nothing...
            if (picPreview == null)
            {
                return "parameter picture box is null";
            }

            // Get the image from the form
            Bitmap workImage = display.getImage(picPreview);

            // If there is no image in the PictureBox, do nothing
            if (workImage == null)
            {
                return "picture is null";
            }

            try
            {
                // Apply the filter
                workImage = extBitmap.Laplacian3x3Filter(workImage);
                // Keep the filtered image in this
                FilteredBitmap = workImage;
            }
            catch (Exception)
            {
                // If there is an exception in the filter, do nothing in the form; filtered image remains null
                return "an exception was thrown by applying the filter";
            }
           
            // Put the filtered image in the form
            display.putImage(picPreview, workImage);


            return "filter successfull applyied";
        }

        // Click on Load image
        public void ClickLoad(PictureBox picPreview)
        {
            iHaveException = false;

            // Get the image from the disk
            try
            {
                // call function for get the image from the disk
                Bitmap workImage = iLoad.LoadImage();
                // If image is not null, put it in the form
                display.putImage(picPreview, workImage);

                // Save the non filtered image - just in case we need it in a next release
                originalBitmap = workImage;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("{0} First exception caught.", e);
                iHaveException = true;
            }
            catch (Exception e)
            {
                // If there is an exception in the filter, do nothing 
                Console.WriteLine("in the exception", e);
                iHaveException = true;
            }

           
        }

        public void ClickSave(PictureBox picPreview)
        {
            iHaveException = false;

            // Get the image from the disk
            try
            {
                // Get the image from the form
                Bitmap workImage = display.getImage(picPreview);

                try
                {
                    // Save the image on the disk
                    iSave.SaveImage(workImage);
                }
                catch (Exception e)
                {
                    // If there is an exception in the filter, do nothing 
                    Console.WriteLine("in the exception", e);
                    iHaveException = true;
                }
            }
            catch (Exception e)
            {
                // If there is an exception in the filter, do nothing 
                Console.WriteLine("in the exception", e);
                iHaveException = true;
            }

            
           

        }


    }
}
