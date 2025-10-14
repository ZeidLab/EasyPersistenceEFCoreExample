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
        
        builder.HasMany(x => x.TodoItems)
            .WithOne(c => c.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}