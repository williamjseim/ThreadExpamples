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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mvvm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        mvvm.ViewModel.FlightViewModel model = new mvvm.ViewModel.FlightViewModel();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FlightPlanViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            model.LoadFlights();
            FlightPlanViewControl.DataContext = model;
        }
        private void FlightPlanViewControl_Loaded2(object sender, RoutedEventArgs e)
        {
            FlightPlanViewControl2.DataContext = model;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            model.Flights.Add(Flight.GenerateFlight());
        }
    }
}
