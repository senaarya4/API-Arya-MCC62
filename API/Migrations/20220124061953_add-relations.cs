using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Account_TB_M_Profilling_ProfillingNIK",
                table: "TB_M_Account");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_Account_AccountNIK",
                table: "TB_M_Employee");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Employee_AccountNIK",
                table: "TB_M_Employee");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Account_ProfillingNIK",
                table: "TB_M_Account");

            migrationBuilder.DropColumn(
                name: "Education_Id",
                table: "TB_M_Profilling");

            migrationBuilder.DropColumn(
                name: "AccountNIK",
                table: "TB_M_Employee");

            migrationBuilder.DropColumn(
                name: "University_Id",
                table: "TB_M_Education");

            migrationBuilder.DropColumn(
                name: "ProfillingNIK",
                table: "TB_M_Account");

            migrationBuilder.RenameColumn(
                name: "ProfillingNIK",
                table: "TB_M_Profilling",
                newName: "NIK");

            migrationBuilder.RenameColumn(
                name: "AccountNIK",
                table: "TB_M_Account",
                newName: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Account_TB_M_Employee_NIK",
                table: "TB_M_Account",
                column: "NIK",
                principalTable: "TB_M_Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Profilling_TB_M_Account_NIK",
                table: "TB_M_Profilling",
                column: "NIK",
                principalTable: "TB_M_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Account_TB_M_Employee_NIK",
                table: "TB_M_Account");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Profilling_TB_M_Account_NIK",
                table: "TB_M_Profilling");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "TB_M_Profilling",
                newName: "ProfillingNIK");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "TB_M_Account",
                newName: "AccountNIK");

            migrationBuilder.AddColumn<int>(
                name: "Education_Id",
                table: "TB_M_Profilling",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccountNIK",
                table: "TB_M_Employee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "University_Id",
                table: "TB_M_Education",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfillingNIK",
                table: "TB_M_Account",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_AccountNIK",
                table: "TB_M_Employee",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Account_ProfillingNIK",
                table: "TB_M_Account",
                column: "ProfillingNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Account_TB_M_Profilling_ProfillingNIK",
                table: "TB_M_Account",
                column: "ProfillingNIK",
                principalTable: "TB_M_Profilling",
                principalColumn: "ProfillingNIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_Account_AccountNIK",
                table: "TB_M_Employee",
                column: "AccountNIK",
                principalTable: "TB_M_Account",
                principalColumn: "AccountNIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
