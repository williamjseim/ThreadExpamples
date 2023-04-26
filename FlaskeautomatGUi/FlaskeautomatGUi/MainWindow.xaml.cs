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
        private System.Windows.Threading.DispatcherTimer Tick = new System.Windows.Threading.DispatcherTimer();
        CancellationTokenSource source = new CancellationTokenSource();
        
        List<Rectangle> rects = new List<Rectangle>();

        object bottleLock = new object();
        Queue<Bottle> bottleBuffer = new Queue<Bottle>(10);
        Queue<Bottle> droppedBottles = new Queue<Bottle>(10);
        

        public MainWindow()
        {
            InitializeComponent();
            Tick.Tick += Process;
            Tick.Start();
            Thread t = new Thread(o => Producer(source.Token));
            Thread a = new Thread(o => ConsumerSplitter(source.Token));
            t.Start();
            a.Start();
        }
        void Process(object sender, EventArgs e)
        {
            if (!source.Token.IsCancellationRequested)
            {
                if (Monitor.TryEnter(bottleLock))
                {
                    try
                    {
                        if (droppedBottles.Count > 0&&rects.Count < 10)
                        {
                            int i = droppedBottles.Count;
                                Rectangle rect = new Rectangle
                                {
                                    Width = 50,
                                    Height = 50,
                                };
                                Bottle bottle = droppedBottles.Dequeue();
                                //rect.RenderTransform = beer.RenderTransform;
                                //rect.Fill = new SolidColorBrush(Colors.Green);
                                if (bottle.GetType() == typeof(BeerBottle))
                                {
                                    Canvas.SetLeft(rect, 530);
                                    rect.Fill = new SolidColorBrush(Colors.Green);
                                }
                                else
                                {
                                    Canvas.SetLeft(rect, 1126);
                                    rect.Fill = new SolidColorBrush(Colors.Red);
                                }
                                rects.Add(rect);
                                canvas.Children.Add(rect);
                        }
                        foreach (Rectangle rectangle in rects)
                        {
                            rectangle.RenderTransform = new TranslateTransform(rectangle.RenderTransform.Value.OffsetX, rectangle.RenderTransform.Value.OffsetY + 0.1f);
                            if (rectangle.RenderTransform.Value.OffsetY > 800)
                            {
                                rectangle.RenderTransform = new TranslateTransform(rectangle.RenderTransform.Value.OffsetX, 0);
                                canvas.Children.Remove(rectangle);
                            }
                        }
                    }
                    finally
                    { 
                        if(droppedBottles.Count == 0)
                        Monitor.PulseAll(bottleLock); 
                        Monitor.Exit(bottleLock); 
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //source.Cancel();
            Rectangle rect = new Rectangle
            {
                Width = 50,
                Height = 50,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransform = beer.RenderTransform,
            };
            rects.Add(rect);
            canvas.Children.Add(rect);
            Canvas.SetTop(rect, 0);
            Canvas.SetLeft(rect, 400);
        }

        Bottle CreateBottle(int i)//creates bottles
        {
            Bottle bottle;
            if (i == 1)
            {
                bottle = new BeerBottle();
            }
            else
            {
                bottle = new SodaBottle();
            }
            return bottle;
        }

        Random rand = new Random();
        void Producer(CancellationToken token)//makes bottles
        {
            while (!token.IsCancellationRequested)
            {
                Monitor.Enter(bottleBuffer);
                try
                {
                    int random = rand.Next(0, 11);
                    for (int i = 0; i < random; i++)
                    {
                        int j = rand.Next(0, 2);
                        bottleBuffer.Enqueue(CreateBottle(j));
                    }
                }
                finally { Monitor.PulseAll(bottleBuffer); Monitor.Exit(bottleBuffer); }
            }
        }

        void ConsumerSplitter(CancellationToken token)//sorts bottles
        {
            while (!token.IsCancellationRequested)
            {
                Monitor.Enter(bottleBuffer);
                try
                {
                    if(bottleBuffer.Count == 0)
                    {
                        Monitor.Wait(bottleBuffer);
                    }
                    Monitor.Enter(bottleLock);
                    try
                    {
                        if(droppedBottles.Count >= 10)
                        {
                            Monitor.Wait(bottleLock);
                        }
                        while (bottleBuffer.Count > 0 && droppedBottles.Count < 10)
                        {
                            droppedBottles.Enqueue(bottleBuffer.Dequeue());
                            //if (obj.GetType() == typeof(BeerBottle))
                            //{
                            //    beerBuffer.Enqueue(obj);
                            //}
                            //else
                            //{
                            //    sodaBuffer.Enqueue(obj);
                            //}
                        }
                    }
                    finally { Monitor.PulseAll(bottleLock); Monitor.Exit(bottleLock); }
                }
                finally { Monitor.PulseAll(bottleBuffer); Monitor.Exit(bottleBuffer); }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            source.Cancel();
            Application.Current.Shutdown();
        }
    }

    class Bottle { }
    class BeerBottle : Bottle { }
    class SodaBottle : Bottle { }
}
