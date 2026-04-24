using Microsoft.Extensions.Logging;

namespace InventoryWarehouseSystem.Infrastructure.Services;

public class AuditService
{
    private readonly ILogger<AuditService> _logger;

    public AuditService(ILogger<AuditService> logger)
    {
        _logger = logger;
    }

    public Task LogAsync(string action, string details, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[AUDIT] {Action} | {Details}", action, details);
        return Task.CompletedTask;
    }
}
