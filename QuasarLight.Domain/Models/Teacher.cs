using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace QuasarLight.Domain.Models
{
    public class Teacher : EntityBase
    {
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual bool IsRussian { get; set; }
        public virtual bool? IsAdmin { get; set; }
        public virtual string FullName { get; set; }
        public virtual string ExperienceInYears { get; set; }
        public virtual string Education { get; set; }
        public virtual string Description { get; set; }
        public virtual string City { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual string AudioPath { get; set; }
        public virtual IList<Language> Lenguages { get; set; }

        public class TeacherMap : ClassMap<Teacher>
        {
            public TeacherMap()
            {
                Table("Teachers");
                Id(r => r.Id);
                Map(r => r.Email).Not.Nullable();
                Map(r => r.Password).Not.Nullable();
                Map(r => r.IsRussian).Not.Nullable();
                Map(r => r.IsAdmin);
                Map(r => r.FullName).Not.Nullable();
                Map(r => r.ExperienceInYears).Not.Nullable();
                Map(r => r.Education).Length(1024).Not.Nullable();
                Map(r => r.Description).Length(1500000).Not.Nullable();
                Map(r => r.City);
                Map(r => r.ImagePath).Length(10240);
                Map(r => r.AudioPath).Length(10240);
                HasManyToMany(r => r.Lenguages)
                    .Cascade.None()
                    .Table("TeacherLenguages")
                    .ParentKeyColumn("TeacherId")
                    .ChildKeyColumn("LenguageId")
                    .Not.LazyLoad();
            }
        }
    }
}