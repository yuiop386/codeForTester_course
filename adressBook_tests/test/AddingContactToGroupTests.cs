using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldData = group.GetContacts();
            ContactData contacts = ContactData.GetAll().Except(oldData).First();

            app.Contacts.AddContactToGroup(contacts, group);

            List<ContactData> newData = group.GetContacts();
            oldData.Add(contacts);
            oldData.Sort();
            newData.Sort();
            Assert.AreEqual(oldData, newData);
        }
    }
}