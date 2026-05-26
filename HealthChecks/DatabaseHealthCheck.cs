using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using ApiDockerPiaget.Data;

namespace ApiDockerPiaget.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly AppDbContext _context;

    public DatabaseHealthCheck(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Teste simples de conexão
            await _context.Database.CanConnectAsync(cancellationToken);

            // Teste mais completo (opcional)
            await _context.Escolas.AnyAsync(cancellationToken);

            return HealthCheckResult.Healthy("Banco de dados está respondendo normalmente.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Banco de dados indisponível.", ex);
        }
    }
}