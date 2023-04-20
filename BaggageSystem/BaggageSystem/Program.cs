using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;

namespace BaggageSystem
{
    internal class Program
    {
        static string folderpath = @"C:\Users\zbcwise\Desktop\source\";
        static string logPath = @"asd.txt";
        static string reservationFile = @"reservations.json";
        static string possibleReservations = @"C:possible.json";
        public int baggegaIndenx;
        public static object sorterLock = new object();
        public static object sortingLock = new object();
        public Dictionary<int, Queue<Baggage>> sortedBaggage = new Dictionary<int, Queue<Baggage>>();
        public bool terminalWorking;


        public object flightLock = new object();
        Queue<FlightPlan> flightPlans = new Queue<FlightPlan>();

        object logLock = new object();
        List<string> sorterLog = new List<string>();
        List<string> TerminalLog = new List<string>();
        Queue<FlightPlan> leavingPlanes= new Queue<FlightPlan>();

        public List<Terminal> terminals = new List<Terminal>
        {
            new Terminal(0,null),
            new Terminal(1,null),
            new Terminal(2,null),
        };

        public List<Skranke> skranker = new List<Skranke>
        {
            new Skranke('a'),
            new Skranke('b'),
            new Skranke('c'),
            new Skranke('d'),
            new Skranke('e'),
        };

        static void Main(string[] args)
        {
            Program pg = new Program();
            Thread w = new Thread(pg.AirPortTowerController);
            Thread a = new Thread(pg.SkrankeController);
            Thread s = new Thread(pg.Sorter);
            Thread d = new Thread(pg.TerminalController);
            Thread e = new Thread(pg.Logger);
            w.Start();
            a.Start();
            s.Start();
            d.Start();
            e.Start();
            pg.RunTime();
        }

        public void RunTime()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo= Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.P)
                {
                    skranker.Add(new Skranke('p'));
                }
                else if(keyInfo.Key == ConsoleKey.O)
                {
                    //terminals.Add(new Terminal(terminals.Count,GenerateFlightPlan(terminals.Count)));
                        Console.WriteLine(sorterLog.Count+" "+TerminalLog.Count+" "+flightPlans.Count);
                }
            }
        }

        void ReservationController()
        {
            JsonSerializer serializer = new JsonSerializer();
            if (File.Exists(folderpath + reservationFile))
            {
                using (StreamReader sw = new StreamReader(folderpath+ reservationFile))
                using (JsonReader reader = new JsonTextReader(sw))
                {
                    serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    JObject obj = serializer.Deserialize<JObject>(reader);
                }
                File.Delete(reservationFile);
            }
        }

        void CreateReservations()
        {
            JsonSerializer serializer = new JsonSerializer();
            if (File.Exists(folderpath + possibleReservations))
            {
                using (StreamWriter sw = new StreamWriter(folderpath+possibleReservations))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    serializer.Serialize(writer,flightPlans);
                }
            }
        }

        void AirPortTowerController()
        {
            bool plansChanged = false;
            Random rand = new Random();
            foreach (Terminal terminal in terminals)
            {
                plansChanged= true;
                terminal.plan = GenerateFlightPlan(terminal.id);
                terminal.stopwatch.Start();
            }
            while (true)
            {
                if(flightPlans.Count < 10)
                if (rand.Next(0, 100) > 95)
                {
                    if (!Monitor.TryEnter(flightLock))
                    {
                            Console.WriteLine("flightlock");
                        Monitor.Enter(flightLock);
                        Monitor.Wait(flightLock);
                    }
                    try
                    {
                        flightPlans.Enqueue(GenerateFlightPlan(-1));
                    }
                    finally { Monitor.PulseAll(flightLock); Monitor.Exit(flightLock); }
                }
                if (terminals.Count <= 2&&flightPlans.Count > terminals.Count)
                {
                    plansChanged = true;
                    FlightPlan t = flightPlans.Dequeue();
                    t.terminalId = terminals.Count;
                    terminals.Add(new Terminal(terminals.Count, t));
                }
                else
                {
                    for (int i = terminals.Count - 1; i >= 0; i--)
                    {
                        if (terminals[i].plan == null)
                        {
                            if (flightPlans.Count == 0)
                            {
                                terminals.RemoveAt(i);
                            }
                            else
                            {
                                plansChanged = true;
                                FlightPlan t = flightPlans.Dequeue();
                                t.terminalId = terminals[i].id;
                                terminals[i].plan = t;
                            }
                        }
                    }
                }
                if (plansChanged)
                {
                    plansChanged = false;
                    Console.Clear();
                    foreach (Terminal terminal in terminals)
                    {
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine("terminal id "+terminal.id+" flight plan id "+terminal.plan);
                    }
                    foreach (FlightPlan flight in flightPlans)
                    {
                        Console.ForegroundColor= ConsoleColor.White;
                        Console.WriteLine(flight+ "ready for arrival");
                    }
                    foreach (FlightPlan flightPlan in leavingPlanes)
                    {
                        if (flightPlan.Crashed)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(flightPlan +" crashed");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(flightPlan + " is leaving");
                        }
                    }
                    CreateReservations();
                }
            }
        }

        void SkrankeController()
        {
            while (true)
            {
                Random rand = new Random();
                foreach (Skranke skranke in skranker)
                {
                    Monitor.Enter(skranke.baggageLock);
                    try
                    {
                        skranke.beingLoaded = true;
                        int j = rand.Next(0,10);
                        List<Baggage> list = new List<Baggage>();
                        for (int i = 0; i < j; i++)
                        {
                            int a = rand.Next(0,terminals.Count);
                            list.Add(new Baggage { id = baggegaIndenx++, sortIn = DateTime.Now, terminalId = a, }) ;
                        }
                        ReceiveBaggage(skranke,list.ToArray());
                    }
                    finally { Monitor.Pulse(skranke.baggageLock); Monitor.Exit(skranke.baggageLock); }
                }
                Thread.Sleep(10000);
            }
        }

        FlightPlan GenerateFlightPlan(int id)
        {
            return new FlightPlan(new TimeSpan(0,1,0),false,id);
        }

        void Sorter()
        {
            foreach (Terminal terminal in this.terminals)
            {
                sortedBaggage.Add(terminal.id, new Queue<Baggage>());
            }
            while (true)
            {
                try
                {
                    Monitor.Enter(sortingLock);
                    try
                    {
                        foreach (Skranke skranke in skranker)
                        {
                            if (!skranke.beingLoaded && skranke.baggageBuffer.Count != 0)
                            {
                                foreach (Baggage baggage in skranke.baggageBuffer)
                                {
                                    if(sortedBaggage.TryGetValue(baggage.terminalId, out Queue<Baggage> queue))
                                    {
                                        baggage.sortOut = DateTime.Now;
                                        queue.Enqueue(baggage);
                                        sortedBaggage[baggage.terminalId] = queue;
                                        Monitor.Enter(sorterLog);
                                        Monitor.Enter(logLock);
                                        try
                                        {
                                            this.sorterLog.Add(baggage.id + " was sorted to " + baggage.terminalId + " from" + skranke.skrankeName+" intime "+baggage.sortIn+" out time "+baggage.sortOut);
                                        }
                                        finally
                                        {
                                            Monitor.Exit(sorterLog);
                                            Monitor.PulseAll(logLock);
                                            Monitor.Exit(logLock);
                                        }
                                    }
                                    else
                                    {
                                        foreach (Terminal terminal in this.terminals)
                                        {
                                            if(!sortedBaggage.ContainsKey(terminal.id))
                                            sortedBaggage.Add(terminal.id, new Queue<Baggage>());
                                        }
                                    }
                                }
                                skranke.baggageBuffer.Clear();
                            }
                        }
                    }
                    finally { Monitor.PulseAll(sortingLock); Monitor.Exit(sortingLock); }
                }
                finally { if (Monitor.IsEntered(sorterLock)) { Monitor.Exit(sorterLock); } }
                Monitor.Enter(sorterLock);
                Monitor.Wait(sorterLock);
            }
        }

        public void Logger()
        {
            List<string>? sorterlog;
            List<string>? terminallog;
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                Monitor.Enter(logLock);
                Monitor.Wait(logLock);
                try
                {
                    if (Monitor.TryEnter(sorterLog))
                    {
                        try
                        {
                            sorterlog = this.sorterLog;
                        }
                        finally { Monitor.Exit(this.sorterLog); }
                        foreach (string log in sorterlog)
                        {
                            sb.Append(log);
                        }
                        sorterlog = null;
                    }
                    if (Monitor.TryEnter(this.TerminalLog))
                    {
                        try
                        {
                            terminallog = this.TerminalLog;
                        }
                        finally { Monitor.Exit(this.TerminalLog); }
                        foreach (string log in terminallog)
                        {
                            sb.AppendLine(log);
                        }
                        sorterlog = null;
                    }
                }
                finally
                {
                    File.AppendAllText(folderpath + logPath, sb.ToString());
                    sb.Clear();
                    Monitor.Exit(logLock);
                }
            }
        }

        void TerminalController()
        {
            while (true)
            {
                Monitor.Enter(sortingLock);
                Monitor.Wait(sortingLock);
                try
                {
                    foreach (Terminal terminal in terminals)
                    {
                        if(terminal.plan != null)
                        {
                            if(sortedBaggage.TryGetValue(terminal.id,out Queue<Baggage> queue))
                            {
                                terminal.baggage = queue;
                                //Console.WriteLine("delivering to terminal "+queue.Count);
                                sortedBaggage[terminal.id].Clear();
                            }
                            if (terminal.plan.planeLeaveTime < terminal.stopwatch.Elapsed)
                            {
                                Random rand = new Random();
                                if(rand.Next(0,100) > 95)
                                {
                                    terminal.plan.Crashed= true;
                                }
                                leavingPlanes.Enqueue(terminal.plan);
                                terminal.plan = null;
                                terminal.stopwatch.Reset();
                            }
                        }                    
                    }
                }
                finally
                {
                    if(Monitor.IsEntered(sortingLock)) { Monitor.Exit(sortingLock); terminalWorking = false; }
                }
            }
        }

        public void ReceiveBaggage(Skranke skranke ,Baggage[] baggageArray)
        {
            skranke.beingLoaded = true;
            Monitor.Enter(skranke.baggageLock);
            try
            {
                foreach (Baggage baggage in baggageArray)
                {
                    //Console.WriteLine(baggage.id+" received by skranke "+skranke.skrankeName);
                    skranke.baggageBuffer.Add(baggage);
                }
            }
            finally
            {
                Monitor.Enter(sorterLock);
                Monitor.PulseAll(sorterLock);
                Monitor.Exit(sorterLock);
                Monitor.PulseAll(skranke.baggageLock);
                Monitor.Exit(skranke.baggageLock);
                skranke.beingLoaded = false;
            }
        }
    }
    
    class Skranke
    {
        public Char skrankeName;
        public bool Open = false;
        public object baggageLock = new object();
        public bool beingLoaded = false;
        public List<Baggage> baggageBuffer = new List<Baggage>();
        public Skranke(char skrankeName)
        {
            Open = true;
            this.skrankeName = skrankeName;
        }
    }

    class Baggage
    {
        public int id;
        public int terminalId;
        public DateTime sortIn;
        public DateTime sortOut;
    }

    class FlightPlan
    {
        public TimeSpan planeLeaveTime;
        public bool planeLeaving = false;
        public int terminalId;
        public bool Crashed = false;
        public int Seats;
        public int reservations;
        public bool reservedByUser = false;

        public FlightPlan(TimeSpan planeLeaveTime, bool planeLeaving, int terminalId)
        {
            Random rand = new Random();
            this.planeLeaveTime = planeLeaveTime;
            this.planeLeaving = planeLeaving;
            this.terminalId = terminalId;
            this.Seats = rand.Next(24, 100);
            this.Seats = rand.Next(24, Seats);
        }
    }

    class Terminal
    {
        public Stopwatch stopwatch = new Stopwatch();
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