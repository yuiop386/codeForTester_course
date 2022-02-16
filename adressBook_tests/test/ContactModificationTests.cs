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
    public class ContactModificationTests : ContactTestBase
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

            List<ContactData> oldContacts = ContactData.GetAll();
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

            List<ContactData> newContacts = ContactData.GetAll();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);

            oldContacts[0].Firstname = newContactData.Firstname;
            oldContacts[0].Lastname = newContactData.Lastname;
            oldContacts[0].HomePhone = newContactData.HomePhone;
            oldContacts[0].MobilePhone = newContactData.MobilePhone;
            oldContacts[0].WorkPhone = newContactData.WorkPhone;
            oldContacts[0].Address = newContactData.Address;
            oldContacts[0].Email = newContactData.Email;
            oldContacts[0].Email2 = newContactData.Email2;
            oldContacts[0].Email3 = newContactData.Email3;
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