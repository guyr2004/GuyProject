using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace GuyProject.App_Code
{
    public class AbsenceTeacherService
    {
        protected OleDbConnection myConnection;
        public AbsenceTeacherService()
        {
            string conn = Connect.getconnectionString();
            myConnection = new OleDbConnection(conn);
        }

        public DataSet GetAllAbsenceTeacherByTeacherID(string teacherID)
        {
            OleDbCommand cmd = new OleDbCommand("GetAllAbsenceTeacherByTeacherID", myConnection);
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
                adapter.Fill(ds, "AbsenceTeacherTbl");
                //ds.Tables["WorkingHoursTbl"].PrimaryKey = new DataColumn[] { ds.Tables["WorkingHoursTbl"].Columns["TeacherID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public bool CheckIfAbsenceTeacherExist(AbsenceTeacherDetails absenceTeacherDetails)
        {
            OleDbCommand myCommand = new OleDbCommand("CheckIfAbsenceTeacherExist", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.TeacherID;

            objParam = myCommand.Parameters.Add("DayOfWeek", OleDbType.Date);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.AbsenceDate;

            objParam = myCommand.Parameters.Add("EndHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.EndHour;

            objParam = myCommand.Parameters.Add("StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.StartHour;

            try
            {
                myConnection.Open();
                object obj = myCommand.ExecuteScalar();
                if (obj == null)
                {
                    return false;
                }
                return true;
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
        public void InsertNewAbsenceTeacher(AbsenceTeacherDetails absenceTeacherDetails)
        {
            OleDbCommand myCommand = new OleDbCommand("InsertNewAbsenceTeacher", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.TeacherID;

            objParam = myCommand.Parameters.Add("DayOfWeek", OleDbType.Date);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.AbsenceDate;

            objParam = myCommand.Parameters.Add("StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.StartHour;

            objParam = myCommand.Parameters.Add("EndHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.EndHour;

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
        public void UpdateTeacherWorkingHours(AbsenceTeacherDetails absenceTeacherDetails)
        {
            //UPDATE AbsenceTeacherTbl SET Starthour = [@Starthour], Endhour = [@Endhour]
            //WHERE(TeacherID = [@TeacherID] AND AbsenceDate = [@AbsenceDate]);
            OleDbCommand myCommand = new OleDbCommand("UpdateAbsenceTeacher", myConnection);
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.StartHour;

            objParam = myCommand.Parameters.Add("@EndHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.EndHour;

            objParam = myCommand.Parameters.Add("@TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.TeacherID;

            objParam = myCommand.Parameters.Add("@AbsenceDate", OleDbType.Date);
            objParam.Direction = ParameterDirection.Input;
            DateTime date = new DateTime(absenceTeacherDetails.AbsenceDate.Date.Year, absenceTeacherDetails.AbsenceDate.Date.Day, absenceTeacherDetails.AbsenceDate.Date.Month);
            objParam.Value = date;

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
        public void DeleteTeacherAbsence(AbsenceTeacherDetails absenceTeacherDetails)
        {
            //DELETE * FROM AbsenceTeacherTbl
            //WHERE(TeacherID = [@TeacherID] AND AbsenceDate = [@AbsenceDate]);
            OleDbCommand myCommand = new OleDbCommand("DeleteAbsenceTeacher", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = absenceTeacherDetails.TeacherID;

            objParam = myCommand.Parameters.Add("@AbsenceDate", OleDbType.DBDate);
            objParam.Direction = ParameterDirection.Input;
            DateTime date = new DateTime(absenceTeacherDetails.AbsenceDate.Date.Year, absenceTeacherDetails.AbsenceDate.Date.Day, absenceTeacherDetails.AbsenceDate.Date.Month);
            objParam.Value = date;

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
        public DataSet GetAllAbsenceTeacherByTeacherIDAndDate(string teacherID, DateTime AbsenceDate)
        {
            OleDbCommand cmd = new OleDbCommand("GetAllAbsenceTeacherByTeacherIdANDDate", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = cmd.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            objParam = cmd.Parameters.Add("AbsenceDate", OleDbType.Date);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = AbsenceDate;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "AbsenceTeacherTbl");
                //ds.Tables["WorkingHoursTbl"].PrimaryKey = new DataColumn[] { ds.Tables["WorkingHoursTbl"].Columns["TeacherID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

    }
}