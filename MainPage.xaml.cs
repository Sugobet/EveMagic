


using Android.AccessibilityServices;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Views.Accessibility;
using EveMagic.Data.Adb;
using EveMagic.Data.Ocr;
using Java.Lang;
using Kotlin.Jvm.Internal;
using System;
using Monitor = EveMagic.Data.Monitor.Monitor;
using Thread = System.Threading.Thread;

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
    int val = 0;

    public MainPage()
	{
		InitializeComponent();

        // Permissions.RequestAsync<Permissions.StorageRead>().Wait();
        // Permissions.RequestAsync<Permissions.StorageWrite>().Result;

    }


    async void OnOn(object o, EventArgs e)
    {
        HttpClient httpClient = new();
        IinsideOcr iinsideOcr = new(httpClient);
        CloudOcr cloudOcr = new(httpClient, iinsideOcr);

        Monitor monitor = new("yvjingji1", cloudOcr);
        Stream stream = await monitor.GetScreen();

        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);

        monitor.IfHaveEnemy(bytes, "", "");


        // img.Source = ImageSource.FromFile(fname);
        return;
        // byte[] file = await File.ReadAllBytesAsync("/sdcard/DCIM/Alipay/1111.jpg");
        // byte[] file = await File.ReadAllBytesAsync("C:/Users/sugob/Desktop/simpleSystem.png");
        // string res = await ocr.Request(file);
        // lab.Text = res;
    }
}

