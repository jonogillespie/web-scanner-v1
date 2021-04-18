using System.Threading.Tasks;
using Application.UseCases.Scans.Queries;
using Application.UseCases.Scans.Queries.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/scan-cycles")]
    public class ScanCyclesController : AppController
    {
        public ScanCyclesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("latest")]
        public Task<ActionResult<ScanVm>> Get()
        {
            return Query(new GetRecentScansQuery());
        }
    }
}