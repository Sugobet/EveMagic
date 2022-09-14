

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


    async void OnOn(object o, EventArgs e)
    {
        Thread t1 = new(new ThreadStart(() =>
        {
            Android.App.Instrumentation inst = new();
            inst.SendKeySync(Android.Views.KeyEvent.ChangeAction(new Android.Views.KeyEvent(1, Android.OS.SystemClock.UptimeMillis(), Android.Views.KeyEventActions.Down, Android.Views.Keycode.Power, 0), Android.Views.KeyEventActions.Down));
            //inst.SendKeySync(Android.Views.KeyEvent.ChangeAction(new Android.Views.KeyEvent(1, 0, Android.Views.KeyEventActions.Down, Android.Views.Keycode.VolumeDown, 0), Android.Views.KeyEventActions.Down));
            //inst.SendKeySync(Android.Views.KeyEvent.ChangeAction(new Android.Views.KeyEvent(Android.OS.SystemClock.UptimeMillis(), 0, Android.Views.KeyEventActions.Up, Android.Views.Keycode.VolumeDown, 0), Android.Views.KeyEventActions.Up));
            inst.SendKeySync(Android.Views.KeyEvent.ChangeAction(new Android.Views.KeyEvent(Android.OS.SystemClock.UptimeMillis(), Android.OS.SystemClock.UptimeMillis(), Android.Views.KeyEventActions.Up, Android.Views.Keycode.Power, 0), Android.Views.KeyEventActions.Up));

        }));

        Thread t2 = new(new ThreadStart(() =>
        {
            Android.App.Instrumentation inst = new();
            inst.SendKeySync(Android.Views.KeyEvent.ChangeAction(new Android.Views.KeyEvent(Android.OS.SystemClock.UptimeMillis(), Android.OS.SystemClock.UptimeMillis(), Android.Views.KeyEventActions.Down, Android.Views.Keycode.VolumeDown, 0), Android.Views.KeyEventActions.Down));
            inst.SendKeySync(Android.Views.KeyEvent.ChangeAction(new Android.Views.KeyEvent(Android.OS.SystemClock.UptimeMillis(), Android.OS.SystemClock.UptimeMillis(), Android.Views.KeyEventActions.Up, Android.Views.Keycode.VolumeDown, 0), Android.Views.KeyEventActions.Up));

        }));

        t1.Start();
        t2.Start();


        //IinsideOcr ocr = new(new HttpClient());
        //CloudOcr cloudOcr = new(new HttpClient(), ocr);
        //Data.Monitor monitor = new("test1", "emulator-5556", cloudOcr);
        //monitor.GetScreen();
        //byte[] byt = File.ReadAllBytes("/EveMagic/1.png");
        //MemoryStream ms = new(byt);
        //img.Source = ImageSource.FromStream(() => ms);
        // img.Source = ImageSource.FromFile(fname);
        return;
        // byte[] file = await File.ReadAllBytesAsync("/sdcard/DCIM/Alipay/1111.jpg");
        // byte[] file = await File.ReadAllBytesAsync("C:/Users/sugob/Desktop/simpleSystem.png");
        // string res = await ocr.Request(file);
        // lab.Text = res;
    }
}