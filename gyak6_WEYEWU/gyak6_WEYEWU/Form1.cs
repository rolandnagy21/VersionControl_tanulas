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
        private List<Ball> _balls = new List<Ball>();

        private BallFactory _factory;
        public BallFactory Factory
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
            Ball ball = Factory.CreateNew();

            _balls.Add(ball);

            ball.Left = -ball.Width;

            mainPanel1.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPozíció = 0;

            foreach (var ball in _balls)
            {
                ball.MoveBall();

                if (ball.Left > maxPozíció)
                    maxPozíció = ball.Left;
            }

            if (maxPozíció > 1000)
            {
                var oldestBall = _balls[0];

                mainPanel1.Controls.Remove(oldestBall);
                _balls.Remove(oldestBall);
            }
        }
    }
}
