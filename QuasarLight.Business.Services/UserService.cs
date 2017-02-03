using System;
using System.Collections.Generic;
using System.Linq;
using QuasarLight.Common.Contracts.Data;
using QuasarLight.Common.Contracts.Services;
using QuasarLight.Data.Model.DataModel;

namespace QuasarLight.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IDataContext _context;
        private readonly ICrypto _crypto;
        private readonly IConfigurationService _config;
        private readonly ITemplateService _templateService;
        private readonly IMailService _mailService;

        public UserService(IDataContext context, ICrypto crypto, IConfigurationService config, ITemplateService templateService, IMailService mailService)
        {
            _context = context;
            _crypto = crypto;
            _config = config;
            _templateService = templateService;
            _mailService = mailService;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.GetAll();
        }

        public IEnumerable<UserImage> GetUserImages(User user)
        {
            return _context.UserImages.GetAll().Where(t => t.UserId.Contains(user.Id));
            //return _context.UserImages.GetAll().Where(t => t.Users.Contains(user));
        }

        public UserImage GetUserImage(string imageId)
        {
            return _context.UserImages.GetAll().FirstOrDefault(t => t.UserId== imageId);
        }

        public User GetUserByNameAndHash(string name, string hash)
        {
            return _context.Users.GetAll().FirstOrDefault(t => t.UserName.ToUpper() == name.ToUpper() && t.PasswordHash == hash);
        }

        public User GetUserById(string id)
        {
            var user = new User()
            {
                Id = "asdf-fghj-klzx-qwert-yuio",
                Email = "alfa512ks@mail.ru",
                EmailConfirmed = true,
                LastName = "Муслимов",
                Name = "Руслан",
                PhoneNumber = "+79618174736",
                UserName = "alfa"
            };
            return user;
            //return _context.Users.GetAll().FirstOrDefault(t => t.Id.ToLower() == id.ToLower());
        }

        public User CreateUser(string name, string hash, string email)
        {
            var newUser = new User() { Name = name, Email = email, PasswordHash = hash/*, TokenValidTo = DateTime.Now*/ };
            newUser = _context.Users.Create(newUser);
            _context.Commit();
            SendWelcomeMail(name);
            return newUser;
        }

        public UserImage CreateUserImage(string usertId, string name = "New Application", bool isDefault = false)
        {

            var image = _context.UserImages.Create(new UserImage()
            {
                //Id = Guid.NewGuid()
                //AppSecret = Guid.NewGuid(),
                //Name = name,
                UserId = usertId
                //IsDefault = isDefault
            });
            _context.Commit();
            return image;
        }

        //ToDo Implement Path Update
        public UserImage UpdateUserImagePath(string imageId, string imagePath)
        {

            var app = _context.UserImages.GetAll().FirstOrDefault(t => t.ImageId== imageId);

            if (app == null) return null;

            //app. = imagePath;
            _context.UserImages.Update(app);
            _context.Commit();

            return app;
        }

        public bool IsUserExists(string name)
        {
            return _context.Users.GetAll().FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) != null;
        }

        public User GetUserByToken(string token)
        {
            return _context.Users.GetAll().FirstOrDefault(t => t.RestorePasswordToken == token);
        }

        public string CreateToken(string userName)
        {
            var token = _crypto.Hash(Guid.NewGuid().ToString());
            var user =
                _context.Users.GetAll().FirstOrDefault(t => t.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                throw new Exception(string.Format("User with name {0} not found", userName));
            }

            user.RestorePasswordToken = token;
            user.TokenValidTo = DateTime.Now.AddHours(24);
            _context.Users.Update(user);
            _context.Commit();
            return token;
        }

        public void SendMailRestorePassword(string userName)
        {
            var token = CreateToken(userName);
            var restoreUrl = _config.RestorePasswordUrl.Replace("{token}", token);
            var user =
                _context.Users.GetAll().FirstOrDefault(t => t.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                var items = new Dictionary<string, string>
                            {
                                {"{restorepasswordurl}", restoreUrl},
                                {"{userName}", userName},
                                {"{validDate}", string.Format("{0:U}",user.TokenValidTo)}
                            };
                var template = _templateService.Process(_config.RestorePasswordMailTemplate, items);
                _mailService.SendMail(user.Email, _config.RestorePasswordMailSubject, template);
            }
        }

        public void SendWelcomeMail(string userName)
        {
            var user =
                _context.Users.GetAll().FirstOrDefault(t => t.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                var items = new Dictionary<string, string>
                            {
                                {"{userName}", userName},
                                {"{userEmail}", user.Email},
                                {"{year}", DateTime.Now.Year.ToString()},
                            };
                var bccEmail = System.Configuration.ConfigurationManager.AppSettings.Get("WelcomeMailBccAddress");
                var template = _templateService.Process(_config.WelcomeMailTemplate, items);
                _mailService.SendMail(user.Email, _config.WelcomeMailSubject, template, MailType.Welcome, bccEmail);
            }
        }

        public void RestorePassword(string token, string hash)
        {
            if (string.IsNullOrEmpty(token)) { throw new Exception("Token not found"); }

            var user = _context.Users.GetAll().FirstOrDefault(t => t.RestorePasswordToken == token);
            if (user != null)
            {
                if (user.TokenValidTo > DateTime.Now)
                {
                    try
                    {
                        user.PasswordHash = hash;
                        user.RestorePasswordToken = string.Empty;
                        _context.Users.Update(user);
                        _context.Commit();
                        return;
                    }
                    catch (Exception)
                    {
                        throw new Exception("Internal error");
                    }
                }
            }
            throw new Exception("Token not found");
        }

        public bool CheckToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            return _context.Users.GetAll().FirstOrDefault(t => t.RestorePasswordToken == token && t.TokenValidTo > DateTime.Now) != null;
        }

        //ToDo Implement Image Deletation
        public void DeleteImage(string id)
        {
            /*var app = _context.UserImages.GetAll().FirstOrDefault(t => t.Id == id);
            if (app != null)
            {
                app.IsDeleted = true;
                _context.UserImages.Update(app);
                _context.Commit();
            }*/

        }

        public void ChangePassword(string userId, string oldPasswordHash, string newPasswordHash)
        {
            var user = _context.Users.GetAll().FirstOrDefault(t => t.Id == userId && t.PasswordHash == oldPasswordHash);
            if (user == null)
            {
                throw new Exception("Old password is incorrect");
            }
            user.PasswordHash = newPasswordHash;
            _context.Users.Update(user);
            _context.Commit();
        }
    }
}