using Allure.Net.Commons;
using Allure.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumPractice.Tests.Config;
using System.Diagnostics;

namespace SeleniumPractice.Tests.Support;

[AllureNUnit]
public abstract class TestBase
{
    protected IWebDriver driver = null!;
    protected WebDriverWait wait = null!;
    private Stopwatch stopwatch = new Stopwatch();

    [SetUp]
    public void SetUp()
    {
        stopwatch.Restart();
        LoggerHelper.Info("TEST STARTED");

        string browser = TestContext.CurrentContext.Test.Properties.Get("Browser")?.ToString() ?? TestSettings.DefaultBrowser;

        driver = DriverFactory.CreateDriver(browser);

        LoggerHelper.Info($"Browser initialized: {browser}");

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TestSettings.ExplicitWaitSeconds));

        driver.Navigate().GoToUrl(TestSettings.BaseUrl);

        LoggerHelper.Info($"Navigated to: {TestSettings.BaseUrl}");

        InitializeTestComponents();
    }

    protected virtual void InitializeTestComponents()
    {
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed && driver != null)
            {
                CaptureEvidence("Failure screenshot");
            }
        }
        finally
        {
            stopwatch.Stop();

            string status = TestContext.CurrentContext.Result.Outcome.Status.ToString();

            LoggerHelper.Info($"TEST {status.ToUpperInvariant()}");
            LoggerHelper.Info($"Duration: {stopwatch.Elapsed.TotalSeconds:F2} seconds");

            driver?.Quit();
            driver?.Dispose();
        }
    }

    protected void CaptureEvidence(string evidenceName)
    {
        EvidenceHelper.CaptureScreenshot(driver, evidenceName);
    }

}