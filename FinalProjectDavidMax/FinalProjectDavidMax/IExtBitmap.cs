using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectDavidMax
{
    public interface IExtBitmap
    {
        Bitmap Laplacian3x3Filter(Bitmap sourceBitmap);
    }
}
