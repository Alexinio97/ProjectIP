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
        static string user; // we'll use user and password to other activities
        static string password;
        static string numberCar; // check if the user has a car registered

        static string nume;
        static string prenume;
        static string cnp;
        static string divizie;
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
                queryResult.Close();
                cmd.Dispose();
                
                // here we will se if the user has a car and store it
                string sqlCommandLogin = "SELECT Nume,Prenume,CNP,Divizia FROM Angajati where marca=\"" + inputUser + "\"";
                MySqlCommand cmdLogin = new MySqlCommand(sqlCommandLogin, myConn);

                var queryResultLogin = cmdLogin.ExecuteReader();
                if (queryResultLogin.Read())
                {
                    nume = queryResultLogin.GetString(0);
                    prenume = queryResultLogin.GetString(1);
                    cnp = queryResultLogin.GetString(2);
                    divizie = queryResultLogin.GetString(3);
                }
                Toast.MakeText(ApplicationContext, "Succes", ToastLength.Short).Show();
                // close connections
                queryResultLogin.Close();
                cmdLogin.Dispose();
                
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

        public static string GetDivizie()
        {
            return divizie;
        }
        public static string GetUser() // with this we could share 'marca' to other c# files
        {
            return user;
        }

        public static string GetPass() // get the access code for bluetooth access
        {
            return password;
        }

        public static string GetNumberCar()
        {
            return numberCar;
        }

        public static string GetNume()
        {
            return nume;
        }

        public static string GetPrenume()
        {
            return prenume;
        }

        public static string GetCNP()
        {
            return cnp;
        }
    }
}