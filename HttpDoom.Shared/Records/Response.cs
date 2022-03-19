using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;

namespace HttpDoom.Shared.Records
{
    public record HttpDoomResponse
    {
        public HttpResponseHeaders ResponseHeaders { get; set; }
        public HttpRequestHeaders RequestHeaders { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<Cookie> Cookies { get; set; }
        public Uri RedirectUri { get; set; }
        public Uri OriginUri { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public string[] Addresses { get; set; }
        public string Content { get; set; }
        public string ContentSha256Sum { get; set; }
        public string ScreenshotPath { get; set; }

    }
}