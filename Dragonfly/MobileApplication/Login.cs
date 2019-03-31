using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileApplication
{
    [Activity(Label = "Login",Theme = "@style/Dragonfly", MainLauncher = true)]
    public class Login : Activity
    {
        const string user = "Alex";
        const string password = "alex";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);

            FindViewById<Button>(Resource.Id.btnLogin).Click += Login_Click;
        }

        private void Login_Click(object sender, EventArgs e)
        {
            var inputUser = FindViewById<EditText>(Resource.Id.txtUser).Text;
            var inputPass = FindViewById<EditText>(Resource.Id.txtPassword).Text;

            if(inputUser.Equals(user) && inputPass.Equals(password))
            {
                Toast.MakeText(ApplicationContext, "Succes", ToastLength.Short).Show();
                StartActivity(typeof(MainActivity));
                Finish(); // this won't allow the user to go back to the login form if he logged in
            }
            else
            {
                Toast.MakeText(ApplicationContext, "Incorrect username or password", ToastLength.Short).Show();
                FindViewById<EditText>(Resource.Id.txtUser).Text = "";
                FindViewById<EditText>(Resource.Id.txtPassword).Text="";
            }
        }
    }
}