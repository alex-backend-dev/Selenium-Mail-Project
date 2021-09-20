using MailTests.ClassHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailTests
{
    public class MailRuMailBoxPage : BaseWebPage
    {
        private const string LetterFormSelector = ".compose-button__txt";
        private const string WhoFormSelector = "[tabindex *= '100']";
        private const string ThemeFormSelector = "[name *= 'Subject']";
        private const string DescriptionFormSelector = "[tabindex *= '505']";
        private const string SendFormSelector = "[tabindex *= '570']";
        private const string UnreadLettersSelector = ".llc";
        private const string EmailFormSelector = "[data-title-shortcut *= 'R']";
        private const string EmailSendingSelector = "[role *= 'textbox']";
        private const string EmailConfirmationSelector = "[tabindex *= '570']";
        private const string EmailSendedLetterFormSelector = ".layer__header";

        private IWebElement SearchLetterForm => FindElement(By.CssSelector(LetterFormSelector));
        private IWebElement SearchWhoForm => FindElement(By.CssSelector(WhoFormSelector));
        private IWebElement SearchThemeForm => FindElement(By.CssSelector(ThemeFormSelector));
        private IWebElement SearchDescriptionForm => FindElement(By.CssSelector(DescriptionFormSelector));
        private IWebElement SearchSendForm => FindElement(By.CssSelector(SendFormSelector));
        private IList<IWebElement> CheckRightLetter => FindElements(By.CssSelector(UnreadLettersSelector));
        private IWebElement SearchAnswerForm => FindElement(By.CssSelector(EmailFormSelector));
        private IWebElement SendAnswerForm => FindElement(By.CssSelector(EmailSendingSelector));
        private IWebElement SearchAnotherSendForm => FindElement(By.CssSelector(EmailConfirmationSelector));
        private IWebElement SearchSendedLetterForm => FindElement(By.CssSelector(EmailSendedLetterFormSelector));

        public MailRuMailBoxPage(IWebDriver driver) : base(driver)
        {
        }

        public void SendLetter()
        {
            InsertWhoForm(TestSettings.MailLetterWhoForm);
            InsertThemeForm(TestSettings.MailLetterThemeForm);
            InsertDescriptionForm(TestSettings.MailLetterDescriptionForm);
            driver.HandleAlert(new WebDriverWait(driver, TimeSpan.FromSeconds(20)));
        }

        public void InsertWhoForm(string data)
        {
            SearchLetterForm.Click();
            driver.GetElement(By.CssSelector(WhoFormSelector), 20);
            SearchWhoForm.SendKeys(data);
            SearchWhoForm.Click();
        }

        public void InsertThemeForm(string data)
        {
            driver.GetElement(By.CssSelector(ThemeFormSelector), 20);
            SearchThemeForm.SendKeys(data);
        }

        public void InsertDescriptionForm(string data)
        {
            driver.GetElement(By.CssSelector(DescriptionFormSelector), 20);
            SearchDescriptionForm.SendKeys(data);
            SearchSendForm.Click();
        }

        public bool CheckMailRuCorrectLetter()
        {
            return CheckRightLetter
            .Where(x => x.FindElement(By.XPath(@$"//span[contains(@title,'{TestSettings.MailLetterWhoForm}')]")) is not null)
            .Any();
        }

        public bool InsertClickMyLetter()
        {
            foreach (var mail in CheckRightLetter)
            {
                if (mail.Text.Contains(TestSettings.MailLetterThemeForm))
                {
                    mail.Click();
                    return true;
                }
            }

            return false;
        }

        public void SendLetterMailRuBox()
        {
            InsertClickMyLetter();
            SearchAnswerForm.Click();
            SendAnswerForm.SendKeys(TestSettings.MailLetterSendText);
            SearchAnotherSendForm.Click();
        }

        public void GoToMailRuBox()
        {
            driver.Navigate().GoToUrl(TestSettings.MailRuMailBoxUrl);
        }

        public bool IsVisibleMailRuBox() => SearchSendedLetterForm.Displayed && SearchSendedLetterForm.Text == "Письмо отправлено";
    }
}