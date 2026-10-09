using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueJobApplicationIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobApplications_SeekerId",
                table: "JobApplications");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_SeekerId_JobId",
                table: "JobApplications",
                columns: new[] { "SeekerId", "JobId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobApplications_SeekerId_JobId",
                table: "JobApplications");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_SeekerId",
                table: "JobApplications",
                column: "SeekerId");
        }
    }
}
