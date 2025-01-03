using Application.Common.Exceptions;
using Application.DTOs.Auth;
using Application.Interfaces.Auths;
using Application.Interfaces.EncryptAndDecrypt;
using Application.ViewModel.Auth;
using MediatR;
using Newtonsoft.Json;

namespace Application.Cqrs.Auth.Commands
{
    public class PostLoginCommand : IRequest<AuthViewModel>
    {
        //public string? String { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        //public string? IpRemote { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }

    }

    public class PostLoginCommandHandler : IRequestHandler<PostLoginCommand, AuthViewModel>
    {
        private IAuthService _authService;
        //private ILogger<PostLoginCommandHandler> _logger;
        //private readonly IEncryptAndDecryptService _encryptAndDecryptService;
        
        public PostLoginCommandHandler(
            IAuthService authService
            //IEncryptAndDecryptService encryptAndDecryptService
        //ILogger<PostLoginCommandHandler> logger
        )
        {
            _authService = authService;
            //_encryptAndDecryptService = encryptAndDecryptService;
            //_logger = logger;
        }

        public async Task<AuthViewModel> Handle(PostLoginCommand request, CancellationToken cancellationToken)
        {
            //var decryptedData = _encryptAndDecryptService.Decrypt(request.String);
            //var decryptedJsonData = JsonConvert.DeserializeObject<PostLoginAuth>(decryptedData);

            //var authData = await _authService.GetAuth(decryptedJsonData) ??
            var authData = await _authService.GetAuth(request) ??
                throw new BadRequestException("No se ha podido ingresar el token");
            
            //authData.IpRemote = request.IpRemote;

            //var AuthDataJson = JsonConvert.SerializeObject(authData);
            //var encryptedData = _encryptAndDecryptService.Encrypt(AuthDataJson);

            //return encryptedData;
            return authData;
        }
    }

}
