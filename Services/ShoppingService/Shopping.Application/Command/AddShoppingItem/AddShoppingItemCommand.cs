using MediatR;

namespace Shopping.Application.Command.AddShoppingItem
{
    public record AddShoppingItemCommand(Guid ShoppingListId, string Name, int Quantity, decimal Price) : IRequest;
}
