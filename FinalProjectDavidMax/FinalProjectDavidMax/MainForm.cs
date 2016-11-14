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
            switch (((Button)sender).Name) {
            
                case "btnOpenOriginal" :
                    BusinessPresentation.getInstance().ClickLoad(picPreview);
                    break;

                case "btnSaveNewImage":
                    BusinessPresentation.getInstance().ClickSave(picPreview);
                    break;

                case "btnFilter":
                    BusinessPresentation.getInstance().ClickFilter(picPreview);
                    break;

            }
        }


    }
}
