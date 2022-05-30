using log4net;
using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Data
{
	public class MavroTagDbContext : DbContext, IMavroTagDbContext
	{
		private static ILog _logger = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);

		public MavroTagDbContext() : base("MavroTagDb")
		{
		}

		public DbSet<Domain.User> Users { get; set; }

		public DbSet<Domain.TagProject> TagProjects { get; set; }

		public DbSet<Domain.TagProjectUserPermission> TagProjectUserPermissions { get; set; }

		public DbSet<Domain.TagProjectCategory> TagProjectCategories { get; set; }

		public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
		{
			return base.Set<TEntity>();
		}

		public new DbEntityEntry Entry<TEntity>(TEntity entity) where TEntity : BaseEntity
		{
			return base.Entry(entity);
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
	}
}