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
    class WorkingHours : Employee
    {
        private DateTime startTime;
        private DateTime exitTime;
        private DateTime workingDay;
        private bool checkIn;

        public WorkingHours(DateTime startTime, DateTime exitTime, DateTime workingDay, bool checkIn, string accessType, string badge, string firstName, string lastName) : 
            base(accessType,badge,firstName,lastName)
        {
            this.startTime = startTime;
            this.exitTime = exitTime;
            this.workingDay = workingDay;
            this.checkIn = checkIn;
        }

        // setters and getters
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime ExitTime { get => exitTime; set => exitTime = value; }
        public DateTime WorkingDay { get => workingDay; set => workingDay = value; }
        public bool CheckIn { get => checkIn; set => checkIn = value; }
    }
}