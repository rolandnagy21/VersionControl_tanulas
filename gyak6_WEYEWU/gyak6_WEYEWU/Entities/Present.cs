using gyak6_WEYEWU.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gyak6_WEYEWU.Entities
{
    public class Present : Toy
    {

        public SolidBrush RibbonColor { get; private set; }

        public SolidBrush BoxColor { get; private set; }

        public Present(Color ribbon, Color box)
        {
            RibbonColor = new SolidBrush(ribbon);

            BoxColor = new SolidBrush(box);
            
        }

        int ribbonPixel = 80 / 5 * 2;

        protected override void DrawImage(Graphics graphics)
        {
            graphics.FillRectangle(RibbonColor, ribbonPixel, 0, Width / 5, Height);
            graphics.FillRectangle(RibbonColor, 0, ribbonPixel, Width, Height / 5);

            graphics.FillRectangle(BoxColor, 0, 0, Width, Height);
        }
    }
}
