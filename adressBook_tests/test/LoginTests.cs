﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAdressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            app.Auth.Logout();
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
            Assert.IsTrue(app.Auth.IsLoggedIn());
        }
        [Test]
        public void LoginWithInvalidCredentials()
        {
            app.Auth.Logout();
            AccountData account = new AccountData("admin", "1234");
            app.Auth.Login(account);
            Assert.IsFalse(app.Auth.IsLoggedIn());
        }
    }
}