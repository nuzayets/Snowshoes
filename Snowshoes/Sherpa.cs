#region

using System;
using System.Threading;
using System.Threading.Tasks;
using D3;

#endregion

namespace Snowshoes
{
    public abstract class Sherpa
    {
        private readonly int _delay;

        protected bool ImmuneToPause;
        private bool _stopping;
        private Thread _thread;
        private int _ticksStart;

        protected Sherpa(int delay)
        {
            _delay = delay;
            Init();
        }

        protected Sherpa()
        {
            _delay = 0;
            Init();
        }

        private void Init()
        {
            _thread = new Thread(Run);
            _thread.Start();
        }

        public int TickRunTime()
        {
            return Environment.TickCount - _ticksStart;
        }

        public static float GetDistance(float x, float y)
        {
            return GetDistance(x, y, GetData(() => Me.X), GetData(() => Me.Y));
        }

        public static float GetDistance(float x, float y, float x2, float y2)
        {
            return (float) Math.Sqrt((x - x2)*(x - x2) + (y - y2)*(y - y2));
        }

        public static T GetData<T>(Func<T> func)
        {
            var task = new Task<T>(func);
            Snowshoes.Tasks.Enqueue(task);
            task.Wait();
            return task.Result;
        }


        public static bool GetBool(Func<bool> func)
        {
            return GetData(func);
        }

        public static void WaitFor(Func<bool> func)
        {
            Task<bool> task;
            do
            {
                task = new Task<bool>(func);
                Snowshoes.Tasks.Enqueue(task);
                task.Wait();
            } while (!task.Result);
        }

        public static void WaitForExclusively(Func<bool> func)
        {
            Snowshoes.Pause();
            Task<bool> task;
            do
            {
                task = new Task<bool>(func);
                Snowshoes.HighPriority.Enqueue(task);

                task.Wait();
            } while (!task.Result);

            Snowshoes.Start();
        }

        public static void WaitForExclusivelyAfterAction(Action act, Func<bool> func)
        {
            Snowshoes.Pause();
            Snowshoes.HighPriority.Enqueue(new Task(act));

            Task<bool> task;
            do
            {
                task = new Task<bool>(func);
                Snowshoes.HighPriority.Enqueue(task);

                task.Wait();
            } while (!task.Result);

            Snowshoes.Start();
        }

        public static void PerformAction(Action act)
        {
            var task = new Task(act);
            Snowshoes.Tasks.Enqueue(task);
            task.Wait();
        }

        public static void Walk(float x, float y)
        {
            PerformAction(() => Me.UsePower(SNOPowerId.Walk, x, y, Me.Z));
            while (GetDistance(x, y) > 10)
            {
                if (GetBool(() => Me.Mode != UnitMode.Running))
                {
                    PerformAction(() => Me.UsePower(SNOPowerId.Walk, x, y, Me.Z));
                }
                Thread.Sleep(100);
            }
        }

        private void Run()
        {
            try
            {
                while (!_stopping)
                {
                    if (Snowshoes.CurrStatus == Status.Running || ImmuneToPause)
                    {
                        _ticksStart = Environment.TickCount;
                        Loop();
                    }

                    Thread.Sleep(_delay);
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        protected void Stop()
        {
            _stopping = true;
        }

        public void HardRestart()
        {
            _thread.Abort();
            _thread = new Thread(Run);
            _thread.Start();
        }


        protected abstract void Loop();
    }
}