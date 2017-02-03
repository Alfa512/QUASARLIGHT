namespace QuasarLight.Common.Contracts.Services
{
    public interface IConfigurationService
    {
        string FileStoragePath { get; }
        int SessionTimeOut { get; }
        string RestorePasswordMailTemplate { get; }
        string RestorePasswordMailSubject { get; }
        string RestorePasswordUrl { get; }
        string WelcomeMailTemplate { get; }
        string WelcomeMailSubject { get; }

    }
}