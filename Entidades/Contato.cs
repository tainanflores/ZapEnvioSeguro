using System;

public class Contato
{
    public long Id { get; set; } // ID do contato
    public required string Nome { get; set; } // Nome do contato
    public required string Telefone { get; set; } // Telefone do contato
    public required string Telefone_Serialized { get; set; } // Telefone do contato
    public required string Sexo { get; set; } // Sexo do contato, com valor padrão 'O' (Outros)
    public bool IsBusiness { get; set; } // Indica se o contato é um negócio
    public string? PushName { get; set; } // Nome de usuário do Push (caso tenha)
    public DateTime? DateLastReceivedMsg { get; set; } // Data da última mensagem recebida
    public DateTime? DateLastSentMsg { get; set; } // Data da última mensagem enviada
    public long? LastSentMsgId { get; set; } // ID da última mensagem enviada
    public long IdEmpresa { get; set; } // ID da empresa associada
    public bool Selecionado { get; internal set; }
    public required string TelefoneOrigem { get; set; }
}