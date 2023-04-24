
namespace flaskeautomat
{
    class Bottle { }
    class BeerBottle : Bottle { }
    class SodaBottle : Bottle { }
    internal class Program
    {
        public static Queue<Bottle> bottleBuffer = new Queue<Bottle>(10);
        public static Queue<Bottle> beerBuffer = new Queue<Bottle>(10);
        public static Queue<Bottle> sodaBuffer = new Queue<Bottle>(10);
        static CancellationTokenSource source = new CancellationTokenSource();
        static object bottleLock = new object();

        static void Main(string[] args)
        {
            Program pg = new Program();
            ThreadPool.QueueUserWorkItem(o=>pg.Producer(source.Token));
            Thread a = new Thread(o=>pg.ConsumerSplitter(source.Token));
            a.Start();
            Thread s = new Thread(O=>pg.Consumer(source.Token));
            s.Start();
            Console.ReadKey();
            source.Cancel();
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
            while(!token.IsCancellationRequested)
            {
                Console.WriteLine("producer");
                Monitor.Enter(bottleBuffer);
                try
                {
                    int random = rand.Next(0, 11);
                    for (int i = 0; i < random; i++)
                    {
                        int j = rand.Next(0, 2);
                        Console.WriteLine("add bottle");
                        bottleBuffer.Enqueue(CreateBottle(j));
                    }

                }
                finally { Monitor.PulseAll(bottleBuffer); Monitor.Exit(bottleBuffer); }
            }
        }

        void ConsumerSplitter(CancellationToken token)//sorts bottles
        {
            while(!token.IsCancellationRequested)
            {
                Console.WriteLine("splitter");
                Monitor.Enter(bottleBuffer);
                try
                {
                    while (bottleBuffer.Count == 0)
                    {
                        Monitor.Wait(bottleBuffer);
                    }
                    Monitor.Enter(bottleLock);
                    try
                    {
                        while (bottleBuffer.Count > 0)
                        {
                            Bottle obj = bottleBuffer.Dequeue();
                            if (obj.GetType() == typeof(BeerBottle))
                            {
                                Console.WriteLine("beer split");
                                beerBuffer.Enqueue(obj);
                            }
                            else
                            {
                                Console.WriteLine("soda split");
                                sodaBuffer.Enqueue(obj);
                            }
                        }
                    }
                    finally { Monitor.PulseAll(bottleLock); Monitor.Exit(bottleLock); }
                }
                finally { Monitor.PulseAll(bottleBuffer); Monitor.Exit(bottleBuffer); }
            }
        }

        void Consumer(CancellationToken token)//eats bottles
        {
            while(!token.IsCancellationRequested)
            {
                if (!BottlesLeft())
                {
                    Monitor.Enter(bottleLock);
                    Monitor.Wait(bottleLock);
                }
                else
                {
                    Monitor.Enter(bottleLock);
                }
                try
                {
                    Console.WriteLine("consumer");
                    while (BottlesLeft())
                    {
                        if (sodaBuffer.Count > 0)
                        {
                            Console.WriteLine("Crush Soda");
                            sodaBuffer.Dequeue();
                        }
                        if (beerBuffer.Count > 0)
                        {
                            Console.WriteLine("Crush beer");
                            beerBuffer.Dequeue();
                        }
                    }
                }
                catch { throw; }
                finally { Monitor.PulseAll(bottleLock); Monitor.Exit(bottleLock); }
            }
        }

        bool BottlesLeft()//checks if there are anymore bottles in the queues
        {
            if(sodaBuffer.Count > 0)
            {
                return true;
            }
            if (beerBuffer.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}