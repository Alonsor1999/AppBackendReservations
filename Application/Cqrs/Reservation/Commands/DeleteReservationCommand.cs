using Application.Common.Response;
using Application.Cqrs.User.Commands;
using Application.Interfaces.Reservation;
using Application.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Reservation.Commands
{
    public class DeleteReservationCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, ApiResponse<bool>>
    {
        private readonly IReservationService _resevationService;
        public DeleteReservationCommandHandler(IReservationService resevationService)
        {
            _resevationService = resevationService;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            return await _resevationService.CancelReservation(request);
        }
    }
}
