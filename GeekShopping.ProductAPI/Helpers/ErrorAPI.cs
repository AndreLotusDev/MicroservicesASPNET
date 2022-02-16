using System.Net;

namespace GeekShopping.ProductAPI.Helpers
{
    public class ErrorAPI
    {
        public string DefaultMessage { get; set; }
        public IEnumerable<string> ListErrors { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
