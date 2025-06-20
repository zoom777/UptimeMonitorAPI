using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.Entities;

namespace UptimeMonitor.API.Services
{
    public class UptimeCheckWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<UptimeCheckWorker> _logger;

        public UptimeCheckWorker(IServiceScopeFactory scopeFactory, ILogger<UptimeCheckWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("UptimeCheckWorker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var now = DateTime.UtcNow;
                var checks = await db.UptimeChecks.ToListAsync(stoppingToken);

                foreach (var check in checks)
                {
                    var lastEvent = await db.UptimeEvents
                        .Where(e => e.UptimeCheckId == check.Id)
                        .OrderByDescending(e => e.StartTime)
                        .FirstOrDefaultAsync(stoppingToken);

                    var shouldRun = lastEvent == null ||
                                    now >= lastEvent.StartTime.AddMinutes(check.CheckInterval);

                    if (!shouldRun) continue;

                    var start = DateTime.UtcNow;
                    var result = await SimulateCheckAsync(check);
                    var end = DateTime.UtcNow;

                    db.UptimeEvents.Add(new UptimeEvent
                    {
                        UptimeCheckId = check.Id,
                        StartTime = start,
                        EndTime = end,
                        IsUp = result.IsUp,
                        Note = result.Note
                    });
                }

                await db.SaveChangesAsync(stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task<(bool IsUp, string Note)> SimulateCheckAsync(UptimeCheck check)
        {
            await Task.Delay(200);

            var random = new Random();
            bool isUp = random.NextDouble() < 0.8;

            return isUp
                ? (true, "Simulated OK")
                : (false, "Simulated DOWN - mock failure");
        }
    }
}