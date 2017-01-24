using FluentNHibernate.Mapping;

namespace QuasarLight.Domain.Models
{
    public class Feedback : EntityBase
    {
        public virtual string Autor { get; set; }
        public virtual string Image { get; set; }
        public virtual string Text { get; set; }

        public class LenguageMap : ClassMap<Feedback>
        {
            public LenguageMap()
            {
                Table("Feedback");
                Id(r => r.Id);
                Map(r => r.Autor).Length(short.MaxValue);
                Map(r => r.Image).Length(short.MaxValue);
                Map(r => r.Text).Length(short.MaxValue);
            }
        }
    }
}