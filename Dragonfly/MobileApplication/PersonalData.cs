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
    [Activity(Label = "@string/personal_data", Theme = "@style/Dragonfly")]
    public class PersonalData : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Personal_Data);
            var currentUser = Login.GetUser(); //this gets 'Marca' from login class

            MySqlConnection myConn = new MySqlConnection(Resources.GetString(Resource.String.myConnectionString));
            try
            {

                if (myConn.State == System.Data.ConnectionState.Closed)
                {
                    myConn.Open(); // connection timeout is set to 60 ,check strings.xml
                }
                string SqlCommand = "SELECT Marca,Nume,Divizia,Acces FROM Angajati WHERE marca=\"" + currentUser+"\"";
                MySqlCommand cmd = new MySqlCommand(SqlCommand, myConn);

                var queryResult = cmd.ExecuteReader();
                queryResult.Read();
                var marca = queryResult.GetString(0);
                var nume = queryResult.GetString(1);
                var divizie = queryResult.GetString(2);
                var tip_acces = queryResult.GetString(3);

                Employee myself = new Employee(marca, nume, divizie, tip_acces);
                DisplayData(myself);
                cmd.Dispose();
                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to database, error " + ex.ToString());
                Toast.MakeText(ApplicationContext, "Error retrieving data!", ToastLength.Long).Show();

                
            }
            finally
            {
                myConn.Close();
            }
        }

        private void DisplayData(Employee employee)
        {
            var txtName = FindViewById<TextView>(Resource.Id.txtName);
            var txtAccess = FindViewById<TextView>(Resource.Id.txtAccess);
            var txtDivision = FindViewById<TextView>(Resource.Id.txtDivision);
            var txtBadge = FindViewById<TextView>(Resource.Id.txtBadge);

            txtName.Text = employee.Name;
            txtAccess.Text = employee.Access;
            txtBadge.Text = employee.Badge;
            txtDivision.Text = employee.Division;
        }
    }
}