using System;
using System.Collections.Generic;
using NUnit.Framework;
using SaintSender.Core.Models;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.ViewModels;

namespace SaintSender.Core.Tests
{
    [TestFixture]
    class MainWindowViewModelTests
    {
        private MainWindowViewModel mwvm;
        private Credentials credentials;

        [SetUp]
        public void SetUp()
        {
            mwvm = new MainWindowViewModel(credentials);
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            credentials = new Credentials("bedecsi2ndtw1@gmail.com", "IHateWPF", "");
        }

        [Test, Order(1)]
        public void NextPage_IncrementsNextPage_IsNextPageIncremented()
        {
            mwvm.NextPage();
            Assert.AreEqual(2, EmailService.PageNumber);
        }

        [Test]
        public void PrevPage_DecrementsPrevPage_IsNotBelow1()
        {
            mwvm.PrevPage();
            Assert.AreEqual(1, EmailService.PageNumber);
        }

        [Test]
        public void PrevPage_DecrementsPrevPage_IsPageDecremented()
        {
            mwvm.NextPage();
            mwvm.PrevPage();
            Assert.AreEqual(1, EmailService.PageNumber);
        }

        [Test]
        public void FilterEmailsBySearchTerms_FiltersEmails_ReturnsCorrectEmails()
        {
            var email1 = new Email("Adam Sender", "SearchTest", DateTime.Now, "This is a test body.");
            var email2 = new Email("Not the same sender", "SearchTest", DateTime.Now, "This is also a test body.");
            mwvm.Emails = new List<Email>() {
                email1,
                email2 };
            var filteredEmails = mwvm.FilterEmailsBySearchTerms("Adam");
            Assert.IsTrue(filteredEmails.Count == 1);
            Assert.AreEqual(email1, filteredEmails[0]);
        }

        [Test]
        public void CollectEmails_CollectEmailsFromUser_NumberOfEmailsIsMaxNumberToBeShown()
        {
            mwvm.CollectEmails(credentials);
            Assert.AreEqual(10, mwvm.Emails.Count);
        }

        [Test]
        public void CollectAllEmails_CollectEmailsFromUser_NumberOfEmailsIsMoreThanMaxNumberToBeShown()
        {
            mwvm.CollectAllEmails(credentials);
            Assert.IsTrue(10 < mwvm.Emails.Count);
        }
    }
}
