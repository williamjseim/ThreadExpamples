using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvm.Model
{
    internal class Runway : INotifyPropertyChanged
    {
		private Flight _planeOnRunway = new Flight();
		public Flight PlaneOnRunway
		{
			get { return _planeOnRunway; }
			set 
            {
                if(_planeOnRunway != value)
                {
				    _planeOnRunway = value; 
                    RaisePropertyChanged("PlaneOnRunway");
                }
			}
		}

		private Queue<Flight> _flightsWaitingForRunway = new Queue<Flight>();
        public Queue<Flight> FlightsWaitingForRunway
        {
			get { return _flightsWaitingForRunway; }
			set 
            {
                if(_flightsWaitingForRunway != value)
                {
                    _flightsWaitingForRunway = value;
                    RaisePropertyChanged("FlightsWaitingForRunway");
                }
            }
		}

        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
