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
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(10), (GenerateRandomString(10))));
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            app.Navigator.GoToHomePage();

            contact.Lastname = "Doe";
            contact.HomePhone = "+7(123)456";
            contact.MobilePhone = "+7123-45-7";
            contact.WorkPhone = "+(712)34-58";
            contact.Address = @"St.Petersburg Glukharskaya str.6/1";
            contact.Email = "email@mail.ru";
            contact.Email2 = "email2@yandex.ru";
            contact.Email3 = "email3@google.com";

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
