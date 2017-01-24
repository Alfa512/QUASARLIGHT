using FluentNHibernate.Mapping;

namespace QuasarLight.Domain.Models
{
    public class Student : EntityBase
    {
        public virtual string Email { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Skype { get; set; }
        public virtual string City { get; set; }
        public virtual int Age { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Language Language { get; set; }

        public class StudentMap : ClassMap<Student>
        {
            public StudentMap()
            {
                Table("Students");
                Id(r => r.Id);
                Map(r => r.Email);
                Map(r => r.FullName);
                Map(r => r.Skype);
                Map(r => r.City);
                Map(r => r.Age);
                References(x => x.Teacher).Column("TeacherId").ForeignKey("Id").Cascade.None().Not.LazyLoad();
                References(x => x.Language).Column("LenguageId").ForeignKey("Id").Cascade.None().Not.LazyLoad();
            }
        }
    }
}