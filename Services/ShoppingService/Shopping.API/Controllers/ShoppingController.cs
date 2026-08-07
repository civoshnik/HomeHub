using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Command.AddShoppingList;

namespace Shopping.API.Controllers
{
    [ApiController]
    [Route("api/shopping")]
    public class ShoppingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/shopping/list")]
        public async Task<IActionResult> GetBudgetHistory(string name)
        {
            var result = await _mediator.Send(new AddShoppingListCommand(name));
            return Ok(result);
        }
    }
}
