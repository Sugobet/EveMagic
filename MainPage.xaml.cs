

using EveMagic.Data.Ocr;

namespace EveMagic;


public partial class MainPage : ContentPage
{
    EveMagicOcr ocr = new(new HttpClient(), "http://192.168.1.8:8888");

    public MainPage()
	{
		InitializeComponent();

        // Permissions.RequestAsync<Permissions.StorageRead>().Wait();
        // Permissions.RequestAsync<Permissions.StorageWrite>().Wait();
    }


    async void OnOn(object o, EventArgs e)
    {
        byte[] file = await File.ReadAllBytesAsync("/sdcard/DCIM/Alipay/1111.jpg");
        // byte[] file = await File.ReadAllBytesAsync("C:/Users/sugob/Desktop/simpleSystem.png");
        string res = await ocr.Request(file);
        lab.Text = res;
    }
}

