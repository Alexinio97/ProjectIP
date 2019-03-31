using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace MobileApplication
{
    [Activity(Label = "@string/app_name", Theme = "@style/Dragonfly")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
           
            FindViewById<Button>(Resource.Id.btnCheck).Click += Check_Data;

            FindViewById<Button>(Resource.Id.btnPersonalData).Click += View_Data;

        }

        private void View_Data(object sender, EventArgs e)
        {
            // open personal data view activity
            StartActivity(typeof(PersonalData));
        }

        private void Check_Data(object sender, EventArgs e)
        {
            // showing a random message
            Toast.MakeText(this.ApplicationContext, "Access granted", ToastLength.Short).Show();

        }
        
    }
}