using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsTestType
    {

        public static DataTable GetAllTestTypes()
        {
            return TestTypeData.GetAllTestTypes();
        } 
        public static bool GetTestTypeByID(int TestTypeID, ref string TestTypeTitle, ref string Description, ref decimal TestFees)
        {
            return TestTypeData.GetTestTypeByID(TestTypeID, ref TestTypeTitle, ref Description, ref TestFees);
        }

        public static bool UpdateTestTypeByID(int TestTypeID, string TestTypeTitle, string Description, decimal TestFees)
        {
            return TestTypeData.UpdateTestTypeByID(TestTypeID, TestTypeTitle, Description, TestFees);
        }
        public static decimal GetTestFeesByTestID(int TestTypeID)
        {
            return TestTypeData.GetTestFeesByTestID(TestTypeID);

        }
    }
}
