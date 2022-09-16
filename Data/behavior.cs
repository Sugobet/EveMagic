

namespace EveMagic.Data
{
    public class Behavior
    {
        Android.App.Instrumentation inst;
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
            inst.SendPointerSync(Android.Views.MotionEvent.Obtain(Android.OS.SystemClock.UptimeMillis(), Android.OS.SystemClock.UptimeMillis(),
                    Android.Views.MotionEventActions.Down, realX, realY, 0));
            //发送Up
            inst.SendPointerSync(Android.Views.MotionEvent.Obtain(Android.OS.SystemClock.UptimeMillis(), Android.OS.SystemClock.UptimeMillis(),
                    Android.Views.MotionEventActions.Up, realX, realY, 0));
        }
    }
}
