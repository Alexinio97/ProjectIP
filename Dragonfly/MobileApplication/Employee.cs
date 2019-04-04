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
        private int _accessCode;
        private string _badge;
        private string _firstName;
        private string _lastName;

        public Employee(int accessCode, string badge, string firstName, string lastName)
        {
            _accessCode = accessCode;
            _badge = badge;
            _firstName = firstName;
            _lastName = lastName;
        }

        public int AccessCode { get => _accessCode; set => _accessCode = value; }
        public string Badge { get => _badge; set => _badge = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
    }
}