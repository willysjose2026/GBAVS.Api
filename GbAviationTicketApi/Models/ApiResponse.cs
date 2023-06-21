using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GbAviationTicketApi.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {

        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMesseges { get; set; } = new();
        public object? Result { get; set; } = null;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
