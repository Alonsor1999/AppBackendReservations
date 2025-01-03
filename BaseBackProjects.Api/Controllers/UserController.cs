using Application.Cqrs.User.Commands;
using Application.Cqrs.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseBackProjects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        /// <summary>
        /// Agrega un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] PostUserCommand command)
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
            return Ok(await Mediator.Send(new GetUsersQuery()));
        }

        /// <summary>
        /// Obtener Usuario por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(Guid Id)
        {
            return Ok(await Mediator.Send(new GetUsersQueryById() { Id = Id }));
        }

        /// <summary>
        /// Actualiza los Usuarios
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] PutUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Eliminar Usuarios
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand() { Id = Id }));
        }

    }
}
