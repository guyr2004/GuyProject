using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuyProject.App_Code;

namespace GuyProject.App_Code
{
    public class LessonsList
    {
        ArrayList lessonslist;

        public LessonsList()
        {
            lessonslist = new ArrayList();
        }
        public ArrayList lessonsList
        {
            get
            {
                return lessonslist;
            }
        }
        public int Length
        {
            get
            {
                return lessonslist.Count;
            }
        }
        private int SearchLesson(DateTime lessondate, TimeSpan starthour, string teacherID, string studentID)
        {
            //מחפש האם שיעור נמצא ברשימה
            for (int i = 0; i < lessonslist.Count; i++)
            {
                if (((LessonsDetails)lessonslist[i]).LessonDate == lessondate && ((LessonsDetails)lessonslist[i]).StartHour == starthour 
                    && ((LessonsDetails)lessonslist[i]).TeacherID == teacherID && ((LessonsDetails)lessonslist[i]).StudentID == studentID)
                    return i;
            }
            return -1;
        }
        public void AddSubjectLevel(LessonsDetails lessonsDetails)
        {
            bool isExist = false;
            for (int i = 0; i < lessonsList.Count; i++)
            {
                if ((((LessonsDetails)lessonsList[i]).LessonDate == lessonsDetails.LessonDate && (((LessonsDetails)lessonsList[i]).StartHour == lessonsDetails.StartHour)
                    && (((LessonsDetails)lessonsList[i]).TeacherID == lessonsDetails.TeacherID) && (((LessonsDetails)lessonsList[i]).StudentID == lessonsDetails.StudentID)))
                {
                    isExist = true;
                }
            }
            if (isExist == false)
            {
                lessonsList.Add(lessonsDetails);
            }
        }
        public void DeleteLesson(LessonsDetails lessonsDetails)
        {
            int index = SearchLesson(lessonsDetails.LessonDate, lessonsDetails.StartHour, lessonsDetails.TeacherID, lessonsDetails.StudentID);
            lessonslist.Remove(((LessonsDetails)lessonslist[index]));
        }
        public void UpdateSubjectLevelPrice(LessonsDetails lessonsDetails, string payStatus)
        {
            int index = SearchLesson(lessonsDetails.LessonDate, lessonsDetails.StartHour, lessonsDetails.TeacherID, lessonsDetails.StudentID);
            LessonService lessonService = new LessonService();
            ((LessonsDetails)lessonsList[index]).PaymentStatus = payStatus;
            lessonService.UpdateLessonPaymentStatus(lessonsDetails, payStatus);
        }
    }
}