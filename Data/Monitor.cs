
using EveMagic.Data.Ocr;


namespace EveMagic.Data
{
    public class Monitor
    {
        CloudOcr _ocr;
        Java.Lang.Process _process;
        Java.IO.PrintStream _outputStream;

        public string DeviceName { get; set; }
        public string DeviceAddress { get; set; }

        public Monitor(string deviceName, string deviceAddress, CloudOcr ocr)
        {
            this._ocr = ocr;
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

            using Android.Graphics.Bitmap bm = Android.Graphics.BitmapFactory.DecodeByteArray(image, 0, image.Length);
            using Android.Graphics.Bitmap new_bm = Android.Graphics.Bitmap.CreateBitmap(bm, x, y, widht, height);

            using MemoryStream ms = new();
            new_bm.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, ms);

            return ms.ToArray();

        }


        public byte[] IfHaveEnemy(byte[] image, string num1, string num2)
        {
            byte[] red = this.Crop(image, 854, 509, 36, 20);
            string res = this._ocr.GetResponse(red);
            return red;
        }
    }
}
