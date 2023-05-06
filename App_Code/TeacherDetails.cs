using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuyProject.App_Code
{
    public class TeacherDetails
    {
        private string _teacherID;
        private string _status;
        private string _learnPlace;
        private string _imageTeacher;
        private string _description;

        public string TeacherID
        {
            get { return this._teacherID; }
            set { this._teacherID = value; }
        }
        public string Status
        {
            get { return this._status; }
            set { this._status = value; }
        }

        public string LearnPlace
        {
            get { return this._learnPlace; }
            set { this._learnPlace = value; }
        }

        public string ImageTeacher
        {
            get { return this._imageTeacher; }
            set { this._imageTeacher = value; }
        }

        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        public TeacherDetails()
        {

        }
        public TeacherDetails(string teacherId, string status ,string leranPlace,string ImageTeacher, string description)
        {
            this._teacherID = teacherId;
            this._status = status;
            this._learnPlace = leranPlace;
            this._imageTeacher = ImageTeacher;
            this._description = description;
        }
    }
}