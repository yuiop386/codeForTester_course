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
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            app.Navigator.GoToGroupsPage();
            if (app.Groups.IsElementPresent(By.Name("selected[]")))
            {
                GroupData newData = new GroupData("NewName");
                newData.Header = "NewHeader";
                newData.Footer = "NewFooter";
                app.Groups.Modify(1, newData);
            }
            else
            {
                GroupData group = new GroupData("Modification_Created_group1");
                group.Header = "MC_header1";
                group.Footer = "MC_footer1";
                app.Groups.Creator(group);
                group.Name = "Modification_Created_group2";
                group.Header = "MC_header2";
                group.Footer = "MC_footer2";
                app.Groups.Modify(1, group);
            }
        }
    }
}
