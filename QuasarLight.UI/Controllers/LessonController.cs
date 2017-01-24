using Incoding.MvcContrib;

namespace QuasarLight.UI.Controllers
{
    public class LessonController : IncControllerBase
    {
        //readonly IRegexUtilities _regexUtilities;

        //public LessonController(IRegexUtilities regexUtilities)
        //{
        //    _regexUtilities = regexUtilities;
        //}
        //public ActionResult TrialLessonModal()
        //{
        //    return View("_TrialLessonModal");
        //}

        //public ActionResult TrialLessonOrder()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public object AddNewLessonTrial(TrialLessonVm data)
        //{
        //    if (data.StEmail.IsEmpty() && _regexUtilities.IsValidEmail(data.StEmail))
        //        return "Некорректный E-mail!";
            
        //    if (data.StFullName.IsEmpty())
        //        return "Не указано имя!";
        //    var student = new Student();
        //    var lesson = new Lesson();

        //    student.FullName = data.StFullName;
        //    student.Email = data.StEmail;
        //    student.Skype = data.StSkype;
        //    student.City = data.StCity;
        //    student.Age = !data.StAge.IsEmpty() ? Convert.ToInt32(data.StAge) : 0;

        //    lesson.Language = new Language {LanguageName = data.LsLang};
        //    lesson.DateTime = data.LsDateTime;
        //    lesson.Comment = data.LsComment;
        //    lesson.IsTrial = true;

        //    var body = RenderToString("_AddTrialLessonNotification", data);

        //    dispatcher.Push(new SendMessageCommand(new Email
        //    {
        //        ToEmail = MailSettings.ToEmail,
        //        Subject = "Заявка на пробный урок",
        //        Body = body
        //    }));
        //    if (data.IsIos != null)
        //        return RedirectToAction("Index", "Home");

        //    return "Success";
        //}

        //public object SendTrialLessonAddingNotificationToAdmin(string name, string email)
        //{
        //    var body = RenderToString("_PaymentRequisitesForm", name);

        //    dispatcher.Push(new SendMessageCommand(new Email
        //    {
        //        ToEmail = email,
        //        Subject = "Онлайн-школа \"Dialogue\"",
        //        Body = body
        //    }));
        //    return true;
        //}

        //public ActionResult AddTrialLessonNotification(TrialLessonVm data)
        //{
        //    if(data.LsComment.IsNullOrWhiteSpace())
        //        data.LsComment = "Эй, Амиго!\nЯ уже говорил тебе, что такое безумие? А?.. \nБезумие - это:\nТочное повторение одного и того же действия,\nраз за разом.\nВ надежде на изменение.\nЭто есть безумие...";
        //    return View("_AddTrialLessonNotification", data);
        //}
    }
}