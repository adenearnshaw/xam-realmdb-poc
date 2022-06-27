using System.Text.Json.Serialization;

namespace XamRealmDb.Api;
public class DataObjectDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}
