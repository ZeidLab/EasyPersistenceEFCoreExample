using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExampleTodoListProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTodoCategoryToApplyDDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoCategories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("17f8a6c2-0075-4c0a-b6ec-8120d786c7b0"), "To Do" },
                    { new Guid("b803efa7-08f3-49be-a98e-9e3a5230883f"), "Done" },
                    { new Guid("f748c929-9637-4b8c-a2ba-876f83eb5390"), "In Progress" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoCategories",
                keyColumn: "Id",
                keyValue: new Guid("17f8a6c2-0075-4c0a-b6ec-8120d786c7b0"));

            migrationBuilder.DeleteData(
                table: "TodoCategories",
                keyColumn: "Id",
                keyValue: new Guid("b803efa7-08f3-49be-a98e-9e3a5230883f"));

            migrationBuilder.DeleteData(
                table: "TodoCategories",
                keyColumn: "Id",
                keyValue: new Guid("f748c929-9637-4b8c-a2ba-876f83eb5390"));
        }
    }
}
