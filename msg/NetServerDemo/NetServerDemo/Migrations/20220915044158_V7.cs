using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetServerDemo.Migrations
{
    public partial class V7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User_TBLs",
                table: "User_TBLs");

            migrationBuilder.AlterColumn<string>(
                name: "User_Id",
                table: "User_TBLs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_TBLs",
                table: "User_TBLs",
                column: "User_Num");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User_TBLs",
                table: "User_TBLs");

            migrationBuilder.UpdateData(
                table: "User_TBLs",
                keyColumn: "User_Id",
                keyValue: null,
                column: "User_Id",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "User_Id",
                table: "User_TBLs",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_TBLs",
                table: "User_TBLs",
                columns: new[] { "User_Id", "User_Num" });
        }
    }
}
