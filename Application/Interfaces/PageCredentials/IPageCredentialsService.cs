using Application.Common.Response;
using Application.Cqrs.PageCredentials.Queries;
using Application.DTOs.ApplicationAndNetworkUser;
using Application.DTOs.PageCredentials;

namespace Application.Interfaces.PageCredentials
{
    public interface IPageCredentialsService
    {
        Task<ApiResponse<List<PageCredentialsServiceDto>>> sp_GetApplicationAndNetworkUser(ApplicationAndNetworkUser request);
    }
}