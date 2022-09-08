
using Android.App;
using Android.Graphics;

using EveMagic.Data.Adb;
using EveMagic.Data.Ocr;


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

        public async Task<Stream> GetScreen()
        {
            if (Screenshot.Default.IsCaptureSupported)
            {
                IScreenshotResult screen = await Screenshot.Default.CaptureAsync();
                Stream stream = await screen.OpenReadAsync();
                return stream;
            }
            return null;

        }

        public byte[] Crop(byte[] image, int x, int y, int widht, int height)
        {
            using Bitmap bm = BitmapFactory.DecodeByteArray(image, 0, image.Length);
            using Bitmap new_bm = Bitmap.CreateBitmap(bm, x, y, widht, height);

            using MemoryStream ms = new();
            new_bm.Compress(Bitmap.CompressFormat.Png, 100, ms);

            return ms.ToArray();
        }


        public void IfHaveEnemy(byte[] image, string num1, string num2)
        {
            byte[] red = this.Crop(image, 854, 509, 36, 20);
            string res = this._ocr.GetResponse(red).Result;
        }
    }
}
