using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNIK",
                table: "TB_M_Employee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_M_University",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_University", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    University_Id = table.Column<int>(type: "int", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_M_Education_TB_M_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "TB_M_University",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Profilling",
                columns: table => new
                {
                    ProfillingNIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Education_Id = table.Column<int>(type: "int", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Profilling", x => x.ProfillingNIK);
                    table.ForeignKey(
                        name: "FK_TB_M_Profilling_TB_M_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "TB_M_Education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Account",
                columns: table => new
                {
                    AccountNIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfillingNIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Account", x => x.AccountNIK);
                    table.ForeignKey(
                        name: "FK_TB_M_Account_TB_M_Profilling_ProfillingNIK",
                        column: x => x.ProfillingNIK,
                        principalTable: "TB_M_Profilling",
                        principalColumn: "ProfillingNIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_AccountNIK",
                table: "TB_M_Employee",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Account_ProfillingNIK",
                table: "TB_M_Account",
                column: "ProfillingNIK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Education_UniversityId",
                table: "TB_M_Education",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Profilling_EducationId",
                table: "TB_M_Profilling",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_Account_AccountNIK",
                table: "TB_M_Employee",
                column: "AccountNIK",
                principalTable: "TB_M_Account",
                principalColumn: "AccountNIK",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_Account_AccountNIK",
                table: "TB_M_Employee");

            migrationBuilder.DropTable(
                name: "TB_M_Account");

            migrationBuilder.DropTable(
                name: "TB_M_Profilling");

            migrationBuilder.DropTable(
                name: "TB_M_Education");

            migrationBuilder.DropTable(
                name: "TB_M_University");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Employee_AccountNIK",
                table: "TB_M_Employee");

            migrationBuilder.DropColumn(
                name: "AccountNIK",
                table: "TB_M_Employee");
        }
    }
}
