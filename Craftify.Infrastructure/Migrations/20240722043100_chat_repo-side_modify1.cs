using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Craftify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class chat_reposide_modify1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_PeerOneId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_PeerTwoId",
                table: "Conversations");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a919685d-61b6-4e47-991b-8779775d0ac0"));

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Conversations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivityTimestamp",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Blocked", "City", "Email", "EmailConfirmed", "FirstName", "JoinDate", "LastName", "PasswordHash", "PostalCode", "ProfilePicture", "Role", "State", "StreetAddress", "UpdatedDate" },
                values: new object[] { new Guid("a5029eec-cfcc-4030-bc9d-0fc1b4988867"), false, null, "craftify.onion0.122@gmail.com", true, "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "pass@FY04", null, null, "ADMIN", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_RoomId",
                table: "Conversations",
                column: "RoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_PeerOneId",
                table: "Conversations",
                column: "PeerOneId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_PeerTwoId",
                table: "Conversations",
                column: "PeerTwoId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_PeerOneId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_PeerTwoId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_RoomId",
                table: "Conversations");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a5029eec-cfcc-4030-bc9d-0fc1b4988867"));

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "LastActivityTimestamp",
                table: "Conversations");

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Blocked", "City", "Email", "EmailConfirmed", "FirstName", "JoinDate", "LastName", "PasswordHash", "PostalCode", "ProfilePicture", "Role", "State", "StreetAddress", "UpdatedDate" },
                values: new object[] { new Guid("a919685d-61b6-4e47-991b-8779775d0ac0"), false, null, "craftify.onion0.122@gmail.com", true, "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "pass@FY04", null, null, "ADMIN", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_PeerOneId",
                table: "Conversations",
                column: "PeerOneId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_PeerTwoId",
                table: "Conversations",
                column: "PeerTwoId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
