using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace GuyProject.App_Code
{
    public class WorkingService
    {
        protected OleDbConnection myConnection;
        public WorkingService()
        {
            string conn = Connect.getconnectionString();
            myConnection = new OleDbConnection(conn);
        }
        
        public void InsertNewWorkingHours(WorkingDetails workingDetails)
        {
            OleDbCommand myCommand = new OleDbCommand("InsertNewWorkingHours", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.TeacherID;

            objParam = myCommand.Parameters.Add("DayOfWeek", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.DayOfWeek;

            objParam = myCommand.Parameters.Add("StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.StartHour;

            objParam = myCommand.Parameters.Add("EndHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.EndHour;

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
        public DataSet GetAllDaysOfWorkingByTeacherID(string teacherID)
        {
            OleDbCommand cmd = new OleDbCommand("GetDaysOfWorkingByTeacherID", myConnection);
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
                adapter.Fill(ds, "WorkingHoursTbl");
                //ds.Tables["WorkingHoursTbl"].PrimaryKey = new DataColumn[] { ds.Tables["WorkingHoursTbl"].Columns["TeacherID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public void UpdateTeacherWorkingHours(WorkingDetails workingDetails)
        {
            //UPDATE DaysWorkingTbl SET DayInWeek = [@DayInWeek], Starthour = [@Starthour], Endhour = [@Endhour]
            //WHERE TeacherID = [@TeacherID];
            OleDbCommand myCommand = new OleDbCommand("UpdateTimeOfWorking", myConnection);
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;
            OleDbParameter objParam;


            objParam = myCommand.Parameters.Add("@StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.StartHour;

            objParam = myCommand.Parameters.Add("@EndHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.EndHour;

            objParam = myCommand.Parameters.Add("@TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.TeacherID;

            objParam = myCommand.Parameters.Add("@DayOfWeek", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.DayOfWeek;
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
        public void DeleteTeacherDaysOfWeek(WorkingDetails workingDetails)
        {
            OleDbCommand myCommand = new OleDbCommand("DeleteDaysOfWorking", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@teacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.TeacherID;

            objParam = myCommand.Parameters.Add("@DayOfWeek", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.DayOfWeek;

            objParam = myCommand.Parameters.Add("@StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.StartHour;

            objParam = myCommand.Parameters.Add("@EndHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.EndHour;

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
        public bool CheckIfDaysInWeekWithHoursExist(WorkingDetails workingDetails)
        {
            OleDbCommand myCommand = new OleDbCommand("CheckIfDaysWithHoursExist", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.TeacherID;

            objParam = myCommand.Parameters.Add("DayOfWeek", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.DayOfWeek;

            objParam = myCommand.Parameters.Add("EndHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.EndHour;

            objParam = myCommand.Parameters.Add("StartHour", OleDbType.DBTime);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = workingDetails.StartHour;

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
        public DataSet GetAllHoursWorkingByDay(string teacherID, string dayInWeek)
        {
            OleDbCommand cmd = new OleDbCommand("GetAllHoursWorkingByIdAndDay", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = cmd.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            objParam = cmd.Parameters.Add("DayInWeek", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = dayInWeek;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "HoursWorkingInDayTbl");
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