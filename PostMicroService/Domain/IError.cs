using Newtonsoft.Json;

namespace PostMicroService.Domain
{
    public class IError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
