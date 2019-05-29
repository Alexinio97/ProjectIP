using System;
using System.Collections.Generic;
using System.IO;
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
    [Activity(Label = "WorkingHours", Theme = "@style/Dragonfly")]
    public class WorkingHours : Activity
    {
        int total_ore;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Working_Hours);
            // Create your application here
            var currentUser = Login.GetUser(); //this gets 'Marca' from login class
            DateTime today = DateTime.Today;
            DateTime yesterday = DateTime.Today.AddDays(-1);
            
            
            

            MySqlConnection myConn = new MySqlConnection(Resources.GetString(Resource.String.myConnectionString));
            try
            {

                if (myConn.State == System.Data.ConnectionState.Closed)
                {
                    myConn.Open(); // connection timeout is set to 60 ,check strings.xml
                }

                // getting total hours yesterday
                string total_oreSql = "SELECT Total_Ore FROM Ore_Munca WHERE marca=\"" + currentUser + "\" AND Data=\"" + yesterday.ToString("yyyy-MM-dd") + "\"";
                MySqlCommand cmdTotal_ore = new MySqlCommand(total_oreSql, myConn);

                var totalResult = cmdTotal_ore.ExecuteReader();
                if (totalResult.HasRows)
                {
                    if (totalResult.Read())
                    {
                        var total_ore_ieri = totalResult.GetInt32(0);
                        total_ore += total_ore_ieri;
                    }
                }
                totalResult.Close();
                cmdTotal_ore.Dispose();

                string SqlCommand = "SELECT Ora_intrare,Ora_iesire,Total_Ore FROM Ore_Munca WHERE marca=\"" + currentUser + "\" AND Data=\"" + today.ToString("yyyy-MM-dd") + "\"";
                MySqlCommand cmd = new MySqlCommand(SqlCommand, myConn);

                var queryResult = cmd.ExecuteReader();
                if (queryResult.HasRows)
                {
                    if (queryResult.Read())
                    {
                        var ora_intrare = queryResult.GetString(0);
                        var ora_iesire = queryResult.GetString(1);


                        TimeSpan hoursPerDay = DateTime.Parse(ora_iesire).Subtract(DateTime.Parse(ora_intrare));
                        if (yesterday.Month < today.Month)
                        {
                            total_ore = 0;
                            total_ore = hoursPerDay.Hours;
                        }
                        else
                        {
                            total_ore += hoursPerDay.Hours;
                        }
                        //write to the cache file
                        Display_Data(total_ore, hoursPerDay);
                        
                        //insert total hours
                        


                    }
                }
                queryResult.Close();
                string InsertSql = "Update Ore_Munca SET total_ore=@total_ore Where marca=\"" + currentUser + "\" AND Data=\"" + today.ToString("yyyy-MM-dd") + "\"";
                MySqlCommand cmdInsert = new MySqlCommand(InsertSql, myConn);
                cmdInsert.Parameters.AddWithValue("@total_ore", total_ore);
                cmdInsert.ExecuteNonQuery();
                cmdInsert.Dispose();

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
        

            private void Display_Data(int total_ore, TimeSpan hoursPerDay)
            {
                var txtOre_azi = FindViewById<TextView>(Resource.Id.txttoday_hours);
                var txtTotal_ore = FindViewById<TextView>(Resource.Id.txtmonth_hours);

                txtOre_azi.Text = hoursPerDay.ToString(@"hh\:mm"); 
                txtTotal_ore.Text = Convert.ToString(total_ore);
        }

        }
    }

