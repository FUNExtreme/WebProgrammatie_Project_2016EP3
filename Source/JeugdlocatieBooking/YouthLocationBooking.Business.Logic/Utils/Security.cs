using System;
using System.Security.Cryptography;

namespace YouthLocationBooking.Business.Logic.Utils
{
    public class Security
    {
        public static string Hash(string password)
        {
            SHA256Managed myhash = new SHA256Managed();
            byte[] hashValue = System.Text.Encoding.UTF8.GetBytes(password);
            hashValue = myhash.ComputeHash(hashValue);
            string hash = "";

            for (int x = 0; x < hashValue.Length; x ++)
                hash += string.Format("{0:x2}", hashValue[x]);

            return hash;
        }
    }
}
