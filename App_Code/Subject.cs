using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuyProject.App_Code
{
    public class Subject
    {
        private int _subjectID;
        private string _subjectName;

        public int SubjectID
        {
            get { return this._subjectID; }
            set { this._subjectID = value; }
        }
        public string SubjectName
        {
            get { return this._subjectName; }
            set { this._subjectName = value; }
        }
        public Subject()
        {

        }
        public Subject(int subjectID, string subjectName)
        {
            this._subjectID = subjectID;
            this._subjectName = subjectName;
        }

        //public Subject(Subject subject)
        //{
        //    this._subjectID = subject._subjectID;
        //    this._subjectName = subject._subjectName;
        //}
    }
}