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
        static object bottleLock = new object();
        static void Main(string[] args)
        {
            Program pg = new Program();
            Thread t = new Thread(pg.Producer);
            t.Start();
            Thread a = new Thread(pg.ConsumerSplitter);
            a.Start();
            Thread s = new Thread(pg.Consumer);
            s.Start();
        }

        Bottle CreateBottle(int i)
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

        void Producer()
        {
            while(true)
            {
                Console.WriteLine("producer");
                Monitor.Enter(bottleBuffer);
                try
                {
                    Random rand = new Random();
                    int random = rand.Next(0, 11);
                    for (int i = 0; i < random; i++)
                    {
                        int j = rand.Next(0, 2);
                        Console.WriteLine("add bottle");
                        bottleBuffer.Enqueue(CreateBottle(j));
                        Thread.Sleep(100);
                    }

                }
                finally { Monitor.PulseAll(bottleBuffer); Monitor.Exit(bottleBuffer); }
            }
        }

        void ConsumerSplitter()
        {
            while(true)
            {
                Console.WriteLine("splitter");
                Monitor.Enter(bottleBuffer);
                try
                {
                    while (bottleBuffer.Count == 0)
                    {
                        Monitor.Wait(bottleBuffer);
                    }
                    if (!Monitor.TryEnter(bottleLock))
                    {

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
                            Thread.Sleep(100);
                        }
                    }
                    finally { Monitor.PulseAll(bottleLock); Monitor.Exit(bottleLock); }
                }
                finally { Monitor.PulseAll(bottleBuffer); Monitor.Exit(bottleBuffer); }
            }
        }

        void Consumer()
        {
            while(true)
            {
                Monitor.Enter(bottleLock);
                Monitor.Wait(bottleLock);
                try
                {
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
                        Thread.Sleep(100);
                    }
                }
                catch { throw; }
                finally { Monitor.PulseAll(bottleLock); Monitor.Exit(bottleLock); }
            }
        }

        bool BottlesLeft()
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