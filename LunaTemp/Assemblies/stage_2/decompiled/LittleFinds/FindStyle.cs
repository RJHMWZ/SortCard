using UnityEngine;

namespace LittleFinds
{
	public static class FindStyle
	{
		public static Color Ink = Hex("252b4b");

		public static Color Soft = Hex("8b90aa");

		public static Color Sage = Hex("6170ff");

		public static string[] Names = new string[6] { "Fish", "Toys", "Fruit", "Flowers", "Sweets", "Music" };

		public static Color[] Colors = new Color[6]
		{
			Hex("5baeff"),
			Hex("9780ed"),
			Hex("ffae35"),
			Hex("ed7399"),
			Hex("e78c59"),
			Hex("38b797")
		};

		public static Color Hex(string value)
		{
			ColorUtility.TryParseHtmlString("#" + value, out var c);
			return c;
		}

		public static float Ease(float t)
		{
			return 1f - Mathf.Pow(1f - t, 3f);
		}
	}
}
