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
using System.Windows.Navigation;
using DAL;

namespace EventBooze
{
    /// <summary>
    /// Interaction logic for KlantToevoegen.xaml
    /// </summary>
    public partial class KlantToevoegen : Window
    {
        public KlantToevoegen()
        {
            InitializeComponent();
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string foutmelding = Validatie();

            if(foutmelding == "")
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

                if(geslaagd == 0)
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
            } else if (txtVAT.Text.Substring(0, 3) != "BTW" || txtVAT.Text is null) {
                foutmelding += "Het opgegeven BTW nummer is niet correct." + Environment.NewLine;
            }


            //Numeriek is in principe fout. Het zou in principe een string moeten zijn.
            if(!int.TryParse(txtNummer.Text, out int huisnummer))
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
    }
}
