#if ANDROID
using Android.Graphics;
#endif

using EveMagic.Data.Ocr;
using System.Diagnostics;

namespace EveMagic.Data.Monitor
{
    public class Monitor
    {
        CloudOcr _ocr;
        public string DeviceName { get; set; }


        public Monitor(string deviceName, CloudOcr ocr)
        {
            this._ocr = ocr;
            this.DeviceName = deviceName;

        }

        public string AdbCommand(string command)
        {
            string cmd = "/EveMagic/adb/adb.exe";

            using Process p = new Process();
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
#endif

#if ANDROID
        public byte[] Crop(byte[] image, int x, int y, int widht, int height)
        {

            using Bitmap bm = BitmapFactory.DecodeByteArray(image, 0, image.Length);
            using Bitmap new_bm = Bitmap.CreateBitmap(bm, x, y, widht, height);

            using MemoryStream ms = new();
            new_bm.Compress(Bitmap.CompressFormat.Png, 100, ms);

            return ms.ToArray();

        }
#endif

        public bool IfHaveEnemy(byte[] image, string num1, string num2)
        {
#if ANDROID
            // Will to do
            byte[] red = this.Crop(image, 854, 509, 36, 20);
            string res = this._ocr.GetResponse(red);
            return false;
#endif
            return false;
        }
    }
}
