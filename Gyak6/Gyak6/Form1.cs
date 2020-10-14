using Gyak6.Entities;
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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace Gyak6
{
    public partial class Form1 : Form
    {
        string XMLeredmeny;
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
            XMLeredmeny = result;
            
            


        }
        private void XmlProcess()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(XMLeredmeny);
            foreach (XmlElement item in xml)
            {
                RateData rd = new RateData();
                rd.Date = DateTime.Parse(item.GetAttribute("Date"));
                var childElement = (XmlElement)item.ChildNodes[0];
                rd.Currency = childElement.GetAttribute("curr");

                // Érték
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rd.Value = value / unit;
                Rates.Add(rd);

            }
        }
        private void Chart()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }
        public Form1()
        {
            InitializeComponent();
            Fuggveny();
            dataGridView1.DataSource = Rates;

        }
    }
}
