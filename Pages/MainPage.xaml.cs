

using EveMagic.Data;
using EveMagic.Data.Ocr;
using EveMagic.Pages;


namespace EveMagic.Pages;


public partial class MainPage : ContentPage
{        
    public MainPage()
	{

		InitializeComponent();

        // Permissions.RequestAsync<Permissions.StorageRead>().Wait();
        // Permissions.RequestAsync<Permissions.StorageWrite>().Result;
    }


    void OnOn(object o, EventArgs e)
    {

        IinsideOcr ocr = new(new HttpClient());
        CloudOcr cloudOcr = new(new HttpClient(), ocr);
        Data.Monitor monitor = new("test1", "emulator-5556", cloudOcr);
        byte[] byt = monitor.GetScreen();
        MemoryStream ms = new(byt);
        img.Source = ImageSource.FromStream(() => ms);
        return;
        // byte[] file = await File.ReadAllBytesAsync("/sdcard/DCIM/Alipay/1111.jpg");
        // byte[] file = await File.ReadAllBytesAsync("C:/Users/sugob/Desktop/simpleSystem.png");
        // string res = await ocr.Request(file);
        // lab.Text = res;
    }
}