using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Navigator.OpenHomePage();
            if (app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                app.Contacts.Remove(1);
            }
            else
            {
                ContactData contact = new ContactData("Kurt");
                contact.Lastname = "Remove_Created";
                app.Contacts
                    .NewContacCreation()
                    .FillContactForm(contact)
                    .SubmitContactCreation();
            }
            app.Contacts.Remove(1);
        }
    }
}
