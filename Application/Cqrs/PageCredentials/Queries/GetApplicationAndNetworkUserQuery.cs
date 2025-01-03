using Application.Common.Response;
using Application.DTOs.ApplicationAndNetworkUser;
using Application.DTOs.Auth;
using Application.DTOs.PageCredentials;
using Application.Interfaces.EncryptAndDecrypt;
using Application.Interfaces.PageCredentials;
using Application.Services.EncryptAndDecrypt;
using MediatR;
using Newtonsoft.Json;

namespace Application.Cqrs.PageCredentials.Queries
{
    public class GetApplicationAndNetworkUserQuery : IRequest<string>
    {
        public string? String { get; set; }
    }
    public class GetApplicationAndNetworkUserQueryHandler : IRequestHandler<GetApplicationAndNetworkUserQuery, string>
    {
        private readonly IPageCredentialsService _pageCredentialsService;
        private readonly IEncryptAndDecryptService _encryptAndDecryptService;
        public GetApplicationAndNetworkUserQueryHandler(IPageCredentialsService pageCredentialsService, IEncryptAndDecryptService encryptAndDecryptService)
        {
            _pageCredentialsService = pageCredentialsService;
            _encryptAndDecryptService = encryptAndDecryptService;
        }

        public async Task<string> Handle(GetApplicationAndNetworkUserQuery request, CancellationToken cancellationToken)
        {
            var decryptedData = _encryptAndDecryptService.Decrypt(request.String);
            var decryptedJsonData = JsonConvert.DeserializeObject<ApplicationAndNetworkUser>(decryptedData);

            var response = await _pageCredentialsService.sp_GetApplicationAndNetworkUser(decryptedJsonData);

            var ResponseJson = JsonConvert.SerializeObject(response);
            var encryptedData = _encryptAndDecryptService.Encrypt(ResponseJson);

            return encryptedData;
        }
    }
}
