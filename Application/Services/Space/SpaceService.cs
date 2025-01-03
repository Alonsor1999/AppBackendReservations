using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Cqrs.Space.Commands;
using Application.Cqrs.Space.Queries;
using Application.DTOs.Space;
using Application.Interfaces.Space;
using AutoMapper;
using Domain.Interfaces;

namespace Application.Services.Space
{
    public class SpaceService : ISpaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        public SpaceService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }

        public async Task<ApiResponse<List<SpaceDto>>> GetSpace()
        {
            var response = new ApiResponse<List<SpaceDto>>();

            try
            {
                response.Data = _autoMapper.Map<List<SpaceDto>>(_unitOfWork.SpaceRepository.Get().ToList());
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al actualizar el registro, consulte con el administrador. {ex.Message} ";
            }

            return response;
        }

        
        public async Task<ApiResponse<SpaceDto>> AddSpace(PostSpaceCommand request)
        {
            var response = new ApiResponse<SpaceDto>();

            try
            {
                var Space = _autoMapper.Map<Domain.Models.Space.Space>(request.SpacePostDto);

                response.Data = _autoMapper.Map<SpaceDto>(await _unitOfWork.SpaceRepository.Add(Space));
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al Crear Usuario. {ex.Message} ";
            }

            return response;
        }
    }
}
