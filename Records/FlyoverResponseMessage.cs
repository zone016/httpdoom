using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace HttpDoom.Records
{
    public class FlyoverResponseMessage
    {
        [JsonPropertyName("domain")] public string Domain { get; set; }
        [JsonPropertyName("requested_uri")] public string Requested { get; set; }
        [JsonPropertyName("port")] public int Port { get; set; }
        [JsonPropertyName("content")] public string Content { get; set; }
        [JsonPropertyName("headers")] public HttpResponseHeaders Headers { get; set; }
        [JsonPropertyName("status_code")] public int StatusCode { get; set; }
    }
}