using DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.Utility
{
    public class Utilities
    {
        public static bool VerifyConnection()
        {
            var result = false;
            var connection = new ConnectionModel();
            try
            {
                connection.Database.Connection.Open();
                connection.Database.Connection.Close();

                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public static string Hash(string password)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hashedPassword = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (byte theByte in crypto)
            {
                hashedPassword.Append(theByte.ToString("x2"));
            }
            return hashedPassword.ToString();
        }
        public static bool ValidatePassword(string password)
        {
            var hasUpperLetter = new Regex(@"[A-Z]+");
            var hasNumber = new Regex(@"[0-9]+");
            var hasMiniumDigits = new Regex(@".{8,}");
            var result = false;

            if (hasNumber.IsMatch(password) &&
            hasUpperLetter.IsMatch(password) &&
            hasMiniumDigits.IsMatch(password))
            {
                result = true;
            }

            return result;
        }
        public static Boolean ValidateFormat(String text, String expresion)
        {
            Boolean result;
            if (Regex.IsMatch(text, expresion))
            {
                if (Regex.Replace(text, expresion, String.Empty).Length == 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}

