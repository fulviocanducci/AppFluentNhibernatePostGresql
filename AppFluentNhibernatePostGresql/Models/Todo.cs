namespace AppFluentNhibernatePostGresql.Models
{
    public class Todo
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
    }
}
