using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snowshoes
{
    public abstract class Sherpa 
    {

        Thread thread;
        int delay;

        protected bool ImmuneToPause = false;
        private bool stopping = false;
        private int ticks_start = 0;

        public Sherpa(int delay)
        {
            this.delay = delay;
            init();
        }

        public Sherpa()
        {
            this.delay = 0;
            init();
        }

        private void init()
        {
            thread = new Thread(Run);
            thread.Start();
        }

        public int tickRunTime()
        {
            return System.Environment.TickCount - ticks_start;
        }

        static public float GetDistance(float x, float y)
        {
            return GetDistance(x, y, getData<float>(() => D3.Me.X), getData<float>(() => D3.Me.Y));
        }

        static public float GetDistance(float x, float y, float x2, float y2)
        {
            return (float)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
        }

        static public T getData<T>(Func<T> func)
        {
            Task<T> task = new Task<T>(func);
            Snowshoes.tasks.Enqueue(task);
            task.Wait();
            return task.Result;
        }


        static public bool getBool(Func<bool> func)
        {
            return getData<bool>(func);
        }

        static public void waitFor(Func<bool> func)
        {
            Task<bool> task;
            do
            {
                task = new Task<bool>(func);
                Snowshoes.tasks.Enqueue(task);
                task.Wait();
            } while (!task.Result);

        }

        static public void performAction(Action act)
        {
            Task task = new Task(act);
            Snowshoes.tasks.Enqueue(task);
            task.Wait();
        }

        
        private void Run()
        {
            try
            {
                while (true && !stopping)
                {
                    if (Snowshoes.CurrStatus == Status.Running || ImmuneToPause)
                    {
                        ticks_start = System.Environment.TickCount;
                        loop();
                    }

                    Thread.Sleep(delay);
                }
            }
            catch (ThreadAbortException) { }    ;

        }

        protected void Stop()
        {
            stopping = true;
        }

        public void HardRestart()
        {
            thread.Abort();
            thread = new Thread(Run);
            thread.Start();
        }


        abstract protected void loop();
    }
}
