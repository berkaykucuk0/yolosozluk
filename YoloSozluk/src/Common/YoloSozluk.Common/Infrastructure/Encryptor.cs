using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Infrastructure
{
    public static class Encryptor
    {
        //MD5 encryptor
        public static string Encrypt(string password)
        {
            var md5 = MD5.Create();
            var bytePassword = Encoding.ASCII.GetBytes(password);
            var hashedBytesPassword = md5.ComputeHash(bytePassword);
            return Convert.ToHexString(hashedBytesPassword);
        }
    }
}
