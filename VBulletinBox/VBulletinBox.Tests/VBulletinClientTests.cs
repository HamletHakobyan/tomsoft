using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using VBulletinBox.Models;
using Developpez.Dotnet;
using Microsoft.VisualBasic;

namespace VBulletinBox.Tests
{
    [TestFixture]
    public class VBulletinClientTests
    {
        [Test, TestCaseSource("PromptCredentials")]
        public void Check_Login(string userName, string password, string wrongPassword)
        {
            VBulletinAccount account = GetAccount();
            account.UserName = userName;
            account.PasswordMD5 = password.GetMD5Digest();
            VBulletinClient client = new VBulletinClient(account);
            Assert.IsTrue(client.Login());
            Assert.IsNotNull(client.Cookies["bbuserid"]);

            account.PasswordMD5 = wrongPassword.GetMD5Digest();
            Assert.IsFalse(client.Login());
        }

        [Test, TestCaseSource("PromptCredentials")]
        public void Check_GetMessages(string userName, string password, string wrongPassword)
        {
            VBulletinAccount account = GetAccount();
            account.UserName = userName;
            account.PasswordMD5 = password.GetMD5Digest();
            VBulletinClient client = new VBulletinClient(account);
            MessageRepository repository = client.GetMessages();
            Assert.IsNotNull(repository);
            Assert.Greater(repository.Folders.Count, 0);
        }

        private static VBulletinAccount GetAccount()
        {
            VBulletinAccount account = new VBulletinAccount
            {
                DisplayName = "Developpez.com",
                SiteUrl = "http://www.developpez.net/forums"
            };
            return account;
        }

        static string _userName;
        static string _password;
        static string _wrongPassword;
        static object[] PromptCredentials
        {
            get
            {
                if (_userName == null)
                    _userName = Interaction.InputBox("Enter user name", "Test parameters", "", -1, -1);
                if (_password == null)
                    _password = Interaction.InputBox("Enter password", "Test parameters", "", -1, -1);
                if (_wrongPassword == null)
                    _wrongPassword = Interaction.InputBox("Enter wrong password", "Test parameters", "", -1, -1);
                return new object[]
                {
                    new object[] { _userName, _password, _wrongPassword }
                };
            }
        }
    }
}
