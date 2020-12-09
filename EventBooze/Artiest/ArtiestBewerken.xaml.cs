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
            if (overzichtArtiest != null)
            {
                txtArtiest.Text = this.overzichtArtiest.Naam;
                lblNaamArtiest.Content = txtArtiest.Text;
                txtTelefoonnummer.Text = this.overzichtArtiest.Telefoon;
                txtEmail.Text = this.overzichtArtiest.Email;
                txtBankaccount.Text = this.overzichtArtiest.Bankrekeningnr;
            }
            else { btnSave.Content = "Toevoegen"; }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Validatie();
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                int ok;

                if (overzichtArtiest != null)
                {
                    overzichtArtiest.Naam = txtArtiest.Text;
                    overzichtArtiest.Telefoon = txtTelefoonnummer.Text;
                    overzichtArtiest.Email = txtEmail.Text;
                    overzichtArtiest.Bankrekeningnr = txtBankaccount.Text;
                    ok = DatabaseOperations.aanpassenArtiest(overzichtArtiest);
                }
                else
                {
                    Artiest artiest = new Artiest();
                    artiest.Naam = txtArtiest.Text;
                    artiest.Telefoon = txtTelefoonnummer.Text;
                    artiest.Email = txtEmail.Text;
                    artiest.Bankrekeningnr = txtBankaccount.Text;
                    ok = DatabaseOperations.toevoegenArtiest(artiest);
                }

                if (ok >= 1) {Close();};
            }
            else
            {
                MessageBox.Show(foutmeldingen, "Foutmelding(en)", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string Validatie()
        {
            string foutmeldingen = "";
            if (string.IsNullOrWhiteSpace(txtArtiest.Text))
            {
                foutmeldingen += "Artiestnaam: verplicht" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(txtTelefoonnummer.Text))
            {
                foutmeldingen += "Telefoonnummer: verplicht" + Environment.NewLine;
            }
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {            
                foutmeldingen += "Email: niet geldig" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                foutmeldingen += "Email: verplicht" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(txtBankaccount.Text))
            {
                foutmeldingen += "Bankaccount: verplicht" + Environment.NewLine;
            }

            return foutmeldingen;
        }

        private void txtArtiest_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblNaamArtiest.Content = txtArtiest.Text;
            
        }
    }
}
