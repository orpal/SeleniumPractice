using NUnit.Framework;
using Serilog;

namespace SeleniumPractice.Tests.Support;

public static class LoggerHelper
{
    private static readonly ILogger logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.File(
            Path.Combine(AppContext.BaseDirectory, "Logs", "automation-.log"),
            rollingInterval: RollingInterval.Day,
            outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
        .CreateLogger();

    public static void Info(string message)
    {
        logger.Information("{TestName} | {Message}", TestContext.CurrentContext.Test.Name, message);
    }

    public static void Error(string message)
    {
        logger.Error("{TestName} | {Message}", TestContext.CurrentContext.Test.Name, message);
    }
}