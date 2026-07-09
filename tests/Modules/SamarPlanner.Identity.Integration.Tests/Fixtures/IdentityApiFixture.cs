namespace SamarPlanner.Identity.Integration.Tests.Fixtures;
public class IdentityApiFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<IdentityDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseInMemoryDatabase($"IdentityTestDb"));
        });
    }

    public async System.Threading.Tasks.Task ResetDatabaseAsync()
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }
}