using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.GoToHomePage();
            ContactData contact = new ContactData("John");
            contact.Lastname = "Doe";
            contact.HomePhone = "+7123456";
            contact.MobilePhone = "+7123457";
            contact.WorkPhone = "+7123458";
            contact.Address = @"St.Petersburg
Glukharskaya str.6/1";
            contact.Email = "email@mail.ru";
            contact.Email2 = "email@yandex.ru";
            contact.Email3 = "email@google.com";

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts
                .NewContactCreation()
                .FillContactForm(contact)
                .SubmitContactCreation();

            app.Navigator.GoToHomePage();

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
