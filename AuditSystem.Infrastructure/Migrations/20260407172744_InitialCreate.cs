using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuditSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AuditLogs",
                columns: new[] { "Id", "Action", "EntityId", "EntityName", "Metadata", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { new Guid("f1f1f1f1-1111-1111-1111-f1f1f1f1f1f1"), "INSERT", "00000000-0000-0000-0000-000000000001", "User", "Initial System Admin Created.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("f2f2f2f2-2222-2222-2222-f2f2f2f2f2f2"), "INSERT", "a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d", "User", "Admin created user: Ali", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("f3f3f3f3-3333-3333-3333-f3f3f3f3f3f3"), "INSERT", "b2c3d4e5-f6a7-5b8c-9d0e-1f2a3b4c5d6e", "User", "Admin created user: Sara", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("f4f4f4f4-4444-4444-4444-f4f4f4f4f4f4"), "INSERT", "c1c1c1c1-1111-1111-1111-c1c1c1c1c1c1", "Course", "Created Course: C# Basics", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("f5f5f5f5-5555-5555-5555-f5f5f5f5f5f5"), "INSERT", "c2c2c2c2-2222-2222-2222-c2c2c2c2c2c2", "Course", "Created Course: ASP.NET Core", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("f6f6f6f6-6666-6666-6666-f6f6f6f6f6f6"), "INSERT", "c3c3c3c3-3333-3333-3333-c3c3c3c3c3c3", "Course", "Created Course: Entity Framework Core", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("f7f7f7f7-7777-7777-7777-f7f7f7f7f7f7"), "ENROLL", "e1e1e1e1-1111-1111-1111-e1e1e1e1e1e1", "Enrollment", "Ali enrolled in C# Basics", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d") },
                    { new Guid("f8f8f8f8-8888-8888-8888-f8f8f8f8f8f8"), "ENROLL", "e2e2e2e2-2222-2222-2222-e2e2e2e2e2e2", "Enrollment", "Ali enrolled in ASP.NET Core", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d") },
                    { new Guid("f9f9f9f9-9999-9999-9999-f9f9f9f9f9f9"), "ENROLL", "e3e3e3e3-3333-3333-3333-e3e3e3e3e3e3", "Enrollment", "Sara enrolled in ASP.NET Core", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2c3d4e5-f6a7-5b8c-9d0e-1f2a3b4c5d6e") },
                    { new Guid("fafafafa-aaaa-aaaa-aaaa-fafafafafafa"), "ENROLL", "e4e4e4e4-4444-4444-4444-e4e4e4e4e4e4", "Enrollment", "Sara enrolled in Entity Framework Core", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2c3d4e5-f6a7-5b8c-9d0e-1f2a3b4c5d6e") }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Author", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("c1c1c1c1-1111-1111-1111-c1c1c1c1c1c1"), "Suzanne Collin", "Learn the fundamentals of C# programming.", 200m, "C# Basics" },
                    { new Guid("c2c2c2c2-2222-2222-2222-c2c2c2c2c2c2"), "Andrew Lock", "Build web applications using ASP.NET Core.", 200m, "ASP.NET Core" },
                    { new Guid("c3c3c3c3-3333-3333-3333-c3c3c3c3c3c3"), "Julia Lerman", "Master data access with Entity Framework Core.", 300m, "Entity Framework Core" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "admin@gmail.com", "Admin", "Admin@123" },
                    { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), "ali@gmail.com", "Ali", "Ali@123" },
                    { new Guid("b2c3d4e5-f6a7-5b8c-9d0e-1f2a3b4c5d6e"), "sara@gmail.com", "Sara", "Sara@123" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CourseId", "IsPaid", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { new Guid("e1e1e1e1-1111-1111-1111-e1e1e1e1e1e1"), new Guid("c1c1c1c1-1111-1111-1111-c1c1c1c1c1c1"), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d") },
                    { new Guid("e2e2e2e2-2222-2222-2222-e2e2e2e2e2e2"), new Guid("c2c2c2c2-2222-2222-2222-c2c2c2c2c2c2"), false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d") },
                    { new Guid("e3e3e3e3-3333-3333-3333-e3e3e3e3e3e3"), new Guid("c2c2c2c2-2222-2222-2222-c2c2c2c2c2c2"), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2c3d4e5-f6a7-5b8c-9d0e-1f2a3b4c5d6e") },
                    { new Guid("e4e4e4e4-4444-4444-4444-e4e4e4e4e4e4"), new Guid("c3c3c3c3-3333-3333-3333-c3c3c3c3c3c3"), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2c3d4e5-f6a7-5b8c-9d0e-1f2a3b4c5d6e") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
