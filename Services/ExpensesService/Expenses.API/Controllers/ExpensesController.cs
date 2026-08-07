using Expenses.Application.Query.GetBudget;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    [Authorize]
    public class ExpensesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("budget")]
        public async Task<IActionResult> SaveBudget([FromBody] SaveBudgetCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("budget")]
        public async Task<IActionResult> GetBudget([FromQuery] int year,[FromQuery] int month)
        {
            var result = await _mediator.Send(new GetBudgetQuery(year, month));
            return Ok(result);
        }
    }
}
