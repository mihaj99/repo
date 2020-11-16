using gyak8.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gyak8.Abstractions;

namespace gyak8
{
    public partial class Form1 : Form
    {
        
        List<Toy> _toys = new List<Toy>();
        private Toy _nextToy;
        private void DisplayNext()
        {
            if (_nextToy!=null)
            {
                Controls.Remove(_nextToy);
                _nextToy = Factory.CreateNew();
                _nextToy.Top = label1.Top + label1.Height + 20;
                _nextToy.Left = label1.Left;
                Controls.Add(_nextToy);
            }
        }
        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set 
            { 
                _factory = value;
                DisplayNext();
            }
        }


        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ujlabda = Factory.CreateNew();
            _toys.Add(ujlabda);
            mainPanel.Controls.Add(ujlabda);
            ujlabda.Left = -ujlabda.Width;


        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var item in _toys)
            {
                item.MoveToy();
                if (item.Left>maxPosition)
                {
                    maxPosition = item.Left;
                }
            }
            if (maxPosition==1000)
            {
                var firstBall = _toys[0];
                _toys.Remove(firstBall);
                mainPanel.Controls.Remove(firstBall);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory();
        }
    }
}
