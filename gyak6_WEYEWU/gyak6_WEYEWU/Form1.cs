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
        private List<Ball> _toys = new List<Ball>();

        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();

            Factory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            Toy ball = Factory.CreateNew();

            _toys.Add(ball);

            ball.Left = -ball.Width;

            mainPanel1.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPozíció = 0;

            foreach (var ball in _toys)
            {
                ball.MoveBall();

                if (ball.Left > maxPozíció)
                    maxPozíció = ball.Left;
            }

            if (maxPozíció > 1000)
            {
                var oldestBall = _toys[0];

                mainPanel1.Controls.Remove(oldestBall);
                _toys.Remove(oldestBall);
            }
        }
    }
}
