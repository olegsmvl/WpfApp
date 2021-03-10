using Microsoft.EntityFrameworkCore.Migrations;

namespace WpfApp.Migrations
{
    public partial class AddLogId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogId",
                table: "Records",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Records_LogId",
                table: "Records",
                column: "LogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Logs_LogId",
                table: "Records",
                column: "LogId",
                principalTable: "Logs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Logs_LogId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_LogId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "LogId",
                table: "Records");
        }
    }
}
