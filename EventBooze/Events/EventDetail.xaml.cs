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
#nullable enable
    /// <summary>
    /// Interaction logic for EventDetail.xaml
    /// </summary>
    public partial class EventDetail : Window
    {
        public EventDetail()
        {            
            InitializeComponent();   
        }

        int eventID = 2;

        List<Event> events = new List<Event>();
        List<Notitie> notities = new List<Notitie>();
        List<ToDo> toDos = new List<ToDo>();
        Locatie eventLocatie = new Locatie();
        Event? Ev = new Event();
        Klant klant = new Klant();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Ev = dbOperations.eventOphalen(eventID);
                if (Ev.KlantID != null)
                {
                    klant = dbOperations.GetKlant((int)Ev.KlantID);
                }
                else
                {

                }
            }
            catch
            {
                
            }
            
            
            EventsDetails.DataContext = Ev;
            events = dbOperations.eventen();
            notities = dbOperations.notities();
            eventLocatie = dbOperations.getLocatie((int)Ev.LocatieID);


            txtEventTypeNaam.Text = dbOperations.getEventType(1);
            txtAantalNotities.Text = notities.Count() + " Notes";
            txtAantalTodos.Text = dbOperations.getTodosCompletedCount() + " / " +  dbOperations.getAllTodosCount();
            txtLocatieNaam.Text = eventLocatie.Naam.ToString();
            txtLocatieGemeente.Text = eventLocatie.Gemeente.ToString();
            if (klant.Naam != null)
            {
                txtKlantContactnaam.Text = klant.Contactnaam;
                txtKlantnaam.Text = klant.Naam;
            }

            
        } 

    }
}
