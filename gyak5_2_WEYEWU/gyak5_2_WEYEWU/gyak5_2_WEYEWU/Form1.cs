using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using gyak5_2_WEYEWU.MnbServiceReference;
using gyak5_2_WEYEWU.Entities;

namespace gyak5_2_WEYEWU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            euroArf_2020_1();

            dataGridView1.DataSource = Rates;

        }

        private void euroArf_2020_1()
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();

            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);
            // Ebben az esetben a "var" a GetExchangeRates visszatérési értékéből kapja a típusát.
            // Ezért a response változó valójában GetExchangeRatesResponseBody típusú.

            string result = response.GetExchangeRatesResult;
            // Ebben az esetben a "var" a GetExchangeRatesResult property alapján kapja a típusát.
            // Ezért a result változó valójában string típusú.

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(result);
            xdoc.Save("proba.xml");
        }

        BindingList<RateData> Rates = new BindingList<RateData>();
        
       
    }
}
