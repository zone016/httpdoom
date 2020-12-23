using System.Net;
using System.Net.Http.Headers;

namespace HttpDoom.Records
{
    public class FlyoverResponseMessage
    {
        public string Domain { get; set; }
        public string[] Addresses { get; set; }
        public string Requested { get; set; }
        public int Port { get; set; }
        public string Content { get; set; }
        public string ScreenshotPath { get; set; }
        public HttpResponseHeaders Headers { get; set; }
        public CookieCollection Cookies { get; set; }
        public int StatusCode { get; set; }
    }
}