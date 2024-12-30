using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZapEnvioSeguro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMensagensRemoveSelecionado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selecionado",
                table: "Mensagens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Selecionado",
                table: "Mensagens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
