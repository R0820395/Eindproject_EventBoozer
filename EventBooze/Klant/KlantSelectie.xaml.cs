using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //public KlantSelectie(int EventIndex) Afgezet wegens testen.
            InitializeComponent();
            huidigEvent = DatabaseOperations.OphalenEvent(4);
            //4 ==> EventIndex . Nu vier om te testen.
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Klant> alleklanten = new List<Klant>(); 

            if (huidigEvent.KlantID == null)
            {
                alleklanten = DatabaseOperations.OphalenKlanten();
                List<String> namenlijst = new List<String>();

                foreach(Klant klant in alleklanten)
                {
                    namenlijst.Add(klant.Naam);
                }

                cmbKlant.ItemsSource = alleklanten;
                cmbKlant.Items.Refresh();
            }
            else
            {
                Klant klant = DatabaseOperations.OphalenKlant(huidigEvent.KlantID);
                alleklanten.Add(klant);
                //Dit verder bekijken waarom cmb default niet getoond wordt.
                cmbKlant.ItemsSource = alleklanten;
                cmbKlant.SelectedIndex = 0;
                txtKlant.Text = klant.Naam;
                txtAdres.Text = klant.Straat + " + " + klant.Huisnr;
                txtVAT.Text = klant.BTWnummer;
                txtContact.Text = klant.Contactnaam;
                txtMail.Text = klant.Email;
                txtPhone.Text = klant.Telefoon;
            }
            
        }

        private void cmbKlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  Klant klant = cmbKlant.SelectedItem.
        }
    }
}
