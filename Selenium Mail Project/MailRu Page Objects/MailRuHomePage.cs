using MailTests.ClassHelpers;
using OpenQA.Selenium;
using System.Threading;

namespace MailTests
{
    public class MailRuHomePage : BaseWebPage
    {
        private const string LoginSelector = "[name *= 'login']";
        private const string PasswordSelector = "[name *= 'password']";

        private IWebElement SearchResultLogin => FindElement(By.CssSelector(LoginSelector));
        private IWebElement SearchResultPassword => FindElement(By.CssSelector(PasswordSelector));

        public MailRuHomePage(IWebDriver driver) : base(driver)
        {
        }

        public void Login()
        {
            EnterLoginData(TestSettings.LoginMailRuMailBox);
            EnterLoginPassword(TestSettings.PasswordMailRuMailBox);
            Thread.Sleep(7000);
        }

        public void EnterLoginData(string data)
        {
            SearchResultLogin.SendKeys(data);
            SearchResultLogin.SendKeys(Keys.Enter);
        }

        public void EnterLoginPassword(string data)
        {
            SearchResultPassword.SendKeys(data);
            SearchResultPassword.SendKeys(Keys.Enter);
        }

        public void GoToMailRuHomePage()
        {
            driver.Navigate().GoToUrl(TestSettings.MailRuHomePageUrl);
        }

        public bool MailRuCorrectLogin() => driver.Url.Contains(TestSettings.MailRuMailBoxUrl);
    }
}
