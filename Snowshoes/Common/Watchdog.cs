namespace Snowshoes.Common
{
    internal class Watchdog : Sherpa
    {
        private readonly Sherpa _dependent;
        private const int TimeoutSeconds = 60;

        public Watchdog(int delay, Sherpa caller)
            : base(delay)
        {
            _dependent = caller;
        }

        protected override void Loop()
        {
            if (_dependent.TickRunTime() <= TimeoutSeconds * 1000) return;
            Snowshoes.Print("Watchdog kill!");
            _dependent.HardRestart();
        }
    }
}