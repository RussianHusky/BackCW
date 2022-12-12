using Microsoft.EntityFrameworkCore;

namespace BackCW.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<SecretSanta> SecretSantas { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}