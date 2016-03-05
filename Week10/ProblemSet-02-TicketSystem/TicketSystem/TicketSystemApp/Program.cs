using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TicketSystem;

namespace TicketSystemApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintCommands();

            string input = string.Empty;
            while (input != "exit")
            {
                Console.Write("Command: ");
                input = Console.ReadLine().Trim();
                if (input == string.Empty) continue;
                string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                switch (words[0].ToLower())
                {
                    #region Add Commands
                    case "add":
                        {
                            if (words.Length != 2 && words.Length != 3) goto default;
                            switch (words[1].ToLower())
                            {
                                case "city":
                                    if (words.Length != 2) goto default;
                                    CreateCity();
                                    break;
                                case "train":
                                    if (words.Length != 2) goto default;
                                    CreateTrain();
                                    break;
                                case "trip":
                                    if (words.Length != 2) goto default;
                                    CreateTrip();
                                    break;
                                case "discount":
                                    {
                                        if (words.Length != 3) goto default;
                                        if (words[2].ToLower() != "card") goto default;
                                        CreateDiscountCard();
                                        break;
                                    }
                                default:
                                    Console.WriteLine("Invalid command! Type help for list of available commands!");
                                    break;
                            }
                            break;
                        }
                    #endregion
                    #region Edit Commands
                    case "edit":
                        {
                            if (words.Length != 3) goto default;
                            switch (words[1].ToLower())
                            {
                                case "train":
                                    EditTrain(words[2]);
                                    break;
                                case "trip":
                                    EditTrip(words[2]);
                                    break;
                                default:
                                    Console.WriteLine("Invalid command! Type help for list of available commands!");
                                    break;
                            }
                            break;
                        }
                    #endregion
                    #region Delete Commands
                    case "delete":
                        {
                            if (words.Length != 3) goto default;
                            switch (words[1].ToLower())
                            {
                                case "city":
                                    DeleteCity(words[2]);
                                    break;
                                case "train":
                                    DeleteTrain(words[2]);
                                    break;
                                case "trip":
                                    DeleteTrip(words[2]);
                                    break;
                                default:
                                    Console.WriteLine("Invalid command! Type help for list of available commands!");
                                    break;
                            }
                            break;
                        }
                    #endregion
                    case "register":
                        {
                            if (words.Length != 2) goto default;
                            if (words[1] != "user") Console.WriteLine("Invalid command! Type help for list of available commands!");
                            else RegisterUser();
                            break;
                        }
                    case "buy":
                        {
                            if (words.Length != 3) goto default;
                            switch (words[1].ToLower())
                            {
                                case "ticket":
                                    {
                                        BuyTicket(words[2]);
                                        break;
                                    }
                                default:
                                    Console.WriteLine("Invalid command! Type help for list of available commands!");
                                    break;
                            }
                            break;
                        }
                    case "list":
                        {
                            if (words.Length != 2) goto default;
                            switch (words[1].ToLower())
                            {
                                case "cities":
                                    PrintCities();
                                    break;
                                case "trains":
                                    PrintTrains();
                                    break;
                                case "trips":
                                    PrintTrips();
                                    break;
                                default:
                                    Console.WriteLine("Invalid command! Type help for list of available commands!");
                                    break;
                            }
                            break;
                        }
                    case "help":
                        PrintCommands();
                        break;
                    case "exit":
                        break;
                    default:
                        Console.WriteLine("Invalid command! Type help for list of available commands!");
                        break;
                }
            }
        }

        private static void PrintCommands()
        {
            Console.WriteLine("List of commands:");
            Console.WriteLine("add city - begin operation for adding a city");
            Console.WriteLine("add train - begin operation for adding a train");
            Console.WriteLine("add trip - begin operation for adding a trip");
            Console.WriteLine("add discount card - begin operation for adding a discount card");
            Console.WriteLine("edit train <trainID> - edit train information");
            Console.WriteLine("edit trip <tripID> - edit trip information");
            Console.WriteLine("delete city <cityID> - deletes city and trips containing it");
            Console.WriteLine("delete train <trainID> - deletes train and trips/tickets containing it");
            Console.WriteLine("delete trip <tripID> - deletes a trip");
            Console.WriteLine("register user - begin operation for registring a user");
            Console.WriteLine("buy ticket <tripID> - buys ticket for a trip. Requires user authentication");
            Console.WriteLine("list cities - lists cities with Id and Name");
            Console.WriteLine("list trains - lists trains with Id and Description");
            Console.WriteLine("list trips - lists trips with Id, Start City, End City");
            Console.WriteLine("help - returns list of commands");
            Console.WriteLine("exit - exits the program");
            Console.WriteLine();
        }

        private static void CreateCity()
        {
            using (var db = new TicketDB())
            {
                Console.WriteLine("Creating city!");
                Console.Write("Name: ");
                var name = Console.ReadLine().Trim();

                try
                {
                    City city = new City
                    {
                        Name = name
                    };

                    db.Cities.Add(city);
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot insert city in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("City created successfully!");
            }
        }

        private static void CreateTrain()
        {
            using (var db = new TicketDB())
            {
                Console.WriteLine("Creating train!");
                Console.Write("Brief Description: ");
                var briefDescription = Console.ReadLine().Trim();
                Console.Write("Number Of Seats: ");
                var numberOfSeatsArg = Console.ReadLine().Trim();

                int numberOfSeats;
                try
                {
                    numberOfSeats = int.Parse(numberOfSeatsArg);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid number format! Operation aborted!");
                    return;
                }

                try
                {
                    Train train = new Train
                    {
                        BriefDescription = briefDescription,
                        NumberOfSeats = numberOfSeats
                    };

                    db.Trains.Add(train);
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot insert train in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Train created successfully!");
            }
        }

        private static void CreateTrip()
        {
            using (var db = new TicketDB())
            {
                Console.WriteLine("Creating trip!");

                Console.WriteLine("List of cities:");
                HashSet<int> cityIds = new HashSet<int>();
                foreach (var city in db.Cities)
                {
                    cityIds.Add(city.CityID);
                    Console.WriteLine($"{city.CityID} - {city.Name}");
                }

                Console.Write("Start City(Write Id): ");
                var startCityArg = Console.ReadLine().Trim();
                Console.Write("End City(Write Id): ");
                var endCityArg = Console.ReadLine().Trim();

                int startCityId, endCityId;
                try
                {
                    startCityId = int.Parse(startCityArg);
                    endCityId = int.Parse(endCityArg);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid ids format! Operation aborted!");
                    return;
                }

                if (!cityIds.Contains(startCityId) || !cityIds.Contains(endCityId))
                {
                    Console.WriteLine("Ids provided does not exist! Operation aborted!");
                    return;
                }

                Console.Write("Departure time (yyyy-MM-dd HH:mm fromat): ");
                var departerTimeArg = Console.ReadLine().Trim();

                DateTime departureTime;
                if (!DateTime.TryParseExact(departerTimeArg, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out departureTime))
                {
                    Console.WriteLine("Invalid date and time format! Operation aborted!");
                    return;
                }

                Console.Write("Travel Time(Format hh:mm): ");
                var travelTimeArg = Console.ReadLine().Trim().Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (travelTimeArg.Length != 2)
                {
                    Console.WriteLine("Invalid time format! Operation aborted!");
                    return;
                }

                TimeSpan travelTime;
                try
                {
                    travelTime = new TimeSpan(int.Parse(travelTimeArg[0]), int.Parse(travelTimeArg[1]), 0);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid id format! Operation aborted!");
                    return;
                }

                Console.WriteLine("List of trains:");
                HashSet<int> trainIds = new HashSet<int>();
                foreach (var train in db.Trains)
                {
                    trainIds.Add(train.TrainID);
                    Console.WriteLine($"{train.TrainID} - {train.BriefDescription}");
                }

                Console.Write("Train(Write Id): ");
                var trainArg = Console.ReadLine().Trim();

                int trainId;
                try
                {
                    trainId = int.Parse(trainArg);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid id format! Operation aborted!");
                    return;
                }

                if (!trainIds.Contains(trainId))
                {
                    Console.WriteLine("Id provided does not exist! Operation aborted!");
                    return;
                }

                Console.Write("Ticket Price: ");
                var ticketPriceArg = Console.ReadLine().Trim();

                decimal ticketPrice;
                try
                {
                    ticketPrice = decimal.Parse(ticketPriceArg);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid price format! Operation aborted!");
                    return;
                }

                try
                {
                    Schedule schedule = new Schedule
                    {
                        StartCityID = startCityId,
                        EndCityID = endCityId,
                        DepartureTime = departureTime,
                        TravelTime = travelTime,
                        TrainID = trainId,
                        TicketPrice = ticketPrice
                    };

                    db.Schedules.Add(schedule);

                    db.SaveChanges();

                    var numberOfSeatsInTrain = (from train in db.Trains
                                                where train.TrainID == trainId
                                                select train.NumberOfSeats).Single();

                    for (int i = 0; i < numberOfSeatsInTrain; i++)
                    {
                        Seat seat = new Seat
                        {
                            SeatNumber = i + 1,
                            TrainID = trainId,
                            ScheduleID = schedule.ScheduleID,
                            Taken = false,
                            Class = SeatClass.SecondClass
                        };

                        db.Seats.Add(seat);
                    }

                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot insert trip in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Trip created successfully!");
            }
        }

        private static void CreateDiscountCard()
        {
            using (var db = new TicketDB())
            {
                Console.WriteLine("Creating discount card!");
                HashSet<string> cardIds = new HashSet<string>();
                foreach (var card in db.DiscountCards)
                {
                    cardIds.Add(card.DiscountCardID);
                    Console.WriteLine($"{card.DiscountCardID}");
                }

                Console.Write("Discount card ID: ");
                var discountID = Console.ReadLine().Trim();

                if (cardIds.Contains(discountID))
                {
                    Console.WriteLine("Discount Id is not unique! Operation aborted!");
                    return;
                }

                Console.WriteLine("Discount Card Types: ");
                HashSet<int> typeNums = new HashSet<int>();
                foreach (DiscountCardType item in Enum.GetValues(typeof(DiscountCardType)))
                {
                    typeNums.Add((int)item);
                    Console.WriteLine($"{(int)item} - {item.ToString()}");
                }

                Console.Write("Choose type: ");
                var typeArg = Console.ReadLine().Trim();

                int typeNum;
                if (!int.TryParse(typeArg, out typeNum))
                {
                    Console.WriteLine("Invalid number format! Operation aborted!");
                    return;
                }

                DiscountCardType type;
                if (!Enum.IsDefined(typeof(DiscountCardType), typeNum))
                {
                    Console.WriteLine("Invalid number format! Operation aborted!");
                    return;
                }
                type = (DiscountCardType)typeNum;

                Console.Write("Discount (from 0.0 to 100.0): ");
                var discountArg = Console.ReadLine().Trim();

                decimal discount;
                if (!decimal.TryParse(discountArg, out discount))
                {
                    Console.WriteLine("Invalid number format! Operation aborted!");
                    return;
                }

                try
                {
                    DiscountCard card = new DiscountCard
                    {
                        DiscountCardID = discountID,
                        Type = type,
                        Discount = discount
                    };

                    db.DiscountCards.Add(card);

                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot insert discount card in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Discount card created successfully!");
            }
        }

        private static void EditTrain(string trainArg)
        {
            using (var db = new TicketDB())
            {
                int trainId;
                if (!int.TryParse(trainArg, out trainId))
                {
                    Console.WriteLine("Invalid train ID! Operation aborted!");
                    return;
                }

                var trainToEdit = (from train in db.Trains
                                   where train.TrainID == trainId
                                   select train).SingleOrDefault();

                if (trainToEdit == null)
                {
                    Console.WriteLine("No such train! Operation aborted!");
                    return;
                }

                Console.WriteLine("Train Found!");
                Console.WriteLine("Brief Descriptopn: ");
                Console.WriteLine(trainToEdit.BriefDescription);
                Console.WriteLine();
                Console.WriteLine("Write new description: ");
                var desc = Console.ReadLine().Trim();

                try
                {
                    trainToEdit.BriefDescription = desc;
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot edit train in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Train edited successfully!");
            }
        }

        private static void EditTrip(string tripArg)
        {
            using (var db = new TicketDB())
            {
                int scheduleId;
                if (!int.TryParse(tripArg, out scheduleId))
                {
                    Console.WriteLine("Invalid train ID! Operation aborted!");
                    return;
                }

                var scheduleToEdit = (from schedule in db.Schedules
                                      where schedule.TrainID == scheduleId
                                      select schedule).SingleOrDefault();

                if (scheduleToEdit == null)
                {
                    Console.WriteLine("No such trip! Operation aborted!");
                    return;
                }

                Console.WriteLine("Trip Found!");
                Console.WriteLine($"StartCityID: {scheduleToEdit.StartCityID}");
                Console.WriteLine($"EndCityID: {scheduleToEdit.EndCityID}");
                Console.WriteLine($"TravelTime: {scheduleToEdit.TravelTime.ToString("hh\\:mm")}");
                Console.WriteLine($"TrainID: {scheduleToEdit.TrainID}");
                Console.WriteLine($"TicketPrice: {scheduleToEdit.TicketPrice}");

                HashSet<int> cityIds = new HashSet<int>();
                foreach (var city in db.Cities)
                {
                    cityIds.Add(city.CityID);
                    Console.WriteLine($"{city.CityID} - {city.Name}");
                }

                Console.WriteLine("Begin of edit operation! If no change is needed leave blank!");

                Console.Write("Start City(Write Id): ");
                var startCityArg = Console.ReadLine().Trim();
                Console.Write("End City(Write Id): ");
                var endCityArg = Console.ReadLine().Trim();

                int startCityId, endCityId;
                try
                {
                    if (startCityArg != string.Empty) startCityId = int.Parse(startCityArg);
                    else startCityId = scheduleToEdit.StartCityID;
                    if (endCityArg != string.Empty) endCityId = int.Parse(endCityArg);
                    else endCityId = scheduleToEdit.EndCityID;

                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid ids format! Operation aborted!");
                    return;
                }

                if (!cityIds.Contains(startCityId)
                    || !cityIds.Contains(endCityId))
                {
                    Console.WriteLine("Ids provided does not exist! Operation aborted!");
                    return;
                }

                TimeSpan travelTime;
                Console.Write("Travel Time(Format hh:mm): ");
                var travelTimeInput = Console.ReadLine().Trim();

                if (travelTimeInput != string.Empty)
                {
                    var travelTimeArg = travelTimeInput.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (travelTimeArg.Length != 2)
                    {
                        Console.WriteLine("Invalid time format! Operation aborted!");
                        return;
                    }

                    try
                    {
                        travelTime = new TimeSpan(int.Parse(travelTimeArg[0]), int.Parse(travelTimeArg[1]), 0);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid id format! Operation aborted!");
                        return;
                    }
                }
                else travelTime = scheduleToEdit.TravelTime;

                Console.WriteLine("List of trains:");
                HashSet<int> trainIds = new HashSet<int>();
                foreach (var train in db.Trains)
                {
                    trainIds.Add(train.TrainID);
                    Console.WriteLine($"{train.TrainID} - {train.BriefDescription}");
                }

                Console.Write("Train(Write Id): ");
                var trainArg = Console.ReadLine().Trim();

                int trainId;
                try
                {
                    if (trainArg != string.Empty) trainId = int.Parse(trainArg);
                    else trainId = scheduleToEdit.TrainID;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid id format! Operation aborted!");
                    return;
                }

                if (!trainIds.Contains(trainId))
                {
                    Console.WriteLine("Id provided does not exist! Operation aborted!");
                    return;
                }

                Console.Write("Ticket Price: ");
                var ticketPriceArg = Console.ReadLine().Trim();

                decimal ticketPrice;
                try
                {
                    if (ticketPriceArg != string.Empty) ticketPrice = decimal.Parse(ticketPriceArg);
                    else ticketPrice = scheduleToEdit.TicketPrice;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid price format! Operation aborted!");
                    return;
                }

                try
                {
                    scheduleToEdit.StartCityID = startCityId;
                    scheduleToEdit.EndCityID = endCityId;
                    scheduleToEdit.TravelTime = travelTime;
                    scheduleToEdit.TrainID = trainId;
                    scheduleToEdit.TicketPrice = ticketPrice;

                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot edit trip in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Trip edited successfully!");
            }
        }

        private static void DeleteCity(string cityArg)
        {
            using (var db = new TicketDB())
            {
                int cityId;
                if (!int.TryParse(cityArg, out cityId))
                {
                    Console.WriteLine("Invalid city ID! Operation aborted!");
                    return;
                }

                var cityToDelete = (from city in db.Cities
                                    where city.CityID == cityId
                                    select city).SingleOrDefault();

                if (cityToDelete == null)
                {
                    Console.WriteLine("No such city! Operation aborted!");
                    return;
                }

                try
                {
                    var schedulesToDelete = from schedule in db.Schedules
                                            where schedule.StartCityID == cityId
                                            || schedule.EndCityID == cityId
                                            select schedule;

                    foreach (var schedule in schedulesToDelete)
                    {

                        var ticketsToDelete = from ticket in db.Tickets
                                              where ticket.ScheduleID == schedule.ScheduleID
                                              select ticket;

                        db.Tickets.RemoveRange(ticketsToDelete);

                        var seatsToDelete = from seat in schedule.Train.Seats
                                            where seat.ScheduleID == schedule.ScheduleID
                                            select seat;

                        db.Seats.RemoveRange(seatsToDelete);

                        db.Schedules.Remove(schedule);
                    }

                    db.Cities.Remove(cityToDelete);

                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot delete city in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("City deleted successfully!");
            }
        }

        private static void DeleteTrain(string trainArg)
        {
            using (var db = new TicketDB())
            {
                int trainId;
                if (!int.TryParse(trainArg, out trainId))
                {
                    Console.WriteLine("Invalid train ID! Operation aborted!");
                    return;
                }

                var trainToDelete = (from train in db.Trains
                                     where train.TrainID == trainId
                                     select train).SingleOrDefault();

                if (trainToDelete == null)
                {
                    Console.WriteLine("No such train! Operation aborted!");
                    return;
                }

                try
                {
                    db.Seats.RemoveRange(trainToDelete.Seats);

                    var ticketsToDelete = from ticket in db.Tickets
                                          where ticket.TrainID == trainId
                                          select ticket;

                    db.Tickets.RemoveRange(ticketsToDelete);

                    var schedulesToDelete = from schedule in db.Schedules
                                            where schedule.TrainID == trainId
                                            select schedule;

                    db.Schedules.RemoveRange(schedulesToDelete);

                    db.Trains.Remove(trainToDelete);

                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot delete train in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Train deleted successfully!");
            }
        }

        private static void DeleteTrip(string tripArg)
        {
            using (var db = new TicketDB())
            {
                int scheduleId;
                if (!int.TryParse(tripArg, out scheduleId))
                {
                    Console.WriteLine("Invalid trip ID! Operation aborted!");
                    return;
                }

                var scheduleToDelete = (from schedule in db.Schedules
                                        where schedule.ScheduleID == scheduleId
                                        select schedule).SingleOrDefault();


                if (scheduleToDelete == null)
                {
                    Console.WriteLine("No such trip! Operation aborted!");
                    return;
                }

                try
                {
                    var ticketsToDelete = from ticket in db.Tickets
                                          where ticket.ScheduleID == scheduleToDelete.ScheduleID
                                          select ticket;

                    db.Tickets.RemoveRange(ticketsToDelete);

                    var seatsToDelete = from seat in scheduleToDelete.Train.Seats
                                        where seat.ScheduleID == scheduleToDelete.ScheduleID
                                        select seat;

                    db.Seats.RemoveRange(seatsToDelete);

                    db.Schedules.Remove(scheduleToDelete);

                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot delete trip in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Trip deleted successfully!");
            }
        }

        private static void RegisterUser()
        {
            using (var db = new TicketDB())
            {
                Console.WriteLine("Registering user!");
                Console.Write("Username: ");
                var username = Console.ReadLine();

                var usernamesQuery = from user in db.Users
                                     where user.UserName == username
                                     select user.UserName;

                if (usernamesQuery.Count() != 0)
                {
                    Console.WriteLine("Username already exists! Operation aborted!");
                    return;
                }

                Console.Write("Password: ");
                var password = ReadPassword();
                Console.Write("Email: ");
                var email = Console.ReadLine().Trim();

                if (!Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("Email is in invalid format! Operation aborted!");
                    return;
                }

                Console.Write("Credit Card Number: ");
                var creditCardNumber = Console.ReadLine().Trim();
                Console.Write("First Name: ");
                var firstName = Console.ReadLine().Trim();
                Console.Write("Last Name: ");
                var lastName = Console.ReadLine().Trim();
                Console.Write("Address: ");
                var address = Console.ReadLine().Trim();
                Console.Write("Zip Code: ");
                var zipCode = Console.ReadLine().Trim();
                Console.Write("Do you have discount card? (Y/N): ");
                var answer = Console.ReadLine().Trim().ToUpper();

                bool hasCard;
                string discountId = null;
                if (answer == "Y")
                {
                    Console.WriteLine("Provide discount card identification number: ");
                    discountId = Console.ReadLine();
                    var discountCard = (from card in db.DiscountCards
                                        where card.DiscountCardID == discountId
                                        select card).SingleOrDefault();

                    if (discountCard == null)
                    {
                        Console.WriteLine("No such discount card! Operaion aborted!");
                        return;
                    }
                    else hasCard = true;
                }
                else if (answer == "N")
                    hasCard = false;
                else
                {
                    Console.WriteLine("Invalid answer! Operaion aborted!");
                    return;
                }

                try
                {
                    var user = new User(username, password);
                    user.Email = email;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.CreditCardNumber = creditCardNumber;
                    user.Address = address;
                    user.ZipCode = zipCode;
                    user.IsAdmin = true;
                    if (hasCard) user.DiscountCardID = discountId;

                    db.Users.Add(user);

                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot delete trip in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("User registration was successful!");
            }
        }

        private static void BuyTicket(string tripArg)
        {
            using (var db = new TicketDB())
            {
                int scheduleId;
                if (!int.TryParse(tripArg, out scheduleId))
                {
                    Console.WriteLine("Invalid trip ID! Operation aborted!");
                    return;
                }

                var scheduleOfTicket = (from schedule in db.Schedules
                                        where schedule.ScheduleID == scheduleId
                                        select schedule).SingleOrDefault();

                if (scheduleOfTicket == null)
                {
                    Console.WriteLine("No such trip! Operation aborted!");
                    return;
                }

                var trainOfSchedule = scheduleOfTicket.Train;

                List<int> availableSeats = new List<int>(trainOfSchedule.NumberOfSeats);
                foreach (var seat in trainOfSchedule.Seats)
                {
                    if (!seat.Taken) availableSeats.Add(seat.SeatNumber);
                }

                if (availableSeats.Count == 0)
                {
                    Console.WriteLine("No more seats! Operation aborted!");
                    return;
                }

                Console.WriteLine($"Available seats: {string.Join(", ", availableSeats)}");

                Console.WriteLine("Seat to buy: ");
                var seatArg = Console.ReadLine().Trim();

                int seatNumber;
                if (!int.TryParse(seatArg, out seatNumber))
                {
                    Console.WriteLine("Invalid number format! Operation aborted!");
                    return;
                }

                if (!availableSeats.Contains(seatNumber))
                {
                    Console.WriteLine("Seat number provided does not exist! Operation aborted!");
                    return;
                }

                Console.WriteLine("Begining user authentication proccess!");
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                var password = ReadPassword();

                User userBuyingTicket;
                if (!TicketSystemSecurity.DoesUserExist(db, username, password, out userBuyingTicket))
                {
                    Console.WriteLine("No such user! Operation aborted!");
                    return;
                }

                decimal discount = userBuyingTicket.DiscountCard?.Discount ?? 0;

                decimal originalPrice = scheduleOfTicket.TicketPrice;

                decimal priceSold = originalPrice - ((originalPrice * discount) / 100);
                if (priceSold < 0) priceSold = 0;

                try
                {
                    Ticket ticket = new Ticket
                    {
                        SeatNumber = seatNumber,
                        TrainID = trainOfSchedule.TrainID,
                        ScheduleID = scheduleId,
                        TripDateAndTime = scheduleOfTicket.DepartureTime,
                        OriginalPrice = originalPrice,
                        PriceSold = priceSold,
                        UserID = userBuyingTicket.UserID
                    };

                    db.Tickets.Add(ticket);

                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Cannot add ticket in database! Operation exited with message:");
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Ticket bought successfully!");
            }
        }

        public static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password.Append(info.KeyChar);
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (password.Length != 0)
                    {
                        password.Length--;
                        int cursorPos = Console.CursorLeft;
                        Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password.ToString();
        }

        public static void PrintCities()
        {
            using (var db = new TicketDB())
            {
                Console.WriteLine("Id - Name");
                Console.WriteLine("---------");
                foreach (var city in db.Cities)
                {
                    Console.WriteLine($"{city.CityID} - {city.Name}");
                }
                Console.WriteLine();
            }
        }

        public static void PrintTrains()
        {
            using (var db = new TicketDB())
            {
                Console.WriteLine("Id - Description");
                Console.WriteLine("----------------");
                foreach (var train in db.Trains)
                {
                    Console.WriteLine($"{train.TrainID} - {train.BriefDescription}");
                }
                Console.WriteLine();
            }
        }

        public static void PrintTrips()
        {
            using (var db = new TicketDB())
            {
                Console.WriteLine("Id - Start City - End City - Departure Time");
                Console.WriteLine("-------------------------------------------");
                foreach (var schedule in db.Schedules)
                {
                    Console.WriteLine($"{schedule.ScheduleID} - {schedule.StartCity.Name} - {schedule.EndCity.Name} - {schedule.DepartureTime}");
                }
                Console.WriteLine();
            }
        }
    }
}
