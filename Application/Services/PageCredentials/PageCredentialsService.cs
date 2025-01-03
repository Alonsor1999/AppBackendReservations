using Application.Common.Response;
using Application.DTOs.ApplicationAndNetworkUser;
using Application.DTOs.PageCredentials;
using Application.Interfaces.EncryptAndDecrypt;
using Application.Interfaces.PageCredentials;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Application.Services.PageCredentials
{
    public class PageCredentialsService : IPageCredentialsService
    {
        private readonly string _connectionStringTelefonicaUsers;
        private readonly IEncryptAndDecryptService _encryptAndDecryptService;
        public PageCredentialsService(IConfiguration config, IEncryptAndDecryptService encryptAndDecryptService)
        {
            _connectionStringTelefonicaUsers = config["ConnectionStrings:BDAdministratorUsersTelefonica"];
            _encryptAndDecryptService = encryptAndDecryptService;
        }

        public async Task<ApiResponse<List<PageCredentialsServiceDto>>> sp_GetApplicationAndNetworkUser(ApplicationAndNetworkUser request)
        {
            var connectionStringTelefonicaUsers = _encryptAndDecryptService.Decrypt(_connectionStringTelefonicaUsers);
            List<PageCredentialsServiceDto> PageCredentialsServiceDtoList = new List<PageCredentialsServiceDto>();
            var response = new ApiResponse<List<PageCredentialsServiceDto>>();
            var connection = new SqlConnection(connectionStringTelefonicaUsers);
            connection.Open();
            var adapter = new SqlDataAdapter();

            adapter.SelectCommand = new SqlCommand("SP_Obtener_Aplicativos_x_UsuarioRed", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@usuarioRed", request.NetworkUser);
            var detailApplication = adapter.SelectCommand.ExecuteReader();

            while (detailApplication.Read())
            {
                var PageCredentialsService = new PageCredentialsServiceDto
                {
                    IdApplication = Convert.ToInt32(detailApplication["idAplicativo"]),
                    AppName = detailApplication["NombreApp"].ToString(),
                    Link = detailApplication["Link"].ToString()
                };

                if (request.NeedPassword)
                {
                    PageCredentialsService.User = detailApplication["Usuario"].ToString();
                    PageCredentialsService.UserWebApp = detailApplication["UsuarioAppWeb"].ToString();
                    var SpResult = await sp_GetPasswordApplication(request, PageCredentialsService.IdApplication);
                    PageCredentialsService.Password = SpResult;

                }

                PageCredentialsServiceDtoList.Add(PageCredentialsService);

            }

            response.Data = PageCredentialsServiceDtoList;
            response.Result = true;

            return response;
        }

        public async Task<string?> sp_GetPasswordApplication(ApplicationAndNetworkUser request, int IdApplication)
        {
            var connectionStringTelefonicaUsers = _encryptAndDecryptService.Decrypt(_connectionStringTelefonicaUsers);
            var PasswordApplication = string.Empty;
            var connection = new SqlConnection(connectionStringTelefonicaUsers);
            connection.Open();
            var adapter = new SqlDataAdapter();

            adapter.SelectCommand = new SqlCommand("SP_Obtener_Usuario_Clave_x_UsuarioRed", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@IdAplicativo", IdApplication);
            adapter.SelectCommand.Parameters.AddWithValue("@Usuario", request.NetworkUser);
            var detailApplication = adapter.SelectCommand.ExecuteReader();

            while (detailApplication.Read())
            {
                PasswordApplication = detailApplication["ClaveAplicativo"].ToString();
            }

            return PasswordApplication;
        }
    }
}
