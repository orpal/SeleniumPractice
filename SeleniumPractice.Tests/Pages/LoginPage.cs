using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumPractice.Tests.Pages;

public class LoginPage
{
    private readonly IWebDriver driver;

    private readonly By usernameLocator = By.Id("user-name");
    private readonly By passwordLocator = By.Id("password");
    private readonly By loginButtonLocator = By.Id("login-button");
    private readonly By errorMessageLocator = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public IWebElement GetUsernameField()
    {
        return driver.FindElement(usernameLocator);
    }

    public IWebElement GetPasswordField()
    {
        return driver.FindElement(passwordLocator);
    }

    public IWebElement GetLoginButton()
    {
        return driver.FindElement(loginButtonLocator);
    }

    public IWebElement GetErrorMessage()
    {
        return driver.FindElement(errorMessageLocator);
    }

    public void Login(string username, string password)
    {
        GetUsernameField().SendKeys(username);
        GetPasswordField().SendKeys(password);
        GetLoginButton().Click();
    }
}