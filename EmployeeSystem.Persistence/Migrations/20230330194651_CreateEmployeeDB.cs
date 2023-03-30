using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Empleyee name"),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Empleyee last name"),
                    RFC = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false, comment: "Empleyee RFC code"),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Employee born date"),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "NotSet", comment: "Employee status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.ID);
                },
                comment: "Employees table");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ID",
                table: "Employee",
                column: "ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
