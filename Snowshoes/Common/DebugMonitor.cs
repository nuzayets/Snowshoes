#region

using System;
using D3;

#endregion

namespace Snowshoes.Common
{
    internal class DebugMonitor : Sherpa
    {
        private float _x;
        private float _y;
        private UI.DebugUI _debugWindow;

        public DebugMonitor(int delay, UI.DebugUI debugWindow) : base(delay)
        {
            ImmuneToPause = true;
            _debugWindow = debugWindow;
        }

        protected override void Loop()
        {
            if (GetBool(updateData))
            {
                _debugWindow.txtX.Text = _x.ToString();
                _debugWindow.txtY.Text = _y.ToString();
            }
        }

        private bool updateData()
        {
            bool changed = changed = (_x != Me.X || _y != Me.Y);
            _x = Me.X;
            _y = Me.Y;
            return changed;
        }
    }
}