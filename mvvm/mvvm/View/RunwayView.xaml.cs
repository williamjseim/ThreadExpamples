using mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mvvm.View
{
    /// <summary>
    /// Interaction logic for Runway.xaml
    /// </summary>
    public partial class RunwayView : UserControl
    {
        public RunwayView()
        {
            InitializeComponent();
        }

        private RunwayViewModel _runway = new RunwayViewModel();

        public RunwayViewModel Runway
        {
            get { return _runway; }
            set 
            {
                if (_runway != value)
                {
                    FlightPlanViewControl.DataContext = value;
                    _runway = value; 
                }
            }
        }

        private void RunWayQueue_Loaded(object sender, RoutedEventArgs e)
        {
            FlightPlanViewControl.DataContext = _runway;
        }
    }
}
