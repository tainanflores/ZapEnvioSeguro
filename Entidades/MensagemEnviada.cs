using System;

public class MensagemEnviada
{
    public long Id { get; set; } // ID do contato
    public int MensagemId { get; set; } // Mensagem enviada
    public int TelefoneId { get; set; } // ID do telefone que tentou enviar
    public required DateTime? DataEnvio { get; set; } // Data do envio da Mensagem
    public required long IdEmpresa { get; set; } // ID da empresa associada
    public string? SucessoEnviada { get; set; } // Indica se a mensagem foi enviada

}