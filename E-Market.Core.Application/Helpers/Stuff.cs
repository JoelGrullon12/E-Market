using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Helpers
{
    public static class Stuff
    {
        public static string SetDescription(string text)
        {
            string[] words=text.Split(' ');
            string description = "";
            int e = words.Length < 16 ? words.Length - 1 : 16;
            for (int i = 0; i < e; i++)
            {
                description += words[i] + " ";
            }
            description += e == 16 ? words[e] + "..." : words[e];
            return description;
        }

        public static string EncryptSHA256(string pass)
        {
            //Create a SHA256
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));

                //Convert To String
                StringBuilder builder = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
