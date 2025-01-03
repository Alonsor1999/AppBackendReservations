using Application.Common.Response;
using Application.DTOs.Space;
using Application.Interfaces.Space;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Space.Commands
{
    public class PostSpaceCommand : IRequest<ApiResponse<SpaceDto>>
    {
        // El DTO que contiene los datos necesarios para la creación de la reserva
        public SpacePostDto SpacePostDto { get; set; }
    }
    public class PostSpaceCommandHandler : IRequestHandler<PostSpaceCommand, ApiResponse<SpaceDto>>
    {
        private readonly ISpaceService _spaceService;
        public PostSpaceCommandHandler(ISpaceService spaceService)
        {
            _spaceService = spaceService;
        }

        public async Task<ApiResponse<SpaceDto>> Handle(PostSpaceCommand request, CancellationToken cancellationToken)
        {
            return await _spaceService.AddSpace(request);
        }
    }
}
