using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LoginExampleServer.Models {
public class Person {
    [Key]
    [Required]
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [Required]
    [NotNull]
    [JsonPropertyName("FirstName")]
    public string FirstName { get; set; }
    [Required]
    [NotNull]
    [JsonPropertyName("LastName")]
    public string LastName { get; set; }

    [NotNull]
    [DefaultValue("Black")]
    [JsonPropertyName("HairColor")]
    public string HairColor { get; set; }

    [NotNull]
    [DefaultValue("Blue")]
    [JsonPropertyName("EyeColor")]
    public string EyeColor { get; set; }
    [JsonPropertyName("Age")]
    public int Age { get; set; }
    [JsonPropertyName("Weight")]
    public float Weight { get; set; }
    [JsonPropertyName("Height")]
    public int Height { get; set; }

    [NotNull]
    [DefaultValue("M")]
    [JsonPropertyName("Sex")]
    public string Sex { get; set; }
}


}