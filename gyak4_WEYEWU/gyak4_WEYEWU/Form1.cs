using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace gyak4_WEYEWU
{
    public partial class Form1 : Form
    {
        List<Flat> Flats; //Flat típusú elemekből álló listára mutató referencia (Nem kell inicializálni new-val)
        RealEstateEntities1 context = new RealEstateEntities1();

        //üres változók
        Excel.Application xlApp;
        // Az MS Excel alkalmazás
        Excel.Workbook xlWB;
        // A létrehozott munkafüzet
        Excel.Worksheet xlSheet;
        // Munkalap a munkafüzeten belül

        public Form1()
        {
            InitializeComponent();

            LoadData();
            CreateExcel();
        }

        void LoadData()
        {
            Flats = context.Flats.ToList();
        }

        void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();   
                // Excel elindítása és az applikáció objektum betöltése

                xlWB = xlApp.Workbooks.Add(Missing.Value);
                // Új munkafüzet létrehozása

                xlSheet = xlWB.ActiveSheet;
                // Új munkalap létrehozása

                CreateTable();
                // Tábla létrehozása

                xlApp.Visible = true;
                xlApp.UserControl = true;
                // Control átadása a felhasználónak
            }

            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                string errMsg = string.Format("Hiba: {0}\nEbben a sorban: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Hibaüzenet"); //elnevezett MessageBox

                xlWB.Close(false, Type.Missing, Type.Missing); //alapértelmezett értékekkel hívjuk meg így a Close-t szerintem
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
                //a catch ágban a munkafüzetből kilépés, és az Excel Applikáció bezárása, és az ezeknek megfelelő változók üressé tétele
            }
        }

        void CreateTable()
        {
            string[] fejlécek = new string[]
            {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzetméter ár (Ft/m2)"
            };

            for (int i = 0; i < fejlécek.Length; i++)
            {
                xlSheet.Cells[1, (i + 1)] = fejlécek[i];
            }

            object[,] adatok = new object[Flats.Count(), fejlécek.Length];


            int sor = 0;
            foreach (Flat lakás in Flats)
            {
                adatok[sor, 0] = lakás.Code;
                adatok[sor, 1] = lakás.Vendor;
                adatok[sor, 2] = lakás.Side;
                adatok[sor, 3] = lakás.District;
                if (lakás.Elevator)
                {
                    adatok[sor, 4] = "Van";
                }
                else
                {
                    adatok[sor, 4] = "Nincs";

                }
                adatok[sor, 5] = lakás.NumberOfRooms;
                adatok[sor, 6] = lakás.FloorArea;
                adatok[sor, 7] = lakás.Price;
                adatok[sor, 8] = "";

                sor++;
            }

            xlSheet.get_Range(
              GetCell(2, 1), //GetCell(2,1) = "A2"
              GetCell(1 + adatok.GetLength(0), adatok.GetLength(1))) //GetCell(1 + 104, 9) = "I105"
              .Value2 = adatok;

            //utolsó, számított oszlop
            //segédváltozók
            //var HoszlopÉrtékTart = xlSheet.get_Range(GetCell(2, adatok.GetLength(1) - 1), GetCell(1 + adatok.GetLength(0), adatok.GetLength(1) - 1));
            //var GoszlopÉrtékTart = xlSheet.get_Range(GetCell(2, adatok.GetLength(1) - 2), GetCell(1 + adatok.GetLength(0), adatok.GetLength(1) - 2));

            //for (int i = 1; i <= adatok.GetLength(1); i++)
            //{
            //    var SzámítottOszlop[i,1] = HoszlopÉrtékTart[i,1] / GoszlopÉrtékTart[i,1];
            //}

            var H2 = GetCell(2, adatok.GetLength(1) - 1);
            var Hvége = GetCell(1 + adatok.GetLength(0), adatok.GetLength(1) - 1);
            var G2 = GetCell(2, adatok.GetLength(1) - 2);
            var Gvége = GetCell(1 + adatok.GetLength(0), adatok.GetLength(1) - 2);

            //utolsó oszlop
            xlSheet.get_Range(GetCell(2, adatok.GetLength(1)), GetCell(1 + adatok.GetLength(0), adatok.GetLength(1)))
            //.Value2 = $"= 1000000 * xlSheet.get_Range(GetCell(2, adatok.GetLength(1) - 1), GetCell(1 + adatok.GetLength(0), adatok.GetLength(1) - 1)).Value2/xlSheet.get_Range(GetCell(2, adatok.GetLength(1) - 2), GetCell(1 + adatok.GetLength(0), adatok.GetLength(1) - 2)).Value2";
            .Value2 = $"= 1000000 * {H2}:{Hvége}/{G2}:{Gvége}";
            //= 1000000 * H2:H105 / G2:G105

            Excel.Range fejlécTartomány = xlSheet.get_Range(GetCell(1, 1), GetCell(1, fejlécek.Length));
            fejlécTartomány.Font.Bold = true;
            fejlécTartomány.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejlécTartomány.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            fejlécTartomány.EntireColumn.AutoFit();
            fejlécTartomány.RowHeight = 40;
            fejlécTartomány.Interior.Color = Color.LightBlue;
            fejlécTartomány.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range egésztáblaTartomány = xlSheet.get_Range(GetCell(1, 1), GetCell(xlSheet.UsedRange.Rows.Count, adatok.GetLength(1)));
            egésztáblaTartomány.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range utolsóOszlopTartomány = xlSheet.get_Range(GetCell(1, adatok.GetLength(1)), GetCell(xlSheet.UsedRange.Rows.Count, adatok.GetLength(1)));
            utolsóOszlopTartomány.Interior.Color = Color.LightGreen;
        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y; //1, 9
            int modulo;

            while (dividend > 0)
            {
                //kommentben: GetCell(2,1), GetCell(1 + 104, 9)

                modulo = (dividend - 1) % 26; //0, 8
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate; //"A", "I"
                //Convert.ToChar(65) = A, innentől kezdődnek a betűk (ASCII tábla)
                dividend = (int)((dividend - modulo) / 26); // 0, 0
            }
            ExcelCoordinate += x.ToString(); //"A2", "I105"

            return ExcelCoordinate;
        }

    }
}
