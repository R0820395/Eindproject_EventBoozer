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
    /// Interaction logic for ArtiestBewerken.xaml
    /// </summary>
    public partial class ArtiestBewerken : Window
    {
        public Artiest overzichtArtiest;

        public ArtiestBewerken()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtArtiest.Text = this.overzichtArtiest.Naam;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
