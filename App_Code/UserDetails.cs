using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuyProject.App_Code
{
    public class UserDetails
    {
        private string _userID;
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;
        private string _phone;
        private string _gender;
        private string _address;
        private int _cityID;
        private string _usertype;
        private string _email;
        private string _userPassword;

        public string UserID
        {
            get { return this._userID; }
            set { this._userID = value; }
        }
        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }
        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }
        public DateTime BirthDate
        {
            get { return this._birthDate; }
            set { this._birthDate = value; }
        }
        public string Phone
        {
            get { return this._phone; }
            set { this._phone = value; }
        }
        public string Gender
        {
            get { return this._gender; }
            set { this._gender = value; }
        }
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }
        public int CityID
        {
            get { return this._cityID; }
            set { this._cityID = value; }
        }
        public string UserType
        {
            get { return this._usertype; }
            set { this._usertype = value; }
        } 
        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }
        public string UserPassword
        {
            get { return this._userPassword; }
            set { this._userPassword = value; } 
        }
        public UserDetails() 
        {

        }
        public UserDetails(string userID, string firstName, string lastName, DateTime birthDate, string phone, string gender, string address, int cityID, string userType, string email, string userPassword )
        {
            this._userID = userID;
            this._firstName = firstName;
            this._lastName = lastName;
            this._birthDate = birthDate;
            this._phone = phone;
            this._gender = gender;
            this._address = address;
            this._cityID = cityID;
            this._usertype = userType;
            this._email = email;
            this._userPassword = userPassword;
        }
    }
}