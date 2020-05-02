using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BSK_DES {
	class FileHandler {

		// 0011000000110001001100100011001100110100001101010011011000110111 - 64 bits 01234567

		public static string ReadFromFile(string path) {
			string text = File.ReadAllText(path);
			text = StringToBinary(text);
			text = AdjustStringTo64(text);
			return text;
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

		private static string AdjustStringTo64(string text) {

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

			return stringBuilder.ToString();
		}
	}
}
