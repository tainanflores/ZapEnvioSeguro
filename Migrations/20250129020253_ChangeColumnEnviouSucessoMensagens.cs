using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZapEnvioSeguro.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnEnviouSucessoMensagens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SucessoEnviada",
                table: "MensagemEnviada",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "SucessoEnviada",
                table: "MensagemEnviada",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
