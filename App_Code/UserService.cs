using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

namespace GuyProject.App_Code
{
    public class UserService
    {
        protected OleDbConnection myConnection;
        protected OleDbDataAdapter adapterSubjects;
        protected OleDbDataAdapter adapterLevels;
        public UserService()
        {
            string conn = Connect.getconnectionString();
            myConnection = new OleDbConnection(conn);
        }
        public DataSet GetCities()
        {
            OleDbCommand oleDbCommand = new OleDbCommand("GetCityName", myConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = oleDbCommand;
            DataSet dataset = new DataSet();
            try
            {
                adapter.Fill(dataset, "CitiesTbl");
                dataset.Tables["CitiesTbl"].PrimaryKey = new DataColumn[] { dataset.Tables["CitiesTbl"].Columns["CityID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataset;
        }
        public DataSet GetAllSubjects()
        {
            OleDbCommand oleDbCommand = new OleDbCommand("GetAllSubjects", myConnection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = oleDbCommand;
            DataSet dataset = new DataSet();
            try
            {
                adapter.Fill(dataset, "SubjectsTbl");
                dataset.Tables["SubjectsTbl"].PrimaryKey = new DataColumn[] { dataset.Tables["SubjectsTbl"].Columns["SubjectID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataset;
        }
        //public UserService()
        //{
        //    string connectionString = Connect.getconnectionString();
        //    myConnection = new OleDbConnection(connectionString);
        //    OleDbCommand oleDbCommandSubjects = new OleDbCommand("GetAllSubjects", myConnection);
        //    oleDbCommandSubjects.CommandType = CommandType.StoredProcedure;
        //    adapterSubjects = new OleDbDataAdapter(oleDbCommandSubjects);
        //    OleDbCommand oleDbCommandLevels = new OleDbCommand("GetAllLevels", myConnection);
        //    oleDbCommandLevels.CommandType = CommandType.StoredProcedure;
        //    adapterLevels = new OleDbDataAdapter(oleDbCommandLevels);
        //}
        //private void CreateRelation(DataSet DS)
        //{
        //    //Get the DataColumn objects from two DataTable objects in a dataset
        //    DataColumn parentcol;
        //    DataColumn childcol;
        //    parentcol = DS.Tables["SubjectsTbl"].Columns["SubjectID"];
        //    childcol = DS.Tables["LevelsTbl"].Columns["LevelID"];
        //    //create relation
        //    DataRelation relKindWord;
        //    relKindWord = new DataRelation("SubjectLevel", parentcol, childcol);
        //    //Add relation to the DataSet
        //    DS.Relations.Add(relKindWord);
        //}

        //public DataSet GetSubjectsAndLevels()
        //{
        //    DataSet dataSet = new DataSet();
        //    try
        //    {
        //        myConnection.Open();
        //        adapterSubjects.Fill(dataSet, "SubjectsTbl");
        //        adapterLevels.Fill(dataSet, "LevelsTbl");
        //        dataSet.Tables["SubjectsTbl"].PrimaryKey = new DataColumn[] { dataSet.Tables["KindsTBl"].Columns["SubjectID"] };
        //        dataSet.Tables["LevelsTbl"].PrimaryKey = new DataColumn[] { dataSet.Tables["LevelsTbl"].Columns["LevelID"] };
        //        CreateRelation(dataSet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }
        //    return dataSet;
        //}
        public bool CheckIfUserIDExist(string userID)
        {
            OleDbCommand myCommand = new OleDbCommand("GetUserID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;
            objParam = myCommand.Parameters.Add("UserID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userID;
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
        public void AddUser(UserDetails userDetails)
        {
            //INSERT INTO UsersTbl ( UserID, firstName, lastName, birthdate, phone, Gender, address, cityID, UserType, Email , userpassword)
            //VALUES(([@userid]), ([@firstname]), ([@lastname]), ([@birthdate]), ([@phone]), ([@Gender]) ([@address]), ([@cityId]), ([@UserType]), ([@Email]), ([@userpassword]));

            myConnection = new OleDbConnection(Connect.getconnectionString());
            OleDbCommand myCommand = new OleDbCommand("InsertNewUser", myConnection);
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@FirstName", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.FirstName;

            objParam = myCommand.Parameters.Add("@LastName", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.LastName;

            objParam = myCommand.Parameters.Add("@BirthDate", OleDbType.Date);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.BirthDate;

            objParam = myCommand.Parameters.Add("@Phone", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.Phone;

            objParam = myCommand.Parameters.Add("@Gender", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.Gender;

            objParam = myCommand.Parameters.Add("@Address", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.Address;

            objParam = myCommand.Parameters.Add("@CityID", OleDbType.Integer);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.CityID;

            objParam = myCommand.Parameters.Add("@UserType", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.UserType;

            objParam = myCommand.Parameters.Add("@Email", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.Email;

            objParam = myCommand.Parameters.Add("@UserPassword", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.UserPassword;

            objParam = myCommand.Parameters.Add("@UserID", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = userDetails.UserID;

            int rowEffects;
            try
            {
                myConnection.Open();
                rowEffects = myCommand.ExecuteNonQuery();
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
        public void InsertNewCityName(string cityName)
        {
            myConnection = new OleDbConnection(Connect.getconnectionString());
            OleDbCommand myCommand = new OleDbCommand("InsertNewCityName", myConnection);
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@CityName", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = cityName;

            int rowEffects;
            try
            {
                myConnection.Open();
                rowEffects = myCommand.ExecuteNonQuery();
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
        public bool CheckIfCityNameExist(string cityName)
        {
            OleDbCommand myCommand = new OleDbCommand("CheckIfCityNameExist", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("CityName", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = cityName;
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
        public UserDetails GetUserByUserIDAndUserPassword(string userID, string pass)
        {
            OleDbCommand myCommand = new OleDbCommand("GetUserByUserIDAndPassword", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("UserID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userID;

            objParam = myCommand.Parameters.Add("UserPassword", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = pass;
            UserDetails userDetails = null;
            try
            {
                myConnection.Open();
                OleDbDataReader reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    userDetails = new UserDetails();
                    userDetails.UserID = reader["UserID"].ToString();
                    userDetails.FirstName = reader["FirstName"].ToString();
                    userDetails.LastName = reader["LastName"].ToString();
                    userDetails.BirthDate = DateTime.Parse(reader["BirthDate"].ToString());
                    userDetails.Phone = reader["Phone"].ToString();
                    userDetails.Gender = reader["Gender"].ToString();
                    userDetails.Address = reader["Address"].ToString();
                    userDetails.CityID = int.Parse(reader["CityID"].ToString());
                    userDetails.UserType = reader["UserType"].ToString();
                    userDetails.Email = reader["Email"].ToString();
                    userDetails.UserPassword = reader["UserPassword"].ToString();
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
            return userDetails;
        }
        public UserDetails GetUserByUserID(string userID)
        {
            OleDbCommand myCommand = new OleDbCommand("GetUserByUserID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("UserID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userID;

            UserDetails userDetails = null;
            try
            {
                myConnection.Open();
                OleDbDataReader reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    userDetails = new UserDetails();
                    userDetails.UserID = reader["UserID"].ToString();
                    userDetails.FirstName = reader["FirstName"].ToString();
                    userDetails.LastName = reader["LastName"].ToString();
                    userDetails.BirthDate = DateTime.Parse(reader["BirthDate"].ToString());
                    userDetails.Phone = reader["Phone"].ToString();
                    userDetails.Gender = reader["Gender"].ToString();
                    userDetails.Address = reader["Address"].ToString();
                    userDetails.CityID = int.Parse(reader["CityID"].ToString());
                    userDetails.UserType = reader["UserType"].ToString();
                    userDetails.Email = reader["Email"].ToString();
                    userDetails.UserPassword = reader["UserPassword"].ToString();
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
            return userDetails;
        }
        public void UpdateUserDetails(UserDetails userDetails)
        {

            // UPDATE UsersTbl SET FirstName = [@FirstName], LastName = [@LastName], Phone = [@Phone], Address = [@Address],
            // CityID = [@CityID], UserType = [@UserType], Email = [@Email], UserPassword = [@UserPassword]
            //WHERE UserID = [@UserID];

            OleDbCommand myCommand = new OleDbCommand("UpdateUserDetails", myConnection);
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@FirstName", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userDetails.FirstName;

            objParam = myCommand.Parameters.Add("@LastName", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userDetails.LastName;

            objParam = myCommand.Parameters.Add("@Phone", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userDetails.Phone;

            objParam = myCommand.Parameters.Add("@Address", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userDetails.Address;

            objParam = myCommand.Parameters.Add("@CityID", OleDbType.Integer);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userDetails.CityID;

            objParam = myCommand.Parameters.Add("@UserType", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userDetails.UserType;

            objParam = myCommand.Parameters.Add("@Email", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userDetails.Email;

            objParam = myCommand.Parameters.Add("@UserPassword", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userDetails.UserPassword;

            objParam = myCommand.Parameters.Add("@UserID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userDetails.UserID;

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
        public void DeleteUserByUserID(string userId)
        {
            OleDbCommand myCommand = new OleDbCommand("DeleteUserByUserID", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@UserID", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = userId;

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