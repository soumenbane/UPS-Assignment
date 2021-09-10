using System;
using UPS.Assignment.EmployeeManagement.Core;

namespace UPS.Assignment.EmployeeManagement.ApplicationServices
{
    interface IEmployeeService
    {
        String AddEmployee(String Name, String Email, String Gender, String Status);
        String EditEmployee(int EmployeeId, String Name, String Email, String Gender, String Status);
        String DeleteEmployee(int EmployeeId);
        EmployeeResponseData GetEmployee();
        EmployeeResponseData GetEmployeePagewise(int PageNo);
        EmployeeResponseData SearchEmployee(String SearchText);
    }
}
