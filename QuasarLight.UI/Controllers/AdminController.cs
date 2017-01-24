using System;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using FluentNHibernate.Conventions;
using Incoding.CQRS;
using Incoding.MvcContrib;
using Microsoft.Ajax.Utilities;
using QuasarLight.Domain.CQRS.Commands;
using QuasarLight.Domain.Models;
using QuasarLight.UI.Infrastructure;
using QuasarLight.UI.Models;

namespace QuasarLight.UI.Controllers
{
    [Auth]
    public class AdminController : IncControllerBase
    {
        public ActionResult Administration()
        {
            return View();
        }

        public bool Registration(Teacher teacher)
        {
            teacher.Password = Crypto.HashPassword(teacher.Password);
            dispatcher.Push(new AddTeacherCommand(teacher));
            return true;
        }

        public void RegTest()
        {
            Registration(new Teacher());
        }

        public ActionResult TeachersPanel()
        {
            var teacherList =
                dispatcher.Query(new GetEntitiesQuery<Teacher>()).Select(teacher => new TeacherVm(teacher)).ToList();
            return View("Teachers/_TeachersPanel", teacherList);
        }

        public ActionResult TeacherSelectList()
        {
            var teachers = dispatcher.Query(new GetEntitiesQuery<Teacher>());
            return View("Teachers/_TeacherSelectList", teachers);
        }

        public ActionResult StudentsPanel()
        {
            var studentsList = dispatcher.Query(new GetEntitiesQuery<Student>());
            if (studentsList == null || !studentsList.Any())
                return View("Students/_StudentsPanel", null);
            studentsList = studentsList.ToList();

            var studentListToVm = studentsList.Select(student => new StudentVm(student)
            {
                StudentLessons =
                    dispatcher.Query(new GetEntitiesQuery<Lesson>())
                        .Where(lesson => lesson.Student.Id == student.Id)
                        .ToList()
            }).ToList();

            return View("Students/_StudentsPanel", studentListToVm);
        }

        public ActionResult GetCourseList()
        {
            var courses = dispatcher.Query(new GetEntitiesQuery<Language>());
            if (courses == null || !courses.Any())
                return View("Language/_AdminLanguagesList");
            var languageList = courses.Select(l => new CourseVm(l)).ToList();
            return View("Language/_AdminLanguagesList", languageList);
        }

        public ActionResult AddTeacherModal()
        {
            return View("Teachers/_AddTeacherModal", new AddTeacherCommand());
        }

        [HttpGet]
        public ActionResult EditTeacherModal(string id = "0")
        {
            if (id == "0")
                return View("Teachers/_EditTeacherModal");
            var teacherVm = new TeacherVm(dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(r => r.Id == id));

            return View("Teachers/_EditTeacherModal", teacherVm);
        }

        public ActionResult SelectLenguageBox()
        {
            var lenguages = dispatcher.Query(new GetEntitiesQuery<Language>());
            return View("_SelectLenguageBox", lenguages);
        }

        public ActionResult LanguageSelectList()
        {
            var lenguages = dispatcher.Query(new GetEntitiesQuery<Language>());
            return View("Language/_LanguageSelectList", lenguages);
        }

        public ActionResult AdminStudentsList()
        {
            var studentsList = dispatcher.Query(new GetEntitiesQuery<Student>());
            if (studentsList == null || !studentsList.Any())
                return View("Students/_StudentsList", null);
            var studentListToVm = studentsList.Select(student => new StudentVm(student)
            {
                StudentLessons = dispatcher.Query(new GetEntitiesQuery<Lesson>()).Where(lesson => lesson.Student.Id == student.Id).ToList()
            }).ToList();
            return View("Students/_StudentsList", studentListToVm);
        }

        [HttpGet]
        public object GetTeacherById(int id)
        {
            var teacher = dispatcher.Query(new GetEntityByIdQuery<Teacher>(id));
            return System.Web.Helpers.Json.Encode(teacher);
        }

        [HttpPost]
        public object AddUser(AddTeacherCommand data)
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null && !EnumerableExtensionsForConventions.IsEmpty(Request.Cookies[FormsAuthentication.FormsCookieName].ToString()))
            {
                var currUser = dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(teacher => RouteData != null && teacher.Email == RouteData.Values["user"].ToString());

                if (currUser?.IsAdmin == null || currUser.IsAdmin != null && (bool)!currUser.IsAdmin)
                {
                    Response.SuppressFormsAuthenticationRedirect = true;
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = 401;
                    return "Ошибка доступа!";
                }
            }

            var user = new Teacher();
            if (!SelectorConditionalExtensions.IsEmpty(data.Email))
                user.Email = data.Email;
            else
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "Некорректный E-mail";
            }
            var message = "";
            var random = new Random();

            user.FullName = data.FullName;
            user.City = data.City;
            user.ExperienceInYears = data.ExperienceInYears;
            user.Education = data.Education;
            user.Description = data.Description;
            user.IsRussian = data.IsRussian;
            user.Password = Convert.ToString(random.Next(100000, 999999));

            if (data.Lenguages != null)
            {
                var lenguages =
                    data.Lenguages.Select(lenguageId => dispatcher.Query(new GetEntityByIdQuery<Language>(lenguageId)))
                        .ToList();
                user.Lenguages = lenguages;
            }

            if (data.ImageFile != null && data.ImageFile.IsEmpty())
            {
                var file = data.ImageFile;
                if (file != null && file.ContentLength != 0)
                {
                    var filename = user.Email + Path.GetExtension(file.FileName);
                    const string imageDirectory = "Content\\images\\";
                    var path = AppDomain.CurrentDomain.BaseDirectory + imageDirectory;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file.SaveAs(path + filename);
                    user.ImagePath = "\\" + imageDirectory + filename;
                }
                else
                {
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = 400;
                    message = "Изображение не загружено!";
                }
            }
            else
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                message = "Изображение не загружено!";
            }

            if (data.AudioFile != null && data.AudioFile.IsEmpty())
            {
                var file = data.AudioFile;
                if (file != null && file.ContentLength != 0)
                {
                    var filename = user.Email + Path.GetExtension(file.FileName);
                    const string audioDirectory = "Content\\AudioSamples\\";
                    var path = AppDomain.CurrentDomain.BaseDirectory + audioDirectory;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file.SaveAs(path + filename);
                    user.AudioPath = "\\" + audioDirectory + filename;
                }
                else
                {
                    user.AudioPath = null;
                }
            }
            else
            {
                user.AudioPath = null;
            }

            if (Response.StatusCode == 400)
                return message;

            Registration(user);

            Response.StatusCode = 200;
            var teachers = dispatcher.Query(new GetEntitiesQuery<Teacher>());

            return PartialView("Teachers/_AdminTeachersTable", teachers);
        }

        [HttpPost]
        public object EditTeacher(AddTeacherCommand data)
        {
            var currUser = dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(t => RouteData != null && t.Email == RouteData.Values["user"].ToString());
            if (currUser.IsAdmin != null && (bool)!currUser.IsAdmin)
            {
                Response.SuppressFormsAuthenticationRedirect = true;
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 401;
                return "AutorizationFailed";
            }

            if (!SelectorConditionalExtensions.IsEmpty(data.Id))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "IdIsEmpty";
            }
            var teacher = dispatcher.Query(new GetEntityByIdQuery<Teacher>(data.Id));
            if (teacher == null)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "NonExistent";
            }

            var message = "";

            teacher.FullName = data.FullName != null || data.FullName != "" ? data.FullName : teacher.FullName;
            teacher.Email = data.Email != null || data.Email != "" ? data.Email : teacher.Email;
            teacher.Education = data.Education != null || data.Education != "" ? data.Education : teacher.Education;
            teacher.ExperienceInYears = data.ExperienceInYears != null || data.ExperienceInYears != "" ? data.ExperienceInYears : teacher.ExperienceInYears;
            teacher.Description = data.Description != null || data.Description != "" ? data.Description : teacher.Description;
            teacher.City = data.City != null || data.City != "" ? data.City : teacher.City;
            teacher.IsRussian = data.IsRussian;

            if (data.Lenguages != null)
            {
                var lenguages =
                    data.Lenguages.Select(lenguageId => dispatcher.Query(new GetEntityByIdQuery<Language>(lenguageId)))
                        .ToList();
                teacher.Lenguages = lenguages;
            }

            if (data.ImageFile != null && data.ImageFile.IsEmpty())
            {
                var file = data.ImageFile;
                if (file != null && file.ContentLength != 0)
                {
                    var filename = teacher.Email + Path.GetExtension(file.FileName);
                    const string imageDirectory = "Content\\images\\";
                    var path = AppDomain.CurrentDomain.BaseDirectory + imageDirectory;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file.SaveAs(path + filename);
                    teacher.ImagePath = "\\" + imageDirectory + filename;

                }
                else
                {
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = 400;
                    message = "Изображение не загружено!";
                }
            }
            else
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                message = "Изображение не загружено!";
            }

            if (data.AudioFile != null && data.AudioFile.IsEmpty())
            {
                var file = data.AudioFile;
                if (file != null && file.ContentLength != 0)
                {
                    var filename = teacher.Email + Path.GetExtension(file.FileName);
                    const string audioDirectory = "Content\\AudioSamples\\";
                    var path = AppDomain.CurrentDomain.BaseDirectory + audioDirectory;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file.SaveAs(path + filename);
                    teacher.AudioPath = "\\" + audioDirectory + filename;
                }
            }
            if (Response.StatusCode == 400)
                return message;

            dispatcher.Push(new EditUserCommand(teacher));

            Response.StatusCode = 200;
            var teachers = dispatcher.Query(new GetEntitiesQuery<Teacher>());

            return PartialView("Teachers/_AdminTeachersTable", teachers);
        }

        [HttpPost]
        public string DellTeacher(string teacherId)
        {
            var currUser = dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(t => RouteData != null && t.Email == RouteData.Values["user"].ToString());
            if (currUser.IsAdmin != null && (bool)!currUser.IsAdmin)
                return "isNotAdmin";

            if (!SelectorConditionalExtensions.IsEmpty(teacherId))
                return "nonexistentTeacher";

            dispatcher.Push(new DeleteEntityByIdCommand<Teacher>(teacherId));
            return "Success";
        }

        [HttpGet]
        public ActionResult GetTeachers()
        {
            var teachers = dispatcher.Query(new GetEntitiesQuery<Teacher>());
            if (teachers != null && teachers.Any())
                teachers = teachers.ToList();
            return View("Teachers/_AdminTeachersTable", teachers);
        }

        [HttpGet]
        public ActionResult EditStudentModal(int id = 0)
        {
            if (id == 0)
                return View("Students/_EditStudentModal", new StudentVm());
            var student = dispatcher.Query(new GetEntityByIdQuery<Student>(id));

            var studentVm = new StudentVm(student);

            return View("Students/_EditStudentModal", studentVm);
        }

        [HttpPost]
        public object AddStudent(StudentVm data)
        {
            var currUser = dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(t => RouteData != null && t.Email == RouteData.Values["user"].ToString());

            if (currUser.IsAdmin != null && (bool)!currUser.IsAdmin)
            {
                Response.SuppressFormsAuthenticationRedirect = true;
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 401;
                return "AutorizationFailed";
            }

            if (SelectorConditionalExtensions.IsEmpty(data.FullName))
            {
                Response.SuppressFormsAuthenticationRedirect = true;
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "NameIsEmpty";
            }

            if (SelectorConditionalExtensions.IsEmpty(data.Email))
            {
                Response.SuppressFormsAuthenticationRedirect = true;
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "EmailIsEmpty";
            }

            if (SelectorConditionalExtensions.IsEmpty(data.Skype))
            {
                Response.SuppressFormsAuthenticationRedirect = true;
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "SkypeIsEmpty";
            }

            var student = new Student
            {
                FullName = data.FullName,
                Email = data.Email,
                Skype = data.Skype
            };
            dispatcher.Push(new EditStudentCommand(student));

            return "Success";
        }

        [HttpPost]
        public string EditStudent(StudentVm data)
        {
            var currUser = dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(t => RouteData != null && t.Email == RouteData.Values["user"].ToString());

            if (currUser.IsAdmin != null && (bool)!currUser.IsAdmin)
            {
                Response.SuppressFormsAuthenticationRedirect = true;
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 401;
                return "AutorizationFailed";
            }

            if (!SelectorConditionalExtensions.IsEmpty(data.Id))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "IdIsEmpty";
            }

            if (data.FullName.IsNullOrWhiteSpace())
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "NameIsEmpty";
            }

            if (data.Email.IsNullOrWhiteSpace())
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "EmailIsEmpty";
            }

            if (data.Skype.IsNullOrWhiteSpace())
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "SkypeIsEmpty";
            }

            var student = dispatcher.Query(new GetEntityByIdQuery<Student>(data.Id));
            if (student == null)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "NonExistent";
            }

            if (data.TeacherId != null)
            {
                var teacher = dispatcher.Query(new GetEntityByIdQuery<Teacher>(data.TeacherId));
                student.Teacher = teacher;
            }

            if (data.LenguageId != 0)
            {
                var lenguage = dispatcher.Query(new GetEntityByIdQuery<Language>(data.LenguageId));
                student.Language = lenguage;
            }

            student.FullName = data.FullName;
            student.Email = data.Email;
            student.Skype = data.Skype;

            dispatcher.Push(new EditStudentCommand(student));
            return "Success";
        }

        [HttpPost]
        public string DellStudent(string studentId)
        {
            var currUser = dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(t => RouteData != null && t.Email == RouteData.Values["user"].ToString());
            if (currUser.IsAdmin != null && (bool)!currUser.IsAdmin)
                return "isNotAdmin";

            if (!SelectorConditionalExtensions.IsEmpty(studentId) && studentId != "")
                return "nonExistent";
            dispatcher.Push(new DeleteEntityByIdCommand<Student>(studentId));
            return "Success";
        }

        [HttpGet]
        public ActionResult EditLanguageModal(int id = 0)
        {
            if (id == 0) return View("Language/_EditLanguageModal");
            var lenguage = dispatcher.Query(new GetEntityByIdQuery<Language>(id));

            var lenguageVm = new CourseVm(lenguage);

            return View("Language/_EditLanguageModal", lenguageVm);
        }

        [HttpPost]
        public object AddCourse(CourseVm data)
        {
            var currUser = dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(t => RouteData != null && t.Email == RouteData.Values["user"].ToString());
            if (currUser.IsAdmin != null && (bool)!currUser.IsAdmin)
                RedirectToAction("Login", "Account");

            if (data.LanguageName == null && SelectorConditionalExtensions.IsEmpty(data.LanguageName))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "Язык введён некорректно!ы";
            }

            var course = new Language
            {
                LanguageName = data.LanguageName,
            };

            if (data.IconFile != null && data.IconFile.IsEmpty())
            {
                var file = data.IconFile;
                if (file != null && file.ContentLength != 0)
                {
                    const string imageDirectory = "Content\\images\\";
                    var path = AppDomain.CurrentDomain.BaseDirectory + imageDirectory;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var filename = course.LanguageName + Path.GetExtension(file.FileName);
                    file.SaveAs(path + filename);
                    course.IconPath = "\\" + imageDirectory + filename;
                }
                else
                {
                    Response.TrySkipIisCustomErrors = true;
                    Response.StatusCode = 400;
                    return "Изображение не загружено!";
                }
            }
            else
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "Изображение не загружено!";
            }
            dispatcher.Push(new AddCourseCommand(course));

            var lenguages = dispatcher.Query(new GetEntitiesQuery<Language>());

            var lenguagesVm = lenguages.Select(lenguage => new CourseVm(lenguage)).ToList();

            return PartialView("Language/_AdminLanguagesList", lenguagesVm);
        }

        [HttpPost]
        public object EditCourse(CourseVm data)
        {
            var currUser = dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(t => RouteData != null && t.Email == RouteData.Values["user"].ToString());
            if (currUser.IsAdmin != null && (bool)!currUser.IsAdmin)
                RedirectToAction("Login", "Account");

            if (data.LanguageName == null && !data.LanguageName.IsNotEmpty())
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "Язык введён некорректно!";
            }

            var course = new Language();
            if (data.Id != null) course = dispatcher.Query(new GetEntityByIdQuery<Language>(data.Id));

            course.LanguageName = data.LanguageName;

            if (data.IconFile != null && data.IconFile.IsEmpty())
            {
                var file = data.IconFile;
                if (file != null && file.ContentLength != 0)
                {
                    const string imageDirectory = "Content\\images\\";
                    var path = AppDomain.CurrentDomain.BaseDirectory + imageDirectory;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var filename = course.LanguageName + Path.GetExtension(file.FileName);
                    file.SaveAs(path + filename);
                    course.IconPath = "\\" + imageDirectory + filename;
                }
            }

            dispatcher.Push(new EditCourseCommand(course));

            var lenguages = dispatcher.Query(new GetEntitiesQuery<Language>());

            var lenguagesVm = lenguages.Select(lenguage => new CourseVm(lenguage)).ToList();

            return PartialView("Language/_AdminLanguagesList", lenguagesVm);
        }

        [HttpPost]
        public object DellCourse(Guid? id)
        {
            var currUser = dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(t => RouteData != null && t.Email == RouteData.Values["user"].ToString());
            if (currUser.IsAdmin != null && (bool)!currUser.IsAdmin)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 401;
                return "Доступ запрещён!";
            }

            if (id == null)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return "Ошибка удаления!";
            }

            dispatcher.Push(new DeleteEntityByIdCommand<Language>(id.ToString()));
            return "Success";
        }

        public ActionResult GetNewLessons()
        {
            var lessons = dispatcher.Query(new GetEntitiesQuery<Lesson>()).Select(r => new LessonVm(r)).ToList();
            return View("Lesson/NewLessons", lessons);
        }

        public ActionResult GetFinishedLessons()
        {
            var lessons = dispatcher.Query(new GetEntitiesQuery<Lesson>()).Select(r => new LessonVm(r)).ToList();
            return View("Lesson/FinishedLessons", lessons);
        }

        [HttpPost]
        public ActionResult EditLesson()
        {
            var currUser = dispatcher.Query(new GetEntitiesQuery<Teacher>()).First(t => RouteData != null && t.Email == RouteData.Values["user"].ToString());
            if (currUser.IsAdmin != null && (bool)!currUser.IsAdmin)
                RedirectToAction("Login", "Account");

            if (Request.Form["lessonId"] == null)
                return RedirectToAction("Administration", "Admin");
            var lessId = Request.Form["lessonId"];
            Lesson lesson = dispatcher.Query(new GetEntityByIdQuery<Lesson>(lessId));

            if (Request.Form["lsText"] != null && !EnumerableExtensionsForConventions.IsEmpty(Request.Form["lsText"]))
                lesson.Comment = Request.Form["lsText"];
            if (Request.Form["lsLang"] != null && !EnumerableExtensionsForConventions.IsEmpty(Request.Form["lsLang"]))
                lesson.Language.LanguageName = Request.Form["lsLang"];

            dispatcher.Push(new EditLessonCommand(lesson));
            return RedirectToAction("Administration", "Admin");
        }
    }
}