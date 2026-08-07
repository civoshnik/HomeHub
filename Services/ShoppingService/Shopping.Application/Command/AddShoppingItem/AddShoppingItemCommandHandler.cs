using Infrastructure;
using MediatR;
using Shopping.Domain.Models;

namespace Shopping.Application.Command.AddShoppingItem
{
    public class AddShoppingItemCommandHandler : IRequestHandler<AddShoppingItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddShoppingItemCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
        }
        public async System.Threading.Tasks.Task Handle(AddShoppingItemCommand request, CancellationToken cancellationToken)
        {
            var item = new ShoppingItem
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ShoppingListId = request.ShoppingListId,
                Quantity = request.Quantity,
                Price = request.Price,
                IsPurchased = false
            };

            await _unitOfWork.ShoppingItems.AddAsync(item, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
