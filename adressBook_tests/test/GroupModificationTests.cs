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
                GroupData group = new GroupData("Modification_Created_group");
                group.Header = "MC_header";
                group.Footer = "MC_footer";
                app.Groups.Creator(group);
            }
        }
    }
}
