using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumPractice.Tests.Pages;

namespace SeleniumPractice.Tests.Steps;

public class InventorySteps
{
    private readonly InventoryPage inventoryPage;
    private readonly WebDriverWait wait;

    public InventorySteps(InventoryPage inventoryPage, WebDriverWait wait)
    {
        this.inventoryPage = inventoryPage;
        this.wait = wait;
    }

    public void VerifyInventoryPageIsDisplayed()
    {
        wait.Until(d => d.Url == "https://www.saucedemo.com/inventory.html");
        wait.Until(d => inventoryPage.GetInventoryPageTitle().Displayed);

        IWebElement title = inventoryPage.GetInventoryPageTitle();

        Assert.That(title.Displayed, Is.True);
        Assert.That(title.Text, Is.EqualTo("Products"));
    }

}