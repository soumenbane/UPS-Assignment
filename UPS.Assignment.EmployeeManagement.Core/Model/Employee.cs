using Newtonsoft.Json;

namespace UPS.Assignment.EmployeeManagement.Core.Model
{
    public class Employee
    {
        [JsonProperty(PropertyName = "id")]
        public string EmployeeID { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
