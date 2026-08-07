using MediatR;

namespace Expenses.Application.Query.GetBudget
{
    public record GetBudgetQuery(int Year, int Month) : IRequest<BudgetDto?>;
}
