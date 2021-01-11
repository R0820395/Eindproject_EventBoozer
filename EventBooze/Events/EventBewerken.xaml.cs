// author: Dieter Daems
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DAL;

namespace EventBooze.Events
{
    /// <summary>
    /// Interaction logic for EventBewerken.xaml
    /// </summary>
    public partial class EventBewerken : Window
    {
        private bool isUpdate = false;
        public Event ev = new Event();
        
        public EventBewerken(int eventId)
        {

            if(eventId != 0)
            {
                renderDate(eventId);
            }
            else
            {
                ev.EventtypeID = 1;
            }
            this.DataContext = ev;


            InitializeComponent();
        }
        

        private void renderDate(int eventId)
        {
            ev = DatabaseOperations.OphalenEvent(eventId);

           // txtEventName.Text = ev.Eventnaam;
           // txtType.Text = ev.Eventtype.Naam.ToString();
           // txtDate.Text = ev.Datum.ToString("dd, MMM, yyyy");
           // txtStart.Text = ev.Startuur;
           // txtEnd.Text = ev.Einduur;

            isUpdate = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //string foutmeldingen = inputControle();
            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
            bool foutmeldingen = ev.IsGeldig();
            string err = ev.Error;

            if (!foutmeldingen)
            {
                MessageBox.Show(err);
                return;
            }
            
            ev.Eventnaam = txtEventName.Text;
            //ev.Eventtype.Naam = txtType.Text;
            ev.Datum = DateTime.Parse(txtDate.Text);
            ev.Startuur = txtStart.Text;
            ev.Einduur = txtEnd.Text;
            // TODO : Toon messagebox on succes
            if (isUpdate) {
                if(DatabaseOperations.AanpassenEvent(ev) == 0)
                {
                    MessageBox.Show("Het aanpassen van de gegevens is niet gelukt!");
                }
                else
                {
                    OnSuccess(EventArgs.Empty);
                    this.Close();
                }
            }
            else
            {
                if (DatabaseOperations.ToevoegenEvent(ev)) {
                    OnSuccess(EventArgs.Empty);
                    this.Close(); 
                } else {
                    MessageBox.Show("Het bewaren van de gegevens is niet gelukt!");
                };
            }

        }
        public event EventHandler Success;
        protected virtual void OnSuccess(EventArgs e)
        {
            Success?.Invoke(this, e);
            MessageBox.Show("gegevens succesvol toegegoegd");
        }

        private string inputControle()
        {
            string foutmeldingen = "";

            if (string.IsNullOrWhiteSpace(txtEventName.Text))
            {
                foutmeldingen += "Een event heeft een naam nodig." + Environment.NewLine;
            }
            
            if (!string.IsNullOrWhiteSpace(txtType.Text))
            {
                string input = txtType.Text.ToLower();
                bool exists = false;
                List<Eventtype> types = DatabaseOperations.EventTypesOphalen();

                foreach (Eventtype val in types)
                {
                    string typenaam = val.Naam.ToString().ToLower();
                    if (typenaam == input)
                    {
                        exists = true;
                        ev.EventtypeID = val.EventtypeID;
                    }
                }

                if (!exists)
                {
                    MessageBox.Show("Does not exist");
                    // TODO : aanmaken eventType
                    foutmeldingen += "Het opgegeven event type bestaat niet." + Environment.NewLine;
                }
            }
            else
            {
                foutmeldingen += "Het Type veld mag niet leeg zijn." + Environment.NewLine;
            }
            
            if (!string.IsNullOrWhiteSpace(txtDate.Text)) {
                if (!DateTime.TryParse(txtDate.Text, out DateTime res))
                {
                    foutmeldingen += "De ingegeven datum is niet geldig." + Environment.NewLine;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtStart.Text))
            {
                if (!DataValidatieBasis.IsValidTime(txtStart.Text))
                {
                    foutmeldingen += "Het opgegeven startuur is niet geldig." + Environment.NewLine;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtEnd.Text))
            {
                if (!DataValidatieBasis.IsValidTime(txtEnd.Text))
                {
                    foutmeldingen += "Het opgegeven einduur is niet geldig." + Environment.NewLine;
                }
            }

            return foutmeldingen;
        }
        

        public void EnableValidateOnErrorsBinding(TextBox name)
        {
            Binding binding = new Binding();

            binding.Path = name.GetBindingExpression(TextBox.TextProperty).ParentBinding.Path;

            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            binding.ValidatesOnDataErrors = true;

            binding.StringFormat = "d";

            name.SetBinding(TextBox.TextProperty, binding);
        }
        bool txtEventnameBinding = false;
        bool txtDateBinding = false;
        bool txtStartBinding = false;
        bool txtEndBinding = false;
        bool txtTypeBinding = false;
        private void txtEventName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtEventnameBinding) return;
            EnableValidateOnErrorsBinding(txtEventName);
            txtEventnameBinding = true;
        }
        private void txtDate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtDateBinding) return;
            EnableValidateOnErrorsBinding(txtDate);
            txtDateBinding = true;
        }

        private void txtType_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTypeBinding) return;
            EnableValidateOnErrorsBinding(txtType);
            txtTypeBinding = true;
        }

        private void txtStart_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtStartBinding) return;
            EnableValidateOnErrorsBinding(txtStart);
            txtStartBinding = true;
        }

        private void txtEnd_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtEndBinding) return;
            EnableValidateOnErrorsBinding(txtEnd);
            txtEndBinding = true;
        }


    }
}

