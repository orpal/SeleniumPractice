using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace SeleniumPractice.Tests.Support;

public static class DriverFactory
{
    public static IWebDriver CreateDriver(string browser)
    {
        return browser.ToLowerInvariant() switch
        {
            "chrome" => new ChromeDriver(),
            "edge" => new EdgeDriver(),
            "firefox" => new FirefoxDriver(),
            _ => throw new ArgumentException($"Unsupported browser: {browser}")
        };
    }
}