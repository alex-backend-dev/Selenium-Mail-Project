using MailTests.Testing_Class;
using NUnit.Framework;

namespace MailTests
{
    [TestFixture]
    public class MailTests : BaseTest
    {
        [Test, Order(1)]
        public void MailRuLogin()
        {
            MailRuHomePage mailRuHomePage = new MailRuHomePage(driver);
            mailRuHomePage.GoToMailRuHomePage();
            mailRuHomePage.Login();

            Assert.IsTrue(mailRuHomePage.MailRuCorrectLogin());
        }

        [Test, Order(2)]
        public void MailRuSendingLetter()
        {
            MailRuMailBoxPage mailRuMailBox = new MailRuMailBoxPage(driver);
            mailRuMailBox.SendLetter();

            Assert.IsTrue(mailRuMailBox.IsVisibleMailRuBox());
        }

        [Test, Order(3)]
        public void YandexLogging()
        {
            YandexHomePage yandexHomePage = new YandexHomePage(driver);
            yandexHomePage.GoToYandexHomePage();
            yandexHomePage.YandexLog();
            yandexHomePage.GoToYandexBoxPage();

            Assert.IsTrue(yandexHomePage.YandexCorrectLogin());
        }

        [Test, Order(4)]
        public void CheckYandexByMail()
        {
            YandexMailBoxPage yandexMailBox = new YandexMailBoxPage(driver);
            var correctMail = yandexMailBox.CheckResultMail();

            Assert.IsTrue(correctMail);
        }

        [Test, Order(5)]
        public void YandexBySendingLetter()
        {
            YandexMailBoxPage yandexMailBox = new YandexMailBoxPage(driver);
            yandexMailBox.SendAnswerMail();

            Assert.IsTrue(yandexMailBox.IsVisibleYandexMailBox());
        }

        [Test, Order(6)]
        public void CheckMailRuLetter()
        {
            MailRuMailBoxPage mailRuMailBox = new MailRuMailBoxPage(driver);

            mailRuMailBox.GoToMailRuBox();
            var correctLetter = mailRuMailBox.CheckMailRuCorrectLetter();

            Assert.IsTrue(correctLetter);
        }

        [Test, Order(7)]
        public void MailRuAnotherSendingLetter()
        {
            MailRuMailBoxPage mailRuMailBox = new MailRuMailBoxPage(driver);
            mailRuMailBox.SendLetterMailRuBox();

            Assert.IsTrue(mailRuMailBox.IsVisibleMailRuBox());
        }
    }
}