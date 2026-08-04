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
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserCredential> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Household> Households { get; set; }
        public DbSet<HouseholdMember> HouseholdMembers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<RecurringExpense> RecurringExpenses { get; set; }
        public DbSet<CategoryExpenseSummary> CategoryExpenseSummaries { get; set; }
        public DbSet<MonthlyExpenseSummary> MonthlyExpenseSummaries { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<UserCredential>(entity =>
            {

                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Email).IsUnique();

                entity.Property(x => x.Email).IsRequired().HasMaxLength(200);
                entity.Property(x => x.PasswordHash).IsRequired();
                entity.Property(x => x.Role)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();
            });

            builder.Entity<RefreshToken>(entity =>
            {

                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Token).IsUnique();
                entity.HasIndex(x => x.UserId);

                entity.HasOne<UserCredential>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            builder.Entity<Household>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
            });

            builder.Entity<UserProfile>(entity =>
            {

                entity.HasKey(x => x.Id);

                entity.Property(x => x.FirsName).HasMaxLength(100);
                entity.Property(x => x.LastName).HasMaxLength(100);

                entity.HasOne<Household>()
                    .WithMany()
                    .HasForeignKey(x => x.HouseholdId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<HouseholdMember>(entity =>
            {
                entity.HasKey(x => new { x.HouseholdId, x.UserId });
            });



            builder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
            });

            builder.Entity<Expense>(entity =>
            {

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Amount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.HasIndex(x => x.HouseholdId);
                entity.HasIndex(x => x.CategoryId);
            });

            builder.Entity<RecurringExpense>(entity =>
            {

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Amount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });

            builder.Entity<CategoryExpenseSummary>(entity =>
            {
                entity.HasKey(x => new { x.HouseholdId, x.CategoryId });
            });

            builder.Entity<MonthlyExpenseSummary>(entity =>
            {
                entity.HasKey(x => new { x.HouseholdId, x.Year, x.Month });
            });


            builder.Entity<Notification>(entity =>
            {

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Message).IsRequired();

                entity.Property(x => x.Type)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasIndex(x => x.UserId);
            });

            builder.Entity<Bill>(entity =>
            {

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Amount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });

            builder.Entity<ShoppingList>(entity =>
            {

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
            });

            builder.Entity<ShoppingItem>(entity =>
            {

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);

                entity.HasOne<ShoppingList>()
                    .WithMany()
                    .HasForeignKey(x => x.ShoppingListId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<TaskItem>(entity =>
            {

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Description).HasMaxLength(1000);

                entity.HasIndex(x => x.AssignedUserId);
            });
        }
    }
}