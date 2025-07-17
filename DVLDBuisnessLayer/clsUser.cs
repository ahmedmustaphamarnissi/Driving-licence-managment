using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsUser
    {
        public clsPerson _Person;
        public string _Password { get; set; }
        public string _UserName { get; set; }
        public int _UserID { get; set; }
        public bool _IsActive { get; set; }

        public clsUser()
        {
            _Person = null;
            _Password = "";
            _UserName = "";
            _UserID = -1;
            _IsActive = false;
        }

        public clsUser(int PersonID,string Password,string UserName ,int UserID,bool IsActive)
        {
            _Person = clsPerson.GetPersonDataByID(PersonID);
            _Password = Password;
            _UserName = UserName;
            _UserID = UserID;
            _IsActive = IsActive;
        }
        public static bool IsUserActive(ref int ID,string UserName, string Password)
        {
            return UsersData.IsExist(ref ID,UserName, Password);
        } 
        
        public static int GetPersonIDFromUsers(int UserID)
        {
            return UsersData.GetPersonIDByUserID(UserID);
        }
        public static clsUser GetUserInformationByID(int UserID)
        {
            string Password = "", UserName = "";
            int PersonID=-1;
            bool IsActive = false;
            if(UsersData.GetUserInformation(UserID,ref PersonID,ref UserName,ref Password,ref IsActive))
            {
                return new clsUser(PersonID, Password, UserName, UserID, IsActive);
            }
            return new clsUser();
        }

        public static DataTable GetAllUsersData()
        {
            return UsersData.GetAllUsersInformation();
        }
        public static DataTable GetAllUsersDataWithFiltration(string FiltreName,string filtre)
        {
            return UsersData.GetAllUsersInformationWithFiltration(FiltreName, filtre);
        }

        public static DataTable GetAllUsersDataWithFiltration(string FiltreName, int filtre)
        {
            return UsersData.GetAllUsersInformationWithFiltration(FiltreName, filtre);
        }
        public static bool AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            return UsersData.AddNewUserInDataBase(PersonID, UserName, Password, IsActive);
        }
        public static bool DeleteUserWithID(int UserID)
        {
            return UsersData.DeleteUserFromDataBase(UserID);
        }

        public static string GetCurrentPasswordByUserID(int UserID)
        {
            return UsersData.GetCurrentPassword(UserID);
        }
        public static bool UpdateUserPassword(int UserID,string NewPassword)
        {
            return UsersData.UpdateUserPassword(UserID, NewPassword);
        }
        public static bool GetUserInformationByPersonID(int PersonID,ref string Password,ref string UserName
            ,ref int UserID,ref bool IsActive)
        {
            
            return UsersData.GetUserInformationByPersonID(ref UserID, PersonID,ref UserName,ref Password, ref IsActive);
        }

        public static bool UpdateUserInformation(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            return UsersData.UpdateUserInformationFromDataBase(UserID, PersonID, UserName, Password, IsActive);
        }
    }
}
