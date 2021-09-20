using OpenQA.Selenium;
using System.Collections.Generic;

namespace MailTests
{
    public class BaseWebPage
    {
        protected readonly IWebDriver driver;
        protected BaseWebPage(IWebDriver driver) => this.driver = driver;
        protected IWebElement FindElement(By selector) => driver.FindElement(selector);
        protected IList<IWebElement> FindElements(By selector) => driver.FindElements(selector);
    }
}
