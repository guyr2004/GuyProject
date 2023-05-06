using System;
using System.Collections;
using System.Linq;
using System.Web;

namespace GuyProject.App_Code
{
    public class SubjectsList
    {
        ArrayList subList;
        public SubjectsList()
        {
            subList = new ArrayList();
        }
        public ArrayList SubList
        {
            get
            {
                return subList;
            }
        }
        public int Length
        {
            get
            {
                return subList.Count;
            }
        }
        private int SearchSubjectWithLevel(int subjectID, int levelID)
        {
            //מחפש האם מקצוע נמצא ברשימה
            for (int i = 0; i < subList.Count; i++)
            {
                if (((SubjectsLevels)subList[i]).SubjectID == subjectID && ((SubjectsLevels)subList[i]).LevelID == levelID)
                    return i;
            }
            return -1;
        }
        public void InsertNewSubjectLevelTeacher(SubjectsLevels subjectsLevels, string teacherID)
        {
            bool isExist = false;
            for (int i = 0; i < subList.Count; i++)
            {
                if ((((SubjectsLevels)subList[i]).SubjectID == subjectsLevels.SubjectID && (((SubjectsLevels)subList[i]).LevelID == subjectsLevels.LevelID)))
                {
                    isExist = true;
                }
            }
            if (isExist == false)
            {
                subList.Add(subjectsLevels);
                SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
                subjectsLevelsService.InsertNewSubjectLevelTeacher(subjectsLevels, teacherID);
            }
        }
        public void AddSubjectLevel(SubjectsLevels subjectsLevels)
        {
            //מוסיף מקצוע ויכול להיות קיים רק פעם אחת
            bool isExist = false;
            for (int i = 0; i < subList.Count; i++)
            {
                if ((((SubjectsLevels)subList[i]).SubjectID == subjectsLevels.SubjectID && (((SubjectsLevels)subList[i]).LevelID == subjectsLevels.LevelID)))
                {
                    isExist = true;
                }
            }
            if (isExist == false)
            {
                subList.Add(subjectsLevels);
            }
        }
        public void DeleteSubjectLevel(SubjectsLevels subjectsLevels, string teacherID)
        {
            // מוחק את הנושא והרמה המסויימת מהרשימה
            int index = SearchSubjectWithLevel(subjectsLevels.SubjectID, subjectsLevels.LevelID);
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            subjectsLevelsService.DeleteTeacherSubjectLevel(((SubjectsLevels)subList[index]), teacherID);
            subList.Remove(((SubjectsLevels)subList[index]));
        }
        public void UpdateSubjectLevelPrice(SubjectsLevels subjectsLevels, int price, string teacherID)
        {
            //מעדכן את המחיר של המקצוע המסויים
            int index = SearchSubjectWithLevel(subjectsLevels.SubjectID, subjectsLevels.LevelID);
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            ((SubjectsLevels)subList[index]).PricePerHour = price;
            subjectsLevelsService.UpdateTeacherSubjectLevelPrice(((SubjectsLevels)subList[index]), teacherID);
        }
    }
}