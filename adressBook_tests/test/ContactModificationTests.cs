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
                app.Contacts
                    .NewContactCreation()
                    .FillContactForm(contact)
                    .SubmitContactCreation();
            }

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            ContactData newContactData = new ContactData("Jane");
            newContactData.Lastname = "Brown";
            app.Contacts.Modify(0, newContactData);

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts[0].Firstname = "Jane";
            oldContacts[0].Lastname = "Brown";
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}