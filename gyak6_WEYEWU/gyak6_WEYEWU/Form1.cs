using gyak6_WEYEWU.Abstractions;
using gyak6_WEYEWU.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyak6_WEYEWU
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();

        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();

            Factory = new CarFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            Toy toy = Factory.CreateNew();

            _toys.Add(toy);

            toy.Left = -toy.Width;

            mainPanel1.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPozíció = 0;

            foreach (var toy in _toys)
            {
                toy.MoveToy();

                if (toy.Left > maxPozíció)
                    maxPozíció = toy.Left;
            }

            if (maxPozíció > 1000)
            {
                var TörlendőItem = _toys[0];

                mainPanel1.Controls.Remove(TörlendőItem);
                _toys.Remove(TörlendőItem);
            }
        }
    }
}
