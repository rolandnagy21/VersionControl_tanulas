using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using gyak6_WEYEWU.Abstractions;

namespace gyak6_WEYEWU.Entities
{
    public class Ball : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }
    }
}
