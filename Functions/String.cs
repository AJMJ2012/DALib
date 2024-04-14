using System.Security.Cryptography;
using System.Text;

namespace DALib.Functions;

public static class StringFunctions {
	public static string MD5Hash(string input) {
		if (input == null) { return ""; }
		MD5 md5Hash = MD5.Create();
		byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
		StringBuilder sBuilder = new StringBuilder();
		for (int i = 0; i < data.Length; i++) {
			sBuilder.Append(data[i].ToString("x2"));
		}
		return sBuilder.ToString();
	}

	public const int hashSize = 12; // Terraria's ModConfig text field isn't wide enough for 12 :/
	public static string HashString(string text, int size = hashSize)
	{
		const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		byte[] bytes = Encoding.UTF8.GetBytes(text);

		SHA256Managed hashstring = new SHA256Managed();
		byte[] hash = hashstring.ComputeHash(bytes);

		char[] hash2 = new char[size];

		// Note that here we are wasting bits of hash! 
		// But it isn't really important, because hash.Length == 32
		for (int i = 0; i < hash2.Length; i++)
		{
			hash2[i] = chars[hash[i] % chars.Length];
		}

		return new string(hash2);
	}
}
