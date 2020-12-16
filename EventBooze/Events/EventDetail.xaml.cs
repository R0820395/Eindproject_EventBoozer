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

        int eventID = 6;

        //List<Event> events = new List<Event>();    events = dbOperations.eventen();
        string eventType = "";
        List<Notitie> notities = new List<Notitie>();
        List<ToDo> toDos = new List<ToDo>();
        List<Artiest> artiesten = new List<Artiest>();
        Locatie eventLocatie = new Locatie();
        Event Ev = new Event();
        Klant klant = new Klant();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //todos
            toDos = dbOperations.todos();
            if (toDos != null)
            {
                int allTodos = dbOperations.getAllTodosCount();
                int completedTodos = dbOperations.getTodosCompletedCount();
                
                txtAantalTodos.Text = completedTodos + " / " + allTodos;

                if (completedTodos == allTodos && allTodos != 0)
                {
                    chTodos.Kind = MaterialDesignThemes.Wpf.PackIconKind.CheckBold;
                    chTodos.Foreground = new SolidColorBrush(Colors.Green);
                }
                else if(allTodos == 0)
                {
                    chTodos.Visibility = Visibility.Hidden;
                }
            }

            // Notities
            notities = dbOperations.notities();
            if (notities != null)
            {
                txtAantalNotities.Text = notities.Count() + " Notes";
            }else
            {
                txtAantalNotities.Text = "0 Notes";
            }

            Ev = dbOperations.eventOphalen(eventID);
            if (Ev != null)
            {
                if (Ev.Eventnaam != null)
                {
                    txtEventNaam.Text = Ev.Eventnaam;
                }
                EventsDetails.DataContext = Ev;
                
                // eventtype
                eventType = dbOperations.getEventType(Ev.EventtypeID);
                if(eventType != null)
                {
                    txtEventTypeNaam.Text = eventType;
                }

                // Locatie
                try
                {
                    if (Ev.LocatieID != null)
                    {
                        eventLocatie = dbOperations.getLocatie((int)Ev.LocatieID);

                        if (eventLocatie.Naam != null) txtLocatieNaam.Text = eventLocatie.Naam.ToString();
                        if (eventLocatie.Gemeente != null) txtLocatieGemeente.Text = eventLocatie.Gemeente.ToString();

                        if(eventLocatie.Naam == null && eventLocatie.Gemeente == null) chLocatie.Visibility = Visibility.Hidden;
                    }
                    else { chLocatie.Visibility = Visibility.Hidden; }
                }
                catch
                {
                    chLocatie.Visibility = Visibility.Hidden;
                }

                // Klant
                try
                {
                    if (Ev.KlantID != null)
                    {
                        klant = dbOperations.GetKlant((int)Ev.KlantID);
                        if (klant.Naam != null) txtKlantnaam.Text = klant.Naam;
                        if (klant.Naam != null) txtKlantContactnaam.Text = klant.Contactnaam;

                        if (klant.Naam == null && klant.Naam == null) chKlant.Visibility = Visibility.Hidden;
                    }
                    else { chKlant.Visibility = Visibility.Hidden; }
                }
                catch { 
                    chKlant.Visibility = Visibility.Hidden;
                }

                // Artiesten
                try
                {
                    artiesten = dbOperations.getEventArtiesten(Ev.EventID);
                    if (artiesten != null)
                    {
                        txtAantalArtiesten.Text = artiesten.Count() + " artists";
                    }
                }
                catch
                {
                    chArtiesten.Visibility = Visibility.Hidden; 
                }


            }
            else
            {
                MessageBox.Show("EV failed");
            }


        } 

    }
}
