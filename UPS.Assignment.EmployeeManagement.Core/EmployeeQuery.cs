using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using UPS.Assignment.EmployeeManagement.Common;

namespace UPS.Assignment.EmployeeManagement.Core
{
    public class EmployeeQuery : IEmployeeQuery
    {

        public EmployeeQuery()
        {
            if (string.IsNullOrEmpty(EmpConfigSettings.RestAPIUrl))
            {
                EmpConfigSettings.RestAPIUrl = ConfigurationManager.AppSettings["baseurl"];
                EmpConfigSettings.APIAuthToken = ConfigurationManager.AppSettings["authtoken"];
            }
        }
        /// <summary>
        /// Generate Url based on I3 Search service supported format
        /// </summary>
        /// <returns></returns>
        public string setQuerySources()
        {
            StringBuilder query = new StringBuilder();

            query.Append(EmpConfigSettings.RestAPIUrl);
            query.Append(string.Concat("?access-token=", EmpConfigSettings.APIAuthToken));

            return query.ToString();
        }
        public string setQuerySources(string SearchText)
        {
            //var es = Uri.EscapeUriString(SearchText);
            //var en = SearchText.ParseString();
            StringBuilder query = new StringBuilder();

            query.Append(EmpConfigSettings.RestAPIUrl);

            query.Append(string.Concat("?access-token=", EmpConfigSettings.APIAuthToken));
            query.Append(string.Concat("&name=", SearchText));
            return query.ToString();
        }
        public string setQuerySources(int pageNo)
        {
            StringBuilder query = new StringBuilder();

            query.Append(EmpConfigSettings.RestAPIUrl);

            query.Append(string.Concat("?page=", pageNo));
            query.Append(string.Concat("&access-token=", EmpConfigSettings.APIAuthToken));
            return query.ToString();
        }
        public string setEditDeleteQuery(int EmpId)
        {
            StringBuilder query = new StringBuilder();

            query.Append(EmpConfigSettings.RestAPIUrl);

            query.Append(string.Concat(EmpId));
            return query.ToString();
        }
        public string setAddQuery()
        {
            StringBuilder query = new StringBuilder();

            query.Append(EmpConfigSettings.RestAPIUrl);

            return query.ToString();
        }
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
