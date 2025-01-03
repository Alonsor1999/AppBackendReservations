using Application.Common.Response;
using Application.Cqrs.Space.Commands;
using Application.Cqrs.Space.Queries;
using Application.Interfaces.Space;
using BaseBackProjects.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BackendReservations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceController : ApiControllerBase
    {
        /// <summary>
        /// Agrega un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddSpace([FromBody] PostSpaceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Trae todos los usuarios de la base de datos
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await Mediator.Send(new GetSpaceQuery()));
        }
    }
}
