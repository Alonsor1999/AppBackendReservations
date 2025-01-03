using Application.Common.Response;
using Application.DTOs.Space;
using Application.DTOs.User;
using Application.Interfaces.Space;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cqrs.Space.Queries
{
    public  class GetSpaceQuery : IRequest<ApiResponse<List<SpaceDto>>>
    {

    }
    public class GetSpaceQueryHandler : IRequestHandler<GetSpaceQuery, ApiResponse<List<SpaceDto>>>
    {
        private readonly ISpaceService _spaceService;
        public GetSpaceQueryHandler(ISpaceService SpaceService)
        {
            _spaceService = SpaceService;
        }

        public async Task<ApiResponse<List<SpaceDto>>> Handle(GetSpaceQuery request, CancellationToken cancellationToken)
        {
            return await _spaceService.GetSpace();
        }
    }
}
