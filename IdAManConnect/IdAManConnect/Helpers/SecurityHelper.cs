using System;
using System.Text;

namespace IdAManConnect.Helpers
{
    public static class SecurityHelper
    {
        private static readonly string Key = "121dsaS2.mqwqasaw162172yad76wqteygysmciw*!*@!99hahsa019219775)(!)*@*#&%*@%!&*GDIUAOIUS!^%@*&!I!(*@)!@_!+_+S";
        
        public static bool IsBase64(this string base64String)
        {
            if (string.IsNullOrEmpty(base64String) ||
                base64String.Length % 4 != 0 ||
                base64String.Contains(" ") ||
                base64String.Contains("\t") ||
                base64String.Contains("\r") ||
                base64String.Contains("\n"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string ToBase64Encode(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text + Key);
            return Convert.ToBase64String(textBytes);
        }

        public static string ToBase64Decode(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] base64EncodedBytes = Convert.FromBase64String(text);
            var textDecode = Encoding.UTF8.GetString(base64EncodedBytes);
            int length = textDecode.Length - Key.Length;

            return textDecode.Substring(0, length);
        }
    }
}
