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
            app.Navigator.OpenHomePage();
            if (app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                ContactData newContactData = new ContactData("Jane");
                newContactData.Lastname = "Brown";
                app.Contacts.Modify(1, newContactData);
            }
            else
            {
                ContactData contact = new ContactData("Dave");
                contact.Lastname = "Modify_Created";
                app.Contacts
                    .NewContacCreation()
                    .FillContactForm(contact)
                    .SubmitContactCreation();
            }
        }
    }
}
