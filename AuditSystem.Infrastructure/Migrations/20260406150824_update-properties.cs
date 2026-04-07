using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "Enrollments",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AuditLogs",
                newName: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Enrollments",
                newName: "EnrollmentDate");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "AuditLogs",
                newName: "CreatedAt");
        }
    }
}
