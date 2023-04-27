using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
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
        private System.Windows.Threading.DispatcherTimer moveTick = new System.Windows.Threading.DispatcherTimer();
        CancellationTokenSource source = new CancellationTokenSource();
        
        Stopwatch stopwatch = new Stopwatch();

        List<Rectangle> rects = new List<Rectangle>();

        int bottleIndex = 0;

        object bottleLock = new object();
        Queue<Bottle> bottleBuffer = new Queue<Bottle>(10);
        Queue<Bottle> droppedBottles = new Queue<Bottle>(10);
        Thread producerThread;
        Thread splitterThread;
        Rectangle? unsortedRect;
        Bottle? unsortedBottle;

        bool pickup = false;
        public MainWindow()
        {
            InitializeComponent();
            gif.Visibility = Visibility.Collapsed;
            Tick.Tick += Process;
            moveTick.Tick += MoveRects;
            Tick.Start();
            moveTick.Start();
            Tick.Interval = new TimeSpan(0, 0, 0,0,125);
            producerThread = new Thread(o => Producer(source.Token));
            producerThread.Start();
            splitterThread = new Thread(o => ConsumerSplitter(source.Token));
            splitterThread.Start();
        }

        void Process(object sender, EventArgs e)
        {
            if (!pickup)
            {
                if (Monitor.TryEnter(bottleLock))
                {
                    try
                    {
                        if (droppedBottles.Count > 0&&rects.Count < 1&&unsortedBottle == null)
                        {
                            int i = droppedBottles.Count;
                            Rectangle rect = new Rectangle
                            {
                                Width = 50,
                                Height = 50,
                            };
                            unsortedBottle = droppedBottles.Dequeue();
                            unsortedRect = rect;
                            rect.Fill = new SolidColorBrush(Colors.Black);
                            Canvas.SetLeft(rect, 800);
                            canvas.Children.Add(rect);
                        }
                        else if (unsortedRect != null)
                        {
                            if (unsortedBottle.GetType() == typeof(BeerBottle))
                            {
                                Canvas.SetLeft(unsortedRect, 530);
                                unsortedRect.Fill = new SolidColorBrush(Colors.Green);
                            }
                            else
                            {
                                Canvas.SetLeft(unsortedRect, 1126);
                                unsortedRect.Fill = new SolidColorBrush(Colors.Red);
                            }
                            rects.Add(unsortedRect);
                            unsortedRect = null;
                            unsortedBottle = null;
                        }
                    }
                    catch { Monitor.PulseAll(bottleLock); Trace.Write(" main failed"); }
                    finally
                    { 
                        if(droppedBottles.Count == 0)
                        Monitor.PulseAll(bottleLock); 
                        Monitor.Exit(bottleLock); 
                    }
                }
            }
        }

        void MoveRects(object sender, EventArgs e)
        {
            if(stopwatch.Elapsed > new TimeSpan(0, 0, 7))
            {
                gif.Visibility = Visibility.Collapsed;
                beerTrash.Visibility = Visibility.Visible;
                sodaTrash.Visibility = Visibility.Visible;
                bottleIndex = 0;
                pickup = false;
                stopwatch.Stop();
                stopwatch.Reset();
            }
            if(bottleIndex == 10&&!pickup)
            {
                stopwatch.Start();
                pickup = true;
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri("C:\\Users\\zbcwise\\Documents\\GitHub\\ThreadExpamples\\FlaskeautomatGUi\\FlaskeautomatGUi\\gif\\angry-garbage.gif");
                image.EndInit();
                ImageBehavior.SetAnimatedSource(gif, image);
                gif.Visibility = Visibility.Visible;
                beerTrash.Visibility = Visibility.Collapsed;
                sodaTrash.Visibility = Visibility.Collapsed;
            }
            else
            {
                if(!pickup)
                for (int i = rects.Count - 1; i >= 0; i--)
                {
                    rects[i].RenderTransform = new TranslateTransform(rects[i].RenderTransform.Value.OffsetX, rects[i].RenderTransform.Value.OffsetY + 0.1f);
                    if (rects[i].RenderTransform.Value.OffsetY > 800)
                    {
                        bottleIndex++;
                        rects[i].RenderTransform = new TranslateTransform(rects[i].RenderTransform.Value.OffsetX, 0);
                        canvas.Children.Remove(rects[i]);
                        rects.RemoveAt(i);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (source.Token.IsCancellationRequested)
            {
                source = new CancellationTokenSource();
                producerThread = new Thread(o => Producer(source.Token));
                producerThread.Start();
                splitterThread = new Thread(o => ConsumerSplitter(source.Token));
                splitterThread.Start();
            }
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
                if(bottleBuffer.Count > 0)
                {
                    while(splitterThread.ThreadState != System.Threading.ThreadState.WaitSleepJoin)
                    {

                    }
                    Thread.Sleep(100);
                    Monitor.PulseAll(bottleBuffer);
                    Monitor.Wait(bottleBuffer);
                }
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
                Thread.Sleep(100);
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
                    while (bottleBuffer.Count > 0 && droppedBottles.Count < 10)
                    {
                        droppedBottles.Enqueue(bottleBuffer.Dequeue());
                    }
                }
                finally { Monitor.PulseAll(bottleBuffer); Monitor.Exit(bottleBuffer); }
                Thread.Sleep(100);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!source.Token.IsCancellationRequested)
            source.Cancel();
        }
    }

    class Bottle { }
    class BeerBottle : Bottle { }
    class SodaBottle : Bottle { }
}
