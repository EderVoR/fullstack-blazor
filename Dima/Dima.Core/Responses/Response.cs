using System.Text.Json.Serialization;

namespace Dima.Core.Responses
{
    public class Response<TData>
    {
        [JsonConstructor]
        public Response()
            => _code = Configuration.DefaultStatusCode;

        public Response(TData data, int code = Configuration.DefaultStatusCode, string message = null)
        {
            _code = code;
            Data = data;
            Message = message;
        }

        private readonly int _code;
        public TData? Data { get; set; }
        public string? Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess 
            => _code is >= 200 and <= 299;
    }
}
