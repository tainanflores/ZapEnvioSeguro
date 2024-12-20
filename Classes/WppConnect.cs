using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZapEnvioSeguro.Entidades;

namespace ZapEnvioSeguro.Classes
{
    internal class WppConnect
    {
        private static readonly string downloadUrl = "https://github.com/wppconnect-team/wa-js/releases/latest/download/wppconnect-wa.js";
        private static readonly string targetDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ZapEnvioSeguro");

        public WppConnect()
        {
            // Verifique se o diretório de destino existe. Se não, crie-o.
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }
        }

        public async Task DownloadWppJs()
        {
            string targetFilePath = Path.Combine(targetDirectory, "wppconnect-wa.js");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] fileBytes = await client.GetByteArrayAsync(downloadUrl);

                    await File.WriteAllBytesAsync(targetFilePath, fileBytes);

                    Console.WriteLine($"Arquivo baixado com sucesso em: {targetFilePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao baixar o arquivo: {ex.Message}");
                }
            }
        }
    }

    public class ContactFromZap
    {
        public required string id { get; set; }
        public string? name { get; set; }
        public string? pushname { get; set; }
        public string? shortName { get; set; }
        public string? type { get; set; }
        public bool isBusiness { get; set; }
        public bool isEnterprise { get; set; }
        public bool isSmb { get; set; }
        public int isContactSyncCompleted { get; set; }
        public bool syncToAddressbook { get; set; }
    }

    public class ContatoComData
    {
        public required string Telefone { get; set; } // Telefone do contato
        public DateTime? DateLastReceivedMsg { get; set; } // Data da última mensagem recebida

    }
    public class ContactListResponse
    {
        public string Type { get; set; }
        public List<ContactFromZap> Data { get; set; }
    }

    public class NovaMsgResponse
    {
        public string Type { get; set; }
        public List<WhatsAppMessage> data { get; set; }
    }
}
