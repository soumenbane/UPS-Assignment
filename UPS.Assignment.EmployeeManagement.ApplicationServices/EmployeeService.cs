using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UPS.Assignment.EmployeeManagement.Common;
using UPS.Assignment.EmployeeManagement.Core;
using UPS.Assignment.EmployeeManagement.Core.Model;

namespace UPS.Assignment.EmployeeManagement.ApplicationServices
{
    public class EmployeeService : IEmployeeService, IDisposable
    {
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Add employee data using GoRest API
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Email"></param>
        /// <param name="Gender"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public string AddEmployee(string Name, string Email, string Gender, string Status)
        {
            Employee emp = new Employee();
            emp.Name = Name;
            emp.Email = Email;
            emp.Gender = Gender;
            emp.Status = Status;
            string json = JsonConvert.SerializeObject(emp, Formatting.Indented);
            using (var employeeQry = new EmployeeQuery())
            {
                return AddEmployee(employeeQry.setAddQuery(),json);
            }
        }
        /// <summary>
        /// Edit employee data using GoRest API
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="Name"></param>
        /// <param name="Email"></param>
        /// <param name="Gender"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public string EditEmployee(int EmployeeId, string Name, string Email, string Gender, string Status)
        {
            Employee emp = new Employee();
            emp.EmployeeID = EmployeeId.ToString();
            emp.Name = Name;
            emp.Email = Email;
            emp.Gender = Gender;
            emp.Status = Status;
            string json = JsonConvert.SerializeObject(emp, Formatting.Indented);
            using (var employeeQry = new EmployeeQuery())
            {
                return EditEmployee(employeeQry.setEditDeleteQuery(EmployeeId), json);
            }
        }
        /// <summary>
        /// delete employee data using GoRest API by employee id
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public string DeleteEmployee(int EmployeeId)
        {
            using (var employeeQry = new EmployeeQuery())
            {
                return DeleteEmployee(employeeQry.setEditDeleteQuery(EmployeeId));
            }
        }

        /// <summary>
        /// Get emplyoee data 
        /// </summary>
        /// <returns></returns>
        public EmployeeResponseData GetEmployee()
        {
            using (var employeeQry = new EmployeeQuery())
            {
                return EmployeeResults(employeeQry.setQuerySources());
            }
        }
        /// <summary>
        /// get pagewise employee data by passing page no
        /// </summary>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public EmployeeResponseData GetEmployeePagewise(int pageNo)
        {
            using (var employeeQry = new EmployeeQuery())
            {
                return EmployeeResults(employeeQry.setQuerySources(pageNo));
            }
        }
        /// <summary>
        /// get employee list searched by search text
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public EmployeeResponseData SearchEmployee(string SearchText)
        {
            using (var employeeQry = new EmployeeQuery())
            {
                return EmployeeResults(employeeQry.setQuerySources(SearchText));
            }
        }
        /// <summary>
        /// Return 1st 20 employee results as per API pattern.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private EmployeeResponseData EmployeeResults(string query)
        {
            EmployeeResponseData EmployeeResponse = null;

            using (var httpClient = new HttpClient(new HttpClientHandler()
            {

            }))
            {
                HttpResponseMessage res;
                try
                {

                    res = httpClient.GetAsync(query).Result;

                    JObject responsestr = JObject.Parse(res.Content.ReadAsStringAsync().Result.ToString());

                    if (res.StatusCode.ToString().ToLower() != "ok")
                    {
                        return null;
                    }

                    EmployeeResponse = JsonConvert.DeserializeObject<EmployeeResponseData>(responsestr.ToString());


                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return EmployeeResponse;
        }
        /// <summary>
        /// Delete employee API request method
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string DeleteEmployee(string query)
        {
            using (var httpClient = new HttpClient(new HttpClientHandler()
            {

            }))
            {
                HttpResponseMessage res;
                try
                {
                    
                    httpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", EmpConfigSettings.APIAuthToken);

                    res = httpClient.DeleteAsync(query).Result;

                    JObject responsestr = JObject.Parse(res.Content.ReadAsStringAsync().Result.ToString());

                    if (res.StatusCode.ToString().ToLower() != "ok")
                    {
                        return null;
                    }

                    EmployeeResponseData EmployeeResponse = JsonConvert.DeserializeObject<EmployeeResponseData>(responsestr.ToString());
                    if (Convert.ToInt32(EmployeeResponse.Code) == 204)
                        return "Success";
                    else
                        return "Failed";
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            
        }
        /// <summary>
        /// Add employee API request method
        /// </summary>
        /// <param name="query"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        private string AddEmployee(string query,string json)
        {
            using (var httpClient = new HttpClient(new HttpClientHandler()
            {

            }))
            {
                HttpResponseMessage res;
                try
                {

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", EmpConfigSettings.APIAuthToken);

                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    res = httpClient.PostAsync(query, httpContent).Result;

                    JObject responsestr = JObject.Parse(res.Content.ReadAsStringAsync().Result.ToString());

                    if (res.StatusCode.ToString().ToLower() != "ok")
                    {
                        return null;
                    }

                    EmployeeResponseData EmployeeResponse = JsonConvert.DeserializeObject<EmployeeResponseData>(responsestr.ToString());
                    if (Convert.ToInt32(EmployeeResponse.Code) == 201)
                        return "Success";
                    else
                        return "Failed";
                }
                catch (Exception ex)
                {
                    return null;
                }

            }

        }
        /// <summary>
        /// Edit employee API request method
        /// </summary>
        /// <param name="query"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        private string EditEmployee(string query, string json)
        {
            using (var httpClient = new HttpClient(new HttpClientHandler()
            {

            }))
            {
                HttpResponseMessage res;
                try
                {

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");
                    
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json"); 
                    res = httpClient.PutAsync(query, httpContent).Result;

                    JObject responsestr = JObject.Parse(res.Content.ReadAsStringAsync().Result.ToString());

                    if (res.StatusCode.ToString().ToLower() != "ok")
                    {
                        return null;
                    }
                    string s = responsestr.ToString();
                    EmployeeResponseData EmployeeResponse = JsonConvert.DeserializeObject<EmployeeResponseData>(responsestr.ToString());
                    if (Convert.ToInt32(EmployeeResponse.Code) == 200)
                        return "Success";
                    else
                        return "Failed";
                }
                catch (Exception ex)
                {
                    return null;
                }

            }

        }

    }
}
