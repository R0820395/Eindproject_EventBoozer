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

        int eventID = 4;

        List<ToDo> toDos = new List<ToDo>();
        Klant klant = new Klant();
        Event Ev = new Event();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Event Ev = DatabaseOperations.OphalenEvent(eventID);
            int notities = Ev.Notities.Count();
            klant = Ev.Klant;
            txtEventNaam.Text = Ev.Eventnaam;
            txtEventTypeNaam.Text = Ev.Eventtype.Naam;

            txtAantalNotities.Text = notities + " Notes";
            txtAantalTodos.Text = DatabaseOperations.getTodosCompletedCount(Ev.EventID) + " / " +  DatabaseOperations.getAllTodosCount(Ev.EventID);
            
            if(Ev.Klant != null)
            {
                runKlantNaam.Text = klant.Naam;
                runKlantContact.Text = klant.Contactnaam;
            }
            
            if(Ev.Locatie != null)
            {
                runKlantAdres.Text = Ev.Locatie.Naam;
                runKlantPlaats.Text = Ev.Locatie.Gemeente;
            }


            runAantalArtiesten.Text = DatabaseOperations.ophalenArtiesten(Ev.EventID).Count().ToString() + " artists";

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
    }
}
