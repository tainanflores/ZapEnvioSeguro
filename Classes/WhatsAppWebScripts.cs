using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using System.Diagnostics.Metrics;

namespace ZapEnvioSeguro.Classes
{
    internal class WhatsAppWebScripts
    {
        public static string ScriptIniciarConversa = @"
            (function(){
                function clickActionButton(){
                    var btn = document.getElementById('action-button');
                    if(btn){
                        btn.click();
                        window.chrome.webview.postMessage('iniciouConversa');
                        clearInterval(interval);
                    }
                }
                var interval = setInterval(clickActionButton, 1000);
                setTimeout(function(){ clearInterval(interval); }, 10000); // Timeout após 10 segundos
            })();
        ";

        public static string ScriptUsarWhatsAppWeb = @"
            (function(){
                function clickWhatsAppWebLink(){
                    var links = document.querySelectorAll('a[href^=""https://web.whatsapp.com/send/?phone=""]');
                    for (var i = 0; i < links.length; i++) {
                        var spans = links[i].querySelectorAll('span');
                        for (var j = 0; j < spans.length; j++) {
                            if (spans[j].innerText.trim() === 'usar o WhatsApp Web') {                                
                                spans[j].click();
                                window.chrome.webview.postMessage('usarWhatsAppWebClicado');                                
                                clearInterval(interval);
                                return;
                            }
                        }
                    }
                }

                var interval = setInterval(clickWhatsAppWebLink, 1000);
                setTimeout(function(){ clearInterval(interval); }, 10000);
            })();
        ";
        
        //public static string ScriptEnviarMensagem = @"           
        //    function clickEnviarButton(){
        //        var enviarButton = document.querySelector('[aria-label=""Enviar""]');
        //        if(enviarButton){
        //            enviarButton.click();
        //            window.chrome.webview.postMessage(enviarClicado');
        //        }
        //    };
        //    clickEnviarButton();
        //";

        public static string ScriptEnviarMensagem = @"
            (function(){
                var count = 0;
                function clickEnviarButton(){
                    count++
                    console.log(`tentando encontrar Send ${count}`);
                    var enviarButton = document.querySelector('[aria-label=""Enviar""]');
                    if(enviarButton){    
                        enviarButton.click();
                        console.log('encontrado e clicado');
                        window.chrome.webview.postMessage('enviarClicado');
                        return;
                    }
                }
                var interval = setInterval(clickEnviarButton, 1000); 
                setTimeout(function(){ clearInterval(interval); }, 10000); 
            })();
        ";

        public static string ScriptVerificarEnvio = @"        
            function findMicButton(){
                var title = document.querySelectorAll('footer div div span div div div button span svg title')
                if(title[2].innerHTML == 'ptt'){
                    window.chrome.webview.postMessage('mensagemEnviada');
                } else{
                   window.chrome.webview.postMessage('mensagemNaoEnviada'); 
                }
            };
            findMicButton();                
        ";

        public static string InjetarWPP = @"
            function apiWPPisReady() {
                var intervaloReady = setInterval(function () {
                    console.log('Verificando se apiWPP está pronta')
                    try {
                        if (WPP.isFullReady) {
                            clearInterval(intervaloReady);
                            console.log(""Api Pronta"")
                            window.chrome.webview.postMessage(`injetado,${WPP.conn.getMyUserId().user}`);
                        } else {
                            console.log('Api foi injetada mas ainda não está pronta')
                        }
                    } catch {
                        console.log('Erro ao verificar se API está pronta. Erro:' + error)
                    }
                }, 3000)
            };

            function clienteCarregado() {
                var intervaloReady = setInterval(function () {
                    console.log('Verificando se o WhatsApp carregou')
                    try {
                        var waInitialHistorySynced = window.localStorage['WaInitialHistorySynced'];
                        if (waInitialHistorySynced) {
                            console.log(""Cliente Carregado"");
                            clearInterval(intervaloReady);
                            window.chrome.webview.postMessage('conectado');
                            apiWPPisReady();
                        } else {
                            console.log('Conectou mas ainda não carregou')
                        }
                    } catch {
                        console.log('Erro ao verificar se API está pronta. Erro:' + error)
                    }
                }, 3000)
            };

            function qrClientReady() {
                var qrClientReady = setInterval(function () {
                    console.log('Verificando se o WhatsApp está conectado');
                    try {
                        var client = window.localStorage['last-wid'] || window.localStorage['last-wid-md']; 

                        if (client) {
                            console.log(""Cliente pronto"");
                            window.chrome.webview.postMessage(`TelefoneOrigem:${client}`);
                            clearInterval(qrClientReady);
                            clienteCarregado();
                        } else {
                            console.log('aguardando...')
                        }
                    } catch {
                        console.log('Erro ao verificar qr code')
                    }
                }, 3000)
            }
            qrClientReady();
        ";

        public static string contactList = @"
            async function contactList(){
                var contacts = await WPP.contact.list();
                var message = {
                    type: ""ContactList"", // Palavra ""ContactList""
                    data: contacts // A lista de contatos
                };
                window.chrome.webview.postMessage(message);
            }
            contactList();
        "
        ;

        public static string MensagemPorContato(string idZap) {
            return "async function messageList(){" +
                        $"var mensagens = await WPP.chat.getMessages('{idZap}', " + "{count: 5});" +
                          @"if (mensagens && mensagens.length > 0){
                              const ultimaMensagem = mensagens.reverse().find(msg => msg.__x_id.fromMe);
                              if (ultimaMensagem)
                              {     
                                var body = ultimaMensagem.body;
                                window.chrome.webview.postMessage(`ultimaMensagem:${body}`);                                  
                              }
                            }
                    }
                    messageList();
";
                    
        }

        public static string apiMessageHandler = @"
            WPP.on('chat.new_message', (msg) => {
                window.chrome.webview.postMessage(msg);
            });
            console.log(new Date().toLocaleTimeString());
        ";

        public static string contatosComData = @"
            async function obterMensagensFiltradas()
            {
              try
              {
                const contatos = await WPP.contact.list();
                const contatosFiltrados = contatos.filter(contact => {
                  const id = contact.id._serialized;
                  return id.endsWith('@c.us') && id.startsWith('55') && id.length >= 17;
                });

                const contatosComData = [];

                for (let i = 0; i < contatosFiltrados.length; i++)
                {
                  const contato = contatosFiltrados[i];
                  const numero = contato.id._serialized;

                  try
                  {   
                    const mensagens = await WPP.chat.getMessages(numero, { count: 10 });
                    if (mensagens && mensagens.length > 0)
                    {
                      const ultimaMensagem = mensagens.reverse().find(msg => !msg.__x_id.fromMe);

                      if (ultimaMensagem)
                      {     
                        const dataEnvio = new Date(ultimaMensagem.t * 1000);
                          contatosComData.push({
                            Telefone: numero,
                            DateLastReceivedMsg: dataEnvio.toISOString()
                          });
                      }
                    }
                  }
                  catch (error)
                  {
                    if (error.message.includes('Chat not found'))
                    {
                      console.log(`Chat não encontrado para o número: ${ numero}. Pulando...`);
                    }
                    else
                    {
                      console.error(`Erro ao obter mensagens para o número ${ numero}:`, error);
                    }
                  }
                }
                window.chrome.webview.postMessage(contatosComData);
              } catch (error) {
                console.error(""Erro geral:"", error);
              }
            }
        ";
    }
}
