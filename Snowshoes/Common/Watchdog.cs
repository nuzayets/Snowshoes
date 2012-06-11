namespace Snowshoes.Common
{
    internal class Watchdog : Sherpa
    {
        private readonly Sherpa _dependent;

        public Watchdog(int delay, Sherpa caller)
            : base(delay)
        {
            _dependent = caller;
        }

        protected override void Loop()
        {
            if (_dependent.TickRunTime() <= 60000) return;
            Snowshoes.Print("Watchdog kill!");
            _dependent.HardRestart();
        }
    }
}