﻿using BusinessLogic;
using DataAcces;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Views;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
<<<<<<< HEAD
            //PrimaryContainer.NavigationService.Navigate(new CheckSalesRecords());
            PrimaryContainer.NavigationService.Navigate(new EndorseContract());
=======
            PrimaryContainer.NavigationService.Navigate(new LoginView());
>>>>>>> 72dd4816c3bda630d1135540e87e00691c4f3805
            this.MinWidth = 1366;
            this.MinHeight = 750;
        }

    }
}
