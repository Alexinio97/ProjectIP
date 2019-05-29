using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Bluetooth;
using System.Linq;
using Java.Util;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;

namespace MobileApplication
{
    [Activity(Label = "@string/app_name", Theme = "@style/Dragonfly")]
    public class MainActivity : AppCompatActivity
    {
        string deviceName; // the device that we will connect the mobile too
        BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
        BluetoothSocket _socket = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            FindViewById<Button>(Resource.Id.btnCheck).Click += Request_AccessAsync;

            FindViewById<Button>(Resource.Id.btnPersonalData).Click += View_Data;

            FindViewById<Button>(Resource.Id.btnWorkingHours).Click += View_Hours;


        }

        private void View_Hours(object sender,EventArgs e)
        {
            StartActivity(typeof(WorkingHours));
        }

        private void View_Data(object sender, EventArgs e)
        {
            // open personal data view activity
            StartActivity(typeof(PersonalData));
        }



        private async void Request_AccessAsync(object sender, EventArgs e)
        {
            byte[] buffer = new byte[256];
            MySqlConnection myConn = new MySqlConnection(Resources.GetString(Resource.String.myConnectionString));
            if (myConn.State == System.Data.ConnectionState.Closed)
            {
                myConn.Open(); // connection timeout is set to 60 ,check strings.xml
            }

            string access_code = Login.GetPass(); //storing password in order to check the access code with arduino
            string numberCar=Login.GetNumberCar(); // this will be set  if the user has a car
            //check if bluetooth is on
            if (!(adapter.IsEnabled))
            {
                Console.WriteLine("Enabling bluetooth");
                Toast.MakeText(ApplicationContext, "Enabling bluetooth first", ToastLength.Long).Show();
                adapter.Enable();
                Thread.Sleep(2000); // if bluetooth is not up
            }

            // this will connect to bluetooth
            if (adapter == null)
                throw new Exception("No bluetooth adapter found.");
            if (!adapter.IsEnabled)
                throw new Exception("Bluetooth adapter is not enabled");
         

            BluetoothDevice device = (from bd in this.adapter.BondedDevices
                                      where bd.Name == "HC-05"
                                      select bd).FirstOrDefault();


            Console.WriteLine("\n\n" + device.Name);

            if (device == null)
                throw new Exception("Named device not found.");
            
            _socket = CreateRfcommSocket(device); // used another function for creating the socket
            Console.WriteLine(device.GetUuids().ToString());
            string InsertSql = "Update Angajat_Intrare SET Marca=@marca, Nume=@nume, Prenume=@prenume, Poza=@poza, Divizia=@divizie";
            MySqlCommand cmdInsert = new MySqlCommand(InsertSql, myConn);
            cmdInsert.Parameters.AddWithValue("@marca", Login.GetUser());
            cmdInsert.Parameters.AddWithValue("@nume", Login.GetNume());
            cmdInsert.Parameters.AddWithValue("@prenume", Login.GetPrenume());
            cmdInsert.Parameters.AddWithValue("@poza", null);
            cmdInsert.Parameters.AddWithValue("@divizie", Login.GetDivizie());
            cmdInsert.ExecuteNonQuery();
            cmdInsert.Dispose();
            try
            {
                await _socket.ConnectAsync();
                

                Toast.MakeText(ApplicationContext, "Connected with bluetooth", ToastLength.Short).Show();
                // Write data to the device
                await _socket.OutputStream.WriteAsync(Encoding.ASCII.GetBytes(access_code));  //send Marca
                

                Console.WriteLine("Access code sent...");

                //var receivedString = Encoding.UTF8.GetString(buffer); //convert the bytes received to string

                //Console.WriteLine($"Converted string is {receivedString}");
                //if(receivedString.Equals("Yes"))
                //    Toast.MakeText(ApplicationContext, "Access Granted", ToastLength.Short).Show(); //arduino will lift the barrier

            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, "couldn't connect with bluetooth", ToastLength.Short).Show();
                Console.WriteLine("\n\n Error is -- " + ex.ToString() + "\n\n");
                // this is the function that overwrites the socket in case of failure
            }
            finally
            {
                adapter.Disable(); // disable bluetooth
                _socket.Close(); //closing communication socket
            }
        }
            BluetoothSocket CreateRfcommSocket(BluetoothDevice bTdevice)
            { // This is an "undocumented" call that is needed to (mostly) avoid a Bluetooth Connection error
              // introduced in Android v4.2 and higher. It is used as a "fallback" connection.
              // Full paths version of code!
              //Java.Lang.Reflect.Method mi = device.Class.GetMethod("createRfcommSocket", new Java.Lang.Class[] { Java.Lang.Integer.Type });
              //_bluetoothSocket = (BluetoothSocket)mi.Invoke(device, 1);

                // Compact version of above
                var mi = bTdevice.Class.GetMethod("createRfcommSocket", Java.Lang.Integer.Type);
                return (BluetoothSocket)mi.Invoke(bTdevice, 1);
            }

        }
    }