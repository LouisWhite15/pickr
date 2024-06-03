using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selections.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitsToSelectionItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Units",
                schema: "selections",
                table: "selectionItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Units",
                schema: "selections",
                table: "selectionItems");
        }
    }
}
