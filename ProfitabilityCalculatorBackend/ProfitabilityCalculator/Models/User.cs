using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfitabilityCalculator.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("username")]
    public string Username { get; set; } = string.Empty;
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("pwdhash")]
    public byte[] PasswordHash { get; set; }
    [Column("pwdsalt")]
    public byte[] PasswordSalt { get; set; }
}