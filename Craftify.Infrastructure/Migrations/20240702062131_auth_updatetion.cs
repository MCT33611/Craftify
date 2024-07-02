using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Craftify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class auth_updatetion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5356ce2d-71ea-471e-80de-c57595a8f749"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Authentications");

            migrationBuilder.AddColumn<int>(
                name: "AuthType",
                table: "Authentications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTokenExpireAt",
                table: "Authentications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Blocked", "City", "Email", "EmailConfirmed", "FirstName", "JoinDate", "LastName", "PasswordHash", "PostalCode", "ProfilePicture", "Role", "State", "StreetAddress", "UpdatedDate" },
                values: new object[] { new Guid("c70b8092-58a1-423e-899b-18755c0a1bd3"), false, null, "craftify.onion0.122@gmail.com", true, "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "pass@FY04", null, null, "ADMIN", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c70b8092-58a1-423e-899b-18755c0a1bd3"));

            migrationBuilder.DropColumn(
                name: "AuthType",
                table: "Authentications");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenExpireAt",
                table: "Authentications");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Authentications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Blocked", "City", "Email", "EmailConfirmed", "FirstName", "JoinDate", "LastName", "PasswordHash", "PostalCode", "ProfilePicture", "Role", "State", "StreetAddress", "UpdatedDate" },
                values: new object[] { new Guid("5356ce2d-71ea-471e-80de-c57595a8f749"), false, null, "craftify.onion0.122@gmail.com", true, "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "pass@FY04", null, null, "ADMIN", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
