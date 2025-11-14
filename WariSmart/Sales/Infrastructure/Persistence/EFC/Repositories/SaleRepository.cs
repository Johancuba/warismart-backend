using WariSmart.API.Sales.Domain.Model.Aggregates;
using WariSmart.API.Sales.Domain.Repositories;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WariSmart.API.Sales.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Sale repository implementation
/// </summary>
public class SaleRepository(AppDbContext context)
    : BaseRepository<Sale>(context), ISaleRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Sale>> FindByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await Context.Set<Sale>()
            .Include(s => s.Items)
            .Where(s => s.FechaVenta >= startDate && s.FechaVenta <= endDate)
            .OrderByDescending(s => s.FechaVenta)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Sale>> FindTodayAsync()
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);
        
        return await Context.Set<Sale>()
            .Include(s => s.Items)
            .Where(s => s.FechaVenta >= today && s.FechaVenta < tomorrow)
            .OrderByDescending(s => s.FechaVenta)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Sale?> FindByIdWithItemsAsync(int idVenta)
    {
        return await Context.Set<Sale>()
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.IdVenta == idVenta);
    }
}
