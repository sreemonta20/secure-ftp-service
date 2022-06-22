namespace secure_ftp_service.Core.Services
{
    /// <summary>
    /// BackgroundHostedService class inherited IHostedService Interface.Also, it implement StartAsync and StopAsync.In StartAsync, 
    /// we are calling our main logic method which is DoWork and in the StopAsync method, HttpClient does indirectly implement an 
    /// IDisposable interface the standard usage of HttpClient is not to dispose of it after every request.Releases unmanaged resources 
    /// used by the HttpClient and optionally disposes of the managed resources.
    /// </summary>
    public class BackgroundHostedService : IHostedService
    {
        /// <summary>
        /// Declaration & Initialization
        /// </summary>
        private readonly ILogService _logService;
        private readonly IWorkExecutor _worker;
        //private HttpClient httpClient;

        public BackgroundHostedService(ILogService logService, IWorkExecutor worker)
        {
            _logService = logService;
            _worker = worker;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _worker.DoWork(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //httpClient.Dispose();
            await Task.CompletedTask;
        }
    }
}
