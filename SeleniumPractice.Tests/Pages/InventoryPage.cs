using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;

namespace SeleniumPractice.Tests.Pages;

public class InventoryPage
{
    private readonly IWebDriver driver;

    private readonly By pageTitleLocator = By.ClassName("title");

    public InventoryPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public IWebElement GetInventoryPageTitle()
    {
        return driver.FindElement(pageTitleLocator);
    }


}
