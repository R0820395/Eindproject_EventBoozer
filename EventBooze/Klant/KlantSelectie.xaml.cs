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
        Klant nieuweklant = new Klant();
        public KlantSelectie()
        {
            //public KlantSelectie(int EventIndex) Afgezet wegens testen.
            InitializeComponent();
            huidigEvent = DatabaseOperations.OphalenEvent(4);
            //4 ==> EventIndex . Nu vier om te testen.
            lblEvent.Content = huidigEvent.Eventnaam;
            btnTerug.Content = "< " + huidigEvent.Eventnaam;
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
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnNieuw.IsEnabled = false;


            }

        }

        private void cmbKlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nieuweklant = (Klant)cmbKlant.SelectedItem;
            txtKlant.Text = nieuweklant.Naam;
            txtAdres.Text = nieuweklant.Straat + " " + nieuweklant.Huisnr;
            txtVAT.Text = nieuweklant.BTWnummer;
            txtContact.Text = nieuweklant.Contactnaam;
            txtMail.Text = nieuweklant.Email;
            txtPhone.Text = nieuweklant.Telefoon;
            btnSave.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            huidigEvent.KlantID = nieuweklant.KlantID;
            int geslaagd = DatabaseOperations.AanpassenEvent(huidigEvent);
            
            if(geslaagd == 0)
            {
                MessageBox.Show("De klant kon niet worden toegewezen. Contacteer Helpdesk", "Melding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
            
            MessageBox.Show("De klant is toegewezen aan dit Event", "Melding", MessageBoxButton.OK);
            //terug naar vorige pagina
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            huidigEvent.KlantID = null;
            int geslaagd = DatabaseOperations.AanpassenEvent(huidigEvent);

            if(geslaagd == 0)
            {
                MessageBox.Show("De klant kon niet worden verwijderd. Contacteer Helpdesk", "Melding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("De klant is verwijderd van dit Event", "Melding", MessageBoxButton.OK);
            }

            //terug naar vorige pagina
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window nieuweKlantAanmaken = new KlantToevoegen(huidigEvent);
            nieuweKlantAanmaken.ShowDialog();
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            //Terug naar Event overzicht (Dieter)
        }
    }
}
