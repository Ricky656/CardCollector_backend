using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardCollector_backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUserCardJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Cards_CardId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCard_Cards_CardId",
                table: "UserCard");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCard_User_UserId",
                table: "UserCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCard",
                table: "UserCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "UserCard",
                newName: "UserCards");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_UserCard_UserId",
                table: "UserCards",
                newName: "IX_UserCards_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCard_CardId",
                table: "UserCards",
                newName: "IX_UserCards_CardId");

            migrationBuilder.RenameIndex(
                name: "IX_User_CardId",
                table: "Users",
                newName: "IX_Users_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCards",
                table: "UserCards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCards_Cards_CardId",
                table: "UserCards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCards_Users_UserId",
                table: "UserCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cards_CardId",
                table: "Users",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCards_Cards_CardId",
                table: "UserCards");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCards_Users_UserId",
                table: "UserCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cards_CardId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCards",
                table: "UserCards");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "UserCards",
                newName: "UserCard");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CardId",
                table: "User",
                newName: "IX_User_CardId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCards_UserId",
                table: "UserCard",
                newName: "IX_UserCard_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCards_CardId",
                table: "UserCard",
                newName: "IX_UserCard_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCard",
                table: "UserCard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Cards_CardId",
                table: "User",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCard_Cards_CardId",
                table: "UserCard",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCard_User_UserId",
                table: "UserCard",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
