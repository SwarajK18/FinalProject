using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainProject.DAL.Migrations.EmployeeDb
{
    public partial class MasterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ContactNo = table.Column<string>(nullable: false),
                    EmailID = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Salary = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SuperVisors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperVName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperVisors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    SuperVisorId = table.Column<int>(nullable: false),
                    JobTypeId = table.Column<int>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Masters_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Masters_JobTypes_JobTypeId",
                        column: x => x.JobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Masters_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Masters_SuperVisors_SuperVisorId",
                        column: x => x.SuperVisorId,
                        principalTable: "SuperVisors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Masters_EmployeeId",
                table: "Masters",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_JobTypeId",
                table: "Masters",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_LocationId",
                table: "Masters",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_SuperVisorId",
                table: "Masters",
                column: "SuperVisorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Masters");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "SuperVisors");
        }
    }
}
