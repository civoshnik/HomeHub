using MediatR;

namespace Expenses.Application.Command.Ai
{
    public record AnalyzeBudgetCommand(decimal Balance,decimal Rent,decimal Utilities,List<ExtraCategoryDto> ExtraCategories) : IRequest<string>;
}
