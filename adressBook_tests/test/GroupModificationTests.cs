using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("NewName");
            newData.Header = "NewHeader";
            newData.Footer = "NewFooter";

            app.Groups.Modify(1, newData);
        }
    }
}
