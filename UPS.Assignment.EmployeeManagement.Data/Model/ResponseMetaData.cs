using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPS.Assignment.EmployeeManagement.Data.Model
{
    public class ResponseMetaData
    {
        [JsonProperty(PropertyName = "pagination")]
        public string Pagination { get; set; }
        [JsonProperty(PropertyName = "total")]
        public string TotalCount { get; set; }
        [JsonProperty(PropertyName = "pages")]
        public string NoOfPages { get; set; }
        [JsonProperty(PropertyName = "page")]
        public string CurrentPage { get; set; }
        [JsonProperty(PropertyName = "limit")]
        public string Limit { get; set; }
    }
}
