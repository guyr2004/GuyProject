using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuyProject.App_Code
{
    public class AbsenceTeacherDetails
    {
        private string _teacherID;
        private DateTime _absencedate;
        private TimeSpan _starthour;
        private TimeSpan _endHour;

        public string TeacherID
        {
            get { return this._teacherID; }
            set { this._teacherID = value; }
        }

        public DateTime AbsenceDate
        {
            get { return this._absencedate; }
            set { this._absencedate = value; }
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

        public AbsenceTeacherDetails()
        {

        }

        public AbsenceTeacherDetails(string teacherID, DateTime absencedate, TimeSpan starthour, TimeSpan endhour)
        {
            this._teacherID = teacherID;
            this._absencedate = absencedate;
            this._starthour = starthour;
            this._endHour = endhour;
        }
    }
}