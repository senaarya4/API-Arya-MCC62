using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class migrasilagiii : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Profilling_TB_M_Education_EducationId",
                table: "TB_M_Profilling");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "TB_M_Profilling",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Profilling_TB_M_Education_EducationId",
                table: "TB_M_Profilling",
                column: "EducationId",
                principalTable: "TB_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Profilling_TB_M_Education_EducationId",
                table: "TB_M_Profilling");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "TB_M_Profilling",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Profilling_TB_M_Education_EducationId",
                table: "TB_M_Profilling",
                column: "EducationId",
                principalTable: "TB_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
