using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZapEnvioSeguro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewColumnMensagens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "QuantidadeContatosPendentes",
                table: "Mensagens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeContatosPendentes",
                table: "Mensagens");
        }
    }
}
