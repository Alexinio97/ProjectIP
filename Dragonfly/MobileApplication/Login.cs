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
using MySql.Data.MySqlClient;

namespace MobileApplication
{
    [Activity(Label = "Dragonfly",Theme = "@style/Dragonfly", MainLauncher = true)]
    public class Login : Activity
    {
        string user;
        string password;
        private string myConnectionString = "Server=db4free.net;" +
            "Port=3306;"+
            "database=dragonfly;" +
            "User Id=dragonfly97;Password=DragonFly123;charset=utf8;oldguids=true";
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

            
            MySqlConnection myConn = new MySqlConnection(myConnectionString);
            try
            {
                
                if(myConn.State == System.Data.ConnectionState.Closed)
                {
                    myConn.Open();
                }
                string SqlCommand = "SELECT FROM ANGAJATI WHERE marca=" + inputUser + " AND CodAcces=" + inputPass;
                MySqlCommand cmd = new MySqlCommand(SqlCommand,myConn);

                var queryResult = cmd.ExecuteReader();
                user = queryResult[1].ToString();
                password = queryResult[2].ToString();
                Console.WriteLine("User = " + user + " Password= " + password);

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to database, error " + ex.ToString());
                Toast.MakeText(ApplicationContext, "Incorrect username or password", ToastLength.Long).Show();
            }
            finally
            {
                myConn.Close();
            }


            //if(inputUser.Equals(user) && inputPass.Equals(password))
            //{
            //    Toast.MakeText(ApplicationContext, "Succes", ToastLength.Short).Show();
            //    StartActivity(typeof(MainActivity));
            //    Finish(); // this won't allow the user to go back to the login form if he logged in
            //}
            //else
            //{
            //    Toast.MakeText(ApplicationContext, "Incorrect username or password", ToastLength.Short).Show();
            //    FindViewById<EditText>(Resource.Id.txtUser).Text = "";
            //    FindViewById<EditText>(Resource.Id.txtPassword).Text="";
            //}
        }
    }
}