using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace LoginExampleServer.Models
{
    public class Job
    {
        [Key]
        [JsonPropertyName("JobId")]
        public int Id { get; set; }
        [JsonPropertyName("JobTitle")]
        public string JobTitle { get; set; }
        [JsonPropertyName("Salary")]
        public int Salary { get; set; }
        
        [JsonIgnore]
        public ICollection<Adult> Adults { get; set; }
    }
}