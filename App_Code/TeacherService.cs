using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace GuyProject.App_Code
{
    public class TeacherService
    {
        protected OleDbConnection myConnection;
        public TeacherService()
        {
            string conn = Connect.getconnectionString();
            myConnection = new OleDbConnection(conn);
        }
        public TeacherDetails GetTeacherByTeacherID(string teacherID)
        {
            OleDbCommand myCommand = new OleDbCommand("GetTeacherByTeacherID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            TeacherDetails teacherDetails = null;
            try
            {
                myConnection.Open();
                OleDbDataReader reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    teacherDetails = new TeacherDetails();
                    teacherDetails.TeacherID = reader["TeacherID"].ToString();
                    teacherDetails.Status = reader["Status"].ToString();
                    teacherDetails.LearnPlace = reader["LearnPlace"].ToString();
                    teacherDetails.ImageTeacher = reader["ImageTeacher"].ToString();
                    teacherDetails.Description = reader["Description"].ToString();
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
            return teacherDetails;
        }
        public void UpdateTeacherStatus(string teacherId, string status)
        {
            //UPDATE TeacherTbl SET Status = [@Status] WHERE TeacherID = [@TeacherID];
            OleDbCommand myCommand = new OleDbCommand("UpdateTeacherStatus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("Status", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = status;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherId;

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally { myConnection.Close(); }
        }
        public bool CheckIfTeacherhaveLessons(string teacherID)
        {
            OleDbCommand myCommand = new OleDbCommand("GetLessonID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;
            objParam = myCommand.Parameters.Add("teacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;
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
        public DataSet GetAllTeachersWithDistinct()
        {
            OleDbCommand cmd = new OleDbCommand("GetAllTeachersWithDistinct", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "TeacherTbl");
                ds.Tables["TeacherTbl"].PrimaryKey = new DataColumn[] { ds.Tables["TeacherTbl"].Columns["TeacherID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet GetTeachersWithoutMe(string teacherID)
        {
            OleDbCommand cmd = new OleDbCommand("GetTeachersWithoutMe", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = cmd.Parameters.Add("@TeacherID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "TeacherTbl");
                ds.Tables["TeacherTbl"].PrimaryKey = new DataColumn[] { ds.Tables["TeacherTbl"].Columns["TeacherID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet GetAllTeachersBySubjectNameAndLevelName(string status, int subjectID, int levelID)
        {
            OleDbCommand cmd = new OleDbCommand("GetAllTeachersBySubjectIDAndLevelID", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = cmd.Parameters.Add("@Status", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = status;

            objParam = cmd.Parameters.Add("@SubjectName", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectID;

            objParam = cmd.Parameters.Add("@LevelName", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = levelID;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "TeacherTbl");
                ds.Tables["TeacherTbl"].PrimaryKey = new DataColumn[] { ds.Tables["TeacherTbl"].Columns["TeacherID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet GetAllTeachersDetails()
        {
            OleDbCommand cmd = new OleDbCommand("GetAllTeachersDetails", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                myConnection.Open();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
            return ds;
        }
        public DataSet GetAllTeachersDataFromUsersAndTeachersTbl()
        {
            OleDbCommand cmd = new OleDbCommand("GetAllTeacherDetailsFrom2Tables", myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, "UsersTbl");
                ds.Tables["UsersTbl"].PrimaryKey = new DataColumn[] { ds.Tables["UsersTbl"].Columns["UserID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public void AddTeacher(TeacherDetails teacherDetails)
        {
            OleDbCommand myCommand = new OleDbCommand("InsertNewTeacher", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherDetails.TeacherID;

            objParam = myCommand.Parameters.Add("Status", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherDetails.Status;

            objParam = myCommand.Parameters.Add("LearnPlace", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherDetails.LearnPlace;

            objParam = myCommand.Parameters.Add("ImageTeacher", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherDetails.ImageTeacher;

            objParam = myCommand.Parameters.Add("Description", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherDetails.Description;

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
        public bool CheckIfSubjectExist(string teacherID, int subjectID, int levelID)
        {
            OleDbCommand myCommand = new OleDbCommand("GetAllByTeacherIDAndSubLevels", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            objParam = myCommand.Parameters.Add("SubjectID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectID;

            objParam = myCommand.Parameters.Add("LevelID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = levelID;
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
        public void AddSubjectsAndLevelsTeacher(SubjectsList subjectsList, string teacherID)
        {
            //INSERT INTO TeacherSubjectsTbl ( SubjectID, LevelID, TeacherID, PricePerHour )
            //VALUES(([@SubjectID]), ([@LevelID]), ([@TeacherID]), ([@PricePerHour]));
            SubjectsLevels subjectsLevels;
            for (int i = 0; i < subjectsList.Length; i++)
            {
                subjectsLevels = (SubjectsLevels)(subjectsList.SubList[i]);
                if (!CheckIfSubjectExist(teacherID, subjectsLevels.SubjectID, subjectsLevels.LevelID))//האם רשום למקצוע
                {
                    OleDbCommand myCommand = new OleDbCommand("AddTeacherSubjects", myConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    OleDbParameter objParam;

                    objParam = myCommand.Parameters.Add("SubjectID", OleDbType.Integer);
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = subjectsLevels.SubjectID;

                    objParam = myCommand.Parameters.Add("LevelID", OleDbType.Integer);
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = subjectsLevels.LevelID;

                    objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = teacherID;

                    objParam = myCommand.Parameters.Add("PricePerHour", OleDbType.Integer);
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = subjectsLevels.PricePerHour;
                    try
                    {
                        myConnection.Open();
                        int rowAffecteed = myCommand.ExecuteNonQuery();
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
        public void UpdateTeacherDetails(TeacherDetails teacherDetails)
        {
            //UPDATE TeacherTbl SET Status = [@Status], LearnPlace = [@LearnPlace], ImageTeacher = [@ImageTeacher], Description = [@Description]
            //WHERE TeacherID = [@TeacherID];
            OleDbCommand myCommand = new OleDbCommand("UpdateTeacherDetails", myConnection);
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@Status", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherDetails.Status;

            objParam = myCommand.Parameters.Add("@LearnPlace", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherDetails.LearnPlace;

            objParam = myCommand.Parameters.Add("@ImageTeacher", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherDetails.ImageTeacher;

            objParam = myCommand.Parameters.Add("@Description", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherDetails.Description;

            objParam = myCommand.Parameters.Add("@TeacherID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherDetails.TeacherID;

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
        public DataSet GetTeacherWithSubjects(string teacherID)
        {
            string sql = " SELECT DISTINCT SubjectsTbl.SubjectName, LevelsTbl.LevelName, TeacherSubjectsTbl.TeacherID, SubjectsTbl.SubjectID, LevelsTbl.LevelID, TeacherSubjectsTbl.PricePerHour FROM(TeacherSubjectsTbl INNER JOIN LevelsTbl ON TeacherSubjectsTbl.LevelID = LevelsTbl.LevelID) INNER JOIN SubjectsTbl ON TeacherSubjectsTbl.SubjectID = SubjectsTbl.SubjectID WHERE(((TeacherSubjectsTbl.TeacherID) = ([@TeacherID])));";
            //string sql = "SELECT SubjectsTbl.SubjectID, SubjectsTbl.SubjectName, LevelsTbl.LevelID, LevelsTbl.LevelName, TeacherSubjectsTbl.TeacherID, TeacherSubjectsTbl.PricePerHour FROM (TeacherSubjectsTbl INNER JOIN LevelsTbl ON TeacherSubjectsTbl.LevelID = LevelsTbl.LevelID) INNER JOIN SubjectsTbl ON TeacherSubjectsTbl.SubjectID = SubjectsTbl.SubjectID WHERE (((TeacherSubjectsTbl.TeacherID)=[@TeacherID]));";
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, myConnection);
            adapter.SelectCommand.Parameters.AddWithValue("[@TeacherID]", teacherID);
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds);
                //    ds.Tables["TeacherSubjectsTbl"].PrimaryKey = new DataColumn[] { ds.Tables["TeacherSubjectsTbl"].Columns["TeacherID"], ds.Tables["TeacherSubjectsTbl"].Columns["SubjectID"],
                //        ds.Tables["TeacherSubjectsTbl"].Columns["LevelID"] }; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}




