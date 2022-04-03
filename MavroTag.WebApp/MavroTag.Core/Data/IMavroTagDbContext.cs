using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Data
{
	public interface IMavroTagDbContext : IObjectContextAdapter, IDisposable
	{
		Database Database { get; }

		DbEntityEntry Entry<TEntity>(TEntity entity) where TEntity : BaseEntity;
		DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
		int SaveChanges();
	}
}