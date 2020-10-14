﻿using Gyak6.Entities;
using Gyak6.MnbSeviceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gyak6
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        private void Fuggveny ()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames="EUR",
                startDate="2020 - 01 - 01",
                endDate="2020-06-30"
                
            };
            var response = mnbService.GetExchangeRates(request);
            string result = response.GetExchangeRatesResult;
            
            


        }
        public Form1()
        {
            InitializeComponent();
            Fuggveny();
            dataGridView1.DataSource = Rates;
        }
    }
}
