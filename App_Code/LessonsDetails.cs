using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuyProject.App_Code
{
    public class LessonsDetails
    {
        private string _lessonID;
        private DateTime _lessonDate;
        private TimeSpan _starthour;
        private string _teacherID;
        private string _studentID;
        private int _subjectID;
        private int _levelID;
        private string _address;
        private string _status;
        private int _pricePerHour;
        private string _paymentStatus;

        public string LessonID
        {
            get { return this._lessonID; }
            set { this._lessonID = value; }
        }
        public DateTime LessonDate
        {
            get { return this._lessonDate; }
            set { this._lessonDate = value; }
        }
        public TimeSpan StartHour
        {
            get { return this._starthour; }
            set { this._starthour = value; }
        }
        public string TeacherID
        {
            get { return this._teacherID; }
            set { this._teacherID = value; }
        }
        public string StudentID
        {
            get { return this._studentID; }
            set { this._studentID = value; }
        }
        public int SubjectID
        {
            get { return this._subjectID; }
            set { this._subjectID = value; }
        }
        public int LevelID
        {
            get { return this._levelID; }
            set { this._levelID = value; }
        }
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }
        public string Status
        {
            get { return this._status; }
            set { this._status = value; }
        }
        public int PricePerHour
        {
            get { return this._pricePerHour; }
            set { this._pricePerHour = value; }
        }

        public string PaymentStatus
        {
            get { return this._paymentStatus; }
            set { this._paymentStatus = value; }
        }

        public LessonsDetails()
        {

        }
        public LessonsDetails(string lessonID, DateTime lessonDate, TimeSpan starthour, string teacherID, string studentID, int subjectID, int levelID, string address, string status,int priceperhour, string paymentStatus)
        {
            this._lessonID = lessonID;
            this.LessonDate = lessonDate;
            this._starthour = starthour;
            this._teacherID = teacherID;
            this._studentID = studentID;
            this._subjectID = subjectID;
            this._levelID = levelID;
            this._address = address;
            this._status = status;
            this._pricePerHour = priceperhour;
            this._paymentStatus = paymentStatus;
        }
    }
}