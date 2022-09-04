

using EveMagic.Data.Adb;
using EveMagic.Data.Ocr;

namespace EveMagic.Data
{
    public class Monitor
    {
        AdbSocket _adb;
        ICloudOcr _ocr;


        public Monitor(AdbSocket adb, ICloudOcr ocr)
        {
            this._adb = adb;
            this._ocr = ocr;
        }
    }
}
