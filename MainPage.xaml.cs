


using Android.AccessibilityServices;
using Android.Views.Accessibility;
using EveMagic.Data.Adb;
using EveMagic.Data.Ocr;
using System;
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


        // img.Source = ImageSource.FromFile(fname);
        return;
        // byte[] file = await File.ReadAllBytesAsync("/sdcard/DCIM/Alipay/1111.jpg");
        // byte[] file = await File.ReadAllBytesAsync("C:/Users/sugob/Desktop/simpleSystem.png");
        // string res = await ocr.Request(file);
        // lab.Text = res;
    }
}

