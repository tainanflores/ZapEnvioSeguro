using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZapEnvioSeguro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContatosSerialized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefone_Serialized",
                table: "Contatos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone_Serialized",
                table: "Contatos");
        }
    }
}
