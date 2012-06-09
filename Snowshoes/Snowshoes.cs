using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Diagnostics;

using D3;

namespace Snowshoes
{
    public delegate void StatusChangedHandler (Status s);
    public enum Status { Running, Paused, Stopped };

    class Snowshoes
    {
        static public event StatusChangedHandler StatusChanged;

        static public ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();
        static public ConcurrentQueue<Task> postponed_tasks = new ConcurrentQueue<Task>();

        static private Status status;
        static public Status CurrStatus
        {
            get { return status; }
            private set 
            { 
                status = value;
                if (StatusChanged != null) StatusChanged(status);
            }
        }
        static private UI.MainUI mainWindow;
        
        static void Main(string[] args)
        {
            mainWindow = new UI.MainUI();
            mainWindow.Show();

            CurrStatus = Status.Stopped; // start stopped

            Game.OnTickEvent += new TickEventHandler(Game_OnTickEvent);

            new AddOns.GoldMonitor(500);
            new Bots.Sarkoth();
        }


        static public void Stop()
        {
            Print("Stopping.");

            CurrStatus = Status.Stopped;
        }

        static public void Start()
        {
            Print("Starting.");

            CurrStatus = Status.Running;
        }

        static public void Pause()
        {
            Print("Pausing.");

            CurrStatus = Status.Paused;
        }

        static public void Print(String str)
        {
            StackTrace trace = new StackTrace();
            mainWindow.PrintLine(string.Format("[{0} - {1}.{2}]: {3}", DateTime.Now.ToShortTimeString(), trace.GetFrame(1).GetMethod().DeclaringType.Name, trace.GetFrame(1).GetMethod().Name, str));
        }

        static public void GoldCount(String gold, String delta_gold, String gph)
        {
            mainWindow.setGold(gold, delta_gold, gph);
        }

        static void Game_OnTickEvent(EventArgs e)
        {

            Task t;

            while (postponed_tasks.TryDequeue(out t))
            {
                tasks.Enqueue(t);
            }

            while (tasks.TryDequeue(out t))
            {
                t.RunSynchronously();
            }
        }
    }
}
