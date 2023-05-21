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
        public DataSet GetAllLessonsByUserID(string userID)
        {
            OleDbCommand cmd = new OleDbCommand("GetAllLessonsByUserID", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = cmd.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userID;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "UserLessons");
                ds.Tables["UserLessons"].PrimaryKey = new DataColumn[] { ds.Tables["UserLessons"].Columns["LessonID"] };
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
        public DataSet GetAllTeachersNameByStudentID(string studentID)
        {
            OleDbCommand cmd = new OleDbCommand("GetAllTeachersNameByStudentID", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = cmd.Parameters.Add("StudentID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = studentID;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "TeachersName");
                ds.Tables["TeachersName"].PrimaryKey = new DataColumn[] { ds.Tables["TeachersName"].Columns["UserName"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet GetAllStudentsNameByTeacherID(string teacherID)
        {
            OleDbCommand cmd = new OleDbCommand("GetAllStudentsNameByTeacherID", myConnection);
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
                adapter.Fill(ds, "StudentsName");
                //ds.Tables["StudentsName"].PrimaryKey = new DataColumn[] { ds.Tables["StudentsName"].Columns["UserName"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public void UpdateLessonPaymentStatus(LessonsDetails lessonsDetails, string paymentStatus)
        {
            OleDbCommand myCommand = new OleDbCommand("UpdatePaymentStatus", myConnection);
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@PaymentStatus", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = paymentStatus;

            objParam = myCommand.Parameters.Add("@LessonDate", OleDbType.Date);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.LessonDate;

            objParam = myCommand.Parameters.Add("@StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.StartHour;

            objParam = myCommand.Parameters.Add("@TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.TeacherID;

            objParam = myCommand.Parameters.Add("@StudentID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = lessonsDetails.StudentID;

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
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
        public LessonsDetails GetLesson(DateTime datelesson, TimeSpan starthour, string teacherID, string studentID)
        {
            OleDbCommand myCommand = new OleDbCommand("GetLessonByDetails", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@LessonDate", OleDbType.Date);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = datelesson;

            objParam = myCommand.Parameters.Add("@StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = starthour;

            objParam = myCommand.Parameters.Add("@TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            objParam = myCommand.Parameters.Add("@StudentID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = studentID;

            LessonsDetails lessonsDetails = null;
            try
            {
                myConnection.Open();
                OleDbDataReader reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    lessonsDetails = new LessonsDetails();
                    lessonsDetails.LessonDate = datelesson;
                    lessonsDetails.StartHour = starthour;
                    lessonsDetails.TeacherID = reader["TeacherID"].ToString();
                    lessonsDetails.StudentID = reader["StudentID"].ToString();
                    lessonsDetails.SubjectID  = int.Parse(reader["SubjectID"].ToString());
                    lessonsDetails.LevelID = int.Parse(reader["LevelID"].ToString());
                    lessonsDetails.Address = reader["Address"].ToString();
                    lessonsDetails.Status = reader["Status"].ToString();
                    lessonsDetails.PricePerHour = int.Parse(reader["PricePerHour"].ToString());
                    lessonsDetails.PaymentStatus = reader["PaymentStatus"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
            return lessonsDetails;
        }
        public DataSet GetAllTeacherLessonsByTeacherID(string userID)
        {
            OleDbCommand cmd = new OleDbCommand("GetLessonsByTeacherID", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = cmd.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userID;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "UserLessons");
                ds.Tables["UserLessons"].PrimaryKey = new DataColumn[] { ds.Tables["UserLessons"].Columns["LessonID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public void InsertNewPayment(DateTime dateTime, string userID, string teacherID, int amountPaid, string kindPayments)
        {
            OleDbCommand myCommand = new OleDbCommand("InsertNewPayment", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("DatePayments", OleDbType.Date);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = dateTime;

            objParam = myCommand.Parameters.Add("UserID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userID;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            objParam = myCommand.Parameters.Add("AmountPaid", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = amountPaid;

            objParam = myCommand.Parameters.Add("KindPayments", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = kindPayments;

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
    }
}
