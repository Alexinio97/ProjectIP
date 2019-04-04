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
    [Activity(Label = "@string/personal_data", Theme = "@style/Dragonfly")]
    public class PersonalData : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Personal_Data);
            Employee myself = new Employee(14, "TM97", "Andricsak", "Alexandru");
            DisplayData(myself);
        }

        private void DisplayData(Employee employee)
        {
            var txtName = FindViewById<TextView>(Resource.Id.txtFirstName);
            var txtLastName = FindViewById<TextView>(Resource.Id.txtLastName);
            var txtCar = FindViewById<TextView>(Resource.Id.txtCar);
            var txtBadge = FindViewById<TextView>(Resource.Id.txtBadge);

            txtName.Text = employee.FirstName;
            txtLastName.Text = employee.LastName;
            txtBadge.Text = employee.Badge;
            txtCar.Text = "TM97LEX";
        }
    }
}