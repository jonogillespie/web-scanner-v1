using System;
using System.Threading.Tasks;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Base
{
    public class AppController : Controller
    {
        private readonly IMediator _mediator;

        public AppController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<ActionResult<T>> Query<T>(IRequest<T> query)
        {
            try
            {
                return Ok(await _mediator.Send(query));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}