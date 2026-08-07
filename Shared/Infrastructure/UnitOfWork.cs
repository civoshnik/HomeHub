using Analytics.Domain.Models;
using Auth.Domain.Models;
using Expenses.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Notifications.Domain.Models;
using Payment.Domain.Models;
using Shopping.Domain.Models;
using Task.Domain.Models;
using User.Domain.Models;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public DbSet<UserCredential> Users => _context.Users;
    public DbSet<RefreshToken> RefreshTokens => _context.RefreshTokens;
    public DbSet<UserProfile> UserProfiles => _context.UserProfiles;
    public DbSet<Household> Households => _context.Households;
    public DbSet<HouseholdMember> HouseholdMembers => _context.HouseholdMembers;
    public DbSet<Category> Categories => _context.Categories;
    public DbSet<Expense> Expenses => _context.Expenses;
    public DbSet<RecurringExpense> RecurringExpenses => _context.RecurringExpenses;
    public DbSet<CategoryExpenseSummary> CategoryExpenseSummaries => _context.CategoryExpenseSummaries;
    public DbSet<MonthlyExpenseSummary> MonthlyExpenseSummaries => _context.MonthlyExpenseSummaries;
    public DbSet<Notification> Notifications => _context.Notifications;
    public DbSet<Bill> Bills => _context.Bills;
    public DbSet<ShoppingList> ShoppingLists => _context.ShoppingLists;
    public DbSet<ShoppingItem> ShoppingItems => _context.ShoppingItems;
    public DbSet<TaskItem> Tasks => _context.Tasks;
    public DbSet<HouseholdBudget> HouseholdBudgets => _context.HouseholdBudgets;
    public DbSet<HouseholdBudgetCategory> HouseholdBudgetCategories => _context.HouseholdBudgetCategories;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        => await _context.Database.BeginTransactionAsync(cancellationToken);
    
}