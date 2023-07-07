using AfisLite.Broker.Core.PersonAggregate.Queries;
using AfisLite.Broker.Core.VerifyAggregate.Commands;
using AfisLite.Broker.Core.VerifyAggregate.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AfisLite.Broker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VerifyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetAllVerifiesQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] CreateSingleVerifyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
