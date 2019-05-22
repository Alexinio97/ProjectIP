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
        static string user;
        static string password;
       
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

            

            MySqlConnection myConn = new MySqlConnection(Resources.GetString(Resource.String.myConnectionString));
            try
            {
                
                if(myConn.State == System.Data.ConnectionState.Closed)
                {
                    myConn.Open(); // connection timeout is set to 60 ,check strings.xml
                }
                string SqlCommand = "SELECT Marca,Cod_Acces FROM Angajati WHERE marca=\""+ inputUser + "\" AND Cod_Acces=\"" + inputPass + "\"";
                MySqlCommand cmd = new MySqlCommand(SqlCommand,myConn);

                var queryResult = cmd.ExecuteReader();
                queryResult.Read();
                user = queryResult.GetString(0);
                password = queryResult.GetString(1);
                Console.WriteLine("User = " + user + " Password= " + password);

                cmd.Dispose();
                Toast.MakeText(ApplicationContext, "Succes", ToastLength.Short).Show();
                StartActivity(typeof(MainActivity));
                Finish(); // this won't allow the user to go back to the login form if he logged in
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to database, error " + ex.ToString());
                Toast.MakeText(ApplicationContext, "Incorrect username or password", ToastLength.Long).Show();

                FindViewById<EditText>(Resource.Id.txtUser).Text = "";
                FindViewById<EditText>(Resource.Id.txtPassword).Text = "";
            }
            finally
            {
                myConn.Close();
            }
        }

        public static string GetUser() // with this we could share 'marca' to other c# files
        {
            return user;
        }

        public static string GetPass() // get the access code for bluetooth access
        {
            return password;
        }
    }
}