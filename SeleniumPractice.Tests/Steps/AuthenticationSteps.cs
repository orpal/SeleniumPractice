using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumPractice.Tests.Pages;

namespace SeleniumPractice.Tests.Steps;

public class AuthenticationSteps
{
    private readonly LoginPage loginPage;

    public AuthenticationSteps(LoginPage loginPage)
    {
        this.loginPage = loginPage;
    }

    public void VerifyLoginFormIsDisplayed()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(loginPage.GetUsernameField().Displayed, Is.True);
            Assert.That(loginPage.GetPasswordField().Displayed, Is.True);
            Assert.That(loginPage.GetLoginButton().Displayed, Is.True);
        }
    }

    public void Login(string username, string password)
    {
        loginPage.Login(username, password);
    }

    public void VerifyLoginError(string expectedError)
    {
        IWebElement error = loginPage.GetErrorMessage();

        Assert.That(error.Displayed, Is.True);
        Assert.That(error.Text, Does.Contain(expectedError));
    }
}