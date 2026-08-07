using MediatR;

namespace Shopping.Application.Command.AddShoppingList
{
    public record AddShoppingListCommand(string Name) : IRequest<Guid>
    {
    }
}
