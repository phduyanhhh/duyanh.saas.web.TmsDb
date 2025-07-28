using System;
using Abp.Zero.EntityFrameworkCore;
using DuyAnh.SaaS.Authorization.Roles;
using DuyAnh.SaaS.Authorization.Users;
using DuyAnh.SaaS.Entities;
using DuyAnh.SaaS.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace DuyAnh.SaaS.EntityFrameworkCore;

public class SaaSDbContext : AbpZeroDbContext<Tenant, Role, User, SaaSDbContext>
{
	/* Define a DbSet for each entity of the application */
	public DbSet<PlaceCategories> PlaceCategories { get; set; }
	public SaaSDbContext(DbContextOptions<SaaSDbContext> options)
			: base(options)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
	}
}
