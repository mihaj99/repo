using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyak4
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;
        public void LoadData()
        {

        }
        public Form1()
        {
            InitializeComponent();
            LoadData();
            Flats = context.Flats.ToList();
        }
    }
}
