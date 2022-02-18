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
            List<GroupData> groupList = GroupData.GetAll();
            List<ContactData> contactList = ContactData.GetAll();
            ContactData newContactData = new ContactData("AddedTo", $"AddingToGroup{rnd.Next(0, 999)}");
            GroupData newGroupData = new GroupData("aTC created");

            if (groupList.Count == 0)
            {
                app.Groups.Creator(newGroupData);

                if (contactList.Count == 0)
                {
                    app.Contacts.Creator(newContactData);
                }
            }
            if (contactList.Count == 0)
            {
                app.Contacts.Creator(newContactData);
            }

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldData = group.GetContacts();

            if (oldData.SequenceEqual(ContactData.GetAll()))
            {
                app.Contacts.Creator(newContactData);
            }

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