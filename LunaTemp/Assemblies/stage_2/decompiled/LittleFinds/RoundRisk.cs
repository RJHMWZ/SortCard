using UnityEngine;

namespace LittleFinds
{
	public class RoundRisk : MonoBehaviour
	{
		[Min(1f)]
		public int maximumLives = 5;

		[Min(0f)]
		public float shuffleSeconds = 4f;

		[Range(0f, 0.75f)]
		public float simultaneousMistakeGrace = 0.35f;

		public GameManager game;

		private float lastDamage = -10f;

		public int Lives { get; private set; } = 5;


		public int Mistakes { get; private set; }

		public void ResetRound()
		{
			Lives = maximumLives;
			Mistakes = 0;
			lastDamage = -10f;
		}

		public bool Damage(MistakeKind kind)
		{
			return Damage(kind, game.Trash ? game.Trash.target.position : game.Feedback.stage.position);
		}

		public bool Damage(MistakeKind kind, Vector3 origin)
		{
			if (game.State != GameState.Playing || (kind != MistakeKind.DiscardedTarget && Time.unscaledTime - lastDamage < simultaneousMistakeGrace))
			{
				return false;
			}
			lastDamage = Time.unscaledTime;
			Mistakes++;
			bool practice = (bool)game.Tutorial && game.Tutorial.InLesson;
			Lives = Mathf.Max(practice ? 1 : 0, Lives - 1);
			game.ChangeScore(-100);
			game.BreakStreak();
			game.Feedback.Penalty((kind == MistakeKind.WrongCategory) ? "WRONG BOX!" : "BOX FULL!", origin);
			GameUIController uI = game.UI;
			object value;
			switch (kind)
			{
			default:
				value = "Penalty: Collection already full. -1 life";
				break;
			case MistakeKind.WrongCategory:
				value = "Penalty: Not a target category. -1 life";
				break;
			case MistakeKind.DiscardedTarget:
				value = "Penalty: Still needed! -1 life";
				break;
			}
			uI.Message((string)value);
			if (Lives == 0)
			{
				game.Finish(false);
			}
			return true;
		}

		public bool PayForShuffle()
		{
			if ((bool)game.Tutorial && game.Tutorial.InLesson)
			{
				return true;
			}
			game.Timer.Advance(shuffleSeconds);
			if (game.State != GameState.Playing)
			{
				return false;
			}
			game.UI.Message("Shuffle cost: -" + shuffleSeconds.ToString("0") + "s");
			return true;
		}
	}
}
