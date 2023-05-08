using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace GuyProject.App_Code
{
    public class LessonService
    {
        OleDbConnection myConnection;
        public LessonService()
        {
            string conn = Connect.getconnectionString();
            myConnection = new OleDbConnection(conn);
        }

        public void InsertNewLesson(LessonsDetails lessonsDetails)
        {
            OleDbCommand myCommand = new OleDbCommand("InsertNewLesson", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("LessonDate", OleDbType.Date);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.LessonDate;

            objParam = myCommand.Parameters.Add("StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.StartHour;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.TeacherID;

            objParam = myCommand.Parameters.Add("StudentID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.StudentID;

            objParam = myCommand.Parameters.Add("SubjectID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.SubjectID;

            objParam = myCommand.Parameters.Add("LevelID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.LevelID;

            objParam = myCommand.Parameters.Add("Address", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.Address;

            objParam = myCommand.Parameters.Add("Status", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.Status;

            objParam = myCommand.Parameters.Add("PricePerHour", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.PricePerHour;

            objParam = myCommand.Parameters.Add("PaymentStatus", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.PaymentStatus;

            int rowAffecteed = 0;
            try
            {
                myConnection.Open();
                rowAffecteed = myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
        }
        public DataSet GetAllLessonsByUserID(string teacherID)
        {
            OleDbCommand cmd = new OleDbCommand("GetAllLessonsByUserID", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = cmd.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "UserLessons");
                //ds.Tables["WorkingHoursTbl"].PrimaryKey = new DataColumn[] { ds.Tables["WorkingHoursTbl"].Columns["TeacherID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet GetAllLessonByTeacherIDAndDate(LessonsDetails lessonsDetails)
        {
            OleDbCommand cmd = new OleDbCommand("GetAllLessonsByTeacherIDAndLessonDate", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = cmd.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.TeacherID;

            objParam = cmd.Parameters.Add("LessonDate", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.LessonDate;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "LessonsTeacher");
                //ds.Tables["WorkingHoursTbl"].PrimaryKey = new DataColumn[] { ds.Tables["WorkingHoursTbl"].Columns["TeacherID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public void DeleteLesson(LessonsDetails lessonsDetails)
        {
            OleDbCommand myCommand = new OleDbCommand("DeleteLesson", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.TeacherID;

            objParam = myCommand.Parameters.Add("@LessonDate", OleDbType.Date);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.LessonDate;

            objParam = myCommand.Parameters.Add("@StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.StartHour;

            objParam = myCommand.Parameters.Add("@StudentID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.StudentID;

            int rowAffected;
            try
            {
                myConnection.Open();
                rowAffected = myCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}