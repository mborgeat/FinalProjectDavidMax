using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectDavidMax
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Method for the on-click listener
        public void click(object sender, EventArgs e)
        {
            BusinessPresentation myBusiness = new BusinessPresentation(new OutputInput(), new OutputInput(), new ExtBitmap(), new Display());
            switch (((Button)sender).Name) {
            
                case "btnOpenOriginal" :
                    myBusiness.ClickLoad(picPreview);
                    break;

                case "btnSaveNewImage":
                    myBusiness.ClickSave(picPreview);
                    break;

                case "btnFilter":
                    myBusiness.ClickFilter(picPreview);
                    break;

            }
        }


    }
}
