namespace gamex.Auth.Services;

public sealed class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<UserLogActivity> UserLogActivities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<User>().Property(u=> u.IsActive).HasDefaultValue(true);

        modelBuilder.Entity<Player>().Property(u => u.Email).HasConversion(email => email.Value, value =>  value.AsEmail());
        modelBuilder.Entity<Player>().Property(u => u.Mobile).HasConversion(mobile => mobile.Value, value => value.AsMobile());
        modelBuilder.Entity<Player>().HasOne(u=> u.User).WithOne(u=> u.Player).HasForeignKey<User>(p=> p.PlayerId);

        modelBuilder.Entity<UserLogActivity>().Property(u=> u.IsActive).HasDefaultValue(true);
        // modelBuilder.Entity<User>().Property(u=> u.CreatedOn).HasDefaultValueSql("GETDATE()");

        base.OnModelCreating(modelBuilder);
    }
}