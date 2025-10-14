using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using WinServicesTool.Forms;
using WinServicesTool.Services;
using WinServicesTool.Utils;

var services = new ServiceCollection();
services.AddSingleton<FormMain>();
services.AddSingleton<IServicePathHelper, ServicePathHelperFactory>();
services.AddSingleton<IPrivilegeService, PrivilegeService>();
services.AddTransient<IServiceOperationOrchestrator, ServiceOperationOrchestrator>();
services.AddSingleton<IRegistryService, RegistryService>();
services.AddSingleton<IRegistryEditor, RegistryEditor>();
services.AddSingleton<IProcessLauncher, ProcessLauncher>();
services.AddSingleton(_ => AppConfig.Load());

// Add NLog
services.AddLogging(builder =>
{
    builder.SetMinimumLevel(LogLevel.Trace);
    builder.AddNLog("nlog.config");
});

var serviceProvider = services.BuildServiceProvider();
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application Starting...");

logger.LogInformation
(
    "Environment: {OperatingSystem}, .NET Version: {Version}, Executable: {CurrentDomainFriendlyName}, CurrentDir: {CurrentDirectory}, CommandLine: {CommandLine}",
    Environment.OSVersion,
    Environment.Version,
    AppDomain.CurrentDomain.FriendlyName,
    Environment.CurrentDirectory,
    Environment.CommandLine
);


try
{
    ApplicationConfiguration.Initialize();
    var mainForm = serviceProvider.GetRequiredService<FormMain>();
    Application.Run(mainForm);
}
catch (Exception ex)
{
    logger.LogError(ex, "Exception not handled");
}
