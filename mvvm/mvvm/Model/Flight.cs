using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvm.Model
{
    internal class Flight : INotifyPropertyChanged
    {
        private string _companyName = string.Empty;
        public string CompanyName
        {
            get { return _companyName; }
            set 
            {
                if( _companyName != value )
                {
                    _companyName = value;
                    RaisePropertyChanged("CompanyName");
                }
            }
        }
        private Destination destination;
        public Destination Destination
        {
            get { return destination; }
            set
            {
                if (destination != value)
                {
                    destination = value;
                    RaisePropertyChanged("Destination");
                }
            }
        }

        private DateTime _takeOfTime;

        public DateTime TakeOfTime
        {
            get { return _takeOfTime; }
            set 
            {
                _takeOfTime = value;
                RaisePropertyChanged("TakeOfTime");
            }
        }

        private PlaneState _state;

        public PlaneState State
        {
            get { return _state; }
            set 
            {
                if ( _state != value )
                {
                    _state = value;
                    RaisePropertyChanged("State");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if( PropertyChanged != null )
            {
                PropertyChanged(this, new PropertyChangedEventArgs( propertyName ) );
            }
        }

        static Random rand = new Random();
        public static Flight GenerateFlight()
        {
            Flight t = new Flight
            {
                _companyName = "CompanyName",
                destination = (Destination)rand.Next(0,Enum.GetNames(typeof(Destination)).Length),
                _state = (PlaneState)rand.Next(0,3),
            };
            if(t._state == PlaneState.Refueling)
            {
                t._takeOfTime = DateTime.Now.AddSeconds(rand.Next(60, 180));
            }
            else if(t._state == PlaneState.Unloading)
            {
                t._takeOfTime = DateTime.Now.AddSeconds(rand.Next(60, 120));
            }
            else if (t._state == PlaneState.Repairing)
            {
                t._takeOfTime = DateTime.Now.AddSeconds(rand.Next(240, 480));
            }
            else
            {
                t._takeOfTime = DateTime.Now.AddSeconds(rand.Next(0, 60));
            }
            return t;
        }
    }


    public enum Destination
    {
        Denmark,
        Usa,
        Canada,
        England,
        Finland,
    }

    public enum PlaneState
    {
        Refueling,
        Repairing,
        Unloading,
        InFlight,
        Loading,
        ReadyForTakeOff,
    }
}
