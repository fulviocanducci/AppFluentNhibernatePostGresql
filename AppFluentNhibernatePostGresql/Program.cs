using AppFluentNhibernatePostGresql.Models;
using System;

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

            var result = connection.ToList<Credit>();

            Console.WriteLine( connection.Count<Credit>() );

            Console.WriteLine( connection.Count<Credit>(x => x.Id > 0, x => x.Created != null) );

            Console.ReadKey();
        }
    }
}
