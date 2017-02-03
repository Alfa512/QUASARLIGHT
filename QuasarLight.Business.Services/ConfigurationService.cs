using System;
using System.Configuration;
using System.IO;
using QuasarLight.Common.Contracts.Services;

namespace QuasarLight.Business.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string FileStoragePath
        {
            get { return GetFileStoragePath(); }
        }

        public int SessionTimeOut { get { return Convert.ToInt16(ConfigurationManager.AppSettings["SessionTimeOut"]); } }

        public string RestorePasswordMailTemplate { get { return Path.Combine(_basePath, ConfigurationManager.AppSettings["RestorePasswordMailTemplate"]); } }

        public string RestorePasswordMailSubject { get { return ConfigurationManager.AppSettings["RestorePasswordMailSubject"]; } }
        public string RestorePasswordUrl { get { return ConfigurationManager.AppSettings["RestorePasswordUrl"]; } }

        public string WelcomeMailTemplate { get { return Path.Combine(_basePath, ConfigurationManager.AppSettings["WelcomeMailTemplate"]); } }

        public string WelcomeMailSubject { get { return ConfigurationManager.AppSettings["WelcomeMailSubject"]; } }
        private string GetFileStoragePath()
        {
            return ConfigurationManager.AppSettings["FileStoragePath"];
        }

        private readonly string _basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
    }
}