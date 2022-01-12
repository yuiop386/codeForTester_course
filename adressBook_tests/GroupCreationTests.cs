﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace adressBook_tests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToGroupsPage();
            NewGroupCreation();
            GroupData group = new GroupData("name_name");
            group.Header = "header_header";
            group.Footer = "footer_footer";
            FillGroupForm(group);
            SubmitGroupCreation();
            GoToGroupsPage();
        }
    }
}
