using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace gyak6_WEYEWU.Entities
{
    internal class Ball : Label
    {
        public Ball()
        {
            AutoSize = false;
            Width = 50;
            Paint += Ball_Paint;
            Height = Width;
            
        }

        private void Ball_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected void DrawImage(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }

        public void MoveBall()
        {
            Left = Left + 1;
        }
    }
}
