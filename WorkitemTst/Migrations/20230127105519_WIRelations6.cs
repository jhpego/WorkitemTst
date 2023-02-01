using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class WIRelations6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WIRelationTypeEnum",
                table: "WIRelation");

            migrationBuilder.AddColumn<int>(
                name: "Relation",
                table: "WIRelation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Relation",
                table: "WIRelation");

            migrationBuilder.AddColumn<string>(
                name: "WIRelationTypeEnum",
                table: "WIRelation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
