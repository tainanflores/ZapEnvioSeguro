using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZapEnvioSeguro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMensagensAddColumnFalhaInesperada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FalhaInesperada",
                table: "Mensagens",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FalhaInesperada",
                table: "Mensagens");
        }
    }
}
