﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wypozyczalniaSamochodow
{
    public partial class App : Form
    {
        
        private Account account;
        private List<UserControl> panels = new List<UserControl>();

        public App()
        {
            InitializeComponent();
            loginPanel1.setParent(this);
            panels.Add(loginPanel1);
            panels.Add(adminClient1);
        }

       

        private void App_Load(object sender, EventArgs e)
        {
            hidePanels();
            loginPanel1.Show();

        }

        private void hidePanels()
        {
            foreach (UserControl panel in panels)
            {
                panel.Hide();
            }
        }
        public void openClient(ref Account acc)
        {
            account = acc;
            if (account.isAdmin)
            {
                hidePanels();
                adminClient1.Show();
            }
        }


    }
}