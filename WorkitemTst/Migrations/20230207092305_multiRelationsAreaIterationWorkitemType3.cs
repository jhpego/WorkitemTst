using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class multiRelationsAreaIterationWorkitemType3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkProjectArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    WorkProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProjectArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkProjectArea_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkProjectArea_WorkProject_WorkProjectId",
                        column: x => x.WorkProjectId,
                        principalTable: "WorkProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkProjectIteration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IterationId = table.Column<int>(type: "int", nullable: false),
                    WorkProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProjectIteration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkProjectIteration_Iteration_IterationId",
                        column: x => x.IterationId,
                        principalTable: "Iteration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkProjectIteration_WorkProject_WorkProjectId",
                        column: x => x.WorkProjectId,
                        principalTable: "WorkProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkProjectWorkitemType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkitemTypeId = table.Column<int>(type: "int", nullable: false),
                    WorkProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProjectWorkitemType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkProjectWorkitemType_WorkProject_WorkProjectId",
                        column: x => x.WorkProjectId,
                        principalTable: "WorkProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkProjectWorkitemType_WorkitemType_WorkitemTypeId",
                        column: x => x.WorkitemTypeId,
                        principalTable: "WorkitemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkProjectArea_AreaId",
                table: "WorkProjectArea",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProjectArea_WorkProjectId",
                table: "WorkProjectArea",
                column: "WorkProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProjectIteration_IterationId",
                table: "WorkProjectIteration",
                column: "IterationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProjectIteration_WorkProjectId",
                table: "WorkProjectIteration",
                column: "WorkProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProjectWorkitemType_WorkitemTypeId",
                table: "WorkProjectWorkitemType",
                column: "WorkitemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProjectWorkitemType_WorkProjectId",
                table: "WorkProjectWorkitemType",
                column: "WorkProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkProjectArea");

            migrationBuilder.DropTable(
                name: "WorkProjectIteration");

            migrationBuilder.DropTable(
                name: "WorkProjectWorkitemType");
        }
    }
}
