using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetServerDemo.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_pw",
                table: "User_TBs",
                newName: "User_Pw");

            migrationBuilder.RenameColumn(
                name: "User_phone",
                table: "User_TBs",
                newName: "User_Phone");

            migrationBuilder.RenameColumn(
                name: "User_name",
                table: "User_TBs",
                newName: "User_Name");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "User_TBs",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "User_birth",
                table: "User_TBs",
                newName: "User_Birth");

            migrationBuilder.RenameColumn(
                name: "User_num",
                table: "User_TBs",
                newName: "User_Num");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_Pw",
                table: "User_TBs",
                newName: "User_pw");

            migrationBuilder.RenameColumn(
                name: "User_Phone",
                table: "User_TBs",
                newName: "User_phone");

            migrationBuilder.RenameColumn(
                name: "User_Name",
                table: "User_TBs",
                newName: "User_name");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "User_TBs",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "User_Birth",
                table: "User_TBs",
                newName: "User_birth");

            migrationBuilder.RenameColumn(
                name: "User_Num",
                table: "User_TBs",
                newName: "User_num");
        }
    }
}
