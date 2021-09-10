using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPS.Assignment.EmployeeManagement.Core
{
    interface IEmployeeQuery : IDisposable
    {
        string setQuerySources();
        string setQuerySources(int PageNo);
        string setQuerySources(string SearchText);

    }
}
