using System;
using System.Security.Cryptography;
using System.Text;

namespace PasteBinSharp.UI
{
    static class PasswordHelper
    {
        public static string UnprotectPassword(string password)
        {
            byte[] protectedBytes = Convert.FromBase64String(password);
            byte[] bytes = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string ProtectPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] protectedBytes = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(protectedBytes);
        }
    }
}
