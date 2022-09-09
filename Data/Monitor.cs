#if ANDROID
using Android.Graphics;
#endif

using EveMagic.Data.Ocr;
using System.Diagnostics;
using System.Drawing;
using Image = System.Drawing.Image;

namespace EveMagic.Data
{
    public class Monitor
    {
        CloudOcr _ocr;
        public string DeviceName { get; set; }
        public string DeviceAddress { get; set; }

        string _path = "C:/EveMagic";


        public Monitor(string deviceName, string deviceAddress, CloudOcr ocr)
        {
            this._ocr = ocr;
            this.DeviceName = deviceName;
            this.DeviceAddress = deviceAddress;

        }

        public string AdbCommand(string command)
        {
            string cmd = $"{this._path}/adb/adb.exe";

            using Process p = new();
            p.StartInfo.FileName = cmd;
            p.StartInfo.Arguments = command;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string res = p.StandardOutput.ReadToEnd();

            return res;
        }

#if ANDROID
        public async Task<Stream> GetScreen()
        {

            if (Screenshot.IsCaptureSupported)
            {
                IScreenshotResult screen = await Screenshot.CaptureAsync();
                Stream stream = await screen.OpenReadAsync();
                return stream;
            }
            return null;


        }
#else

        public void GetScreen()
        {
            string file = $"C:/EveMagic/{this.DeviceName}.png";

            this.AdbCommand($"-s {this.DeviceAddress} exec-out screencap -p > {file}");
        }
#endif

#if ANDROID
        public byte[] Crop(byte[] image, int x, int y, int widht, int height)
        {

            using Android.Graphics.Bitmap bm = BitmapFactory.DecodeByteArray(image, 0, image.Length);
            using Android.Graphics.Bitmap new_bm = Android.Graphics.Bitmap.CreateBitmap(bm, x, y, widht, height);

            using MemoryStream ms = new();
            new_bm.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, ms);

            return ms.ToArray();

        }
#else
        public byte[] Crop(byte[] image, int x1, int y1, int width, int height)
        {
            Rectangle region = new(x1, y1, width, height);
            using Bitmap result = new(region.Width, region.Height);
            using Graphics graphics = Graphics.FromImage(result);
            using MemoryStream memoryStream = new(image);
            using Image img = Image.FromStream(memoryStream);
            graphics.DrawImage(img, new Rectangle(x1, y1, region.Width, region.Height), region, GraphicsUnit.Pixel);
            using MemoryStream ms = new();
            result.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }
#endif

        public byte[] IfHaveEnemy(byte[] image, string num1, string num2)
        {
            byte[] red = this.Crop(image, 854, 509, 36, 20);
            string res = this._ocr.GetResponse(red);
            return red;
        }
    }
}
