using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FinalProjectDavidMax;
using System.Drawing;
using System.Windows.Forms;

namespace TestFinalProject
{
    
    [TestClass]
    public class UnitTest1
    {
        /*
        // Test of the ClickLoad in Business layer
        public void TestLoad()
        {
            // Create a substitute for the interfaces
            var display = Substitute.For<IDisplay>();
            var load = Substitute.For<ILoad>();
        }

        // Test of the ClickSave in Business layer
        [TestMethod()]
        public void TestClickSave()
        {
            // Create a substitute for the interfaces
            var display = Substitute.For<IDisplay>();
            var save = Substitute.ForPartsOf<ISave>();

            // Get the instance of BusinessPresentation
            BusinessPresentation bp = BusinessPresentation.getInstance();

            // Create a fake picture box
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = Properties.Resources.firefox;
            display.getImage(Arg.Any<PictureBox>()).Returns(fakePictureBox.Image);
            
            bp.ClickSave(fakePictureBox);

            save.Received(1).SaveImage(Arg.Any<Bitmap>());
        }
        */

        
        

        // Test of the ClickFilter in Business layer
        [TestMethod()]
        public void TestClickFilter()
        {
            // Create a substitute for the interface
            var display = Substitute.For<IDisplay>();

            // Get the instance of BusinessPresentation
            BusinessPresentation bp = BusinessPresentation.getInstance();

            // Get a fake picture from the form
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = Properties.Resources.firefox;
            display.getImage(Arg.Any<PictureBox>()).Returns(fakePictureBox.Image);

            // Apply the filter
            bp.ClickFilter(fakePictureBox);

            // Compare the result with a well filtered image
            // Get the well filtered image
            Bitmap controlPicture = Properties.Resources.firefox_filtered;

            // Control the size of images
            Assert.AreEqual(bp.FilteredBitmap.Size, controlPicture.Size);

            // Control each pixel
            for (int i = 0; i < controlPicture.Width; i++)
            {
                for (int j = 0; j < controlPicture.Height; j++)
                {
                    Assert.AreEqual(bp.FilteredBitmap.GetPixel(i, j), controlPicture.GetPixel(i, j));
                }
            }
        }

        // This method throws an exception in ClickFilter
        [TestMethod()]
        public void TestExceptionInClickFilter()
        {
            // Create a substitute for the interface
            var display = Substitute.For<IDisplay>();
            var extBitmap = Substitute.For<IExtBitmap>();

            // Get the instance of BusinessPresentation and reinitialise the filtered bitmap
            BusinessPresentation bp = BusinessPresentation.getInstance();
            bp.FilteredBitmap = null;

            // Get a fake picture from the form
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = Properties.Resources.firefox;
            display.getImage(Arg.Any<PictureBox>()).Returns(fakePictureBox.Image);
            bp.setExtBitmap(extBitmap);

            // Throw exception
            extBitmap.Laplacian3x3Filter(Arg.Any<Bitmap>(), false).ReturnsForAnyArgs(x => { throw new Exception(); });

            try
            {
                // Apply the filter
                bp.ClickFilter(fakePictureBox);
            }
            catch (Exception)
            {
                Assert.Fail("The picture was note filtered");
            }

            // The filtered picture must be null
            Assert.IsNull(bp.FilteredBitmap);
        }

    }
}
