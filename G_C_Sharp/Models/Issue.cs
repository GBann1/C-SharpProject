#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace G_C_Sharp.Models;

public class Issue
{
    [Key]
    public int IssueID {get; set;}

    [Required]
    public string Title {get; set;}

    [Required]
    public string Description {get; set;}
    // TODO add in the one to one relationship with the Resolution
    // ! Decided to try to just make it an update on the admin side
    public string Resolution {get; set;}
}