

using EveMagic.Data.Adb;
using EveMagic.Data.Ocr;


namespace EveMagic.Data
{
    public class Monitor
    {

        public AdbSocket Adb;
        ICloudOcr _ocr;
        public string DeviceAddress { get; set; }
        public string DeviceName { get; set; }


        public Monitor(string deviceName, AdbSocket adb, ICloudOcr ocr)
        {
            this.Adb = adb;
            this._ocr = ocr;

            this.DeviceAddress = adb.Address;
            this.DeviceName = deviceName;
        }
    }
}
