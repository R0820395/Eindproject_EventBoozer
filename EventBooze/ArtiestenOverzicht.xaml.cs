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
            Artiesten = DatabaseOperations.ophalenArtiesten();
            listBox.ItemsSource = Artiesten;

            //foreach (var artiest in Artiesten)
            //{
            //    var naam = artiest.Naam;
            //    var email = artiest.Email;
            //    var telefoon = artiest.Telefoon;
            //}
        }

        private void btnAddArtist_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ArtiestBewerken artiestBewerken = new ArtiestBewerken();
            artiestBewerken.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            listBox.SelectedItem.ToString();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
