﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;

namespace OffCampusHousingDatabase
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChangePassword : Window
    {
        #region Variables

        DatabaseHelper dbHelper;

        #endregion

        #region Listeners

        public ChangePassword()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            emailTextbox.Focus();
        }

        #endregion

        private void changePass_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main;
            main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        #region Logic

        #endregion
    }
}
