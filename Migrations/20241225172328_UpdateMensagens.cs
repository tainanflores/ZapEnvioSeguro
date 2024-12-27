using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZapEnvioSeguro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMensagens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuatidadeContatosSucesso",
                table: "Mensagens",
                newName: "QuantidadeContatosSucesso");

            migrationBuilder.RenameColumn(
                name: "QuatidadeContatosSolicitados",
                table: "Mensagens",
                newName: "QuantidadeContatosSolicitados");

            migrationBuilder.AddColumn<bool>(
                name: "Selecionado",
                table: "Mensagens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "TelefoneId",
                table: "MensagemEnviada",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "MensagemId",
                table: "MensagemEnviada",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selecionado",
                table: "Mensagens");

            migrationBuilder.RenameColumn(
                name: "QuantidadeContatosSucesso",
                table: "Mensagens",
                newName: "QuatidadeContatosSucesso");

            migrationBuilder.RenameColumn(
                name: "QuantidadeContatosSolicitados",
                table: "Mensagens",
                newName: "QuatidadeContatosSolicitados");

            migrationBuilder.AlterColumn<string>(
                name: "TelefoneId",
                table: "MensagemEnviada",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MensagemId",
                table: "MensagemEnviada",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
