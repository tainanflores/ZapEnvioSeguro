using System;
using Microsoft.VisualBasic.ApplicationServices;

public class Users
{   
    public int Id { get; set; }         // ID do usuário
    public string Email { get; set; }       // Email do usuário
    public string PasswordHash { get; set; } // Senha do usuário (armazenada em hash)
    public string AuthToken { get; set; }   // Token de autenticação
    public bool IsBlocked { get; set; }     // Status de bloqueio (0 = desbloqueado, 1 = bloqueado)
    public string DeviceId { get; set; }  // Informações sobre o dispositivo (identificador único)
    public DateTime CreatedAt { get; set; } // Data de criação
    public DateTime? LastLogin { get; set; } // Data do último login (pode ser nula)
    public int PlanoAtual { get; set; }
}