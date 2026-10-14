using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddJobValidationConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "JobApplications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Jobs_Salary_NonNegative",
                table: "Jobs",
                sql: "[Salary] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Jobs_YearsOfExperience_NonNegative",
                table: "Jobs",
                sql: "[YearsOfExperience] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Jobs_Salary_NonNegative",
                table: "Jobs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Jobs_YearsOfExperience_NonNegative",
                table: "Jobs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "JobApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
