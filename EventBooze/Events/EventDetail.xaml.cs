//Nisse

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

namespace EventBooze.Events
{
    /// <summary>
    /// Interaction logic for EventDetail.xaml
    /// </summary>
    public partial class EventDetail : Window
    {
        public EventDetail()
        {
            InitializeComponent();
        }

        int eventID = 6; //6, 4

        List<ToDo> toDos = new List<ToDo>();
        Klant klant = new Klant();
        Event Ev = new Event();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Event Ev = DatabaseOperations.OphalenEvent(eventID);
                int notities = Ev.Notities.Count();
                klant = Ev.Klant;
                txtEventNaam.Text = Ev.Eventnaam;
                txtEventTypeNaam.Text = Ev.Eventtype.Naam;

                txtAantalNotities.Text = notities + " Notes";

                if (Ev.ToDos != null)
                {
                    int allTodos = DatabaseOperations.getAllTodosCount(Ev.EventID);
                    int completedTodos = DatabaseOperations.getTodosCompletedCount(Ev.EventID);

                    txtAantalTodos.Text = completedTodos + " / " + allTodos;

                    if (completedTodos == allTodos && allTodos != 0)
                    {
                        chTodos.Kind = MaterialDesignThemes.Wpf.PackIconKind.CheckBold;
                        chTodos.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else if (allTodos == 0)
                    {
                        chTodos.Visibility = Visibility.Hidden;
                    }
                }


                if (Ev.Klant != null)
                {
                    txtKlantContactNaam.Text = klant.Contactnaam;
                    txtKlantNaam.Text = klant.Naam;
                }
                else
                {
                    chKlant.Visibility = Visibility.Hidden;
                }

                if (Ev.Locatie != null)
                {
                    txtLocatietAdres.Text = Ev.Locatie.Naam;
                    txtLocatiePlaats.Text = Ev.Locatie.Gemeente;
                }else
                {
                    chLocatie.Visibility = Visibility.Hidden;
                }

                int artiesten = DatabaseOperations.ophalenArtiesten(Ev.EventID).Count();
                    txtAantalArtiesten.Text = artiesten + " artists";

                if (artiesten == 0) chArtiesten.Visibility = Visibility.Hidden;

            }
            catch {
                MessageBox.Show("Dit event kan niet gevonden worden in de database");
            }
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            Window klantselectie = new KlantSelectie(eventID);
            klantselectie.ShowDialog();
        }

        private void btnArtist_Click(object sender, RoutedEventArgs e)
        {
            Window artiestenoverzicht = new ArtiestenOverzicht(eventID);
            artiestenoverzicht.ShowDialog();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
