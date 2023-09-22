#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace G_C_Sharp.Models;

public class Urls
{
    [Key]
    public int UrlID {get; set;}

    public string Youtube {get; set;}
    public string Maps {get; set;}
}