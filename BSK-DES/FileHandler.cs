using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BSK_DES {
	class FileHandler {

		// 0011000000110001001100100011001100110100001101010011011000110111 - 64 bits 01234567

		public static string ReadFromTextFile(string path) {
			string text = File.ReadAllText(path);
			text = hex2binary(text);
			//text = AdjustStringTo64(text);
			return text;
		}

		public static string ReadFromBinFile(string path) {
			byte[] fileBytes = File.ReadAllBytes(path);
			StringBuilder sb = new StringBuilder();
			string output;

			foreach (byte b in fileBytes) {
				sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
			}

			output = sb.ToString();
			output = AdjustStringTo64(output);

			return output;
		}

		private static string StringToBinary(string data) {
			StringBuilder sb = new StringBuilder();

			foreach (char c in data.ToCharArray()) {
				sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
			}
			return sb.ToString();
		}

		public static string BinaryToString(string data) {
			List<Byte> byteList = new List<Byte>();

			for (int i = 0; i < data.Length; i += 8) {
				byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
			}
			return Encoding.ASCII.GetString(byteList.ToArray());
		}

		public static string AdjustStringTo64(string text) {

			int length = text.Length;
			int base64, new64, addedBits = 0;
			StringBuilder stringBuilder = new StringBuilder(text);
			if(length % 64 != 0) {
				base64 = length / 64;
				new64 = base64*64 + 64;
				for(int i = length; i < new64 - 8; i++) {
					stringBuilder.Append("0");
					addedBits++;
				}
				stringBuilder.Append(StringToBinary(((addedBits/8)+1).ToString()));

			}
			else {
				for (int i = 0; i < 56; i++) {
					stringBuilder.Append("0");
					addedBits++;
				}
				stringBuilder.Append(StringToBinary(8.ToString()));
			}

			return stringBuilder.ToString();
		}




		public static string BinaryStringToHexString(string binary)
		{
			if (string.IsNullOrEmpty(binary))
				return binary;

			StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

			// TODO: check all 1's or 0's... throw otherwise

			int mod4Len = binary.Length % 8;
			if (mod4Len != 0)
			{
				// pad to length multiple of 8
				binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
			}

			for (int i = 0; i < binary.Length; i += 8)
			{
				string eightBits = binary.Substring(i, 8);
				result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
			}

			return result.ToString();
		}

		public static string hex2binary(string hexvalue)
		{
			string binaryval = "";
			binaryval = Convert.ToString(Convert.ToInt64(hexvalue, 16), 2);
			return binaryval;
		}
	}
}
