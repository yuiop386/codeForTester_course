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
            List<GroupData> groupList = GroupData.GetAll();
            List<ContactData> contactList = ContactData.GetAll();
            ContactData newContactData = new ContactData("AddedTo", $"AddingToGroup{rnd.Next(0, 999)}");
            GroupData newGroupData = new GroupData("Automatic Creation");

            if (groupList.Count == 0)
            {
                app.Groups.Creator(newGroupData);

                if (contactList.Count == 0)
                {
                    app.Contacts.Creator(newContactData);
                    GroupData tempGroup = GroupData.GetAll()[0];
                    List<ContactData> tempOldData = tempGroup.GetContacts();
                    ContactData tempContact = ContactData.GetAll().Except(tempOldData).First();
                    app.Contacts.AddContactToGroup(tempContact, tempGroup);
                }
            }
            else
            {
                if (contactList.Count == 0)
                {
                    app.Contacts.Creator(newContactData);
                }

                GroupData tempGroup = GroupData.GetAll()[0];
                List<ContactData> tempOldData = tempGroup.GetContacts();

                if (tempOldData.Count == 0)
                {
                    ContactData tempContact = ContactData.GetAll().Except(tempOldData).First();
                    app.Contacts.AddContactToGroup(tempContact, tempGroup);
                }
            }

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