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




        // Test of ExtBitmap method Laplacian 3x3
        [TestMethod()]
        public void TestLaplacian3x3()
        {
            // Definition of start image and control image
            Bitmap startPicture = Properties.Resources.firefox;
            Bitmap controlPicture = Properties.Resources.firefox_filtered;

            // Instanciation of a ExtBitmap
            ExtBitmap eb = new ExtBitmap();

            // Execution of the method
            Bitmap resultPicture = eb.Laplacian3x3Filter(startPicture);

            // Test the size of result image
            Assert.AreEqual(resultPicture.Size, startPicture.Size);

            // Control each pixel
            for (int i = 0; i < controlPicture.Width; i++)
            {
                for (int j = 0; j < controlPicture.Height; j++)
                {
                    Assert.AreEqual(resultPicture.GetPixel(i, j), controlPicture.GetPixel(i, j));
                }
            }
        }

        // Test of the ClickFilter with all correct
        [TestMethod()]
        public void TestClickFilter()
        {
            // Create a substitute for all interface
            var display = Substitute.For<IDisplay>();
            var extBitmap = Substitute.For<IExtBitmap>();
            var iLoad = Substitute.For<ILoad>();
            var iSave = Substitute.For<ISave>();

            // Get the instance of BusinessPresentation
            BusinessPresentation bp = new BusinessPresentation(iLoad, iSave, extBitmap, display);

            // Make a fake picture box with a fake picture
            PictureBox fakePictureBox = new PictureBox();
            fakePictureBox.Image = Properties.Resources.firefox;
            display.getImage(Arg.Any<PictureBox>()).Returns(fakePictureBox.Image);

            Assert.AreEqual("filter successfull applyied", bp.ClickFilter(fakePictureBox));
        }


       

        // Test of the ClickFilter with a null picture box
        [TestMethod()]
        public void TestClickFilterWithNullPictureBox()
        {
            // Create a substitute for all interface
            var display = Substitute.For<IDisplay>();
            var extBitmap = Substitute.For<IExtBitmap>();
            var iLoad = Substitute.For<ILoad>();
            var iSave = Substitute.For<ISave>();
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
            // Create a substitute for all interface
            var display = Substitute.For<IDisplay>();
            var extBitmap = Substitute.For<IExtBitmap>();
            var iLoad = Substitute.For<ILoad>();
            var iSave = Substitute.For<ISave>();
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
            // Create a substitute for all interface
            var display = Substitute.For<IDisplay>();
            var extBitmap = Substitute.For<IExtBitmap>();
            var iLoad = Substitute.For<ILoad>();
            var iSave = Substitute.For<ISave>();
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
            // Create a substitute for all interface
            var display = Substitute.For<IDisplay>();
            var extBitmap = Substitute.For<IExtBitmap>();
            var iLoad = Substitute.For<ILoad>();
            var iSave = Substitute.For<ISave>();
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
            // Create a substitute for all interface
            var display = Substitute.For<IDisplay>();
            var extBitmap = Substitute.For<IExtBitmap>();
            var iLoad = Substitute.For<ILoad>();
            var iSave = Substitute.For<ISave>();
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

        


        //test method clickSave with no exception
        [TestMethod]
        public void TestClickSaveWithNoException()
        {
            // Create a substitute for all interface
            var display = Substitute.For<IDisplay>();
            var extBitmap = Substitute.For<IExtBitmap>();
            var iLoad = Substitute.For<ILoad>();
            var iSave = Substitute.For<ISave>();
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
            // Create a substitute for all interface
            var display = Substitute.For<IDisplay>();
            var extBitmap = Substitute.For<IExtBitmap>();
            var iLoad = Substitute.For<ILoad>();
            var iSave = Substitute.For<ISave>();
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
        

    }
}
