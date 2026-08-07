using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Application.Query.GetBudgetHistory
{
    public class GetBudgetHistoryQueryHandler
     : IRequestHandler<GetBudgetHistoryQuery, List<MonthlyBudgetDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public GetBudgetHistoryQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<List<MonthlyBudgetDto>> Handle(GetBudgetHistoryQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var profile = await _unitOfWork.UserProfiles.FirstAsync(x => x.UserId == userId, cancellationToken);

            var householdId = profile.HouseholdId;

            var budgets = await _unitOfWork.HouseholdBudgets.Where(x => x.HouseholdId == householdId && x.Year == request.Year).ToListAsync(cancellationToken);

            return budgets
                .Select(x => new MonthlyBudgetDto(
                    x.Month,
                    x.Rent + x.Utilities +
                    _unitOfWork.HouseholdBudgetCategories
                        .Where(c => c.HouseholdBudgetId == x.Id)
                        .Sum(c => c.Amount)
                ))
                .ToList();
        }
    }
}
