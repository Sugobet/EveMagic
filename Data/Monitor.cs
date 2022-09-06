
#if ANDROID
using Android.Graphics;
#endif

using EveMagic.Data.Adb;
using EveMagic.Data.Ocr;


namespace EveMagic.Data.Monitor
{
    public class Monitor
    {

        public AdbSocket Adb;
        ICloudOcr _ocr;
        public string AdbSocketAddress { get; set; }
        public string DeviceName { get; set; }
        public string DeviceAddress { get; set; }


        public Monitor(string deviceName, AdbSocket adb, ICloudOcr ocr, string deviceAddress)
        {
            this.Adb = adb;
            this._ocr = ocr;

            this.AdbSocketAddress = $"{adb.Address}:{adb.Port}";
            this.DeviceName = deviceName;
            this.DeviceAddress = deviceAddress;
        }

        public void GetScreen()
        {
            // this.Adb.SendCommand($"exec-out:screencap -p > /sdcard/DCIM/EveMagic/{this.DeviceName}.png");
            this.Adb.SendCommand($"host:transport:{this.DeviceAddress}");
            string path; ;
#if ANDROID
            path = "/sdcard/DCIM/EveMagic";
#else
            path = "/EveMagic";
#endif

            // !!!!!!!!!!!!!!!!!!! Have a bug
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            this.Adb.SendCommand($"shell,v2,TERM=xterm-256color,raw:screencap -p {path}/{this.DeviceName}.png");
        }

        public byte[] Crop(int x, int y, int widht, int height)
        {
#if ANDROID
            using Bitmap bm = BitmapFactory.DecodeFile($"/sdcard/DCIM/EveMagic/{this.DeviceName}.png");
            using Bitmap new_bm = Bitmap.CreateBitmap(bm, x, y, widht, height);

            MemoryStream ms = new();
            new_bm.Compress(Bitmap.CompressFormat.Png, 100, ms);

            return ms.ToArray();
#else
            System.Drawing.Rectangle cropRegion = new(x, y, widht, height);
            System.Drawing.Image originImage = System.Drawing.Image.FromFile($"/EveMagic/{this.DeviceName}.png");
            System.Drawing.Bitmap result = new(widht, height);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(result);
            graphics.DrawImage(originImage, new System.Drawing.Rectangle(0, 0, widht, height), cropRegion, System.Drawing.GraphicsUnit.Pixel);

            using MemoryStream memoryStream = new();
            result.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
            
            originImage.Dispose();
            result.Dispose();

            return memoryStream.ToArray();

#endif
        }
    }
}
