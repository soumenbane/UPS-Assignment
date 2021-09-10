using Newtonsoft.Json;

namespace UPS.Assignment.EmployeeManagement.Core.Model
{
    /// <summary>
    /// Get and sets properties for meta section of JSON response
    /// </summary>
    public class ResponseMetaData
    {
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; set; }
        
        
    }
}
