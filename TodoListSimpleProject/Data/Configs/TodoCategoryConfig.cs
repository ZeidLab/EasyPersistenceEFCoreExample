using ExampleTodoListProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleTodoListProject.Data.Configs;

public class TodoCategoryConfig : IEntityTypeConfiguration<TodoCategory>
{
    public void Configure(EntityTypeBuilder<TodoCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        
        builder.HasMany<TodoItem>(x=> x.TodoItems) 
            .WithOne(c => c.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Tell EF Core to use the field for storage
        builder.Navigation(e => e.TodoItems)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField(TodoCategory.EF_TODO_ITEMS_FIELD_NAME);

        // Add concurrency token configuration
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.ToTable("TodoCategories");

        builder.HasData(new object[]
        {
            new {
                Id = Guid.Parse("17f8a6c2-0075-4c0a-b6ec-8120d786c7b0"),
                Title = "To Do"
            },
            new {
                Id = Guid.Parse("f748c929-9637-4b8c-a2ba-876f83eb5390"),
                Title = "In Progress"
            },
            new {
                Id = Guid.Parse("b803efa7-08f3-49be-a98e-9e3a5230883f"),
                Title = "Done"
            }

        });
    }
}