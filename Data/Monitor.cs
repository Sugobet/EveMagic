
using EveMagic.Data.Ocr;
using Newtonsoft.Json.Linq;

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

            using Android.Graphics.Bitmap bm = Android.Graphics.BitmapFactory.DecodeFile("mnt/sdcard/Pictures/Screenshots/test1.png");
            using Android.Graphics.Bitmap new_bm = Android.Graphics.Bitmap.CreateBitmap(bm, x, y, widht, height);

            using MemoryStream ms = new();
            new_bm.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, ms);

            return ms.ToArray();

        }


        public Tuple<bool, string, string> LocalHaveEnemy(byte[] image, string num1, string num2)
        {
            byte[] red = this.Crop(image, 854, 509, 36, 20);
            var res = this._ocr.GetResponse(red);
            string redNum = "";
            foreach (var v in res)
            {
                redNum = (v["text"].ToString()).Replace('o', '0').Replace('O', '0').Replace('D', '0').Replace('U', '0').Replace("口", "0");
                break;
            }
            if (redNum != num1)
            {
                return new Tuple<bool, string, string>(true, redNum, num2);
            }

            byte[] white = this.Crop(image, 916, 509, 36, 20);
            var res1 = this._ocr.GetResponse(white);
            string whiteNum = "";
            foreach (var v in res1)
            {
                whiteNum = (v["text"].ToString()).Replace('o', '0').Replace('O', '0').Replace('D', '0').Replace('U', '0').Replace("口", "0");
                break;
            }
            if (whiteNum != num2)
            {
                return new Tuple<bool, string, string>(true, num1, whiteNum);
            }

            return new Tuple<bool, string, string>(false, num1, num2);
        }
    }
}
