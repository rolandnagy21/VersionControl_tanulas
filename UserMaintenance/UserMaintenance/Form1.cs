using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}

    

