using Application.Common.Response;
using Application.Cqrs.Space.Commands;
using Application.DTOs.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Space
{
    public interface ISpaceService
    {
        Task<ApiResponse<SpaceDto>> AddSpace(PostSpaceCommand request);
        Task<ApiResponse<List<SpaceDto>>> GetSpace();
    }
}
