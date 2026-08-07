using Expenses.Application.Command.Ai;
using Expenses.Application.Query.GetBudget;
using Expenses.Application.Query.GetBudgetHistory;
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

        [HttpGet("budget/history")]
        public async Task<IActionResult> GetBudgetHistory(int year)
        {
            var result = await _mediator.Send(new GetBudgetHistoryQuery(year));
            return Ok(result);
        }

        [HttpPost("ai-analysis")]
        public async Task<IActionResult> AnalyzeBudget([FromBody] AnalyzeBudgetCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { analysis = result });
        }
    }
}
