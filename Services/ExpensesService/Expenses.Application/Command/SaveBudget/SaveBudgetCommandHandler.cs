using Expenses.Domain.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Application.Command.Budget;

public class SaveBudgetCommandHandler : IRequestHandler<SaveBudgetCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;

    public SaveBudgetCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async System.Threading.Tasks.Task Handle(SaveBudgetCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        var profile = await _unitOfWork.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId)
            ?? throw new Exception("Профиль пользователя не найден");

        var householdId = profile.HouseholdId;

        var existing = await _unitOfWork.HouseholdBudgets.FirstOrDefaultAsync(x =>
                x.HouseholdId == householdId &&
                x.Year == request.Year &&
                x.Month == request.Month,
                cancellationToken);

        if (existing == null)
        {
            existing = new HouseholdBudget
            {
                Id = Guid.NewGuid(),
                HouseholdId = householdId,
                Balance = request.Balance,
                Rent = request.Rent,
                Utilities = request.Utilities,
                Year = request.Year,
                Month = request.Month
            };

            await _unitOfWork.HouseholdBudgets.AddAsync(existing, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        else
        {
            existing.Balance = request.Balance;
            existing.Rent = request.Rent;
            existing.Utilities = request.Utilities;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var oldCategories = _unitOfWork.HouseholdBudgetCategories
            .Where(x => x.HouseholdBudgetId == existing.Id);

        _unitOfWork.HouseholdBudgetCategories.RemoveRange(oldCategories);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var category in request.ExtraCategories)
        {
            await _unitOfWork.HouseholdBudgetCategories.AddAsync(
                new HouseholdBudgetCategory
                {
                    Id = Guid.NewGuid(),
                    HouseholdBudgetId = existing.Id,
                    Name = category.Name,
                    Amount = category.Amount
                },
                cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}