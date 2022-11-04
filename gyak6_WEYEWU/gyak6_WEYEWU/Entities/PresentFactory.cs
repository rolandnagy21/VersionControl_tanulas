using gyak6_WEYEWU.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gyak6_WEYEWU.Entities
{
    internal class PresentFactory: IToyFactory
    {
        public Color BoxColor { get; set; }
        public Color RibbonColor { get; set; }
        public Toy CreateNew()
        {
            return new Present(BoxColor, RibbonColor);
        }
    }
}
