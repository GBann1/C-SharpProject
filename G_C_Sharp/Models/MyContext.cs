#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
// Either using or namespace for the models
namespace G_C_Sharp.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) {}
    public DbSet<User> Users { get; set; } 
    public DbSet<Urls> Urls { get; set; } 
    public DbSet<Issue> Issues { get; set; } 
    public DbSet<Employee> Employees { get; set; } 
}