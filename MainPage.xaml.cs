


using Android.AccessibilityServices;
using Android.Views;
using Android.Views.Accessibility;
using EveMagic.Data.Ocr;
using Java.IO;
using Monitor = EveMagic.Data.Monitor.Monitor;

namespace EveMagic;

class MyAccessibilityService : AccessibilityService
{
    public override void OnAccessibilityEvent(AccessibilityEvent? accessibilityEvent)
    {
    }

    public override void OnInterrupt()
    {
    }
}


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
        SurfaceControl.Screenshot();

        return;
        Thread.Sleep(7000);
        HttpClient httpClient = new();
        IinsideOcr iinsideOcr = new(httpClient);
        CloudOcr cloudOcr = new(httpClient, iinsideOcr);

        Monitor monitor = new("yvjingji1", cloudOcr);
        Stream stream = monitor.GetScreen().Result;

        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);

        //byte[] by = monitor.IfHaveEnemy(bytes, "", "");
        //MemoryStream memoryStream = new(by);
        byte[] by = monitor.Crop(bytes, 200, 200, 100, 100);
        MemoryStream memoryStream = new(by);
        img.Source = ImageSource.FromStream(() => memoryStream);


        // img.Source = ImageSource.FromFile(fname);
        return;
        // byte[] file = await File.ReadAllBytesAsync("/sdcard/DCIM/Alipay/1111.jpg");
        // byte[] file = await File.ReadAllBytesAsync("C:/Users/sugob/Desktop/simpleSystem.png");
        // string res = await ocr.Request(file);
        // lab.Text = res;
    }
}

