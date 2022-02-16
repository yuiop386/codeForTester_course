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
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Navigator.GoToHomePage();
            if (!app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                ContactData contact = new ContactData("Kurt");
                contact.Lastname = "Remove_Created";
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
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];

            app.Contacts.Remove(toBeRemoved);

            List<ContactData> newContacts = ContactData.GetAll();

            Assert.AreEqual(oldContacts.Count - 1, newContacts.Count);

            oldContacts.RemoveAt(0);

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
