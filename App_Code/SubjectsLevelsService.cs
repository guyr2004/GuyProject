using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace GuyProject.App_Code
{
    public class SubjectsLevelsService
    {
        protected OleDbConnection myConnection;
        public SubjectsLevelsService()
        {
            string conn = Connect.getconnectionString();
            myConnection = new OleDbConnection(conn);
        }
        public DataSet GetAllLevelsNameBySubjectsID(int subjectID)
        {
            OleDbCommand myCommand = new OleDbCommand("GetAllLevelNameBySubjectID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("SubjectID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectID;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = myCommand;
            DataSet dataset = new DataSet();
            try
            {
                myConnection.Open();
                adapter.Fill(dataset, "LevelsTbl");
                dataset.Tables["LevelsTbl"].PrimaryKey = new DataColumn[] { dataset.Tables["LevelsTbl"].Columns["LevelID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
            return dataset;
        }
        public string GetLevelNameByLevelID(int levelID)
        {
            OleDbCommand mycommand = new OleDbCommand("GetLevelNameByLevelID", myConnection);
            mycommand.CommandType = System.Data.CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = mycommand.Parameters.Add("@levelID", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = levelID;
            string levelName = "";
            try
            {
                myConnection.Open();
                object obj = mycommand.ExecuteScalar();
                if (obj != null)
                {
                    levelName = (string)obj;
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
            return levelName;
        }
        public string GetSubjectNameBySubjectID(int subID)
        {
            OleDbCommand mycommand = new OleDbCommand("GetSubjectNameBySubjectID", myConnection);
            mycommand.CommandType = System.Data.CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = mycommand.Parameters.Add("@SubjectID", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = subID;
            string subjectName = "";
            try
            {
                myConnection.Open();
                object obj = mycommand.ExecuteScalar();
                if (obj != null)
                {
                    subjectName = (string)obj;
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
            return subjectName;
        }
        public int GetLevelIDByLevelName(string levelName)
        {
            OleDbCommand mycommand = new OleDbCommand("GetLevelIDByLevelName", myConnection);
            mycommand.CommandType = System.Data.CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = mycommand.Parameters.Add("@levelName", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = levelName;

            int levelID = 0;
            try
            {
                myConnection.Open();
                object obj = mycommand.ExecuteScalar();
                if (obj != null)
                {
                    levelID = (int)obj;
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
            return levelID;
        }
        public int GetSubjectIDBySubjectName(string subName)
        {
            OleDbCommand mycommand = new OleDbCommand("GetSubjectIDBySubjectName", myConnection);
            mycommand.CommandType = System.Data.CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = mycommand.Parameters.Add("@subName", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = subName;

            int subID = 0;
            try
            {
                myConnection.Open();
                object obj = mycommand.ExecuteScalar();
                if (obj != null)
                {
                    subID = (int)obj;
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
            return subID;
        }
        public DataSet GetAllLevelsNameAndSubNameBySubjectsID(int subjectID)
        {
            OleDbCommand myCommand = new OleDbCommand("GetAllLevelsAndSubjectsNameBySubjectID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("SubjectID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectID;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = myCommand;
            DataSet dataset = new DataSet();
            try
            {
                myConnection.Open();
                adapter.Fill(dataset, "LevelsTbl");
                dataset.Tables["LevelsTbl"].PrimaryKey = new DataColumn[] { dataset.Tables["LevelsTbl"].Columns["LevelID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
            return dataset;
        }
        public DataSet GetSubjectsAndLevels()
        {
            OleDbCommand oleDbCommand = new OleDbCommand("GetAllSubjectsAndLevels", myConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = oleDbCommand;
            DataSet dataset = new DataSet();
            try
            {
                adapter.Fill(dataset, "SubjectAndLevelTbl");
                dataset.Tables["SubjectAndLevelTbl"].PrimaryKey = new DataColumn[] {/* dataset.Tables["SubjectAndLevelTbl"].Columns["SubjectID"],*/
                   /* dataset.Tables["SubjectAndLevelTbl"].Columns["LevelID"]*/  dataset.Tables["SubjectAndLevelTbl"].Columns["SubjectName"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataset;
        }
        public void InsertNewSubject(string subName)
        {
            OleDbCommand myCommand = new OleDbCommand("InsertNewSubject", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("subjectName", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subName;


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
        public void InsertNewLevel(string levelName)
        {
            OleDbCommand myCommand = new OleDbCommand("InsertNewLevel", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("LevelName", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = levelName;


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
        public void InsertNewSubjectLevel(int subID, int levelID)
        {
            OleDbCommand myCommand = new OleDbCommand("InsertNewSubjectLevel", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("SubjectID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subID;

            objParam = myCommand.Parameters.Add("LevelID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = levelID;

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
        public void InsertNewSubjectLevelTeacher(SubjectsLevels subjectsLevels, string teacherID)
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

            objParam = myCommand.Parameters.Add("teacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            objParam = myCommand.Parameters.Add("PricePerHour", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectsLevels.PricePerHour;


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
        public bool CheckIfSubjectNameExist(string subName)
        {
            OleDbCommand myCommand = new OleDbCommand("GetSubjectIDBySubjectName", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("SubjectName", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subName;
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
        public bool CheckIfLevelNameExist(string levelName)
        {
            OleDbCommand myCommand = new OleDbCommand("GetLevelIDByLevelName", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("LevelName", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = levelName;
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
        public void UpdateTeacherSubjectLevelPrice(SubjectsLevels subjectsLevel ,string teacherID)
        {
            //UPDATE TeacherSubjectsTbl SET PricePerHour = [@PricePerHour]
            //WHERE(TeacherID = [@TeacherID] AND SubjectID = [@SubjectID] AND LevelID = [@LevelID]);
            OleDbCommand myCommand = new OleDbCommand("UpdatePricePerSubjectLevel", myConnection);
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@PricePerHour", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectsLevel.PricePerHour;

            objParam = myCommand.Parameters.Add("@teacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            objParam = myCommand.Parameters.Add("@SubjectID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectsLevel.SubjectID;

            objParam = myCommand.Parameters.Add("@LevelID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectsLevel.LevelID;

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
        public void DeleteTeacherSubjectLevel(SubjectsLevels subjectsLevels, string teacherID)
        {
            OleDbCommand myCommand = new OleDbCommand("DeleteTeacherSubjectLevel", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@teacherID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = teacherID;

            objParam = myCommand.Parameters.Add("@SubjectID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectsLevels.SubjectID;

            objParam = myCommand.Parameters.Add("@LevelID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectsLevels.LevelID;

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
        public bool CheckIfTeacherSubjectLevelExist(SubjectsLevels subjectsLevels, string teacherID)
        {
            OleDbCommand myCommand = new OleDbCommand("CheckIfTeacherSubjectLevelExist", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("SubjectID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectsLevels.SubjectID;

            objParam = myCommand.Parameters.Add("LevelID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = subjectsLevels.LevelID;

            objParam = myCommand.Parameters.Add("TeacherID", OleDbType.BSTR);
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


        //public bool CheckIfSubjectLevelExist(int subID, int levelID)
        //{
        //    OleDbCommand myCommand = new OleDbCommand("CheckIfSubjectLevelExist", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    OleDbParameter objParam;

        //    objParam = myCommand.Parameters.Add("SubjectID", OleDbType.BSTR);
        //    objParam.Direction = ParameterDirection.Input;
        //    objParam.Value = subID;

        //    objParam = myCommand.Parameters.Add("LevelID", OleDbType.BSTR);
        //    objParam.Direction = ParameterDirection.Input;
        //    objParam.Value = levelID;
        //    try
        //    {
        //        myConnection.Open();
        //        object obj = myCommand.ExecuteScalar();
        //        if (obj == null)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }
        //}
        //כרגע הפעולה לא בשימוש המטרה הייתה לבדוק האם קשר גומלין מסוים נמצא במסד הנתונים אבל עקפתי מהצד

    }
}