﻿using System;
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
using DAL;

namespace EventBooze
{
    /// <summary>
    /// Interaction logic for Klant.xaml
    /// </summary>
    public partial class KlantSelectie : Window
    {
        Event huidigEvent = new Event();

        public KlantSelectie()
        {
            InitializeComponent();
            huidigEvent = DatabaseOperations.OphalenEvent(1);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (huidigEvent == null)
            {
                List<Klant> alleklanten = DatabaseOperations.OphalenKlanten();
                cmbKlant.ItemsSource = alleklanten.First;

            }
            
        }
    }
}