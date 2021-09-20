using MailTests.ClassHelpers;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace MailTests
{
    public class YandexMailBoxPage : BaseWebPage
    {
        private const string MailListSelector = ".ns-view-messages-item-wrap";
        private const string SenderClickSelector = ".mail-QuickReply-Placeholder_text";
        private const string SenderAnswerSelector = "[role *= 'textbox']";
        private const string ConfirmClickXPath = "//span[contains(text(),'Отправить')]";
        private const string ConfirmAnswerSelector = "[data-key *= 'view=quick-reply-done-success']";

        private IList<IWebElement> SearchResultMail => FindElements(By.CssSelector(MailListSelector));
        private IWebElement ClickOnAnswer => FindElement(By.CssSelector(SenderClickSelector));
        private IWebElement SendAnswer => FindElement(By.CssSelector(SenderAnswerSelector));
        private IWebElement ConfirmAnswer => FindElement(By.XPath(ConfirmClickXPath));
        private IWebElement SearchSendedForm => FindElement(By.CssSelector(ConfirmAnswerSelector));

        public YandexMailBoxPage(IWebDriver driver) : base(driver)
        {
        }

        public bool CheckResultMail() => SearchResultMail
           .Where(x => x.FindElement(By.CssSelector($"span[title=\"{TestSettings.MailRuCorrectEmail}\"]")) is not null)
           .Any();

        public void SendAnswerMail()
        {
            InsertClickMyLetter();
            InsertAnswer(TestSettings.YandexLetterSendText);
        }

        public bool InsertClickMyLetter()
        {
            foreach (var mail in SearchResultMail)
            {
                if (mail.Text.Contains(TestSettings.MailLetterThemeForm))
                {
                    mail.Click();
                    return true;
                }
            }

            return false;
        }

        public void InsertAnswer(string data)
        {
            ClickOnAnswer.Click();
            SendAnswer.SendKeys(data);
            ConfirmAnswer.Click();
        }
        public bool IsVisibleYandexMailBox() => SearchSendedForm.Displayed && SearchSendedForm.Text == "Письмо отправлено";
    }
}
