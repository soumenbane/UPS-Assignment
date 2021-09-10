using Newtonsoft.Json;

namespace UPS.Assignment.EmployeeManagement.Core.Model
{
    /// <summary>
    /// Get and sets properties for pagination JSON response
    /// </summary>
    public class Pagination
    {
        [JsonProperty(PropertyName = "total")]
        public int TotalCount { get; set; }
        [JsonProperty(PropertyName = "pages")]
        public int NoOfPages { get; set; }
        [JsonProperty(PropertyName = "page")]
        public int CurrentPage { get; set; }
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

    }
}
