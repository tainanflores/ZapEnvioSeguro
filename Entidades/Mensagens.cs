using System;

public class Mensagens
{
    public long Id { get; set; } // ID da mensagem
    public required string Mensagem { get; set; } // Mensagem enviada
    public required DateTime DataEnvio { get; set; } // Data do envio da Mensagem
    public required long QuatidadeContatosSolicitados { get; set; }
    public long QuatidadeContatosSucesso { get; set; }
    public required long IdEmpresa { get; set; } // ID da empresa associada
}