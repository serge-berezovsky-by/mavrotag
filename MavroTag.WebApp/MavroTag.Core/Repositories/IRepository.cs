using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Repositories
{
	public interface IRepository<TEntity> where TEntity : BaseEntity

	{
		IQueryable<TEntity> GetAll();
		TEntity GetById(long id);
		TEntity Insert(TEntity entity);
		IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);
		void Update(TEntity entity);
		IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);
		void Delete(TEntity entity);
		void Delete(IEnumerable<TEntity> entities);
		void Delete(long entityId);
		IList<T> ExecuteSqlQuery<T>(string sqlQuery, params object[] parameters);
	}
}
