using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace adressBook_tests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));

            NewContacCreation();
            ContactData contact = new ContactData("John");
            contact.Lastname = "Doe";
            FillContactForm(contact);
            SubmitContactCreation();
            OpenHomePage();
        }
    }
}
