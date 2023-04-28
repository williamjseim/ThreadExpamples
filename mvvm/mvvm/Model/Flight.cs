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



        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if( PropertyChanged != null )
            {
                PropertyChanged(this, new PropertyChangedEventArgs( propertyName ) );
            }
        }

        public static Flight GenerateFlight()
        {
            Random rand = new Random();
            return new Flight
            {
                _companyName = "CompanyName",
                _takeOfTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour+1, 0,0,0,0),
                destination = (Destination)rand.Next(0,Enum.GetNames(typeof(Destination)).Length)
            };
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
}
