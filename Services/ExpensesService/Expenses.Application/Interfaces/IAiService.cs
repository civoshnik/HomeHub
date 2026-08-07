namespace Expenses.Application.Interfaces
{
    public interface IAiService
    {
        Task<string> GenerateAsync(string prompt);
    }
}
