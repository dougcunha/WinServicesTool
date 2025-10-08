using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using WinServicesTool.Forms;


var services = new ServiceCollection();
services.AddSingleton<FormPrincipal>();

// Add NLog
services.AddLogging(builder =>
{
    builder.SetMinimumLevel(LogLevel.Trace);
    builder.AddNLog("nlog.config");
});


var serviceProvider = services.BuildServiceProvider();

// Obtem o logger e faz um log de inicialização
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application Starting...");

// Loga informações do ambiente e versão do executável
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
var mainForm = serviceProvider.GetRequiredService<FormPrincipal>();
Application.Run(mainForm);
