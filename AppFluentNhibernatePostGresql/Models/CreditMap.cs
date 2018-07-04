using FluentNHibernate.Mapping;

namespace AppFluentNhibernatePostGresql.Models
{
    public class CreditMap: ClassMap<Credit>
    {
        public CreditMap()
        {
            Table("credit");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Sequence("credit_id_seq");

            Map(x => x.Description)
                .Column("description")
                .Length(100)
                .Not.Nullable();

            Map(x => x.Created)
                .Column("created")
                .Nullable();
        }
    }
}
