using FluentNHibernate.Mapping;
using Incoding.Data;

namespace Dialogue.Domain.Models
{
    public class StudentCourse : IncEntityBase
    {
        public new virtual int Id { get; set; }
        public virtual int StudentId { get; set; }
        public virtual int LessonId { get; set; }
        public virtual string Date { get; set; }

        public class StudentCourseMap : ClassMap<StudentCourse>
        {
            public StudentCourseMap()
            {
                Table("StudentsCourses");
                Id(r => r.Id).GeneratedBy.Increment();
                Map(r => r.StudentId);
                Map(r => r.LessonId);
                Map(r => r.Date);

            }
        }
    }
}