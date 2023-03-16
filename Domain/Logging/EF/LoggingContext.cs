using Microsoft.EntityFrameworkCore;

public class LoggingContext : DbContext
{
    public LoggingContext(DbContextOptions<LoggingContext> options) : base(options)
    {

    }
    protected LoggingContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //registration in startup.cs
        //optionsBuilder.UseSqlServer(@"Servedotnet buildr=AAAPC;Database=testdb;User Id=tl;Password=QwErT123;");
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
}
