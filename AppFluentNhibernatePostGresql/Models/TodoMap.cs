using FluentNHibernate.Mapping;

namespace AppFluentNhibernatePostGresql.Models
{
    public class TodoMap: ClassMap<Todo>
    {
        public TodoMap()
        {
            Table("todos");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Sequence("todos_id_seq");

            Map(x => x.Description)
                .Column("description")
                .Length(100)
                .Not.Nullable();
        }
    }
}
