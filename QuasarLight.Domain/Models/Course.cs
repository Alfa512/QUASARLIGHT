using FluentNHibernate.Mapping;

namespace QuasarLight.Domain.Models
{
    public class Course : EntityBase
    {
        public virtual string Text { get; set; }

        public class CourseMap : ClassMap<Course>
        {
            public CourseMap()
            {
                Table("Courses");
                Id(r => r.Id);
                Map(r => r.Text);
            }
        }
    }
}