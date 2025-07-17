using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsTests
    {

        public static bool AddNewTest(int TestAppointmentID, bool TestResult, int CreatedByUserID, string Notes = "")
        {
            return TestsData.AddNewTest(TestAppointmentID, TestResult, CreatedByUserID, Notes);
        }
    }
}
