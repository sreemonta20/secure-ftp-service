using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using secure_ftp_service.Core.AutoMapper;
using secure_ftp_service.Core.Models;
using secure_ftp_service.Core.Repositories;
using secure_ftp_service.Core.Services;
using secure_ftp_service.Helpers;
using secure_ftp_service.Persistence.ORM;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;


//// Reading appsettings 
IConfiguration config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile(ConstantSupplier.APP_SETTINGS_FILE_NAME)
                  .Build();

//// Setup a static Log.Logger instance
Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .CreateLogger();

try
{
    //// The below region just to define the service intoduction or title
    #region Service Title Introduction
    StringBuilder sb = new StringBuilder();
    sb.AppendLine();
    sb.AppendLine(ConstantSupplier.LOG_INFO_APPEND_LINE_FIRST);
    sb.AppendLine(ConstantSupplier.LOG_INFO_APPEND_LINE_SECOND_GATEWAY);
    sb.AppendLine(ConstantSupplier.LOG_INFO_APPEND_LINE_THIRD_VERSION);
    sb.AppendLine(ConstantSupplier.LOG_INFO_APPEND_LINE_FOURTH_COPYRIGHT);
    sb.AppendLine(ConstantSupplier.LOG_INFO_APPEND_LINE_END);
    Log.Logger.Information(sb.ToString());
    #endregion

    //// Web hosting is started and it is captured in the log.
    Log.Information(ConstantSupplier.LOG_INFO_WEB_HOST_START_MSG);
    var builder = WebApplication.CreateBuilder(args);
    builder.WebHost.UseKestrel();

    //// Configure Services started. It is separated in below region.
    #region Add services to the container (ConfigureServices(IServiceCollection services) Method from the last .NET 5)

    // PostgreSql service addeds
    #region Registers the given security db context as a service into the services

    builder.Services.AddMvc();
    builder.Services.AddEntityFrameworkNpgsql().AddDbContext<SFTPDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString(ConstantSupplier.APP_CONFIG_SFTP_DB_CONN_NAME)));

    #endregion

    // In case of null property, Add newtonsoftjon into the services container.
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    }).AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });
    
    //Swagger added
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    

    //Set serilog as a logging provider.
    builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
      loggerConfiguration.ReadFrom
      .Configuration(hostingContext.Configuration));
    builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
    
    // Add Automapper in case if we needed.
    #region AutoMappers
    builder.Services.AddAutoMapper(typeof(MapperInitializer));
    #endregion

    // Custom services injected.
    builder.Services.AddTransient<ILogService, LogService>();
    builder.Services.AddTransient<IRemoteSftpService, RemoteSftpService>();
    builder.Services.AddTransient<IGenericRepository<SftpFileDetails>, GenericRepository<SftpFileDetails>>();
    builder.Services.AddTransient<ISftpDataService, SftpDataService>();

    var app = builder.Build();

    // To get the support of legacy datetime in the postgresql (apart from utc.now()
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    #endregion

    //// Configuring request pipeline, which consists of middlewares.
    #region Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    #endregion

    app.Run();
}
catch (Exception Ex)
{
    Log.Fatal(Ex, ConstantSupplier.LOG_ERROR_WEB_HOST_TERMINATE_MSG);
    throw;
}


