using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using WinServicesTool.Forms;


var services = new ServiceCollection();
// Services and DI
services.AddSingleton<FormMain>();
services.AddSingleton<WinServicesTool.Services.IWindowsServiceManager, WinServicesTool.Services.WindowsServiceManager>();
services.AddSingleton<WinServicesTool.Services.IPrivilegeService, WinServicesTool.Services.PrivilegeService>();

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


ApplicationConfiguration.Initialize();
var mainForm = serviceProvider.GetRequiredService<FormMain>();
Application.Run(mainForm);
