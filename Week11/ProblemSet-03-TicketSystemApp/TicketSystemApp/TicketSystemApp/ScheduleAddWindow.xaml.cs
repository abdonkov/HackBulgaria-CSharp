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
    /// Interaction logic for ScheduleAddWindow.xaml
    /// </summary>
    public partial class ScheduleAddWindow : Window
    {
        TicketDB dbContext;
        City startCity;
        City endCity;
        Train train;

        public ScheduleAddWindow(TicketDB dbContext, City startCity, City endCity, Train train)
        {
            InitializeComponent();

            this.dbContext = dbContext;
            this.startCity = startCity;
            this.endCity = endCity;
            this.train = train;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startCityLabel.Content = $"{startCity.CityID} - {startCity.Name}";
            endCityLabel.Content = $"{endCity.CityID} - {endCity.Name}";
            trainTextBlock.Text = $"{train.TrainID} - {train.BriefDescription}";
            departureTimeDatePicker.SelectedDate = DateTime.Now;
            travelTimeTextBox.Text = "00:00";
            ticketPriceTextBox.Text = "0";
        }

        private void addScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan travelTime;

            if (departureTimeDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Invalid departure date format!");
                return;
            }

            if (departureTimeDatePicker.SelectedDate <= DateTime.Now)
            {
                MessageBox.Show("Cannot set past date for schedule!");
                return;
            }

            var travelTimeArgs = travelTimeTextBox.Text.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (travelTimeArgs.Length != 2)
            {
                MessageBox.Show("Invalid travel time format! Please use HH:mm format!");
                return;
            }
            else
            {
                int hours, minutes;
                if (!int.TryParse(travelTimeArgs[0], out hours))
                {
                    MessageBox.Show("Invalid hours number format!");
                    return;
                }
                if (!int.TryParse(travelTimeArgs[1], out minutes))
                {
                    MessageBox.Show("Invalid minutes number format!");
                    return;
                }

                if ((hours < 0 || hours > 24) || (hours == 24 && minutes != 0))
                {
                    MessageBox.Show("Invalid travel time! Travel time cannot be more than 24 hours!");
                    return;
                }

                travelTime = new TimeSpan(hours, minutes, 0);
            }

            decimal ticketPrice;
            if (!decimal.TryParse(ticketPriceTextBox.Text, out ticketPrice))
            {
                MessageBox.Show("Invalid ticket price number format!");
                return;
            }

            var otherSchedulesForTrain = from s in dbContext.Schedules
                                         where s.TrainID == train.TrainID
                                         select s;

            DateTime beginOfSchedule = (DateTime)departureTimeDatePicker.SelectedDate;
            DateTime endOfSchedule = beginOfSchedule + travelTime;

            foreach (var curSchedule in otherSchedulesForTrain)
            {
                if (!((curSchedule.DepartureTime + curSchedule.TravelTime < beginOfSchedule)
                     || (curSchedule.DepartureTime > endOfSchedule)))
                {
                    MessageBox.Show("Train is not available in current time span!");
                    return;
                }
            }

            try
            {
                Schedule schedule = new Schedule()
                {
                    StartCityID = startCity.CityID,
                    EndCityID = endCity.CityID,
                    TrainID = train.TrainID,
                    DepartureTime = (DateTime)departureTimeDatePicker.SelectedDate,
                    TravelTime = travelTime,
                    TicketPrice = ticketPrice
                };

                dbContext.Schedules.Add(schedule);

                for (int i = 1; i <= schedule.Train.NumberOfSeats; i++)
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

                dbContext.SaveChanges();

                MessageBox.Show("Schedule added successfully!");
                Close();
            }
            catch (OptimisticConcurrencyException ex)
            {
                Console.WriteLine("Cannot add schedule in database! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
