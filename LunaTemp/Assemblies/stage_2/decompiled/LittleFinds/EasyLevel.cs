namespace LittleFinds
{
	public sealed class EasyLevel
	{
		public readonly string Name;

		public readonly int[] Targets;

		public readonly float Seconds;

		public readonly float ComboWindow;

		public readonly float OpenSeconds;

		public readonly float ReturnSeconds;

		public readonly int Distractors;

		public EasyLevel(string name, int a, int b, int c, float seconds, float comboWindow, int distractors, float openSeconds, float returnSeconds)
		{
			Name = name;
			Targets = new int[3] { a, b, c };
			Seconds = seconds;
			ComboWindow = comboWindow;
			Distractors = distractors;
			OpenSeconds = openSeconds;
			ReturnSeconds = returnSeconds;
		}
	}
}
