#region

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using D3;
using Snowshoes.Bots;
using Snowshoes.Common;
using Snowshoes.UI;

#endregion

namespace Snowshoes
{
    public delegate void StatusChangedHandler(Status s);

    public enum Status
    {
        Running,
        Paused,
        Stopped
    };

    internal class Snowshoes
    {
        public static ConcurrentQueue<Task> Tasks = new ConcurrentQueue<Task>();
        public static ConcurrentQueue<Task> HighPriority = new ConcurrentQueue<Task>();

        private static Status _status;

        private static MainUI _mainWindow;

        public static Status CurrStatus
        {
            get { return _status; }
            private set
            {
                _status = value;
                if (StatusChanged != null) StatusChanged(_status);
            }
        }

        public static event StatusChangedHandler StatusChanged;

// ReSharper disable UnusedParameter.Local
        private static void Main(string[] args)
// ReSharper restore UnusedParameter.Local
        {
            _mainWindow = new MainUI();
            _mainWindow.Show();

            CurrStatus = Status.Stopped; // start stopped

            Game.OnTickEvent += Game_OnTickEvent;

            new GoldMonitor(500);
            new Sarkoth();
        }


        public static void Stop()
        {
            Print("Stopping.");

            CurrStatus = Status.Stopped;
        }

        public static void Start()
        {
            Print("Starting.");

            CurrStatus = Status.Running;
        }

        public static void Pause()
        {
            Print("Pausing.");

            CurrStatus = Status.Paused;
        }

        public static void Print(String str)
        {
            var trace = new StackTrace();
            _mainWindow.PrintLine(string.Format("[{0} - {1}.{2}]: {3}", DateTime.Now.ToShortTimeString(),
                                               trace.GetFrame(1).GetMethod().DeclaringType.Name,
                                               trace.GetFrame(1).GetMethod().Name, str));
        }

        public static void GoldCount(String gold, String deltaGold, String gph)
        {
            _mainWindow.SetGold(gold, deltaGold, gph);
        }

        private static void Game_OnTickEvent(EventArgs e)
        {
            Task t;

            while (HighPriority.TryDequeue(out t))
            {
                t.RunSynchronously();
            }

            if (CurrStatus != Status.Running) return;

            while (Tasks.TryDequeue(out t))
            {
                t.RunSynchronously();
            }
        }
    }
}