using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$11$QmWG5nQKzx2Xo1Yv279kgeFCjT8OBZkSSiH.5f6LSYb71FnU0V5bq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                column: "PasswordHash",
                value: "$2a$11$dspUDbAVdjI26f9CfEp1KeAV0zhVyZF/YaVCbbud2e0Vh4VD0.y7e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-5b8c-9d0e-1f2a3b4c5d6e"),
                column: "PasswordHash",
                value: "$2a$11$Inm7gFRhdjfr0yXqocW/tO..ntsTtfsz/lPET5c./Cnqu3oywWHx6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "Admin@123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                column: "PasswordHash",
                value: "Ali@123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-5b8c-9d0e-1f2a3b4c5d6e"),
                column: "PasswordHash",
                value: "Sara@123");
        }
    }
}
