using Flurl.Http;
using HttpDoom.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HttpDoom
{
    public sealed class Wordlist
    {
        public string Origin { get; private set; }

        public Action<WordlistError> OnError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin">Origin local or Web url</param>
        public Wordlist(string origin)
        {
            Origin = origin;
        }

        public async Task<IEnumerable<WordlistDomain>> GetAsync()
        {
            if (IsURL(Origin))
            {
                return await GetByWebAsync();

            }
            else if (File.Exists(Origin))
            {
                return await GeByLocalAsync();
            }
            else
            {
                return new List<WordlistDomain>();
            }
        }

        private async Task<IEnumerable<WordlistDomain>> GetByWebAsync()
        {
            try
            {
                //Request with Flurl
                var response = await Origin.GetStringAsync();

                if (!string.IsNullOrEmpty(response))
                {
                    MemoryStream stream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(stream);
                    writer.Write(response);
                    writer.Flush();
                    stream.Position = 0;

                    return await ProcessFileAsync(stream);
                }
            }
            catch (Exception)
            {
                OnError(new WordlistError(WordlistError.Types.FILE_WEB_NOT_FOUND, $"Invalid worlist {Origin}"));
            }

            return default;
        }

        private async Task<IEnumerable<WordlistDomain>> GeByLocalAsync()
        {
            try
            {
                await using var fileStream = new FileStream(Origin, FileMode.Open, FileAccess.Read);

                return await ProcessFileAsync(fileStream);
            }
            catch (Exception)
            {
                OnError(new WordlistError(WordlistError.Types.FILE_LOCAL_NOT_FOUND, $"Invalid worlist {Origin}"));
            }

            return default;
        }

        private async Task<IEnumerable<WordlistDomain>> ProcessFileAsync(Stream stream)
        {
            var domains = new List<WordlistDomain>();

            using var streamReader = new StreamReader(stream);
            while (streamReader.Peek() != -1)
            {
                var target = await streamReader.ReadLineAsync();
                if (string.IsNullOrEmpty(target))
                    continue;

                target = target.RemoveSchema();

                while (target.EndsWith("/"))
                    target = target.Remove(target.Length - 1);

                if (Uri.CheckHostName(target) == UriHostNameType.Unknown)
                {
                    domains.Add(new WordlistDomain()
                    {
                        Domain = target,
                        IsValid = false
                    });

                    OnError(new WordlistError(WordlistError.Types.DOMAIN_UNKNOWN,
                        $"{target} has an invalid format to be a fully qualified domain!"));
                }
                else
                {
                    domains.Add(new WordlistDomain()
                    {
                        Domain = target,
                        IsValid = true
                    });
                }
            }

            return domains;
        }

        public static bool IsURL(string value)
        {
            Uri uriResult;
            return Uri.TryCreate(value, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
        }
    }

    public struct WordlistDomain
    {
        public string Domain { get; set; }

        public bool IsValid { get; set; }     
    }

    public class WordlistError{

        public enum Types
        {
            DOMAIN_UNKNOWN,
            FILE_WEB_NOT_FOUND,
            FILE_LOCAL_NOT_FOUND            
        }

        public string Message { get; set; }
        public readonly Types Type;

        public WordlistError(Types type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
