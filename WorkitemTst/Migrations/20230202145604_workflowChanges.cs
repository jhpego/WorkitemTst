using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class workflowChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkflowId",
                table: "WorkitemType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Workflow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkflowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkflowId = table.Column<int>(type: "int", nullable: false),
                    InitialStatusId = table.Column<int>(type: "int", nullable: false),
                    NextStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transition_Status_InitialStatusId",
                        column: x => x.InitialStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transition_Status_NextStatusId",
                        column: x => x.NextStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transition_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkitemType_WorkflowId",
                table: "WorkitemType",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_WorkflowId",
                table: "Status",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_Transition_InitialStatusId",
                table: "Transition",
                column: "InitialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Transition_NextStatusId",
                table: "Transition",
                column: "NextStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Transition_WorkflowId",
                table: "Transition",
                column: "WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkitemType_Workflow_WorkflowId",
                table: "WorkitemType",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkitemType_Workflow_WorkflowId",
                table: "WorkitemType");

            migrationBuilder.DropTable(
                name: "Transition");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_WorkitemType_WorkflowId",
                table: "WorkitemType");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                table: "WorkitemType");
        }
    }
}
