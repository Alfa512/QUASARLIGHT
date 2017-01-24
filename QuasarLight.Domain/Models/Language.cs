using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace QuasarLight.Domain.Models
{
    public class Language : EntityBase
    {
        public virtual string LanguageName { get; set; }
        public virtual string IconPath { get; set; }
        public virtual IList<Teacher> Teachers { get; set; }

        public class LenguageMap : ClassMap<Language>
        {
            public LenguageMap()
            {
                Table("Lenguages");
                Id(r => r.Id);
                Map(r => r.LanguageName);
                Map(r => r.IconPath);
                HasManyToMany(r => r.Teachers)
                    .Cascade.None()
                    .Table("TeacherLenguages")
                    .ParentKeyColumn("LenguageId")
                    .ChildKeyColumn("TeacherId")
                    .Inverse()
                    .Not.LazyLoad();
            }
        }
    }
}