using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace P1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = new DateTime(2020, 7, 3);
            TimeSpan ts = dt.Subtract(new DateTime(1970, 1, 1));
            string secretString = args[0];
            string givenencryptedString = args[1];
            Random rng = new Random((int)ts.TotalMinutes);
            byte[] key = BitConverter.GetBytes(rng.NextDouble());
            string calculatedencryptedString = Encrypt(key, secretString);
            int bla = 0;
            while(givenencryptedString != calculatedencryptedString){
                int seed = (int)ts.TotalMinutes + bla;
                rng = new Random(seed);
                key = BitConverter.GetBytes(rng.NextDouble());
                calculatedencryptedString = Encrypt(key, secretString);
                bla++;
            }
            Console.WriteLine((int)ts.TotalMinutes + bla - 1);
            // Console.WriteLine(calculatedencryptedString);
            static string Encrypt(byte[] key, string secretString)
            {
                DESCryptoServiceProvider csp = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms ,csp.CreateEncryptor(key, key), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);
                sw.Write(secretString);
                sw.Flush();
                cs.FlushFinalBlock();
                sw.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0,(int)ms.Length);
            }
        }
    }
}
