

using Android.App;
using Android.OS;
using Android.Views;

namespace EveMagic.Data
{
    public class Behavior
    {
        Instrumentation inst;
        Random rand;

        public Behavior()
        {
            this.inst = new();
            this.rand = new();
        }

        private void Tap(int x, int y, int rangeX, int rangeY)
        {
            int realX = x + this.rand.Next(1, rangeX);
            int realY = y + this.rand.Next(1, rangeY);
            //发送Down
            inst.SendPointerSync(MotionEvent.Obtain(SystemClock.UptimeMillis(), SystemClock.UptimeMillis(),
                    MotionEventActions.Down, realX, realY, 0));
            //发送Up
            inst.SendPointerSync(MotionEvent.Obtain(SystemClock.UptimeMillis(), SystemClock.UptimeMillis(),
                    MotionEventActions.Up, realX, realY, 0));
        }
    }
}
