using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    public class ContactInformationTests : ContactTestBase
    {
        [Test]
        public void TestContactInformationInTableAndForm()
        {
            app.Navigator.GoToHomePage();

            if (!app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                ContactData contact = new ContactData("James");
                contact.Lastname = "ContactInformationTests_Created";
                contact.HomePhone = "+7(123)456";
                contact.MobilePhone = "+7123-45-7";
                contact.WorkPhone = "+(712)34-58";
                contact.Address = @"St.Petersburg Glukharskaya str.6/1";
                contact.Email = "email@mail.ru";
                contact.Email2 = "email2@yandex.ru";
                contact.Email3 = "email3@google.com";
                app.Contacts
                    .NewContactCreation()
                    .FillContactForm(contact)
                    .SubmitContactCreation();
            }

            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]
        public void TestContactInformationInFormAndDetails()
        {
            app.Navigator.GoToHomePage();

            if (!app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                ContactData contact = new ContactData("Sonnie");
                contact.Lastname = "ContactInformationTests_Created";
                contact.HomePhone = "+7(123)456";
                contact.MobilePhone = "+7123-45-7";
                contact.WorkPhone = "+(712)34-58";
                contact.Address = @"St.Petersburg Glukharskaya str.6/1";
                contact.Email = "email@mail.ru";
                contact.Email2 = "email2@yandex.ru";
                contact.Email3 = "email3@google.com";
                app.Contacts
                    .NewContactCreation()
                    .FillContactForm(contact)
                    .SubmitContactCreation();
            }

            ContactData fromForm = app.Contacts.GetContactInformationFromForm(0);
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(0);

            Assert.AreEqual(fromForm, fromDetails);
            Assert.AreEqual(fromForm.Address, fromDetails.Address);
            Assert.AreEqual(fromForm.AllEmails, fromDetails.AllEmails);
            Assert.AreEqual(fromForm.AllPhones, fromDetails.AllPhones);
        }

        [Test]
        public void TestContactInformationInTableAndDetails()
        {
            app.Navigator.GoToHomePage();

            if (!app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                ContactData contact = new ContactData("Nelson");
                contact.Lastname = "ContactInformationTests_Created";
                contact.HomePhone = "+7(123)456";
                contact.MobilePhone = "+7123-45-7";
                contact.WorkPhone = "+(712)34-58";
                contact.Address = @"St.Petersburg Glukharskaya str.6/1";
                contact.Email = "email@mail.ru";
                contact.Email2 = "email2@yandex.ru";
                contact.Email3 = "email3@google.com";
                app.Contacts
                    .NewContactCreation()
                    .FillContactForm(contact)
                    .SubmitContactCreation();
            }

            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(0);

            Assert.AreEqual(fromTable, fromDetails);
            Assert.AreEqual(fromTable.Address, fromDetails.Address);
            Assert.AreEqual(fromTable.AllEmails, fromDetails.AllEmails);
            Assert.AreEqual(fromTable.AllPhones, fromDetails.AllPhones);
        }
    }
}