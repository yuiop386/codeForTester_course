using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            app.Navigator.GoToGroupsPage();
            if (!app.Groups.IsElementPresent(By.Name("selected[]")))
            {
                GroupData group = new GroupData("Removal_Created_group");
                group.Header = "RC_header";
                group.Footer = "RC_footer";
                app.Groups.Creator(group);
            }
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeRemoved = oldGroups[0];

            app.Groups.Remove(toBeRemoved);

            List<GroupData> newGroups = GroupData.GetAll();

            Assert.AreEqual(oldGroups.Count - 1, newGroups.Count);

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
