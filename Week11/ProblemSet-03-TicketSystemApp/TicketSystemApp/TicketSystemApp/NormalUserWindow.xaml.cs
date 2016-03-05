using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
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
using TicketSystem;

namespace TicketSystemApp
{
    /// <summary>
    /// Interaction logic for NormalUserWindow.xaml
    /// </summary>
    public partial class NormalUserWindow : Window
    {
        User user;
        TicketDB dbContext;
        Schedule selectedScheduleForTicket;
        bool populatedBuyTicketTabItem;
        decimal originalTicketPrice;
        decimal discountedTicketPrice;
        public NormalUserWindow(TicketDB dbContext, User user)
        {
            InitializeComponent();
            this.dbContext = dbContext;
            this.user = user;
            selectedScheduleForTicket = null;
            populatedBuyTicketTabItem = false;
            originalTicketPrice = 0;
            discountedTicketPrice = 0;
        }

        class ComboBoxCustomItem : ComboBoxItem
        {
            public object CustomItem { get; private set; }

            public ComboBoxCustomItem(City city)
            {
                CustomItem = city;
                Content = city.Name;
            }

            public ComboBoxCustomItem(Seat seat)
            {
                CustomItem = seat;
                Content = seat.SeatNumber;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource trainViewSource = ((CollectionViewSource)(this.FindResource("trainViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // trainViewSource.Source = [generic data source]
            CollectionViewSource scheduleViewSource = ((CollectionViewSource)(this.FindResource("scheduleViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // scheduleViewSource.Source = [generic data source]
            var sortedCitiesByName = (from c in dbContext.Cities
                                     orderby c.Name
                                     select c).ToList();

            foreach (var city in sortedCitiesByName)
            {
                fromCityComboBox.Items.Add(new ComboBoxCustomItem(city));
                toCityComboBox.Items.Add(new ComboBoxCustomItem(city));
            }
        }

        private void trainScheduleDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            trainDataGrid.ItemsSource = null;
            trainDataGrid.Items.Clear();
            trainSchedulesDataGrid.ItemsSource = null;
            trainSchedulesDataGrid.Items.Clear();

            if (trainScheduleDatePicker.SelectedDate != null)
            {
                var selectedDate = (DateTime)trainScheduleDatePicker.SelectedDate;

                var trainsInGivenDate = from s in dbContext.Schedules
                                        where s.DepartureTime.Year == selectedDate.Year
                                        && s.DepartureTime.Month == selectedDate.Month
                                        && s.DepartureTime.Day == selectedDate.Day
                                        select s.Train;

                if (!trainsInGivenDate.Any())
                {
                    MessageBox.Show("No trains in given date!");
                }
                else
                {
                    trainDataGrid.ItemsSource = trainsInGivenDate.ToList();
                }
            }
        }

        private void trainDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Any())
            {
                var selectedCell = e.AddedCells.First();

                var train = selectedCell.Item as Train;

                if (train != null)
                {
                    var schedulesForGivenTrain = from s in dbContext.Schedules
                                                 where s.TrainID == train.TrainID
                                                 select s;

                    trainSchedulesDataGrid.ItemsSource = schedulesForGivenTrain.ToList();
                }
            }
        }

        private void checkForTrips_Click(object sender, RoutedEventArgs e)
        {
            tripsDataGrid.ItemsSource = null;
            tripsDataGrid.Items.Clear();

            var fromCityCBItem = fromCityComboBox.SelectedItem as ComboBoxCustomItem;
            var toCityCBItem = toCityComboBox.SelectedItem as ComboBoxCustomItem;
            var datePickerDate = tripDatePicker.SelectedDate;
            if (fromCityCBItem != null && toCityCBItem != null && datePickerDate != null)
            {
                var fromCity = fromCityCBItem.CustomItem as City;
                var toCity = toCityCBItem.CustomItem as City;
                var tripDate = (DateTime)datePickerDate;

                var schedules = from s in dbContext.Schedules
                                where s.StartCityID == fromCity.CityID
                                && s.EndCityID == toCity.CityID
                                && s.DepartureTime.Year == tripDate.Year
                                && s.DepartureTime.Month == tripDate.Month
                                && s.DepartureTime.Day == tripDate.Day
                                select s;

                if (schedules.Any())
                {
                    tripsDataGrid.ItemsSource = schedules.ToList();
                }
                else
                {
                    MessageBox.Show("No trips meeting the requirements!");
                }
            }
            else MessageBox.Show("Please select cities and trip date!");
        }

        private void schedulesDataGrids_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Any())
            {
                var selectedCell = e.AddedCells.First();

                var schedule = selectedCell.Item as Schedule;

                if (schedule != null)
                {
                    selectedScheduleForTicket = schedule;
                    populatedBuyTicketTabItem = false;
                }
            }

            if (tripsDataGrid.Items.Count == 0 && trainSchedulesDataGrid.Items.Count == 0)
            {
                selectedScheduleForTicket = null;
                populatedBuyTicketTabItem = false;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (buyTicketTabItem.IsSelected)
            {
                if (!populatedBuyTicketTabItem)
                {
                    if (selectedScheduleForTicket == null)
                    {
                        MessageBox.Show("Please select a trip first!");
                        mainTabControl.SelectedIndex = 1;
                    }
                    else
                    {
                        bool hasDiscountCard = user.DiscountCard != null;
                        decimal ticketPrice = selectedScheduleForTicket.TicketPrice;
                        decimal discountedPrice = ticketPrice;
                        if (hasDiscountCard) discountedPrice = ticketPrice - (ticketPrice * user.DiscountCard.Discount);
                        if (discountedPrice < 0) discountedPrice = 0;

                        originalTicketPrice = ticketPrice;
                        discountedTicketPrice = discountedPrice;

                        scheduleIDLabel.Content = selectedScheduleForTicket.ScheduleID;
                        startCityLabel.Content = selectedScheduleForTicket.StartCity.Name;
                        endCityLabel.Content = selectedScheduleForTicket.EndCity.Name;
                        trainLabel.Content = string.Format("ID: {0}, Train description: {1}",
                                                           selectedScheduleForTicket.TrainID,
                                                           selectedScheduleForTicket.Train.BriefDescription);
                        departureDateLabel.Content = selectedScheduleForTicket.DepartureTime;
                        timeTravelLabel.Content = selectedScheduleForTicket.TravelTime;
                        if (hasDiscountCard)
                            ticketPriceLabel.Content = string.Format("{0:N2} discounted from {1:N2}",
                                                                     discountedPrice, ticketPrice);
                        else
                            ticketPriceLabel.Content = ticketPrice;

                        var seatsToSelectFrom = from s in dbContext.Seats
                                                where s.ScheduleID == selectedScheduleForTicket.ScheduleID
                                                && s.Taken == false
                                                orderby s.SeatNumber
                                                select s;

                        if (seatsToSelectFrom.Any())
                        {
                            seatComboBox.Items.Clear();
                            foreach (var seat in seatsToSelectFrom)
                            {
                                seatComboBox.Items.Add(new ComboBoxCustomItem(seat));
                            }

                            populatedBuyTicketTabItem = true;
                        }
                        else
                        {
                            var tabControl = buyTicketTabItem.Parent as TabControl;
                            MessageBox.Show("No more seats available for this trip!");
                            tabControl.SelectedIndex = 1;
                        }
                    }
                }
            }
        }

        private void buyTicketButton_Click(object sender, RoutedEventArgs e)
        {
            var seatCBItem = seatComboBox.SelectedItem as ComboBoxCustomItem;
            if (seatCBItem != null)
            {
                var selectedSeat = seatCBItem.CustomItem as Seat;

                try
                {
                    Ticket ticket = new Ticket
                    {
                        SeatNumber = selectedSeat.SeatNumber,
                        TrainID = selectedScheduleForTicket.TrainID,
                        ScheduleID = selectedScheduleForTicket.ScheduleID,
                        TripDateAndTime = selectedScheduleForTicket.DepartureTime,
                        OriginalPrice = originalTicketPrice,
                        PriceSold = discountedTicketPrice,
                        UserID = user.UserID
                    };

                    dbContext.Tickets.Add(ticket);

                    selectedSeat.Taken = true;

                    dbContext.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot buy ticket! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                MessageBox.Show("Ticket bought successfully!");
                seatComboBox.Items.Remove(seatCBItem);
            }
            else MessageBox.Show("Please select seat to buy!");
        }
    }
}
