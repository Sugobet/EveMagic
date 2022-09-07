


using EveMagic.Data.Adb;
using EveMagic.Data.Ocr;
using Monitor = EveMagic.Data.Monitor.Monitor;

namespace EveMagic;


public partial class MainPage : ContentPage
{

    public MainPage()
	{
		InitializeComponent();

        // Permissions.RequestAsync<Permissions.StorageRead>().Wait();
        // Permissions.RequestAsync<Permissions.StorageWrite>().Result;

    }


    async void OnOn(object o, EventArgs e)
    {
        IinsideOcr ocr = new(new HttpClient());
        AdbSocket adb = new("127.0.0.1", 5037);
        CloudOcr cloudOcr = new(new HttpClient(), ocr);
        Monitor monitor = new("kuanggong1", adb, cloudOcr, "192.168.1.3:5667");
        monitor.GetScreen();
        byte[] image = File.ReadAllBytes("/sdcard/DCIM/EveMagic/kuanggong1.png");
        byte[] byt = monitor.Crop(image, 10, 20, 100, 200);
        string str = cloudOcr.GetResponse(byt).Result;
        lab.Text = str;
        MemoryStream stream = new(byt);
        img.Source = ImageSource.FromStream(()=>stream);
        return;


        // img.Source = ImageSource.FromFile(fname);
        return;
        // byte[] file = await File.ReadAllBytesAsync("/sdcard/DCIM/Alipay/1111.jpg");
        // byte[] file = await File.ReadAllBytesAsync("C:/Users/sugob/Desktop/simpleSystem.png");
        // string res = await ocr.Request(file);
        // lab.Text = res;
    }
}

