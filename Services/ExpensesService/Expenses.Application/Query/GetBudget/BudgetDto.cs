namespace Expenses.Application.Query.GetBudget
{
    public record BudgetDto(decimal Balance, decimal Rent, decimal Utilities, List<ExtraCategoryDto> ExtraCategories);
}