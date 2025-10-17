using ExampleTodoListProject.Data.Entities;
using ExampleTodoListProject.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExampleTodoListProject.Data;
/// <summary>
/// Represents the database context for the application, integrating ASP.NET Core Identity and custom domain logic.
/// </summary>
/// <remarks>This class extends <see cref="IdentityDbContext{TUser}"/> to provide Identity functionality for the
/// application, while also managing the application's domain entities and custom behaviors. It includes configurations
/// for entity mappings, custom interceptors, and additional conventions. <para> The <see cref="ApplicationDbContext"/>
/// is typically used with dependency injection and configured in the application's startup or program configuration. It
/// is responsible for managing the database connection, tracking changes, and persisting entities. </para></remarks>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly DomainEventPublishingInterceptor _eventPublishingInterceptor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        DomainEventPublishingInterceptor eventPublishingInterceptor) : base(options)
    {
        _eventPublishingInterceptor = eventPublishingInterceptor;
       
    }

    public DbSet<TodoCategory> TodoCategories { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
    /// <summary>
    /// Configures the database context with the specified options.
    /// </summary>
    /// <remarks>This method adds custom interceptors to the database context configuration.  It is called
    /// automatically by the framework and is not intended to be invoked directly.</remarks>
    /// <param name="optionsBuilder">The builder used to configure the database context options.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_eventPublishingInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
    /// <summary>
    /// Configures the model for the database context by applying entity configurations and custom conventions.
    /// </summary>
    /// <remarks>This method applies all entity configurations from the current assembly and registers custom
    /// fuzzy search methods. It also invokes the base implementation to ensure default configurations are
    /// applied.</remarks>
    /// <param name="builder">The <see cref="ModelBuilder"/> used to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Apply all entity configurations from the current assembly automatically
        // This will scan for all classes that implement IEntityTypeConfiguration<T>
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Register custom fuzzy search methods otherwise they won't be recognized in LINQ queries
        builder.RegisterFuzzySearchMethods();
        base.OnModelCreating(builder);
    }
}