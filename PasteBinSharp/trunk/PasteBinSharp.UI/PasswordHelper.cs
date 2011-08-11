using System;
using System.Security.Cryptography;
using System.Text;

namespace PasteBinSharp.UI
{
    static class PasswordHelper
    {
        public static string UnprotectPassword(string password)
        {
            try
            {
                byte[] protectedBytes = Convert.FromBase64String(password);
                byte[] bytes = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(bytes);
            }
            catch(Exception ex)
            {
                throw new PasswordDecodeException(ex);
            }
        }

        public static string ProtectPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] protectedBytes = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(protectedBytes);
        }
    }

    [Serializable]
    public class PasswordDecodeException : Exception
    {
        private const string DefaultMessage = "Error decoding password";
        public PasswordDecodeException() : base(DefaultMessage) { }
        public PasswordDecodeException(Exception inner) : base(DefaultMessage, inner) { }
        public PasswordDecodeException(string message) : base(message) { }
        public PasswordDecodeException(string message, Exception inner) : base(message, inner) { }
        protected PasswordDecodeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
