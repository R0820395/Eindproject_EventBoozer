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

        List<Event> events = new List<Event>();
        List<Notitie> notities = new List<Notitie>();
        List<ToDo> toDos = new List<ToDo>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Event Ev = dbOperations.eventOphalen(eventID);
            EventsDetails.DataContext = Ev;
            events = dbOperations.eventen();
            notities = dbOperations.notities();


            txtEventTypeNaam.Text = dbOperations.getEventType(1);
            txtAantalNotities.Text = notities.Count() + " Notes";
            txtAantalTodos.Text = dbOperations.getTodosCompletedCount() + " / " +  dbOperations.getAllTodosCount();
        }



    }
}
