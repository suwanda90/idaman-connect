using System;
using System.Text;

namespace ApplicationCore.Helpers
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

        public static string ToBase64EncodeWithKey(this string text, string key)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key))
            {
                return text;
            }

            var keyValue = key.ToBase64Decode();

            byte[] textBytes = Encoding.UTF8.GetBytes(text + keyValue);
            return Convert.ToBase64String(textBytes);
        }

        public static string ToBase64DecodeWithKey(this string text, string key)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key))
            {
                return text;
            }

            var keyValue = key.ToBase64Decode();

            byte[] base64EncodedBytes = Convert.FromBase64String(text);
            var textDecode = Encoding.UTF8.GetString(base64EncodedBytes);
            int length = textDecode.Length - keyValue.Length;

            return textDecode.Substring(0, length);
        }
    }
}
