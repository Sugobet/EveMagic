

using EveMagic.Data.Ocr;

namespace EveMagic.Data
{
    public class Behavior
    {
        CloudOcr _ocr;
        Android.App.Instrumentation inst;
        Random rand;
        Java.Lang.Process _process;
        Java.IO.PrintStream _outputStream;

        public string DeviceName { get; set; }
        public string DeviceAddress { get; set; }

        public Behavior(string deviceName, string deviceAddress, CloudOcr ocr)
        {
            this._ocr = ocr;
            this.inst = new();
            this.rand = new();

            this.DeviceName = deviceName;
            this.DeviceAddress = deviceAddress;

            this._process = Java.Lang.Runtime.GetRuntime().Exec("su");
            this._outputStream = new Java.IO.PrintStream(this._process.OutputStream);
        }

        public byte[] GetScreen()
        {
            string path = $"mnt/sdcard/Pictures/Screenshots/{this.DeviceName}.png";
            this._outputStream.Println($"screencap -p {path}");
            this._outputStream.Flush();

            return File.ReadAllBytes(path);
        }

        public byte[] Crop(byte[] image, int x, int y, int widht, int height)
        {

            using Android.Graphics.Bitmap bm = Android.Graphics.BitmapFactory.DecodeFile($"mnt/sdcard/Pictures/Screenshots/{this.DeviceName}.png");
            using Android.Graphics.Bitmap new_bm = Android.Graphics.Bitmap.CreateBitmap(bm, x, y, widht, height);

            using MemoryStream ms = new();
            new_bm.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, ms);

            return ms.ToArray();

        }

        private void Tap(int x, int y, int rangeX, int rangeY)
        {
            int realX = x + this.rand.Next(1, rangeX);
            int realY = y + this.rand.Next(1, rangeY);

            inst.SendPointerSync(Android.Views.MotionEvent.Obtain(Android.OS.SystemClock.UptimeMillis(), Android.OS.SystemClock.UptimeMillis(),
                    Android.Views.MotionEventActions.Down, realX, realY, 0));
            inst.SendPointerSync(Android.Views.MotionEvent.Obtain(Android.OS.SystemClock.UptimeMillis(), Android.OS.SystemClock.UptimeMillis(),
                    Android.Views.MotionEventActions.Up, realX, realY, 0));
        }

        public string GetShipName(int sleep)
        {
            this.Tap(250, 250, 1, 1);
            // this.Tap(7, 18, 40, 25);
            Thread.Sleep(sleep);
            this.Tap(110, 135, 50, 50);
            Thread.Sleep(5);

            byte[] img = this.GetScreen();
            byte[] cropImg = this.Crop(img, 4, 164, 182, 34);

            var res = (this._ocr.GetResponse(cropImg))[0]["text"].ToString();

            this.Tap(924, 31, 2, 2);

            if (res.Contains("回"))
            {
                return "回旋者级";
            }
            if (res.Contains("获"))
            {
                return "猎获级";
            }
            if (res.Contains("想级二"))
            {
                return "妄想级二型";
            }
            if (res.Contains("想"))
            {
                return "妄想级";
            }
            if (res.Contains("逆"))
            {
                return "逆戟鲸级";
            }

            return res;
        }
    }
}
