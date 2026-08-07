using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Application.Query.GetBudget
{
    public class GetBudgetQueryHandler : IRequestHandler<GetBudgetQuery, BudgetDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public GetBudgetQueryHandler(IUnitOfWork unitOfWork,ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<BudgetDto?> Handle(GetBudgetQuery request,CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var profile = await _unitOfWork.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (profile == null)
                return null;

            var householdId = profile.HouseholdId;

            var budget = await _unitOfWork.HouseholdBudgets
                .FirstOrDefaultAsync(x =>
                    x.HouseholdId == householdId &&
                    x.Year == request.Year &&
                    x.Month == request.Month,
                    cancellationToken);

            if (budget == null)
                return null;

            var categories = await _unitOfWork.HouseholdBudgetCategories
                .Where(x => x.HouseholdBudgetId == budget.Id)
                .Select(x => new ExtraCategoryDto(x.Name, x.Amount))
                .ToListAsync(cancellationToken);

            return new BudgetDto(
                budget.Balance,
                budget.Rent,
                budget.Utilities,
                categories
            );
        }
    }
}
