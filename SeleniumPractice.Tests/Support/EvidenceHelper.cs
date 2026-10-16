using Allure.Net.Commons;
using OpenQA.Selenium;

namespace SeleniumPractice.Tests.Support;

public static class EvidenceHelper
{
    public static void CaptureScreenshot(IWebDriver driver, string evidenceName)
    {
        byte[] screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;

        AllureApi.AddAttachment(evidenceName, "image/png", screenshot, ".png");
    }
}