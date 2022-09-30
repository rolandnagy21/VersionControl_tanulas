using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();

        public Form1()
        {
            InitializeComponent();
            //design elemek beállítása
            label1.Text = Resource1.Fullname;
            button1.Text = Resource1.Add;

            //listbox 1 beállítása
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "Fullname";

            //button1 beállítása
            button1.Click += Button1_Click;

            //button2 beállítása
            button2.Text = Resource1.ExportButton;
            button2.Click += Button2_Click;
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            //másik féle értékadás 
            //var NewUser = new User()
            // { 
            //    LastName = textBox1.Text,
            //    FirstName = textBox2.Text 
            // };

            User NewUser = new User();
            NewUser.FullName = textBox1.Text;

            users.Add(NewUser);
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Comma Seperated Values (*.csv) | *.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw1 = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var felhasználó in users)
                {
                    sw1.WriteLine($"{felhasználó.ID}; {felhasználó.FullName}");
                    // sw1.Write(felhasználó.ID);
                    // sw1.Write(";");
                    // sw1.Write(felhasználó.FullName);
                    // sw1.WriteLine();
                }

            }
        }
    }
}

    

