using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPS.Assignment.EmployeeManagement.Data.Model;

namespace UPS.Assignment.EmployeeManagement.Data
{
    public class EmployeeManagementContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
