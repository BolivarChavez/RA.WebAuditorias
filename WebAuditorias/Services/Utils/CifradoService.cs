using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using WebAuditorias.Interfaces.Utils;

namespace WebAuditorias.Services.Utils
{
    public class CifradoService : ICifradoService
    {
        private static Rfc2898DeriveBytes _Rfc2898DeriveBytes;
        private static Byte[] _Byte;

        public string Encriptar(string valor)
        {
            try
            {
                string _Key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();
                using (RijndaelManaged _RijndaelManaged = new RijndaelManaged())
                {
                    _Rfc2898DeriveBytes = new Rfc2898DeriveBytes(_Key, new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
                    _RijndaelManaged.Key = _Rfc2898DeriveBytes.GetBytes(_RijndaelManaged.Key.Length);
                    _RijndaelManaged.IV = _Rfc2898DeriveBytes.GetBytes(_RijndaelManaged.IV.Length);

                    MemoryStream _MemoryStream = new MemoryStream();
                    CryptoStream _CryptoStream = new CryptoStream(_MemoryStream, _RijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write);
                    _Byte = Encoding.UTF8.GetBytes(valor);
                    _CryptoStream.Write(_Byte, 0, _Byte.Length);
                    _CryptoStream.Close();
                    return Convert.ToBase64String(_MemoryStream.ToArray());
                }
            }
            catch { return string.Empty; }
        }
    }
}