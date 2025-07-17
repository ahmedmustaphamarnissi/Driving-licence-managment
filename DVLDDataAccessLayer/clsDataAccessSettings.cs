using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    internal class clsDataAccessSettings
    {
        // you can put your data base id and password
        public static string ConnectionString = "Server=.;Database=DVLD;" +
            "User Id=your data base id;Password=your password;";
    }
}
