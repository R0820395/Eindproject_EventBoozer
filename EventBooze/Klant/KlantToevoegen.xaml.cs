/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
/*+======================================================================= Gemaakt door: Nisse ============================================================================================+*/
/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

using DAL;
using System;
using System.Windows;

namespace EventBooze
{
    /// <summary>
    /// Interaction logic for KlantToevoegen.xaml
    /// </summary>
    public partial class KlantToevoegen : Window
    {
        Event huidigEvent = new Event();
        public KlantToevoegen(int eventID)
        {
            InitializeComponent();
            huidigEvent = DatabaseOperations.OphalenEvent(eventID);
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string foutmelding = Validatie();

            if (foutmelding == "")
            {
                Klant nieuweklant = new Klant();
                nieuweklant.Naam = txtKlant.Text;
                nieuweklant.Straat = txtStraat.Text;
                nieuweklant.Huisnr = int.Parse(txtNummer.Text);
                nieuweklant.Postcode = int.Parse(txtPostcode.Text);
                nieuweklant.Gemeente = txtStad.Text;
                nieuweklant.BTWnummer = txtVAT.Text;
                nieuweklant.Contactnaam = txtcontact.Text;
                nieuweklant.Email = txtmail.Text;
                nieuweklant.Telefoon = txtphone.Text;

                int geslaagd = DatabaseOperations.ToevoegenKlant(nieuweklant);

                if (geslaagd == 0)
                {
                    MessageBox.Show("De klant kon niet worden toegevoegd omwille van onbekende reden", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("De klant is toegevoegd.");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(foutmelding, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string Validatie()
        {
            string foutmelding = "";

            if (!txtmail.Text.Contains("@"))
            {
                foutmelding += "Het opgegeven mailadres is niet correct." + Environment.NewLine;
            }
            if (txtVAT.Text.Length < 4)
            {
                foutmelding += "Het opgegeven BTW nummer is te kort." + Environment.NewLine;
            }
            else if (txtVAT.Text.Substring(0, 3) != "BTW" || txtVAT.Text is null)
            {
                foutmelding += "Het opgegeven BTW nummer is niet correct." + Environment.NewLine;
            }

            //Numeriek is in principe fout. Het zou in principe een string moeten zijn.
            if (!int.TryParse(txtNummer.Text, out int huisnummer))
            {
                foutmelding += "Het opgegeven huisnummer is niet nummeriek!" + Environment.NewLine;
            }

            //Numeriek is in principe fout. Het zou in principe een string moeten zijn.
            if (!int.TryParse(txtPostcode.Text, out int postcode))
            {
                foutmelding += "De opgegeven postcode mag geen letters bevatten." + Environment.NewLine;
            }
            return foutmelding;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtEventNaam.Text = huidigEvent.Eventnaam;
            txtEventTypeNaam.Text = huidigEvent.Eventtype.Naam;
        }
    }
}
