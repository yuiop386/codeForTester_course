using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Navigator.GoToHomePage();

            if (!app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                ContactData contact = new ContactData("Dave");
                contact.Lastname = "Modify_Created";
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

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData oldData = oldContacts[0];

            ContactData newContactData = new ContactData("Jane");
            newContactData.Lastname = "Brown";
            newContactData.HomePhone = "+7(123)456";
            newContactData.MobilePhone = "+7123-45-7";
            newContactData.WorkPhone = "+(712)34-58";
            newContactData.Address = @"St.Petersburg Glukharskaya str.6/1";
            newContactData.Email = "email@mail.ru";
            newContactData.Email2 = "email2@yandex.ru";
            newContactData.Email3 = "email3@google.com";
            app.Contacts.Modify(0, newContactData);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);

            oldContacts[0].Firstname = "Jane";
            oldContacts[0].Lastname = "Brown";
            oldContacts[0].HomePhone = "+7(123)456";
            oldContacts[0].MobilePhone = "+7123-45-7";
            oldContacts[0].WorkPhone = "+(712)34-58";
            oldContacts[0].Address = @"St.Petersburg Glukharskaya str.6/1";
            oldContacts[0].Email = "email@mail.ru";
            oldContacts[0].Email2 = "email2@yandex.ru";
            oldContacts[0].Email3 = "email3@google.com";
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual((newContactData.Lastname + newContactData.Firstname),
                        (contact.Lastname + contact.Firstname));
                }
            }
        }
    }
}