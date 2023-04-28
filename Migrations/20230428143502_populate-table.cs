using Microsoft.EntityFrameworkCore.Migrations;

namespace AlunosApi.Migrations
{
    public partial class populatetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Age", "Email", "Name" },
                values: new object[] { 1, 25, "Manuelito-bandeira@email.com", "Jesielo Manuelito" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Age", "Email", "Name" },
                values: new object[] { 2, 50, "Manuelita-bandeira@email.com", "Jesiela Manuelita" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
