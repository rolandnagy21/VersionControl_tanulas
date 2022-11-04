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
            set { 
                _factory = value;
                DisplayNext();
                }

        }

        private Toy _nextToy;

        public Form1()
        {
            InitializeComponent();

            Factory = new CarFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();

            _toys.Add(toy);

            toy.Left = -toy.Left;

            mainPanel1.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPoz = 0;

            foreach (var t in _toys)
            {
                t.MoveToy();

                if (t.Left > maxPoz)
                    maxPoz = t.Left;
            }

            if (maxPoz > 1000)
            {
                var TörlendőItem = _toys[0];

                mainPanel1.Controls.Remove(TörlendőItem);
                _toys.Remove(TörlendőItem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory
            {
                BallColor = button3.BackColor
            };
        }

        private void DisplayNext()
        {
            if (_nextToy != null) Controls.Remove(_nextToy);

            _nextToy = Factory.CreateNew();

            _nextToy.Top = label1.Top + label1.Height + 5;

            _nextToy.Left = label1.Left;

            Controls.Add(_nextToy);
        }

        private void button3_Click(object sender, EventArgs e)
        {
                var button = (Button)sender;

                var colorPicker = new ColorDialog();

                colorPicker.Color = button.BackColor;

                if (colorPicker.ShowDialog() != DialogResult.OK)
                    return;

                button.BackColor = colorPicker.Color;
            
        }
    }
}
