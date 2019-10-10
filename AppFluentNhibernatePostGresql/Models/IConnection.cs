using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppFluentNhibernatePostGresql.Models
{
    public interface IConnection
    {
        T Add<T>(T model);
        Task<T> AddAsync<T>(T model);

        T Edit<T>(T model);
        Task<T> EditAsync<T>(T model);

        T Find<T>(object id);
        T Find<T>(object id, LockMode lockMode);
        Task<T> FindAsync<T>(object id);
        Task<T> FindAsync<T>(object id, LockMode lockMode);

        IList<T> ToList<T>() where T : class;
        Task<IList<T>> ToListAsync<T>() where T : class;

        ISession Session();
        ISessionFactory SessionFactory();

        IList SqlQuery(string sql);
        ISQLQuery CreateSqlQuery(string sql);
        
        IQuery Query(string sql);
        IQueryOver<T, T> QueryOver<T>() where T : class;

        bool Delete<T>(T model);
        Task<bool> DeleteAsync<T>(T model);

        int Count<T>() where T : class;
        int Count<T>(params Expression<Func<T, bool>>[] where) where T : class;

        long CountLong<T>() where T : class;
        long CountLong<T>(params Expression<Func<T, bool>>[] where) where T : class;
    }
}
