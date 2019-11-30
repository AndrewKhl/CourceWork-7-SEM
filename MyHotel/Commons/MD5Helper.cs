using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Commons
{
    public class MD5Helper : IDisposable
    {
        private readonly MD5 _hash;

        public MD5Helper()
        {
            _hash = MD5.Create();
        }

        public string Shifr(string str)
        {
            byte[] data = _hash.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public bool VerifyString(string str, string coded)
        {
            string hashOfInput = Shifr(str);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, coded) == 0;
        }

        public void Dispose()
        {
            _hash.Dispose();
        }
    }
}
