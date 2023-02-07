using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class workflowClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workitem_Interaction_InteractionId",
                table: "Workitem");

            migrationBuilder.DropTable(
                name: "Interaction");

            migrationBuilder.AddColumn<int>(
                name: "IterationId",
                table: "Workitem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Iteration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iteration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Iteration_Iteration_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Iteration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Iteration_ParentId",
                table: "Iteration",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workitem_Iteration_InteractionId",
                table: "Workitem",
                column: "InteractionId",
                principalTable: "Iteration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workitem_Iteration_InteractionId",
                table: "Workitem");

            migrationBuilder.DropTable(
                name: "Iteration");

            migrationBuilder.DropColumn(
                name: "IterationId",
                table: "Workitem");

            migrationBuilder.CreateTable(
                name: "Interaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interaction_Interaction_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Interaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interaction_ParentId",
                table: "Interaction",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workitem_Interaction_InteractionId",
                table: "Workitem",
                column: "InteractionId",
                principalTable: "Interaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
