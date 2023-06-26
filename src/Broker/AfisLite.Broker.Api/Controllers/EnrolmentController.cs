using AfisLite.Broker.Core.EnrolmentAggregate.Commands;
using AfisLite.Broker.Core.EnrolmentAggregate.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AfisLite.Broker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrolmentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetAllEnrolmentsQuery());
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Index([FromBody] CreateEnrolmentCommand command)
        {
            _mediator.Send(command);
            return Ok();
        }
    }
}
