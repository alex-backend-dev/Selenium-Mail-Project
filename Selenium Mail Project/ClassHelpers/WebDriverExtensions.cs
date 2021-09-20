using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MailTests
{
    public static class WebDriverExtensions
    {
        public static IWebElement GetElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }

            return driver.FindElement(by);
        }

        public static void HandleAlert(this IWebDriver driver, WebDriverWait wait)
        {
            if (wait == null)
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            }

            try
            {

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                IAlert alert = wait.Until(drv =>
                {
                    try
                    {
                        return drv.SwitchTo().Alert();
                    }
                    catch (NoAlertPresentException)
                    {
                        return null;
                    }
                });
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                alert?.Accept();
            }

            catch (WebDriverTimeoutException) { /* Ignore */ }
        }
    }
}
