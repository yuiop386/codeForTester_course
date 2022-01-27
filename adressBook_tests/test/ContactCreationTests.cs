using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("John");
            contact.Lastname = "Doe";
            app.Contacts
                .NewContacCreation()
                .FillContactForm(contact)
                .SubmitContactCreation();
        }
    }
}
