﻿using gyak8.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyak8
{
    public partial class Form1 : Form
    {
        List<Ball> _balls = new List<Ball>();
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
            Ball ujlabda = Factory.CreateNew();
            _balls.Add(ujlabda);
            mainPanel.Controls.Add(ujlabda);
            ujlabda.Left = -ujlabda.Width;


        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var item in _balls)
            {
                item.MoveBall();
                if (item.Left>maxPosition)
                {
                    maxPosition = item.Left;
                }
            }
            if (maxPosition==1000)
            {
                var firstBall = _balls[0];
                _balls.Remove(firstBall);
                mainPanel.Controls.Remove(firstBall);

            }

        }
    }
}
