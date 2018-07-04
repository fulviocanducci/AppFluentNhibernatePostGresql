using System;
namespace AppFluentNhibernatePostGresql.Models
{
    public class Credit
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? Created { get; set; }
    }
}
