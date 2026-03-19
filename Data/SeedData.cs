using Maal.Models;
using Maal.Services;
using Microsoft.EntityFrameworkCore;

namespace Maal.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new MaalContext(
            serviceProvider.GetRequiredService<DbContextOptions<MaalContext>>(),
            serviceProvider.GetRequiredService<IAppSettingsProvider>());

        if (context.Games.Any())
            return;

        // No seed data - start fresh
    }
}
