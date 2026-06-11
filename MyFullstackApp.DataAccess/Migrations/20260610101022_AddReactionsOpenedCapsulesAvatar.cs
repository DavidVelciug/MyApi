using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFullstackApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddReactionsOpenedCapsulesAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "UserAccounts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OpenedCapsules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CapsuleId = table.Column<int>(type: "int", nullable: false),
                    OpenedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpenedFrom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenedCapsules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CapsuleId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenedCapsules_UserId_CapsuleId",
                table: "OpenedCapsules",
                columns: new[] { "UserId", "CapsuleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserId_CapsuleId",
                table: "Reactions",
                columns: new[] { "UserId", "CapsuleId" },
                unique: true,
                filter: "[CapsuleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserId_ProductId",
                table: "Reactions",
                columns: new[] { "UserId", "ProductId" },
                unique: true,
                filter: "[ProductId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenedCapsules");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "UserAccounts");
        }
    }
}
