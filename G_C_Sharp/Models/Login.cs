#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace G_C_Sharp.Models;

public class Login
{
    [Required]
    public string LogUsername { get; set; }
    
    [Required]
    [MinLength(8)]
    public string LogPassword {get;set;}
}
