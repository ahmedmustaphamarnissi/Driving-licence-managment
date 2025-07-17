using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsPerson
    {
        public string _FirstName { get; set; }
        public string _SecondName { get; set; }
        public string _ThirdName { get; set; }
        public string _LastName { get; set; }
        public string _NationalNo { get; set; }
        public string _Phone { get; set; }
        public string _Email { get; set; }
        public string _Address { get; set; }
        public string _ImagePath { get; set; }
        public int _PersonID { get; set; }
        public int _CountryID { get; set; }
        public int _Gendor { get; set; }
        public DateTime _BirthDate { get; set; }

        clsPerson()
        {
            _PersonID = -1;
            _FirstName = "";
            _SecondName = "";
            _ThirdName = "";
            _LastName = "";
            _NationalNo = "";
            _Phone = "";
            _Email = "";
            _Address = "";
            _ImagePath = "";
            _CountryID = -1;
            _Gendor = -1;
            _BirthDate = DateTime.Now;
        }

        clsPerson(int PersonID,string NationalNo, string FirstName, string SecondName
            , string ThirdName, string LastName, string Phone, DateTime DateOfBirth, int Gendor, string Address,
            string Email, int NationalityCountryID, string ImagePath)
        {
            _PersonID = PersonID;
            _FirstName = FirstName;
            _SecondName = SecondName;
            _ThirdName = ThirdName;
            _LastName = LastName;
            _NationalNo = NationalNo;
            _Phone = Phone;
            _Email = Email;
            _Address = Address;
            _ImagePath = ImagePath;
            _CountryID = NationalityCountryID;
            _Gendor = Gendor;
            _BirthDate = DateOfBirth;
        }
        public static DataTable GetAllPeopleData()
        {
            return PeopleData.GetPeopleData();
        }
        public static DataTable FiltrePeople(string Filtre)
        {
            return PeopleData.GetPeopleDataByFiltre(Filtre);
        }
        public static DataTable FiltrePeopleWithTextBox(string Filtre,string Text)
        {
            return PeopleData.GetPeopleDataByStartWith(Filtre, Text);
        }

        public static bool IsPersonExistByNationalNo(string NationalNo)
        {
            return PeopleData.IsPersonExistWithNationalNo(NationalNo);
        }

        public static DataTable GetAllCountries()
        {
            return PeopleData.GetAllCountrieNames();
        }
        public static bool AddPerson(string NationalNo, string FirstName, string SecondName
            , string ThirdName, string LastName,string Phone, DateTime DateOfBirth, int Gendor, string Address,
            string Email, int NationalityCountryID, string ImagePath)
        {
            return PeopleData.AddPersonToDatabase(NationalNo, FirstName, SecondName, ThirdName, LastName,Phone
                , DateOfBirth,Gendor, Address, Email, NationalityCountryID, ImagePath);
        }

        public static clsPerson GetPersonDataByID(int ID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Phone = "", Email = "", Address = "", ImagePath = "";
            int CountryID = -1, Gendor = -1;
            DateTime BirthDate = DateTime.MinValue;
            PeopleData.GetPersonInformationByPersonID(ref ID, ref NationalNo, ref FirstName, ref SecondName,
                ref ThirdName, ref LastName, ref Phone, ref BirthDate, ref Gendor, ref Address,
                ref Email, ref CountryID, ref ImagePath);
            if (ID == -1)
            {
                return new clsPerson();
            }
            else
            {
                return new clsPerson(ID, NationalNo, FirstName, SecondName, ThirdName, LastName, Phone
                , BirthDate, Gendor, Address, Email, CountryID, ImagePath);
            }
        }
        public static clsPerson GetPersonDataByNationalNo(string NationalNo )
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Phone = "", Email = "", Address = "", ImagePath = "";
            int CountryID = -1, Gendor = -1,ID=-1;
            DateTime BirthDate = DateTime.MinValue;
            PeopleData.GetPersonInformationByNationalNo(ref ID, ref NationalNo, ref FirstName, ref SecondName,
                ref ThirdName, ref LastName, ref Phone, ref BirthDate, ref Gendor, ref Address,
                ref Email, ref CountryID, ref ImagePath);
            if (ID == -1)
            {
                return new clsPerson();
            }
            else
            {
                return new clsPerson(ID, NationalNo, FirstName, SecondName, ThirdName, LastName, Phone
                , BirthDate, Gendor, Address, Email, CountryID, ImagePath);
            }
        }
        public static bool UpdatePersoninformation(int ID, string NationalNo, string FirstName, string SecondName,
     string ThirdName, string LastName, string Phone, DateTime DateOfBirth, int Gendor,
     string Address, string Email, int NationalityCountryID, string ImagePath)
        {
            return PeopleData.UpdatePersonInfo(ID,NationalNo, FirstName, SecondName, ThirdName, LastName, Phone
                , DateOfBirth, Gendor, Address, Email, NationalityCountryID, ImagePath);
        }

        public static bool DeletePerson(int PersonID)
        {
            return PeopleData.DeletePersonFromDataBase(PersonID);
        }
        public string GetCountryName()
        {
            return PeopleData.GetCountryNameUsingID(_CountryID);
        }

        public static int GetLastID()
        {
            return PeopleData.GetLastPersonID();
        }
        public static string GetPersonNameByPersonID(int PersonID)
        {
            return PeopleData.GetFullNameByID(PersonID);
        }
    }
}
