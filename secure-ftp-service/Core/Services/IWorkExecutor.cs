namespace secure_ftp_service.Core.Services
{
    /// <summary>
    /// It is an interface which helps to create loosely coupled solution and gives the privilege to execute the backgound task.<br/>
    /// </summary>
    public interface IWorkExecutor
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
