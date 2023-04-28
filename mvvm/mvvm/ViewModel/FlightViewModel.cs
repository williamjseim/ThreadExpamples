using mvvm.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvm.ViewModel
{
    internal class FlightViewModel
    {
        public ObservableCollection<Flight> Flights { get; set; } = new ObservableCollection<Flight>();

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
