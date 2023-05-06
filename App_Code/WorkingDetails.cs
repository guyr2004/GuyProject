using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuyProject.App_Code
{
    public class WorkingDetails
    {
        private string _teacherID;
        private string _DayOfWeek;
        private TimeSpan _starthour;
        private TimeSpan _endHour;

        public string TeacherID
        {
            get { return this._teacherID; }
            set { this._teacherID = value; }
        }

        public string DayOfWeek
        {
            get { return this._DayOfWeek; }
            set { this._DayOfWeek = value; }
        }
        public TimeSpan StartHour
        {
            get { return this._starthour; }
            set { this._starthour = value; }
        }
        public TimeSpan EndHour
        {
            get { return this._endHour; }
            set { this._endHour = value; }
        }

        public WorkingDetails()
        {

        }
            
        public WorkingDetails(string teacherID, string dayofweek, TimeSpan starthour, TimeSpan endhour)
        {
            this._teacherID = teacherID;
            this._DayOfWeek = dayofweek;
            this._starthour = starthour;
            this._endHour = endhour;
        }
    }
}