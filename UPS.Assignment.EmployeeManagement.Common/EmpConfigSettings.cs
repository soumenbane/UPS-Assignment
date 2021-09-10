using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UPS.Assignment.EmployeeManagement.Common
{
    public static class EmpConfigSettings 
    {
        public static string RestAPIUrl { get; set; }
        public static string APIAuthToken { get; set; }
    }
}
