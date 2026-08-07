using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Models;

namespace Shopping.Application.Command.AddShoppingList
{
    public class AddShoppingListCommandHandler : IRequestHandler<AddShoppingListCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        public AddShoppingListCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) 
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }
        public async Task<Guid> Handle(AddShoppingListCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var profile = await _unitOfWork.UserProfiles.FirstOrDefaultAsync(u => u.UserId == userId)
                ?? throw new Exception($"Профиль пользователя с userId {userId} не найден");

            var list = new ShoppingList
            {
                Id = Guid.NewGuid(),
                HouseholdId = profile.HouseholdId,
                Name = request.Name,
                IsCompleted = false
            };

            await _unitOfWork.ShoppingLists.AddAsync(list, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return list.Id;
        }
    }
}
