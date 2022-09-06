


using EveMagic.Data.Adb;
using EveMagic.Data.Ocr;
using Monitor = EveMagic.Data.Monitor.Monitor;

namespace EveMagic;


public partial class MainPage : ContentPage
{
    EveMagicOcr ocr = new(new HttpClient(), "http://192.168.1.224:8501");

    public MainPage()
	{
		InitializeComponent();

        // Permissions.RequestAsync<Permissions.StorageRead>().Wait();
        // Permissions.RequestAsync<Permissions.StorageWrite>().Result;

    }


    async void OnOn(object o, EventArgs e)
    {
        AdbSocket adb = new("127.0.0.1", 5037);
        Monitor monitor = new("kuanggong1", adb, ocr, "192.168.1.3:5667");
        monitor.GetScreen();
        byte[] byt = monitor.Crop(10, 20, 100, 200);
        File.WriteAllBytes("/EveMagic/kuanggong1.png", byt);
        img.Source = ImageSource.FromFile("/EveMagic/kuanggong1.png");
        return;


        // img.Source = ImageSource.FromFile(fname);
        return;
        // byte[] file = await File.ReadAllBytesAsync("/sdcard/DCIM/Alipay/1111.jpg");
        // byte[] file = await File.ReadAllBytesAsync("C:/Users/sugob/Desktop/simpleSystem.png");
        // string res = await ocr.Request(file);
        // lab.Text = res;
    }
}

