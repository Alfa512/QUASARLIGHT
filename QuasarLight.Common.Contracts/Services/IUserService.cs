using System;
using System.Collections.Generic;
using QuasarLight.Data.Model.DataModel;

namespace QuasarLight.Common.Contracts.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        IEnumerable<UserImage> GetUserImages(User user);

        UserImage GetUserImage(string imageId);

        User GetUserById(string id);

        User GetUserByNameAndHash(string name, string hash);

        User CreateUser(string name, string hash, string email);

        UserImage CreateUserImage(string userId, string name, bool isDefault);

        UserImage UpdateUserImagePath(string imageId, string imagePath);
        bool IsUserExists(string name);
        User GetUserByToken(string token);

        string CreateToken(string userName);
        void SendMailRestorePassword(string userName);
        void SendWelcomeMail(string userName);
        void RestorePassword(string token, string password);
        bool CheckToken(string token);
        void DeleteImage(string id);
        void ChangePassword(string userId, string oldPasswordHash, string newPassword);
    }
}