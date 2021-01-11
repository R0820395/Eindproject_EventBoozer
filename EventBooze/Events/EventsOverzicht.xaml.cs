// author: Dieter Daems
using System;
using System.Collections.Generic;
using System.IO;
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
using MaterialDesignThemes.Wpf;

namespace EventBooze.Events
{
    /// <summary>
    /// Interaction logic for EventsOverzicht.xaml
    /// </summary>
    public partial class EventsOverzicht : Window
    {
        public EventsOverzicht()
        {
            InitializeComponent();
            createView();
        }
        public void updateView()
        {
            gridEvents.Children.Clear();
            createView();
        }
        private void createView()
        {
            List<Event> events = DatabaseOperations.OphalenAllEvents();
            int items = events.Count();

            // voeg de aantal rijen toe aan de mainGrid
            var gridContainer = new Grid();
            for (int i = 0; i < items; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(100);
                gridContainer.RowDefinitions.Add(row);
            }

            // 1,System.Windows.GridUnitType.Auto
            //    grid.Height = new GridLength(Double.NaN); //Double.NaN
            int colorIndex = 1;
            int itemIndexer = 0;
            foreach (Event val in events)
            {
                string idEvent = val.EventID.ToString();
                //Maak een grid element aant
                var grid = new Grid();
                ColumnDefinition gridCol0 = new ColumnDefinition();
                gridCol0.Width = new GridLength(15);
                ColumnDefinition gridCol1 = new ColumnDefinition();
                gridCol1.Width = new GridLength(5);
                ColumnDefinition gridCol2 = new ColumnDefinition();
                ColumnDefinition gridCol3 = new ColumnDefinition();
                gridCol3.Width = new GridLength(5);
                ColumnDefinition gridCol4 = new ColumnDefinition();
                gridCol4.Width = new GridLength(35);
                ColumnDefinition gridCol5 = new ColumnDefinition();
                gridCol5.Width = new GridLength(35);
                ColumnDefinition gridCol6 = new ColumnDefinition();
                gridCol6.Width = new GridLength(5);
                grid.ColumnDefinitions.Add(gridCol0);
                grid.ColumnDefinitions.Add(gridCol1);
                grid.ColumnDefinitions.Add(gridCol2);
                grid.ColumnDefinitions.Add(gridCol3);
                grid.ColumnDefinitions.Add(gridCol4);
                grid.ColumnDefinitions.Add(gridCol5);
                grid.ColumnDefinitions.Add(gridCol6);

                RowDefinition gridRow1 = new RowDefinition();
                gridRow1.Height = new GridLength(17);
                RowDefinition gridRow2 = new RowDefinition();
                gridRow2.Height = new GridLength(15);
                RowDefinition gridRow3 = new RowDefinition();
                gridRow3.Height = new GridLength(5);
                RowDefinition gridRow4 = new RowDefinition();
                gridRow4.Height = new GridLength(12);
                RowDefinition gridRow5 = new RowDefinition();
                gridRow2.Height = new GridLength(14);
                RowDefinition gridRow6 = new RowDefinition();
                gridRow6.Height = new GridLength(35);
                RowDefinition gridRow7 = new RowDefinition();
                gridRow7.Height = new GridLength(1);
                grid.RowDefinitions.Add(gridRow1);
                grid.RowDefinitions.Add(gridRow2);
                grid.RowDefinitions.Add(gridRow3);
                grid.RowDefinitions.Add(gridRow4);
                grid.RowDefinitions.Add(gridRow5);
                grid.RowDefinitions.Add(gridRow6);
                grid.RowDefinitions.Add(gridRow7);
                // Voeg een rij toe, daarna het gridelement en zetten we deze in de juist rij
                // gridContainer.RowDefinitions.Add(new RowDefinition());
                gridContainer.Children.Add(grid);
                Grid.SetRow(grid, itemIndexer);
                itemIndexer++;

                // kolom 0, de kleur schakeling
                SolidColorBrush kleur;
                switch (colorIndex)
                {
                    case 1: kleur = new SolidColorBrush(Colors.White); break;
                    case 2: kleur = new SolidColorBrush(Colors.Yellow); break;
                    case 3: kleur = new SolidColorBrush(Colors.Green); break;
                    case 4: kleur = new SolidColorBrush(Colors.Blue); break;
                    case 5: kleur = new SolidColorBrush(Colors.Red); break;
                    default: kleur = new SolidColorBrush(Colors.White); break;
                }


                var rec = new Rectangle();
                rec.Fill = kleur;
                grid.Children.Add(rec);
                Grid.SetColumn(rec, 0);
                Grid.SetRow(rec, 0);
                Grid.SetRowSpan(rec, 6);

                if (colorIndex < 5) { colorIndex++; } else { colorIndex = 1; }
                // kolom 1 = spacer
                // kolom 2
                var textBlock = new TextBlock() { Text = val.Eventnaam.ToUpper() };
                grid.Children.Add(textBlock);
                Grid.SetColumn(textBlock, 2);
                Grid.SetRow(textBlock, 0);

                var textBlock2 = new TextBlock()
                {
                    Text = val.Eventtype.Naam.ToString(),
                    FontSize = 12
                };
                grid.Children.Add(textBlock2);
                Grid.SetColumn(textBlock2, 2);
                Grid.SetRow(textBlock2, 1);

                var seperator = new Separator()
                {
                    Margin = new Thickness(0),
                    Padding = new Thickness(0),
                    Height = 1,
                    MaxHeight = 1
                };
                grid.Children.Add(seperator);
                Grid.SetColumn(seperator, 2);
                Grid.SetRow(seperator, 2);

                DateTime date = DateTime.Parse(val.Datum.ToString());
                var textBlock3 = new TextBlock() { Text = date.DayOfWeek.ToString(), FontSize = 10 };
                grid.Children.Add(textBlock3);
                Grid.SetColumn(textBlock3, 2);
                Grid.SetRow(textBlock3, 3);

                var textBlock4 = new TextBlock() { Text = date.ToString("dd, MMM, yyyy") + ' ' + val.Startuur + '-' + val.Einduur, FontSize = 12 };
                grid.Children.Add(textBlock4);
                Grid.SetColumn(textBlock4, 2);
                Grid.SetRow(textBlock4, 4);

                var btnSelect = new Button()
                {
                    Content = "SELECT",
                    Background = new SolidColorBrush(Colors.Orange),
                    FontSize = 13,
                    Width = 100,
                    Height = 25,
                    HorizontalAlignment = (HorizontalAlignment)0,
                    Margin = new Thickness(15, 0, 0, 5),
                    Tag = idEvent,
                };
                btnSelect.Click += selectEvent_Click;
                grid.Children.Add(btnSelect);
                Grid.SetColumn(btnSelect, 2);
                Grid.SetRow(btnSelect, 5);

                // kolom 3 = spacer
                // kolom 4 en kolom 5
                var logo = new Image()
                {
                    Source = LoadImage(val.Eventtype.Logo),
                    Height = 70,
                    Width = 70,
                };
                grid.Children.Add(logo);
                Grid.SetColumn(logo, 3);
                Grid.SetColumnSpan(logo, 3);
                Grid.SetRowSpan(logo, 5);

                var gear = new PackIcon()
                {
                    Height = 35,
                    Width = 35,
                    Cursor = Cursors.Hand,
                    Tag = idEvent
                };
                gear.MouseLeftButtonDown += settings_Click;
                gear.Kind = PackIconKind.Gear;
                grid.Children.Add(gear);
                Grid.SetRow(gear, 5);
                Grid.SetColumn(gear, 4);
                Grid.SetRowSpan(gear, 2);

                var trashcan = new PackIcon()
                {
                    Height = 35,
                    Width = 35,
                    Cursor = Cursors.Hand,
                    Tag = idEvent
                };
                trashcan.MouseLeftButtonDown += verwijderen_Click;
                trashcan.Kind = PackIconKind.Trash;
                grid.Children.Add(trashcan);
                Grid.SetRow(trashcan, 5);
                Grid.SetColumn(trashcan, 5);
                Grid.SetRowSpan(trashcan, 2);
                // kolom 6 = spacer
                // Scheiding card
                var seperatorItem = new Separator()
                {
                    Margin = new Thickness(0),
                    Padding = new Thickness(0),
                    Height = 1,
                    MaxHeight = 1
                };
                grid.Children.Add(seperatorItem);
                Grid.SetRow(seperatorItem, 6);
                Grid.SetColumnSpan(seperatorItem, 7);
            }
            gridEvents.Children.Add(gridContainer);
        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            EventBewerken toevoegenEvent = new EventBewerken(0);
            toevoegenEvent.Success += callViewUpdate;
            toevoegenEvent.Show();
        }
        private void callViewUpdate(object sender, EventArgs e)
        {
            updateView();
        }

        private void selectEvent_Click(object sender, RoutedEventArgs e)
        {
            string idEvent = ((Button)sender).Tag.ToString();
            int id = int.Parse(idEvent);
            Window selectEvent = new EventDetail(id);
            selectEvent.Show();
        }


        private void settings_Click(object sender, RoutedEventArgs e)
        {


            string idEvent = ((PackIcon)sender).Tag.ToString();
            int id = int.Parse(idEvent);
            EventBewerken editEvent = new EventBewerken(id);
            editEvent.Success += callViewUpdate;
            editEvent.Show();

        }

        private void verwijderen_Click(object sender, RoutedEventArgs e)
        {
            string idEvent = ((PackIcon)sender).Tag.ToString();
            int id = int.Parse(idEvent);

            Event ev = DatabaseOperations.OphalenEvent(id);
            // ev.EventtypeID is niet nullable, dus gaan we ervanuit dat het hier alleen om de klant en locatie gegevens gaat
            if (ev.KlantID == null && ev.LocatieID == null)
            {
                bool succes = DatabaseOperations.verwijderEvent(ev);
                if (succes)
                {
                    MessageBox.Show("Event verwijderd");
                    updateView();
                }
                else
                {
                    MessageBox.Show("Event kon niet verwijderd worden uit de database");
                    // TODO : FOUTLOGGEN
                }
                
            }
            else {
                MessageBox.Show("Event kan niet verwijderd worden, er zijn nog gegevens aan verbonden.", "Melding", MessageBoxButton.OK);
            }
            // Men kan enkel een event verwijderen als er geen
            // gegevens meer mee gekoppeld zijn
        }
    }
}
