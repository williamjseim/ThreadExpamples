using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfAnimatedGif;

namespace FlaskeautomatGUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        void test()
        {
                box.RenderTransform = new TranslateTransform(box.RenderTransformOrigin.X, box.RenderTransformOrigin.Y + 5);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            asd.RenderTransform = new TranslateTransform(asd.RenderTransform.Value.OffsetX+100, asd.RenderTransform.Value.OffsetY);

        }
    }
}
