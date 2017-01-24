
using QuasarLight.Domain.Models;

namespace QuasarLight.UI.Models
{
    public class LessonVm
    {
        public LessonVm(Lesson lesson)
        {
            Id = lesson.Id;
            DateTime = lesson.DateTime;
            IsTrial = lesson.IsTrial;
            Language = lesson.Language?.LanguageName;
            Comment = lesson.Comment;
            LessonStudent = lesson.Student;
            LessonTeacher = lesson.Teacher;
        }
        public string Id;
        public int StudentId;
        public int TeacherId;
        public string Language;
        public string DateTime;
        public string Comment;
        public bool IsTrial;
        public Student LessonStudent;
        public Teacher LessonTeacher;
    }
}