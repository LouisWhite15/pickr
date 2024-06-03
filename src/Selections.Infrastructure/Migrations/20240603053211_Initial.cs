using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selections.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "selections");

            migrationBuilder.CreateTable(
                name: "selections",
                schema: "selections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_selections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "selectionItems",
                schema: "selections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_selectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_selectionItems_selections_SelectionId",
                        column: x => x.SelectionId,
                        principalSchema: "selections",
                        principalTable: "selections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_selectionItems_SelectionId",
                schema: "selections",
                table: "selectionItems",
                column: "SelectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "selectionItems",
                schema: "selections");

            migrationBuilder.DropTable(
                name: "selections",
                schema: "selections");
        }
    }
}
