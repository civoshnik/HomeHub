using Analytics.Domain.Models;
using Auth.Domain.Models;
using Expenses.Domain.Models;
using Notifications.Domain.Models;
using Payment.Domain.Models;
using Shopping.Domain.Models;
using Task.Domain.Models;
using User.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public interface IUnitOfWork
    {
        DbSet<UserCredential> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }

        DbSet<UserProfile> UserProfiles { get; }
        DbSet<Household> Households { get; }
        DbSet<HouseholdMember> HouseholdMembers { get; }

        DbSet<Category> Categories { get; }
        DbSet<Expense> Expenses { get; }
        DbSet<RecurringExpense> RecurringExpenses { get; }

        DbSet<CategoryExpenseSummary> CategoryExpenseSummaries { get; }
        DbSet<MonthlyExpenseSummary> MonthlyExpenseSummaries { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<Bill> Bills { get; }
        DbSet<ShoppingList> ShoppingLists { get; }
        DbSet<ShoppingItem> ShoppingItems { get; }
        DbSet<TaskItem> Tasks { get; }
        DbSet<HouseholdBudget> HouseholdBudgets { get; }
        DbSet<HouseholdBudgetCategory> HouseholdBudgetCategories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}