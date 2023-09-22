#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G_C_Sharp.Models;

public class Employee
{
    [Key]
    public int EmployeeID { get; set; }

    [Required]
    [MinLength(2)]
    public int IdentNum { get; set; } 

    public string Name {get; set;}
    
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password {get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public int AccessLevel { get; set; }
}