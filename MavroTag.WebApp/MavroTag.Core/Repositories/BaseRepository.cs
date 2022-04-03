using MavroTag.Core.Data;
using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Repositories
{
	public class BaseRepository<T> : IRepository<T> where T : BaseEntity
	{
		protected readonly IMavroTagDbContext _db;

		protected IDbSet<T> _entities => _db.Set<T>();

		public BaseRepository(IMavroTagDbContext db)
		{
			_db = db;
		}

		public virtual IQueryable<T> GetAll()
		{
			return _entities;
		}

		public virtual T GetById(long id)
		{
			return _entities.Find(id);
		}

		public virtual T Insert(T entity)
		{
			_entities.Add(entity);
			_db.SaveChanges();
			return entity;
		}

		public virtual IEnumerable<T> Insert(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
			{
				_entities.Add(entity);
			}
			_db.SaveChanges();
			return entities;
		}

		public virtual void Update(T entity)
		{
			var originEntity = _entities.Find(entity.Id);
			_db.Entry(originEntity).CurrentValues.SetValues(entity);
			_db.SaveChanges();
		}

		public virtual IEnumerable<T> Update(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
			{
				var originEntity = _entities.Find(entity.Id);
				_db.Entry(originEntity).CurrentValues.SetValues(entity);
			}
			_db.SaveChanges();
			return entities;
		}

		public virtual void Delete(T entity)
		{
			var origin = _entities.Find(entity.Id);
			_entities.Remove(origin);
			_db.SaveChanges();
		}

		public virtual void Delete(long entityId)
		{
			var origin = _entities.Find(entityId);
			_entities.Remove(origin);
			_db.SaveChanges();
		}

		public virtual void Delete(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
			{
				var origin = _entities.Find(entity.Id);

				_entities.Remove(origin);
			}
			_db.SaveChanges();
		}

		public IList<T> ExecuteSqlQuery<T>(string sqlQuery, params object[] parameters)
		{
			var query = _db.Database.SqlQuery<T>(sqlQuery, parameters);
			return query.ToList<T>();
		}
	}
}
