using Application.Services.EncryptAndDecrypt;
using Microsoft.Extensions.Configuration;

namespace Application.Common.ConnectionString
{
    public class ConnectionString
    {
        private readonly IConfiguration _configuration;
        private readonly string Connection = "NqlPb3bcz/c75ig3ASNQYg==bteOKmATaP3nnNyiserc850skAKBiZnCIRwMIk/jZBziecv+wc2SGdvneQAXMowcgugD4z+2+lXSHEZHr8ltfphVb7oiPXqFPgrdvfslvkh5i9JEhaR909ajsPGtw4qIEcfS8fCGSqKMxlqmqBN5KRFgFp9q872uye0mqqa4gHUWEHBBocqumnHCKIm8XErVQt5m2o7vvS9WWKhawPTYSD4w9+ysJtkfNsEVLVXQtJ4av2WEkFoRtbbAgErIVSnG";
        public ConnectionString(IConfiguration config)
        {
            _configuration = config;
        }
        public string? AplicationDBContext { get; set; }

        public string DecryptConnection()
        {
            var _encryptAndDecryptService = new EncryptAndDecryptService(_configuration);
            return this.AplicationDBContext = _encryptAndDecryptService.Decrypt(Connection);
        }
    }
}
