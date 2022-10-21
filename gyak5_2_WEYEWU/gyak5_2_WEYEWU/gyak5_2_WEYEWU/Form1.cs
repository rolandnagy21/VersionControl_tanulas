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
            euroArf_adatok_2020_1();
            XMLfeldolgozás(euroArf_adatok_2020_1());

            dataGridView1.DataSource = Rates;
        }

        BindingList<RateData> Rates = new BindingList<RateData>();


        private string euroArf_adatok_2020_1()
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
            return result;
        }



        private void XMLfeldolgozás(string result2)
        {
            // XML document létrehozása és az aktuális XML szöveg betöltése
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result2);

            foreach (XmlElement elem in xml.DocumentElement) //XmlElement típus fontos
            {
                //string dátum = elem.GetAttribute("date");
                //XmlElement valuta = (XmlElement)elem.ChildNodes[0]; //alapból a ChildNodes property egy XmlNode elemekből álló tömb
                                                                    //cast, hogy a GetAttribute függvény elérhető legyen 
                
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
    }
}
