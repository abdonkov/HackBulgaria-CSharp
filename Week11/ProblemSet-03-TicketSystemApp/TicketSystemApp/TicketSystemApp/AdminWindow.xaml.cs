using System;
using System.Collections.Generic;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        User user;
        TicketDB dbContext;

        City startCityForSchedule;
        City endCityForSchedule;
        Train trainForSchedule;
        City beginEditCity;
        Train beginEditTrain;
        Schedule beginEditSchedule;

        public AdminWindow(TicketDB dbContext, User user)
        {
            InitializeComponent();
            this.dbContext = dbContext;
            this.user = user;
            startCityForSchedule = null;
            endCityForSchedule = null;
            trainForSchedule = null;
            beginEditCity = null;
            beginEditTrain = null;
            beginEditSchedule = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource cityViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cityViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // cityViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource scheduleViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("scheduleViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // scheduleViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource trainViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("trainViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // trainViewSource.Source = [generic data source]

            UpdateDataGrids();
        }

        private void UpdateDataGrids()
        {
            var cities = from c in dbContext.Cities
                         select c;
            var trains = from t in dbContext.Trains
                         select t;
            var schedules = from s in dbContext.Schedules
                            select s;

            cityDataGrid.ItemsSource = null;
            cityDataGrid.ItemsSource = cities.ToList();

            trainDataGrid.ItemsSource = null;
            trainDataGrid.ItemsSource = trains.ToList();

            scheduleDataGrid.ItemsSource = null;
            scheduleDataGrid.ItemsSource = schedules.ToList();
        }

        private void mainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (citiesTab.IsSelected)
            {

            }
        }

        private void cityDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (!e.Row.IsEditing)
            {
                var city = e.Row.Item as City;
                if (city != null)
                {
                    beginEditCity = new City();
                    beginEditCity.CityID = city.CityID;
                    beginEditCity.Name = city.Name;
                }
            }
        }

        private void cityDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var city = e.Row.Item as City;
            if (city != null)
            {
                if (city.CityID != 0)
                {
                    try
                    {
                        if (city.Name.Length < 1)
                        {
                            MessageBox.Show("City should have a name! Nothing saved to database!");
                            city.Name = beginEditCity.Name;
                            e.Row.Item = null;
                            e.Row.Item = city;
                        }
                        else
                            dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Could not update database! Error message: {ex.Message}");
                    }
                }
                else
                {
                    try
                    {
                        if (city.Name.Length < 1)
                        {
                            MessageBox.Show("City should have a name! Nothing saved to database!");
                            var currentItemsSource = trainDataGrid.ItemsSource;
                            trainDataGrid.ItemsSource = null;
                            trainDataGrid.Items.Remove(e.Row);
                            trainDataGrid.ItemsSource = currentItemsSource;
                        }
                        else
                        {
                            dbContext.Cities.Add(city);
                            dbContext.SaveChanges();
                            e.Row.Item = null;
                            e.Row.Item = city;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Could not update database! Error message: {ex.Message}");
                    }
                }
            }

            beginEditCity = null;
        }

        private void cityDataGrid_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var cityToDelete = cityDataGrid.SelectedItem as City;

            if (e.Command == DataGrid.DeleteCommand)
            {
                if (MessageBox.Show(string.Format("Would you like to delete {0}?",
                                                  cityToDelete.Name),
                                                  "Confirm Delete",
                                                  MessageBoxButton.YesNo,
                                                  MessageBoxImage.Question,
                                                  MessageBoxResult.No) != MessageBoxResult.Yes)
                {
                    e.Handled = true;
                }
                else
                {
                    try
                    {
                        var schedulesToDelete = from schedule in dbContext.Schedules
                                                where schedule.StartCityID == cityToDelete.CityID
                                                || schedule.EndCityID == cityToDelete.CityID
                                                select schedule;

                        foreach (var schedule in schedulesToDelete)
                        {

                            var ticketsToDelete = from ticket in dbContext.Tickets
                                                  where ticket.ScheduleID == schedule.ScheduleID
                                                  select ticket;

                            dbContext.Tickets.RemoveRange(ticketsToDelete);

                            var seatsToDelete = from seat in schedule.Train.Seats
                                                where seat.ScheduleID == schedule.ScheduleID
                                                select seat;

                            dbContext.Seats.RemoveRange(seatsToDelete);

                            dbContext.Schedules.Remove(schedule);
                        }

                        dbContext.Cities.Remove(cityToDelete);

                        dbContext.SaveChanges();

                        if(cityToDelete.CityID == (startCityForSchedule?.CityID ?? -1))
                        {
                            startCityForSchedule = null;
                            chosenStartCityLabel.Content = "Start city: None";
                            startCityInScheduleLabel.Content = "None";
                        }
                        else if (cityToDelete.CityID == (endCityForSchedule?.CityID ?? -1))
                        {
                            endCityForSchedule = null;
                            chosenEndCityLabel.Content = "End city: None";
                            endCityInScheduleLabel.Content = "None";
                        }

                        UpdateDataGrids();
                    }
                    catch (Exception ex)
                    {
                        e.Handled = true;
                        MessageBox.Show($"Could not delete item in database! Error message: {ex.Message}");
                    }
                }
            }
        }

        private void trainDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (!e.Row.IsEditing)
            {
                var train = e.Row.Item as Train;
                if (train != null)
                {
                    beginEditTrain = new Train();
                    beginEditTrain.TrainID = train.TrainID;
                    beginEditTrain.NumberOfSeats = train.NumberOfSeats;
                    beginEditTrain.BriefDescription = train.BriefDescription;
                }
            }
        }

        private void trainDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var train = e.Row.Item as Train;
            if (train != null)
            {
                if (train.TrainID != 0)
                {
                    try
                    {
                        if (train.NumberOfSeats <= 0)
                        {
                            MessageBox.Show("Seats number must be possitive! Nothing saved to database!");
                            train.NumberOfSeats = beginEditTrain.NumberOfSeats;
                            train.BriefDescription = beginEditTrain.BriefDescription;
                            e.Row.Item = null;
                            e.Row.Item = train;
                        }
                        else if (train.BriefDescription.Length < 1)
                        {
                            MessageBox.Show("Train must have a description! Nothing saved to database!");
                            train.NumberOfSeats = beginEditTrain.NumberOfSeats;
                            train.BriefDescription = beginEditTrain.BriefDescription;
                            e.Row.Item = null;
                            e.Row.Item = train;
                        }
                        else
                        {
                            if (train.NumberOfSeats < beginEditTrain.NumberOfSeats)
                            {
                                var seatsToRemove = from s in dbContext.Seats
                                                    where s.TrainID == train.TrainID
                                                          && s.SeatNumber > train.NumberOfSeats
                                                    select s;

                                var ticketsToRemove = from t in dbContext.Tickets
                                                      where t.TrainID == train.TrainID
                                                            && t.SeatNumber > train.NumberOfSeats
                                                      select t;

                                dbContext.Tickets.RemoveRange(ticketsToRemove);
                                dbContext.Seats.RemoveRange(seatsToRemove);
                            }
                            else if (train.NumberOfSeats > beginEditTrain.NumberOfSeats)
                            {
                                var schedulesForTrain = from s in dbContext.Schedules
                                                         where s.TrainID == train.TrainID
                                                         select s;

                                foreach (var schedule in schedulesForTrain)
                                {
                                    for (int i = beginEditTrain.NumberOfSeats; i < train.NumberOfSeats; i++)
                                    {
                                        dbContext.Seats.Add(new Seat()
                                        {
                                            SeatNumber = i + 1,
                                            TrainID = train.TrainID,
                                            ScheduleID = schedule.ScheduleID,
                                            Taken = false,
                                            Class = SeatClass.FirstClass
                                        });
                                    }
                                }
                            }
                            dbContext.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Could not update database! Error message: {ex.Message}");
                    }
                }
                else
                {
                    try
                    {
                        if (train.NumberOfSeats <= 0)
                        {
                            MessageBox.Show("Seats number must be possitive! Nothing saved to database!");
                            var currentItemsSource = trainDataGrid.ItemsSource;
                            trainDataGrid.ItemsSource = null;
                            trainDataGrid.Items.Remove(e.Row);
                            trainDataGrid.ItemsSource = currentItemsSource;
                        }
                        else if (train.BriefDescription.Length < 1)
                        {
                            MessageBox.Show("Train must have a description! Nothing saved to database!");
                            var currentItemsSource = trainDataGrid.ItemsSource;
                            trainDataGrid.ItemsSource = null;
                            trainDataGrid.Items.Remove(e.Row);
                            trainDataGrid.ItemsSource = currentItemsSource;
                        }
                        else
                        {
                            dbContext.Trains.Add(train);
                            dbContext.SaveChanges();
                            e.Row.Item = null;
                            e.Row.Item = train;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Could not update database! Error message: {ex.Message}");
                    }
                }
            }

            beginEditTrain = null;
        }

        private void trainDataGrid_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var trainToDelete = trainDataGrid.SelectedItem as Train;

            if (e.Command == DataGrid.DeleteCommand)
            {
                if (MessageBox.Show(string.Format("Would you like to delete train {0}?",
                                                  trainToDelete.TrainID),
                                                  "Confirm Delete",
                                                  MessageBoxButton.YesNo,
                                                  MessageBoxImage.Question,
                                                  MessageBoxResult.No) != MessageBoxResult.Yes)
                {
                    e.Handled = true;
                }
                else
                {
                    try
                    {
                        dbContext.Seats.RemoveRange(trainToDelete.Seats);

                        var ticketsToDelete = from ticket in dbContext.Tickets
                                              where ticket.TrainID == trainToDelete.TrainID
                                              select ticket;

                        dbContext.Tickets.RemoveRange(ticketsToDelete);

                        var schedulesToDelete = from schedule in dbContext.Schedules
                                                where schedule.TrainID == trainToDelete.TrainID
                                                select schedule;

                        dbContext.Schedules.RemoveRange(schedulesToDelete);

                        dbContext.Trains.Remove(trainToDelete);

                        dbContext.SaveChanges();

                        if (trainToDelete.TrainID == (trainForSchedule?.TrainID ?? -1))
                        {
                            trainForSchedule = null;
                            chosenTrainLabel.Content = "Train: None";
                            trainInScheduleTextBlock.Text = "None";
                        }

                        UpdateDataGrids();
                    }
                    catch (Exception ex)
                    {
                        e.Handled = true;
                        MessageBox.Show($"Could not delete item in database! Error message: {ex.Message}");
                    }
                }
            }
        }

        private void startCityButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = cityDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var selectedCity = selectedItem as City;
                if (selectedCity != null)
                {
                    int endCityId = endCityForSchedule?.CityID ?? -1;
                    if (selectedCity.CityID != endCityId)
                    {
                        startCityForSchedule = selectedCity;
                        chosenStartCityLabel.Content = $"Start city: {selectedCity.CityID} - {selectedCity.Name}";
                        startCityInScheduleLabel.Content = $"{selectedCity.CityID} - {selectedCity.Name}";
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Start and end city cannot be the same!");
                        return;
                    }
                }
            }

            MessageBox.Show("Plese select a city from the grid!");            
        }

        private void endCityButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = cityDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var selectedCity = selectedItem as City;
                if (selectedCity != null)
                {
                    int startCityId = startCityForSchedule?.CityID ?? -1;
                    if (selectedCity.CityID != startCityId)
                    {
                        endCityForSchedule = selectedCity;
                        chosenEndCityLabel.Content = $"End city: {selectedCity.CityID} - {selectedCity.Name}";
                        endCityInScheduleLabel.Content = $"{selectedCity.CityID} - {selectedCity.Name}";
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Start and end city cannot be the same!");
                        return;
                    }
                }
            }

            MessageBox.Show("Plese select a city from the grid!");
        }

        private void trainButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = trainDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                var selectedTrain = selectedItem as Train;
                if (selectedTrain != null)
                {
                    trainForSchedule = selectedTrain;
                    chosenTrainLabel.Content = $"Train: {selectedTrain.TrainID} - {selectedTrain.BriefDescription}";
                    trainInScheduleTextBlock.Text = $"{selectedTrain.TrainID} - {selectedTrain.BriefDescription}";
                    return;
                }
            }

            MessageBox.Show("Plese select a train from the grid!");
        }

        private void scheduleDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (!e.Row.IsEditing)
            {
                var schedule = e.Row.Item as Schedule;
                if (schedule != null)
                {
                    beginEditSchedule = new Schedule();
                    beginEditSchedule.ScheduleID = schedule.ScheduleID;
                    beginEditSchedule.StartCityID = schedule.StartCityID;
                    beginEditSchedule.EndCityID = schedule.EndCityID;
                    beginEditSchedule.TrainID = schedule.TrainID;
                    beginEditSchedule.DepartureTime = schedule.DepartureTime;
                    beginEditSchedule.TravelTime = schedule.TravelTime;
                    beginEditSchedule.TicketPrice = schedule.TicketPrice;
                }
            }
        }

        private void scheduleDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var schedule = e.Row.Item as Schedule;
            if (schedule != null)
            {
                if (schedule.ScheduleID != 0)
                {
                    try
                    {
                        bool validInput = true;

                        if (dbContext.Cities.Find(schedule.StartCityID) == null)
                        {
                            MessageBox.Show("Start city Id is invalid! No such city in database!");
                            validInput = false;
                        }
                        else if (dbContext.Cities.Find(schedule.EndCityID) == null)
                        {
                            MessageBox.Show("End city Id is invalid! No such city in database!");
                            validInput = false;
                        }
                        else if (dbContext.Trains.Find(schedule.TrainID) == null)
                        {
                            MessageBox.Show("Train Id is invalid! No such train in database!");
                            validInput = false;
                        }
                        else if (schedule.DepartureTime < DateTime.Now)
                        {
                            MessageBox.Show("Cannot set past date for schedule!");
                            validInput = false;
                        }
                        else if (schedule.TravelTime > new TimeSpan(24, 0, 0))
                        {
                            MessageBox.Show("Travel time cannot be more than 24 hours!");
                            validInput = false;
                        }
                        else if (schedule.TicketPrice <= 0)
                        {
                            MessageBox.Show("Ticket price must be possitive number!");
                            validInput = false;
                        }

                        if (!validInput)
                        {
                            schedule.ScheduleID = beginEditSchedule.ScheduleID;
                            schedule.StartCityID = beginEditSchedule.StartCityID;
                            schedule.EndCityID = beginEditSchedule.EndCityID;
                            schedule.TrainID = beginEditSchedule.TrainID;
                            schedule.DepartureTime = beginEditSchedule.DepartureTime;
                            schedule.TravelTime = beginEditSchedule.TravelTime;
                            schedule.TicketPrice = beginEditSchedule.TicketPrice;
                            e.Row.Item = null;
                            e.Row.Item = schedule;
                        }
                        else
                        {
                            if (schedule.TrainID != beginEditSchedule.TrainID)
                            {
                                var oldTrain = dbContext.Trains.Find(beginEditSchedule.TrainID);
                                var newTrain = dbContext.Trains.Find(schedule.TrainID);

                                var seatsToRemove = from s in dbContext.Seats
                                                    where s.TrainID == oldTrain.TrainID
                                                          && s.ScheduleID == schedule.ScheduleID
                                                    select s;

                                var ticketsToRemove = from t in dbContext.Tickets
                                                      where t.TrainID == oldTrain.TrainID
                                                            && t.ScheduleID == schedule.ScheduleID
                                                      select t;

                                dbContext.Tickets.RemoveRange(ticketsToRemove);
                                dbContext.Seats.RemoveRange(seatsToRemove);

                                for (int i = 1; i <= newTrain.NumberOfSeats; i++)
                                {
                                    dbContext.Seats.Add(new Seat()
                                    {
                                        SeatNumber = i,
                                        TrainID = schedule.TrainID,
                                        ScheduleID = schedule.ScheduleID,
                                        Taken = false,
                                        Class = SeatClass.FirstClass
                                    });
                                }
                            }
                            else //if train is the same
                            {
                                var tickets = from t in dbContext.Tickets
                                              where t.TrainID == schedule.TrainID
                                                    && t.ScheduleID == schedule.ScheduleID
                                              select t;

                                foreach (var ticket in tickets)
                                {
                                    ticket.TripDateAndTime = schedule.DepartureTime;
                                }
                            }

                            dbContext.SaveChanges();

                            UpdateDataGrids();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Could not update database! Error message: {ex.Message}");
                    }
                }
            }
        }

        private void scheduleDataGrid_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var scheduleToDelete = scheduleDataGrid.SelectedItem as Schedule;

            if (e.Command == DataGrid.DeleteCommand)
            {
                if (MessageBox.Show(string.Format("Would you like to delete schedule {0}?",
                                                  scheduleToDelete.ScheduleID),
                                                  "Confirm Delete",
                                                  MessageBoxButton.YesNo,
                                                  MessageBoxImage.Question,
                                                  MessageBoxResult.No) != MessageBoxResult.Yes)
                {
                    e.Handled = true;
                }
                else
                {
                    try
                    {
                        var ticketsToDelete = from ticket in dbContext.Tickets
                                              where ticket.ScheduleID == scheduleToDelete.ScheduleID
                                              select ticket;

                        dbContext.Tickets.RemoveRange(ticketsToDelete);

                        var seatsToDelete = from seat in scheduleToDelete.Train.Seats
                                            where seat.ScheduleID == scheduleToDelete.ScheduleID
                                            select seat;

                        dbContext.Seats.RemoveRange(seatsToDelete);

                        dbContext.Schedules.Remove(scheduleToDelete);

                        dbContext.SaveChanges();

                        UpdateDataGrids();
                    }
                    catch (Exception ex)
                    {
                        e.Handled = true;
                        MessageBox.Show($"Could not delete item in database! Error message: {ex.Message}");
                    }
                }
            }
        }

        private void addScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            bool chosenAllItems = true;

            if (startCityForSchedule == null)
            {
                MessageBox.Show("Choose start city first!");
                chosenAllItems = false;
            }
            else if (endCityForSchedule == null)
            {
                MessageBox.Show("Choose end city first!");
                chosenAllItems = false;
            }
            else if (trainForSchedule == null)
            {
                MessageBox.Show("Choose train first!");
                chosenAllItems = false;
            }

            if (chosenAllItems)
            {
                var scheduleWindow = new ScheduleAddWindow(dbContext,
                                                           startCityForSchedule,
                                                           endCityForSchedule,
                                                           trainForSchedule);

                scheduleWindow.ShowDialog();
                UpdateDataGrids();
            }
        }
    }
}
