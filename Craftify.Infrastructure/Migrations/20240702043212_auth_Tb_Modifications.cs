using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Craftify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class auth_Tb_Modifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e0143948-7b2a-4f1a-9c8a-7420b633bb55"));

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Authentications",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "ResetToken",
                table: "Authentications",
                newName: "PasswordResetToken");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "Authentications",
                newName: "RefreshTokenExpiryDate");

            migrationBuilder.RenameColumn(
                name: "ExpireAt",
                table: "Authentications",
                newName: "OTPExpireAt");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Blocked", "City", "Email", "EmailConfirmed", "FirstName", "JoinDate", "LastName", "PasswordHash", "PostalCode", "ProfilePicture", "Role", "State", "StreetAddress", "UpdatedDate" },
                values: new object[] { new Guid("5356ce2d-71ea-471e-80de-c57595a8f749"), false, null, "craftify.onion0.122@gmail.com", true, "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "pass@FY04", null, null, "ADMIN", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5356ce2d-71ea-471e-80de-c57595a8f749"));

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiryDate",
                table: "Authentications",
                newName: "ExpiryDate");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "Authentications",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "PasswordResetToken",
                table: "Authentications",
                newName: "ResetToken");

            migrationBuilder.RenameColumn(
                name: "OTPExpireAt",
                table: "Authentications",
                newName: "ExpireAt");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Blocked", "City", "Email", "EmailConfirmed", "FirstName", "JoinDate", "LastName", "PasswordHash", "PostalCode", "ProfilePicture", "Role", "State", "StreetAddress", "UpdatedDate" },
                values: new object[] { new Guid("e0143948-7b2a-4f1a-9c8a-7420b633bb55"), false, null, "craftify.onion0.122@gmail.com", true, "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "pass@FY04", null, null, "ADMIN", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
