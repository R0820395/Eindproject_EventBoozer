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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gridArtiesten.ItemsSource = DatabaseOperations.ophalenArtiesten();
        }
    }
}
