using System.ComponentModel.DataAnnotations.Schema;

namespace BackCW.Data;

public class SecretSanta
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Likes { get; set; }
    public string? Token { get; set; }
    public SecretSanta? Partner { get; set; }
    
}