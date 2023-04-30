using mvvm.Model;
using mvvm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace mvvm.ViewModel
{
    internal class FlightViewModel
    {
        DispatcherTimer timer = new DispatcherTimer();
        public ObservableCollection<Flight> Flights { get; set; } = new ObservableCollection<Flight>();

        FlightViewModel? plan;

        public void Start(FlightViewModel plan)
        {
            timer.Tick += Tick;
            this.plan = plan;
            timer.Start();
        }
        Random rand = new Random();
        void Tick(object? sender, EventArgs e)
        {
            CheckFlights();
            if(rand.Next(0,1000000) > 999900)
            {
                Flights.Add(Flight.GenerateFlight());
            }
        }

        void CheckFlights()
        {
            for (int i = Flights.Count - 1; i >= 0; i--)
            {
                if (Flights[i].TakeOfTime < DateTime.Now)
                {
                    Flights[i].State = PlaneState.ReadyForTakeOff;
                    plan.Flights.Add(Flights[i]);
                    Flights.RemoveAt(i);
                }
            }
        }

        public void LoadFlights()
        {
            ObservableCollection<Flight> flights = new ObservableCollection<Flight>();
            if(flights.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Flight flight = Flight.GenerateFlight();
                    flights.Add(flight);
                }
                Flights = flights;
            }
        }
    }
}
