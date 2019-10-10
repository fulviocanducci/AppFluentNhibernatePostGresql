using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppFluentNhibernatePostGresql.Models
{
    public class Connection: IConnection
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        public Connection()
        {
            OpenSession();
        }

        public ISession Session()
        {
            return session;
        }

        public ISessionFactory SessionFactory()
        {
            return sessionFactory;
        }

        public T Add<T>(T model)
        {
            ITransaction trans = session.BeginTransaction();
            session.Save(model);
            trans.Commit();
            session.Flush();
            return model;
        }

        public async Task<T> AddAsync<T>(T model)
        {
            ITransaction trans = session.BeginTransaction();
            await session.SaveAsync(model);
            await trans.CommitAsync();
            await session.FlushAsync();
            return model;
        }

        public T Edit<T>(T model)
        {
            ITransaction trans = session.BeginTransaction();
            session.SaveOrUpdate(model);
            trans.Commit();
            session.Flush();
            return model;
        }

        public async Task<T> EditAsync<T>(T model)
        {
            ITransaction trans = session.BeginTransaction();
            await session.SaveOrUpdateAsync(model);
            await trans.CommitAsync();
            await session.FlushAsync();
            return model;
        }

        public T Find<T>(object id)
        {
            return session.Get<T>(id);
        }

        public T Find<T>(object id, LockMode lockMode)
        {            
            return session.Get<T>(id, lockMode); 
        }

        public async Task<T> FindAsync<T>(object id)
        {
            return await session.GetAsync<T>(id);
        }

        public async Task<T> FindAsync<T>(object id, LockMode lockMode)
        {
            return await session.GetAsync<T>(id, lockMode);
        }

        public IList<T> ToList<T>() where T : class
        {
            return session.CreateCriteria(typeof(T))
                .List<T>();
        }

        public async Task<IList<T>> ToListAsync<T>() where T : class
        {
            return await session
                .CreateCriteria(typeof(T))
                .ListAsync<T>();
        }

        private void OpenSession()
        {

            if (sessionFactory == null)
            {
                sessionFactory = Fluently.Configure()
                        .Database(
                            PostgreSQLConfiguration
                            .Standard
                            .ConnectionString("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=senha;").ShowSql())                            
                            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TodoMap>())
                            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, false))
                            .Diagnostics(x =>
                            {
                                x.Enable(false);
                                x.OutputToConsole();
                            })                            
                            .BuildSessionFactory();
                session = sessionFactory.OpenSession();
            }
        }

        public void Dispose()
        {
            session.Close();
            sessionFactory.Close();
            session.Dispose();
            sessionFactory.Dispose();
        }

        public IList SqlQuery(string sql)
        {
            return CreateSqlQuery(sql).List();
        }

        public ISQLQuery CreateSqlQuery(string sql)
        {
            return session.CreateSQLQuery(sql);
        }

        public IQuery Query(string sql)
        {            
            return session.CreateQuery(sql);
        }
        
        public bool Delete<T>(T model)
        {
            try
            {
                ITransaction trans = session.BeginTransaction();
                session.Delete(model);
                trans.Commit();
                session.Flush();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync<T>(T model)
        {
            try
            {
                ITransaction trans = session.BeginTransaction();
                await session.DeleteAsync(model);
                await trans.CommitAsync();
                await session.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Count<T>() where T: class
        {
            return session.QueryOver<T>()
                .RowCount();
        }

        public int Count<T>(params Expression<Func<T, bool>>[] where) where T : class
        {
            IQueryOver<T, T> query = session.QueryOver<T>();
            foreach(var w in where)
            {
                query = query.And(w);
            }
            return query.RowCount();
        }

        public long CountLong<T>() where T : class
        {
            return session.QueryOver<T>()
                .RowCountInt64();
        }

        public long CountLong<T>(params Expression<Func<T, bool>>[] where) where T : class
        {
            IQueryOver<T, T> query = session.QueryOver<T>();
            foreach (var w in where)
            {
                query = query.And(w);
            }
            return query.RowCountInt64();
        }

        public IQueryOver<T, T> QueryOver<T>() where T : class
        {
            return session.QueryOver<T>();
        }
    }
}
