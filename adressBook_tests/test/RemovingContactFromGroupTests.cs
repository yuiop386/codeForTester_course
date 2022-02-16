using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemovingContactFromGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldData = group.GetContacts();
            ContactData contact = GroupData.GetAll()[0].GetContacts().First();

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newData = group.GetContacts();
            oldData.Remove(contact);
            oldData.Sort();
            newData.Sort();
            Assert.AreEqual(oldData, newData);
        }
    }
}
