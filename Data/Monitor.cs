
using Android.Graphics;

using EveMagic.Data.Adb;
using EveMagic.Data.Ocr;


namespace EveMagic.Data.Monitor
{
    public class Monitor
    {

        public AdbSocket Adb;
        CloudOcr _ocr;
        public string AdbSocketAddress { get; set; }
        public string DeviceName { get; set; }
        public string DeviceAddress { get; set; }


        public Monitor(string deviceName, AdbSocket adb, CloudOcr ocr, string deviceAddress)
        {
            this.Adb = adb;
            this._ocr = ocr;

            this.AdbSocketAddress = $"{adb.Address}:{adb.Port}";
            this.DeviceName = deviceName;
            this.DeviceAddress = deviceAddress;

            this.Adb.SendCommand($"host:transport:192.168.1.3:5667");
        }

        public byte[] GetScreen()
        {
            this.Adb.SendCommand($"exec:screencap '-p'");
            string response = "DATA";
            var total = 0;
            var bytes = new byte[65536];
            using MemoryStream memoryStream = new();

            while (true)
            {
                if (response.Equals("CLSE"))
                {
                    break;
                }

                var size = this.Adb.ReadInt32();
                this.Adb.Read(bytes, size);
                memoryStream.Write(bytes);
                total += size;

                response = this.Adb.ReadString(4);
            }

            return memoryStream.ToArray();


            // this.Adb.SendCommand($"exec-out:screencap -p > /sdcard/DCIM/EveMagic/{this.DeviceName}.png");
            //this.Adb.SendCommand($"host:transport:{this.DeviceAddress}");
            //string path; ;
            //path = "/sdcard/DCIM/EveMagic";

            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            //this.Adb.SendCommand($"shell,v2,TERM=xterm-256color,raw:screencap -p {path}/{this.DeviceName}.png");
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
