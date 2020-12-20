using System;

namespace HttpDoom.Records
{
    public class Options
    {
        public bool Debug { get; set; } = false;
        public int HttpTimeout { get; set; } = 5000;
        public int Threads { get; set; } = Environment.ProcessorCount;
        public string OutputFile { get; set; }
        public int[] Ports { get; set; } = {80, 443, 8080, 8443};
        public string Proxy { get; set; }
        public string WordList { get; set; }
    }
}