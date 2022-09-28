

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
        Thread.Sleep(5000);
        CnOcr ocr = new(new HttpClient(), "http://192.168.1.6:8501");
        CloudOcr cloudOcr = new(new HttpClient(), ocr);
        Data.Monitor monitor = new("test1", "emulator-5556", cloudOcr);
        Data.Behavior behavior = new("test1", "emulator-5556", cloudOcr);
        string res = "";
        byte[] byt = monitor.GetScreen();
        new Thread(new ThreadStart(() => res = behavior.GetShipName(3000))).Start();
        lab.Text = res;
        

        /*CnOcr ocr = new(new HttpClient(), "http://192.168.1.6:8501");
        CloudOcr cloudOcr = new(new HttpClient(), ocr);
        Data.Monitor monitor = new("test1", "emulator-5556", cloudOcr);
        byte[] byt = monitor.GetScreen();
        var jobj = cloudOcr.GetResponse(byt);
        lab.Text = jobj[0]["position"].ToString();
        MemoryStream ms = new(byt);
        img.Source = ImageSource.FromStream(() => ms);
        return;*/
    }
}