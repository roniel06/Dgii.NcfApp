using System;
using Dgii.NcfApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Dgii.NcfApp.Context
{
	public class ProjectDbContext : DbContext
	{
		public ProjectDbContext(DbContextOptions options) : base(options)
		{
			Database.EnsureCreated();
		}


		public DbSet<NcfResultHistory> NcfResultHistory { get; set; }
		public DbSet<PlacasResultHistory> PlacasResultHistory { get; set; }
    }
}

