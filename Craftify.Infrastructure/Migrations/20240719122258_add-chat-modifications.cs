using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Craftify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addchatmodifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_RoomId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RoomId",
                table: "Messages");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2fbd9a2a-3c75-4546-ac4e-b8a900f2d7d6"));

            migrationBuilder.RenameColumn(
                name: "TimeSpan",
                table: "Messages",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Messages",
                newName: "ToId");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Messages",
                newName: "FromId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Messages",
                newName: "ConversationId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Conversations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RoomId",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Blocked", "City", "Email", "EmailConfirmed", "FirstName", "JoinDate", "LastName", "PasswordHash", "PostalCode", "ProfilePicture", "Role", "State", "StreetAddress", "UpdatedDate" },
                values: new object[] { new Guid("0b6a28c2-af3f-49c1-a030-7cb927ee0434"), false, null, "craftify.onion0.122@gmail.com", true, "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "pass@FY04", null, null, "ADMIN", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0b6a28c2-af3f-49c1-a030-7cb927ee0434"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Conversations");

            migrationBuilder.RenameColumn(
                name: "ToId",
                table: "Messages",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Messages",
                newName: "TimeSpan");

            migrationBuilder.RenameColumn(
                name: "FromId",
                table: "Messages",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "ConversationId",
                table: "Messages",
                newName: "ReceiverId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Blocked", "City", "Email", "EmailConfirmed", "FirstName", "JoinDate", "LastName", "PasswordHash", "PostalCode", "ProfilePicture", "Role", "State", "StreetAddress", "UpdatedDate" },
                values: new object[] { new Guid("2fbd9a2a-3c75-4546-ac4e-b8a900f2d7d6"), false, null, "craftify.onion0.122@gmail.com", true, "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "pass@FY04", null, null, "ADMIN", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RoomId",
                table: "Messages",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_RoomId",
                table: "Messages",
                column: "RoomId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
