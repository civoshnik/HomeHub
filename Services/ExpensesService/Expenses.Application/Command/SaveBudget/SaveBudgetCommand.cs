using MediatR;

public record SaveBudgetCommand(decimal Balance, decimal Rent, decimal Utilities, List<ExtraCategoryDto> ExtraCategories, int Year, int Month) : IRequest;