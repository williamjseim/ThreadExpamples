namespace BaggageSystem
{
    internal class Program
    {
        public static object sorterLock = new object();

        Terminal[] terminals = new Terminal[]
        {
            new Terminal(0,null),
            new Terminal(1,null),
            new Terminal(2,null),
        };

        Skranke[] skranker = new Skranke[]
        {
            new Skranke(),
            new Skranke(),
            new Skranke(),
            new Skranke(),
            new Skranke(),
        };

        static void Main(string[] args)
        {
            Program pg = new Program();
        }

        void AirPortTowerController()
        {
            while (true)
            {

            }
        }

        void SkrankeController()
        {

        }

        void GenerateFlightPlan(Terminal terminal)
        {
            terminal.plan = new FlightPlan(new TimeSpan(0,1,0),false,terminal.id);
        }

        void Sorter()
        {
            while (true)
            {
                try
                {
                    foreach (Skranke skranke in skranker)
                    {
                        if (!skranke.beingLoaded && skranke.baggageBuffer.Count != 0)
                        {
                            
                        }
                    }
                }
                finally { if (Monitor.IsEntered(sorterLock)) { Monitor.Exit(sorterLock); } }
                Monitor.Enter(sorterLock);
                Monitor.Wait(sorterLock);
            }
        }

        void Terminal()
        {

        }

        public void ReceiveBaggage(Skranke skranke ,Baggage[] baggageArray)
        {
            skranke.beingLoaded = true;
            Monitor.Enter(skranke.baggageLock);
            try
            {
                foreach (Baggage baggage in baggageArray)
                {
                    Console.WriteLine(baggage.id+" received");
                    skranke.baggageBuffer.Add(baggage);
                }
            }
            finally
            {
                Monitor.Pulse(Program.sorterLock);
                Monitor.Pulse(skranke.baggageLock);
                Monitor.Exit(skranke.baggageLock);
                skranke.beingLoaded = false;
            }
        }
    }
    
    class Skranke
    {
        public bool Open = false;
        public object baggageLock = new object();
        public bool beingLoaded = false;
        public List<Baggage> baggageBuffer = new List<Baggage>();
        public Skranke()
        {
            Open = true;
        }
    }

    class Baggage
    {
        public int id;
        public DateTime sortIn;
        public DateTime sortOut;
    }

    class FlightPlan
    {
        public TimeSpan planeLeaveTime;
        public bool planeLeaving = false;
        public int terminalId;

        public FlightPlan(TimeSpan planeLeaveTime, bool planeLeaving, int terminalId)
        {
            this.planeLeaveTime = planeLeaveTime;
            this.planeLeaving = planeLeaving;
            this.terminalId = terminalId;
        }
    }

    class Terminal
    {
        public int id = -1;
        public FlightPlan? plan = null;
        public Queue<Baggage> baggage = new Queue<Baggage>();
        public bool waitingForBaggage = true;

        public Terminal(int id, FlightPlan? plan)
        {
            this.id = id;
            this.plan = plan;
        }
    }
}