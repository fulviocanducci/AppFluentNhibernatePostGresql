using AppFluentNhibernatePostGresql.Models;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AppFluentNhibernatePostGresql
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnection connection = new Connection();

            //var credit = new Credit
            //{
            //    Description = "Bol.com.br",
            //    Created = DateTime.Now.AddDays(-1)
            //};

            //connection.Add(credit);

            //int id = 1;
            //var creditFindId = connection
            //    .CreateSqlQuery("SELECT * FROM credit WHERE id=:id")
            //    .SetResultTransformer(Transformers.AliasToBean<Credit>())
            //    .SetParameter("id", id)
            //    .List<Credit>();

            int id = 1;
            var item1 = connection.QueryOver<Credit>()
                .And(x => x.Id == id)
                .SingleOrDefault<Credit>();

            var list1 = connection.QueryOver<Credit>()
                .And(x => x.Id == id)
                .List<Credit>();
            

            var result = connection.ToList<Credit>();

            Console.WriteLine( connection.Count<Credit>() );

            Console.WriteLine( connection.Count<Credit>(x => x.Id > 0, x => x.Created != null) );

            Console.ReadKey();
        }
    }
}
