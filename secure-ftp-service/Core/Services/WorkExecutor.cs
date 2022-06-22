using Newtonsoft.Json;
using secure_ftp_service.Core.Responses;
using secure_ftp_service.Helpers;

namespace secure_ftp_service.Core.Services
{
    /// <summary>
    /// this class implements the method defined in the <see  cref="IWorkExecutor"/>, which executes the main work in the background.
    /// </summary>
    public class WorkExecutor : IWorkExecutor
    {
        /// <summary>
        /// Declaration & Initialization
        /// </summary>
        private readonly ILogService _logService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _client;
        AssistantHelper helper = new();

        /// <summary>
        /// Constructor initialization 
        /// </summary>
        /// <param name="logService"></param>
        /// <param name="clientFactory"></param>
        public WorkExecutor(ILogService logService,IHttpClientFactory clientFactory)
        {
            _logService = logService;
            _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient(ConstantSupplier.HTTP_CLIENT_LOGICAL_NAME);
        }

        /// <summary>
        /// It calls or act as a client or as background worker method to call the downloadfiles http verb or method.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DoWork(CancellationToken cancellationToken)
        {
            SftpFileDetailsRes finalResponse;
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = await _client.GetAsync("https://www.c-sharpcorner.com/members/atul-warade");
                    if (result.IsSuccessStatusCode)
                    {
                        string contentResult = await result.Content.ReadAsStringAsync();
                        finalResponse =  JsonConvert.DeserializeObject<SftpFileDetailsRes>(contentResult)!;
                        if (helper.IsNotNull(finalResponse))
                        {
                            if(helper.IsNotNull(finalResponse.State))
                            {
                                string numOfStateWisStrPart = helper.MoreThanZero(finalResponse.State) && helper.MoreThanOne(finalResponse.State) ? "s are" : " is";
                                _logService.LogInfo(String.Format("{0} File{1} downloaded. Attempt details are given below: \n{2}", finalResponse.State, numOfStateWisStrPart, JsonConvert.SerializeObject(new { finalResponse }, Formatting.Indented)));
                            }
                            else
                            {
                                _logService.LogInfo(String.Format("Attempt details are given below: \n{0}", JsonConvert.SerializeObject(new { finalResponse }, Formatting.Indented)));
                            }
                        }
                        
                    }
                    else
                    {
                        string contentResult = await result.Content.ReadAsStringAsync();
                        finalResponse = JsonConvert.DeserializeObject<SftpFileDetailsRes>(contentResult)!;
                        _logService.LogError(String.Format("Attempt details are given below: \n{0}", JsonConvert.SerializeObject(new { finalResponse }, Formatting.Indented)));
                    }
                }
                catch (Exception Ex)
                {
                    _logService.LogError($"{String.Format(ConstantSupplier.BACKGROUND_WORK_ERROR_MSG, nameof(DoWork), Ex.Message)}");
                }
                finally
                {
                    await Task.Delay(1000 * 60, cancellationToken);
                }
            }
        }
    }
}
