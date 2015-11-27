using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentsIntersection
{
    class Program
    {
        static void FindIntersectingAppointments(DateTime[] startDates, TimeSpan[] durations)
        {
            for (int i = 0; i < startDates.Length; i++)
            {
                for (int j = i + 1; j < startDates.Length; j++)
                {
                    var app1Start = startDates[i];
                    var app1End = startDates[i] + durations[i];
                    var app2Start = startDates[j];
                    var app2End = startDates[j] + durations[j];

                    if (!(app1End < app2Start) && !(app2End < app1Start))
                    {
                        var intersectStart = app1Start > app2Start ? app1Start : app2Start;
                        var intersectEnd = app1End < app2End ? app1End : app2End;
                        Console.WriteLine("The appointment starting at {0:dd/MM/yyyy hh:mm} intersects the appointment starting at {1:dd/MM/yyyy hh:mm} with exactly {2:F2} minutes.", startDates[i], startDates[j], (intersectEnd - intersectStart).TotalMinutes);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            DateTime[] appointments = new DateTime[]
            {
                new DateTime(2015,11,25,13,00,00),
                new DateTime(2015,11,25,14,20,00),
                new DateTime(2015,11,25,15,30,00),
                new DateTime(2015,11,25,16,00,00),
                new DateTime(2015,11,25,17,50,00),
                new DateTime(2015,11,25,18,10,00)
            };
            TimeSpan[] durations = new TimeSpan[]
            {
                new TimeSpan(1 , 30 , 00),
                new TimeSpan(0 , 40 , 00),
                new TimeSpan(0 , 50 , 00),
                new TimeSpan(1 , 30 , 00),
                new TimeSpan(0 , 40 , 00),
                new TimeSpan(2 , 00 , 00)
            };
            FindIntersectingAppointments(appointments, durations);
            Console.ReadKey();
        }
    }
}
