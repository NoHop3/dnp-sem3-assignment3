using System.Text.Json.Serialization;

namespace LoginExampleServer.Models
{
    public class Adult : Person
    {
        [JsonPropertyName("Job")] public Job Job { get; set; }
    }
}