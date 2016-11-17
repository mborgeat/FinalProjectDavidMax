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
        private static IDisplay display = Substitute.For<IDisplay>();
        private static IExtBitmap extBitmap = Substitute.For<IExtBitmap>();
        private static ILoad iLoad = Substitute.For<ILoad>();
        private static ISave iSave = Substitute.For<ISave>();

        // Create a substitute for all interface



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
            // Get the instance of BusinessPresentation
            BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);

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

        // Test of the ClickFilter with a null picture box
        [TestMethod()]
        public void TestClickFilterWithNullPictureBox()
        {
            // Get the instance of BusinessPresentation
            BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);
            // Apply the filter with a null parameter
            string response = bp.ClickFilter(null);

            // Control the response
            Assert.AreEqual(response, "parameter picture box is null");
        }

        [TestMethod()]
        // Test of the ClickFilter with a null picture in the box (which is not null)
        public void TestClickFilterWithNullPicture()
        {
            // Get the instance of BusinessPresentation
            BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);
            // Create a fake picture preview picture box, with a null picture
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = null;
            display.getImage(Arg.Any<PictureBox>()).Returns(fakePictureBox.Image);

            // Apply the filter
            string response = bp.ClickFilter(fakePictureBox);

            // Control the response
            Assert.AreEqual(response, "picture is null");
        }

        // This method throws an exception in ClickFilter
        [TestMethod()]
        public void TestExceptionInClickFilter()
        {
            // Get the instance of BusinessPresentation
            BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);
            bp.FilteredBitmap = null;

            // Get a fake picture from the form
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = Properties.Resources.firefox;
            display.getImage(Arg.Any<PictureBox>()).Returns(fakePictureBox.Image);
            bp.setExtBitmap(extBitmap);

            // Throw exception
            extBitmap.Laplacian3x3Filter(Arg.Any<Bitmap>()).ReturnsForAnyArgs(x => { throw new Exception(); });

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



        //test method clickLoad with no exception
        [TestMethod]
        public void TestClickLoadWithNoException()
        {
            // Get the instance of BusinessPresentation
            BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);

            // Get a fake picture from the form
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = Properties.Resources.firefox;

            bp.ClickLoad(fakePictureBox);

            //access to internal variables of the class
            Assert.AreEqual(bp.iHaveException, false);
        }

        //test method clickLoad with fake exception on LoadImage
        [TestMethod()]
        public void TestClickLoadWithExcpetionOnLoadImage()
        {
            display = Substitute.For<IDisplay>();
            extBitmap = Substitute.For<IExtBitmap>();
            iLoad = Substitute.For<ILoad>();
            iSave = Substitute.For<ISave>();
            // Get the instance of BusinessPresentation
            BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);

            // Get a fake picture from the form
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = Properties.Resources.firefox;

            //instructions to NSubstitute : Throw an exception
            iLoad
                .When(x => x.LoadImage())
               .Do(x => { throw new Exception(); });

            bp.ClickLoad(fakePictureBox);

            //access to internal variables of the class
            Assert.AreEqual(bp.iHaveException, true);
        }

        //test method clickLoad with fake exception on putImage
        [TestMethod()]
        public void TestClickLoadWithExcpetionOnPutImage()
        {
            display = Substitute.For<IDisplay>();
            extBitmap = Substitute.For<IExtBitmap>();
            iLoad = Substitute.For<ILoad>();
            iSave = Substitute.For<ISave>();
            // Get the instance of BusinessPresentation
            BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);

            // Get a fake picture from the form
            PictureBox fakePictureBox = new PictureBox();
            Bitmap imageTemp = Properties.Resources.firefox;

            //instructions to NSubstitute : Throw an exception
            display
                .When(x => x.putImage(fakePictureBox, imageTemp))
               .Do(x => { throw new Exception(); });

            bp.ClickLoad(fakePictureBox);

            //access to internal variables of the class
            Assert.AreEqual(bp.iHaveException, true);
        }


        //test method clickSave with no exception
        [TestMethod]
        public void TestClickSaveWithNoException()
        {
            // Get the instance of BusinessPresentation
            BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);

            // Get a fake picture from the form
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = Properties.Resources.firefox;

            bp.ClickSave(fakePictureBox);

            //access to internal variables of the class
            Assert.AreEqual(bp.iHaveException, false);
        }

        //test method clickLoad with fake exception on LoadImage
        [TestMethod()]
        public void TestClickSaveWithExcpetionOnGetImage()
        {
        display = Substitute.For<IDisplay>();
        extBitmap = Substitute.For<IExtBitmap>();
        iLoad = Substitute.For<ILoad>();
        iSave = Substitute.For<ISave>();
        // Get the instance of BusinessPresentation
        BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);

            // Get a fake picture from the form
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = Properties.Resources.firefox;

            //instructions to NSubstitute : Throw an exception
            display
                .When(x => x.getImage(fakePictureBox))
               .Do(x => { throw new Exception(); });

            bp.ClickSave(fakePictureBox);

            //access to internal variables of the class
            Assert.AreEqual(bp.iHaveException, true);
        }

        //test method clickLoad with fake exception on putImage
        [TestMethod()]
        public void TestClickSaveWithNullExcpetionOnSaveImage()
        {
            display = Substitute.For<IDisplay>();
            extBitmap = Substitute.For<IExtBitmap>();
            iLoad = Substitute.For<ILoad>();
            iSave = Substitute.For<ISave>();
            // Get the instance of BusinessPresentation
            BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);

            // Get a fake picture from the form
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = Properties.Resources.firefox;
            Bitmap imageTemp = Properties.Resources.firefox;

            //instructions to NSubstitute : Throw an exception
            iSave
                .When(x => x.SaveImage(imageTemp))
               .Do(x => { throw new ArgumentNullException(); });

            bp.ClickSave(fakePictureBox);

            //access to internal variables of the class
            Assert.AreEqual(bp.iHaveException, true);
        }

    }
}
