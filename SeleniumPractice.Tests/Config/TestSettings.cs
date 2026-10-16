using Microsoft.Extensions.Configuration;

namespace SeleniumPractice.Tests.Config;

public static class TestSettings
{
    private static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("Config/appsettings.json", optional: false)
        .Build();

    public static string BaseUrl => configuration["TestSettings:BaseUrl"]!;
    public static string DefaultBrowser => configuration["TestSettings:DefaultBrowser"]!;
    public static int ExplicitWaitSeconds => int.Parse(configuration["TestSettings:ExplicitWaitSeconds"]!);
}