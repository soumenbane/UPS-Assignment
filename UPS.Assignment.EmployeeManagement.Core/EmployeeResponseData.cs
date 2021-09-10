using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPS.Assignment.EmployeeManagement.Common;
using UPS.Assignment.EmployeeManagement.Core.Model;

namespace UPS.Assignment.EmployeeManagement.Core
{
   public class EmployeeResponseData
    {
        /// <summary>
        /// Generate Response deserialized from GoRest API response 
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        [JsonProperty(PropertyName = "meta")]
        public ResponseMetaData MetaData { get; set; }
        [JsonProperty(PropertyName = "data")]
        [JsonConverter(typeof(SingleOrArrayConverter<Employee>))]
        public List<Employee> EmployeeDetails { get; set; }
       

    }
}
