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
using System.Windows.Shapes;
using DAL;

namespace EventBooze
{
    /// <summary>
    /// Interaction logic for ArtiestenOverzicht.xaml
    /// </summary>
    public partial class ArtiestenOverzicht : Window
    {
        public ArtiestenOverzicht()
        {
            InitializeComponent();

        }

        List<Artiest> Artiesten = new List<Artiest>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBox.ItemsSource = DatabaseOperations.ophalenArtiesten();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ArtiestBewerken artiestBewerken = new ArtiestBewerken();
            artiestBewerken.Show();
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Artiest artiest = (sender as Button).DataContext as Artiest;
            var ok = DatabaseOperations.verwijderenArtiest(artiest);
            if (ok>0)
            {
                listBox.ItemsSource = DatabaseOperations.ophalenArtiesten();
                listBox.Items.Refresh();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Artiest artiest = (sender as Button).DataContext as Artiest;
            ArtiestBewerken artiestBewerken = new ArtiestBewerken();
            artiestBewerken.overzichtArtiest = artiest;
            artiestBewerken.Show();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            Artiesten = DatabaseOperations.ophalenArtiesten();
            listBox.ItemsSource = Artiesten;
        }
    }
}
