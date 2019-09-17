using System;
using System.IO;
using System.Text;
using System.Security;
using System.Security.Cryptography;


namespace AppMiTaller.Intranet.BE
{

	public class MD5HashAlgorithm
	{
		public static Byte[] ConvertStringToByteArray(String s)
		{
			return (new UnicodeEncoding()).GetBytes(s);
		}


		public string ComputeHash(string DataToHash)
		{
			Byte[] valueToHash = ConvertStringToByteArray(DataToHash);
			byte[] hashvalue = (new MD5CryptoServiceProvider()).ComputeHash(valueToHash); 
			return BitConverter.ToString(hashvalue);
		}
	}
}
