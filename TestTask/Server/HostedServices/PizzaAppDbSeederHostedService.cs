namespace TestTask.Server.HostedServices
{
    public class PizzaAppDbSeederHostedService:IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        public PizzaAppDbSeederHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<PizzaAppDbDataSeeder>();
            await seeder.SeedAsync();

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
