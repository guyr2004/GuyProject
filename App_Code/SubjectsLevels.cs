using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuyProject.App_Code
{
    public class SubjectsLevels
    {
        private int _subjectID;
        private string _subjectName;
        private int _levelID;
        private string _LevelName;
        private int _pricePerHour;

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
        public int LevelID
        {
            get { return this._levelID; }
            set { this._levelID = value; }
        }
        public string LevelName
        {
            get { return this._LevelName; }
            set { this._LevelName = value; }
        }
        public int PricePerHour
        {
            get { return this._pricePerHour; }
            set { this._pricePerHour = value; }
        }

        public SubjectsLevels(int subjectID, string subjectName, int levelID, string levelName, int priceperhour)
        {
            this._subjectID = subjectID;
            this._subjectName = subjectName;
            this._levelID = levelID;
            this._LevelName = levelName;
            this._pricePerHour = priceperhour;

        }
        public SubjectsLevels()
        {

        }
        //public SubjectsLevels(SubjectsLevels subjectlevels): base(subjectlevels)
        //{
        //    this._levelID = subjectlevels._levelID;
        //    this._pricePerHour = subjectlevels._pricePerHour;
        //}
    }
}