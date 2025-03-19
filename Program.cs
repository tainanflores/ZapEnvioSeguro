using Newtonsoft.Json.Linq;
using Velopack;
using ZapEnvioSeguro.Forms;

/*

dotnet publish -c Release --self-contained -r win-x64 -o .\publish
vpk pack -u com.teadigital.zapenvioseguro -v 0.0.1 -p .\publish -e ZapEnvioSeguro.exe --packTitle ZapEnvioSeguro -i ./Resources/zapenvioseguro_ico.ico -s ./Resources/zapenvioseguro_splash.png 

*/////

namespace ZapEnvioSeguro
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            VelopackApp.Build().Run();
            
            ApplicationConfiguration.Initialize();

            //DateTime dataLocal = DateTime.Now;
            //DateTime dataOnline = ObterDataOnline();

            //if (dataOnline == new DateTime(2001, 1, 1, 0,0,0))
            //{
            //    Environment.Exit(1);
            //}

            //int toleranciaEmMinutos = 30;

            //if (!EstaoAproximadas(dataLocal, dataOnline, toleranciaEmMinutos))
            //{
            //    MessageBox.Show("Por favor, atualize a data do dipositivo antes de continuar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Environment.Exit(1);
            //}

            var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK) // Somente se o login for bem-sucedido
            {
                Application.Run(new MainForm());
            }
            else
            {
                // Fecha o aplicativo se o login falhar ou for cancelado
                Application.Exit();
            }
        }
        //static DateTime ObterDataOnline()
        //{
        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            string url = "http://worldtimeapi.org/api/timezone/Etc/UTC";
        //            var resposta = client.GetStringAsync(url).Result;

        //            var json = JObject.Parse(resposta);
        //            string dataHoraStr = json["datetime"].ToString();

        //            DateTime dataHora = DateTime.Parse(dataHoraStr);
        //            return dataHora;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Não foi possível conectar ao servidor. Verifique sua conexão com a internet e tente novamente","Falha ao conectar",MessageBoxButtons.OK,MessageBoxIcon.Error);
        //        return new DateTime(2001, 1, 1, 0, 0, 0);
        //    }
        //}

        static DateTime ObterDataOnline()
        {
            const int maxTentativas = 5; // Número máximo de tentativas
            const string url = "http://worldtimeapi.org/api/timezone/Etc/UTC";

            for (int tentativa = 1; tentativa <= maxTentativas; tentativa++)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var resposta = client.GetStringAsync(url).Result;

                        var json = JObject.Parse(resposta);
                        string dataHoraStr = json["datetime"].ToString();

                        DateTime dataHora = DateTime.Parse(dataHoraStr);
                        return dataHora; // Retorna a data/hora se bem-sucedido
                    }
                }
                catch
                {
                    if (tentativa == maxTentativas)
                    {
                        // Exibe erro apenas na última tentativa
                        MessageBox.Show(
                            "Não foi possível conectar ao servidor. Verifique sua conexão com a internet e tente novamente.",
                            "Falha ao conectar",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }

            // Retorna a data padrão se todas as tentativas falharem
            return new DateTime(2001, 1, 1, 0, 0, 0);
        }


        static bool EstaoAproximadas(DateTime data1, DateTime data2, int toleranciaEmMinutos)
        {
            TimeSpan diferenca = data1 - data2;

            return Math.Abs(diferenca.TotalMinutes) <= toleranciaEmMinutos;
        }        
    }
}