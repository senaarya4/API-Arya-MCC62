using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class tambahRoleAccountsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Account_TB_M_Role_RoleId",
                table: "TB_M_Account");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Account_RoleId",
                table: "TB_M_Account");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "TB_M_Account");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "TB_M_Account",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Account_RoleId",
                table: "TB_M_Account",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Account_TB_M_Role_RoleId",
                table: "TB_M_Account",
                column: "RoleId",
                principalTable: "TB_M_Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
