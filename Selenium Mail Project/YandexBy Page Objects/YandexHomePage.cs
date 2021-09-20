using MailTests.ClassHelpers;
using OpenQA.Selenium;

namespace MailTests
{
    public class YandexHomePage : BaseWebPage
    {
        private const string EnterFormXPath = "//div[contains(text(),'Войти')]";
        private const string LoginFormSelector = "#passp-field-login";
        private const string PasswordFormSelector = "#passp-field-passwd";
        private const string ClickFormSelector = "a[class='home-link desk-notif-card__domik-mail-line home-link_black_yes']";

        private IWebElement SearchResultEnter => FindElement(By.XPath(EnterFormXPath));
        private IWebElement SearchResultLogin => FindElement(By.CssSelector(LoginFormSelector));
        private IWebElement SearchResultPassword => FindElement(By.CssSelector(PasswordFormSelector));
        private IWebElement SearchResultClick => FindElement(By.CssSelector(ClickFormSelector));

        public YandexHomePage(IWebDriver driver) : base(driver)
        {
        }

        public void YandexLog()
        {
            EnterLoginData(TestSettings.LoginYandexByMailBox);
            EnterPasswordData(TestSettings.PasswordYandexByMailBox);
        }

        public void EnterLoginData(string data)
        {
            SearchResultEnter.Click();
            SearchResultLogin.SendKeys(data);
            SearchResultLogin.SendKeys(Keys.Enter);
        }

        public void EnterPasswordData(string data)
        {
            SearchResultPassword.SendKeys(data);
            SearchResultPassword.SendKeys(Keys.Enter);
        }

        public void GoToYandexBoxPage()
        {
            var mailYandexBy = SearchResultClick.GetAttribute("href");
            driver.Navigate().GoToUrl(mailYandexBy);
        }

        public void GoToYandexHomePage()
        {
            driver.Navigate().GoToUrl(TestSettings.YandexByHomePageUrl);
        }

        public bool YandexCorrectLogin() => driver.Url.Contains(TestSettings.YandexByMailBoxUrl);
    }
}