using FluentNHibernate.Mapping;

namespace QuasarLight.Domain.Models
{
    public class Lesson : EntityBase
    {
        public virtual string DateTime { get; set; }
        public virtual string Comment { get; set; }
        public virtual bool IsTrial { get; set; }
        public virtual Language Language { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher{ get; set; }

        public class LessonMap : ClassMap<Lesson>
        {
            public LessonMap()
            {
                Table("Lessons");
                Id(r => r.Id);
                Map(r => r.DateTime);
                Map(r => r.Comment);
                Map(r => r.IsTrial);
                References(x => x.Language).Column("LenguageId").ForeignKey("Id").Cascade.All().Not.LazyLoad();
                References(x => x.Student).Column("StudentId").ForeignKey("Id").Cascade.All().Not.LazyLoad();
                References(x => x.Teacher).Column("TeacherId").ForeignKey("Id").Cascade.All().Not.LazyLoad();
            }
        }
    }
}