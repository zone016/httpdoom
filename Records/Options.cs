using System;

namespace HttpDoom.Records
{
    public class Options
    {
        public bool Debug { get; set; } = false;
        public bool Screenshot { get; set; } = false;
        public int HttpTimeout { get; set; } = 5000;
        public int Threads { get; set; } = Environment.ProcessorCount;
        public string OutputDirectory { get; set; }
        public int[] Ports { get; set; } = {80, 443, 8080, 8443};

        public string[] Headers { get; set; } =
        {
            "User-Agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/87.0.4280.88 Safari/537.36"
        };

        public string Proxy { get; set; }
        public string WordList { get; set; }
    }
}