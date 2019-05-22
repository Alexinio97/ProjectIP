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
    class Employee
    {
        private string _badge; //marca
        private string _name;
        private string _division;
        private string _access;

        public Employee(string badge, string name, string division, string access)
        {
            _badge = badge;
            _name = name;
            _division = division;
            _access = access;
        }

        public string Badge { get => _badge; set => _badge = value; }
        public string Name { get => _name; set => _name = value; }
        public string Division { get => _division; set => _division = value; }
        public string Access { get => _access; set => _access = value; }
    }
}