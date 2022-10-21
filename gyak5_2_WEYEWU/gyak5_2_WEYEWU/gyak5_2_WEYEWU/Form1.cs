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
using System.Windows.Forms.DataVisualization.Charting;

namespace gyak5_2_WEYEWU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GetPénznemek();
            PénznemekXMLFeldolgozás(GetPénznemek());
            comboBox1.DataSource = Currencies;
            comboBox1.SelectedIndex = 2;

            RefreshData();
        }

        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>(); 

        private void RefreshData()
        {
            Rates.Clear();
            GetÁrfolyamok();
            ÁrfolyamokXMLfeldolgozás(GetÁrfolyamok());
            dataGridView1.DataSource = Rates;
            Vizualizacio();
        }

        private string GetÁrfolyamok()
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();

            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody()
            {
                currencyNames = Convert.ToString(comboBox1.SelectedItem),
                startDate = Convert.ToString(dateTimePicker2.Value),
                endDate = Convert.ToString(dateTimePicker3.Value)
            };

            GetExchangeRatesResponseBody response = mnbService.GetExchangeRates(request);
            // Ebben az esetben a "var" a GetExchangeRates visszatérési értékéből kapja a típusát.
            // Ezért a response változó valójában GetExchangeRatesResponseBody típusú.

            string result = response.GetExchangeRatesResult;
            // Ebben az esetben a "var" a GetExchangeRatesResult property alapján kapja a típusát.
            // Ezért a result változó valójában string típusú.

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(result);
            xdoc.Save("proba.xml");
            return result;
        }

        private void ÁrfolyamokXMLfeldolgozás(string result2)
        {
            // XML document létrehozása és az aktuális XML szöveg betöltése
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result2);

            foreach (XmlElement elem in xml.DocumentElement) //XmlElement típus fontos
            {
                //string dátum = elem.GetAttribute("date");
                //XmlElement valuta = (XmlElement)elem.ChildNodes[0]; //alapból a ChildNodes property egy XmlNode elemekből álló tömb
                                                                    //cast, hogy a GetAttribute függvény elérhető legyen 
              
                if (elem.ChildNodes[0] == null) continue;

                RateData RateAdat = new RateData()
                {
                    Date = DateTime.Parse(elem.GetAttribute("date")),
                    Currency = ((XmlElement)elem.ChildNodes[0]).GetAttribute("curr")
                };


                if (((XmlElement)elem.ChildNodes[0]).GetAttribute("unit") != "0")
                {
                    RateAdat.Value = decimal.Parse(elem.ChildNodes[0].InnerText) /
                                decimal.Parse(((XmlElement)elem.ChildNodes[0]).GetAttribute("unit"));
                }

                Rates.Add(RateAdat);
            }

            // Végigmegünk a dokumentum fő elemének gyermekein
            //foreach (XmlElement element in xml.DocumentElement)
            //{
                // Létrehozzuk az adatsort és rögtön hozzáadjuk a listához
                // Mivel ez egy referencia típusú változó, megtehetjük, hogy előbb adjuk a listához és csak később töltjük fel a tulajdonságait
                //var rate = new RateData();
                //Rates.Add(rate);

                // Dátum
                //rate.Date = DateTime.Parse(element.GetAttribute("date"));

                // Valuta
                //var childElement = (XmlElement)element.ChildNodes[0];
                //rate.Currency = childElement.GetAttribute("curr");

                // Érték
                //var unit = decimal.Parse(childElement.GetAttribute("unit"));
                //var value = decimal.Parse(childElement.InnerText);
                //if (unit != 0)
                //    rate.Value = value / unit;
            //}

        }
        private void Vizualizacio()
        {
            chart1.Name = "chartRateData";
            chart1.DataSource = Rates;

            // A Chart több adatsor megjelenítésére alkalmas. A Series tulajdonsága egy adatsorokból álló tömb,
            // ami alapértelmezetten egy elemű.A tömb első elemét érdemes lekérdezni egy változóba,
            // hogy könnyebb legyen átírni a tulajdonságait.
            var series_adatsor = chart1.Series[0];
            series_adatsor.ChartType = SeriesChartType.Line;
            series_adatsor.XValueMember = "Date";
            series_adatsor.YValueMembers = "Value";
            series_adatsor.BorderWidth = 2;

            var jelmagyarázat = chart1.Legends[0];
            jelmagyarázat.Enabled = false;

            var chartArea1 = chart1.ChartAreas[0];
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.IsStartedFromZero = false;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private string GetPénznemek()
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();

            GetCurrenciesRequestBody request = new GetCurrenciesRequestBody();

            GetCurrenciesResponseBody response = mnbService.GetCurrencies(request);

            string result = response.GetCurrenciesResult;

            Console.WriteLine(result);

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(result);
            xdoc.Save("proba2.xml");

            return result;
        }

        private void PénznemekXMLFeldolgozás(string result2)
        {
            // XML document létrehozása és az aktuális XML szöveg betöltése
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result2);

            foreach (XmlNode elem in xml.DocumentElement["Currencies"].ChildNodes) //XmlElement típus fontos
            {
                string EgyPénznem = elem.InnerText;

                Currencies.Add(EgyPénznem);
            }
        }
    }
}
