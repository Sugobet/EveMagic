
using EveMagic.Data;
using EveMagic.Data.Ocr;

namespace EveMagic;


public partial class MainPage : ContentPage
{        
    int i = 1;

    public MainPage()
	{

		InitializeComponent();

        // Permissions.RequestAsync<Permissions.StorageRead>().Wait();
        // Permissions.RequestAsync<Permissions.StorageWrite>().Result;

    }


    async void OnOn(object o, EventArgs e)
    {
        IinsideOcr ocr = new(new HttpClient());
        CloudOcr cloudOcr = new(new HttpClient(), ocr);
        Data.Monitor.Monitor monitor = new("test1", cloudOcr);
        lab.Text = $"{monitor.AdbCommand("devices")}    {++i}";

        // img.Source = ImageSource.FromFile(fname);
        return;
        // byte[] file = await File.ReadAllBytesAsync("/sdcard/DCIM/Alipay/1111.jpg");
        // byte[] file = await File.ReadAllBytesAsync("C:/Users/sugob/Desktop/simpleSystem.png");
        // string res = await ocr.Request(file);
        // lab.Text = res;
    }
}

