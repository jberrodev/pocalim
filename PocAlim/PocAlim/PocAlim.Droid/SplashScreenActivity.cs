using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Threading.Tasks;

namespace PocAlim.Droid
{
	[Activity (Theme = "@style/MyTheme.Splash", MainLauncher = true, Icon = "@drawable/splash_logo")]
	public class SplashScreenActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashScreenActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() => {
                Task.Delay(5000);  // Simulate a bit of startup work.
            });

            startupWork.ContinueWith(t => {
                StartActivity(new Intent(Application.Context, typeof(MapActivity)));
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
    }
}


