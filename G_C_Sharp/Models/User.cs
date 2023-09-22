#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G_C_Sharp.Models;

public class User
{
    [Key]
    public int UserID { get; set; }

    [Required]
    [MinLength(2)]
    [UniqueUsername]
    public string Username { get; set; } 

    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password {get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword {get;set;}
}

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
        {
            return new ValidationResult("Email is required");
        }
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

        if(_context.Users.Any(e => e.Email == value.ToString()))
        {
            return new ValidationResult("Email must be unique!");
        } else {
            return ValidationResult.Success;
        }
    }
}
public class UniqueUsernameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
        {
            return new ValidationResult("Username is required");
        }
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

        if(_context.Users.Any(e => e.Username == value.ToString()))
        {
            return new ValidationResult("Username must be unique!");
        } else {
            return ValidationResult.Success;
        }
    }
}