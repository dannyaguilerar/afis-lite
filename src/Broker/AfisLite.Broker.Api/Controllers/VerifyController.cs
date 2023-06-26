using AfisLite.Broker.Core.PersonAggregate.Queries;
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
            var result = await _mediator.Send(new GetAllPeopleQuery());
            return Ok(result);
        }

    }
}
