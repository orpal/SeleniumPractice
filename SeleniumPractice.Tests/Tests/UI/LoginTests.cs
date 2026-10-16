using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumPractice.Tests.Pages;
using SeleniumPractice.Tests.Steps;
using SeleniumPractice.Tests.Data;
using SeleniumPractice.Tests.Support;
using SeleniumPractice.Tests.Config;

namespace SeleniumPractice.Tests.UI;


[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class LoginTests : TestBase
{
    LoginPage loginPage = null!;
    InventoryPage inventoryPage = null!;
    AuthenticationSteps authSteps = null!;
    InventorySteps inventorySteps = null!;


    protected override void InitializeTestComponents()
    {
        loginPage = new LoginPage(driver);
        inventoryPage = new InventoryPage(driver);

        authSteps = new AuthenticationSteps(loginPage);
        inventorySteps = new InventorySteps(inventoryPage, wait);
    }


    [Test]
    [Category("Smoke_C")]
    [Property("Browser", "chrome")]
    //[DependsOnTest(nameof(LoginWithValidCredentials))]
    public void LoginFormShouldBeVisible()
    {
        authSteps.VerifyLoginFormIsDisplayed();
        CaptureEvidence("Login Page");
    }


    [Test]
    [Category("Smoke_C")]
    [Property("Browser", "edge")]
    //[DependsOnTest(nameof(LoginFormShouldBeVisible))]
    public void LoginWithValidCredentials() {

        authSteps.Login(
            AuthenticationTestData.ValidUsername, 
            AuthenticationTestData.ValidPassword
        );
        inventorySteps.VerifyInventoryPageIsDisplayed();

        CaptureEvidence("Inventory Page");

    }

    [Test]
    [Category("Smoke_C")]
    [Property("Browser", "chrome")]
    public void LoginWithInvalidCredentials()
    {
        authSteps.Login(
            AuthenticationTestData.InvalidUsername, 
            AuthenticationTestData.InvalidPassword
        );
        authSteps.VerifyLoginError("Username and password do not match");
    }

    [TestCase("invalid_user", "wrong_password", "Username and password do not match")]
    [TestCase("", "secret_sauce", "Username is required")]
    [TestCase("standard_user", "", "Password is required")]
    [Category("Regression_C")]
    [Property("Browser", "chrome")]
    public void LoginWithInvalidCredentials(string username, string password, string expectedError)
    {
        authSteps.Login(username, password);
        authSteps.VerifyLoginError(expectedError);
    }

}