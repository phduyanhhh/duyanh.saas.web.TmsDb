using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DuyAnh.SaaS.EntityFrameworkCore;

public static class SaaSDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<SaaSDbContext> builder, string connectionString)
    {
        builder.UseNpgsql(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<SaaSDbContext> builder, DbConnection connection)
    {
        builder.UseNpgsql(connection);
    }
}
