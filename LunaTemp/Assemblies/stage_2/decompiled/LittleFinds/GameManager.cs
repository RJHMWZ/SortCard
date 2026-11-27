using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LittleFinds
{
	public class GameManager : MonoBehaviour
	{
		public RoundRisk Risk;

		public TutorialController Tutorial;

		public CardManager Cards;

		public CollectionManager Collections;

		public ShuffleController Shuffle;

		public GameTimer Timer;

		public GameUIController UI;

		public FeedbackController Feedback;

		public TrashBin Trash;

		public const int CorrectPoints = 100;

		public const int ComboBonus = 25;

		public const int MistakePoints = 100;

		private ScoreHUD scoreHUD;

		private ComboHUD comboHUD;

		public int LevelIndex { get; private set; }

		public EasyLevel Level => EasyLevels.All[LevelIndex];

		public int BestCombo { get; private set; }

		public float ComboRemaining { get; private set; }

		public float ComboFraction => Mathf.Clamp01(ComboRemaining / Level.ComboWindow);

		public int Score { get; private set; }

		public GameState State { get; private set; } = GameState.Ready;


		public int Streak { get; private set; }

		public void ChangeScore(int delta)
		{
			Score = Mathf.Max(0, Score + delta);
			if ((bool)scoreHUD)
			{
				scoreHUD.SetScore(Score, delta);
			}
		}

		public void ResetScore()
		{
			Score = 0;
			if ((bool)scoreHUD)
			{
				scoreHUD.SetScore(0, 0);
			}
		}

		public void EnsureTrash()
		{
			if (!Trash)
			{
				Trash = TrashBin.Create(this);
			}
		}

		private void Start()
		{
			Application.runInBackground = true;
			Application.targetFrameRate = 60;
			LevelIndex = 0;
			EnsureCombo();
			EnsureTrash();
			if (!scoreHUD)
			{
				scoreHUD = ScoreHUD.Create(this);
			}
			if ((bool)EventSystem.current)
			{
				EventSystem.current.pixelDragThreshold = Mathf.Clamp(Screen.width / 60, 10, 24);
			}
			if (!Tutorial.InLesson && PlayerPrefs.GetInt("LittleFinds.TutorialSeen", PlayerPrefs.GetInt("LittleFinds.TutorialComplete", 0)) == 0)
			{
				PlayerPrefs.SetInt("LittleFinds.TutorialSeen", 1);
				PlayerPrefs.Save();
				Tutorial.isTutorialLevel = true;
			}
			Timer.Expired = delegate
			{
				Finish(false);
			};
			Cards.game = this;
			Shuffle.game = this;
			if (Tutorial.InLesson)
			{
				Tutorial.StartLesson();
			}
			else
			{
				Begin();
			}
		}

		public void EnsureCombo()
		{
			if (!comboHUD)
			{
				comboHUD = ComboHUD.Create(this);
			}
		}

		private void Update()
		{
			if (State == GameState.Playing && !UI.overlay.activeSelf && ComboRemaining > 0f)
			{
				ComboRemaining = Mathf.Max(0f, ComboRemaining - Time.deltaTime);
				if (ComboRemaining == 0f)
				{
					BreakStreak();
				}
			}
		}

		public void BreakStreak()
		{
			Streak = 0;
			ComboRemaining = 0f;
		}

		public void RegisterCombo()
		{
			Streak = ((!(ComboRemaining > 0f)) ? 1 : (Streak + 1));
			BestCombo = Mathf.Max(BestCombo, Streak);
			ComboRemaining = Level.ComboWindow;
			if ((bool)comboHUD)
			{
				comboHUD.RefreshNow();
			}
		}

		public void PlayLevel(int index)
		{
			LevelIndex = Mathf.Clamp(index, 0, EasyLevels.All.Length - 1);
			Begin();
		}

		public void ContinueCampaign()
		{
			PlayLevel((LevelIndex != EasyLevels.All.Length - 1) ? (LevelIndex + 1) : 0);
		}

		public void SetState(GameState state)
		{
			State = state;
		}

		public void Begin()
		{
			StopAllCoroutines();
			if ((bool)Trash)
			{
				Trash.Cancel();
			}
			Shuffle.Cancel();
			Cards.Freeze();
			Feedback.Clear();
			ResetScore();
			Collections.ResetCollections();
			Risk.ResetRound();
			BreakStreak();
			BestCombo = 0;
			UI.CloseModal();
			UI.ResetClock();
			Timer.ResetTimer(((bool)Tutorial && Tutorial.InLesson) ? 120f : Level.Seconds);
			Shuffle.InitialDeal();
		}

		public bool WouldDiscardCostLife(CardController card)
		{
			return card.Data != null && Collections.IsTarget(card.Data.category) && Collections.Count(card.Data.category) < 4;
		}

		public void Discarded(CardController card, bool costLife)
		{
			if (State == GameState.Playing)
			{
				if (costLife)
				{
					Risk.Damage(MistakeKind.DiscardedTarget, card.HomePosition);
				}
				else
				{
					Feedback.Play(9);
				}
				if ((bool)Tutorial)
				{
					Tutorial.OnDiscarded(card);
				}
				if (State == GameState.Playing && !Shuffle.TryRefreshEmpty())
				{
					Shuffle.TryQueuedShuffle();
				}
			}
		}

		public void Collect(CardController card)
		{
			if (State != GameState.Playing || card.State != CardState.FaceUp)
			{
				return;
			}
			int c = card.Data.category;
			if (!Collections.Reserve(c))
			{
				if (!Collections.IsTarget(c) || Collections.Count(c) >= 4)
				{
					card.Shatter();
					Risk.Damage((!Collections.IsTarget(c)) ? MistakeKind.WrongCategory : MistakeKind.FullCollection, card.HomePosition);
					if ((bool)Tutorial)
					{
						Tutorial.OnRejected(card);
					}
				}
				else
				{
					card.Return(false);
					UI.Message("Pending: The last treasure is already on its way.");
				}
			}
			else
			{
				RegisterCombo();
				int points = 100 + (Streak - 1) * 25;
				ChangeScore(points);
				card.BeginCollect();
				Feedback.Play(1);
				Feedback.Impact(c, Streak, Collections.IsSatisfiedOrReserved(c), card.HomePosition, points);
			}
		}

		public void Collected(CardController card)
		{
			if (State == GameState.Playing || State == GameState.Shuffling)
			{
				int c = card.Data.category;
				Collections.Commit(c, card.Data.art);
				if (Collections.Count(c) == 4)
				{
					Cards.RefreshCategories();
					Feedback.Play(2);
				}
				if ((bool)Tutorial)
				{
					Tutorial.OnCollected(card);
				}
				if (Collections.Total == 12)
				{
					Finish(true);
				}
				else if (!Shuffle.TryRefreshEmpty())
				{
					Shuffle.TryQueuedShuffle();
				}
			}
		}

		public void Restart()
		{
			StopAllCoroutines();
			if ((bool)Trash)
			{
				Trash.Cancel();
			}
			Timer.Running = false;
			Shuffle.Cancel();
			Cards.Freeze();
			Collections.CancelReservations();
			Risk.ResetRound();
			Streak = 0;
			if ((bool)Tutorial && Tutorial.InLesson)
			{
				Feedback.Clear();
				Tutorial.StartLesson();
			}
			else
			{
				Begin();
			}
		}

		public void Finish(bool won)
		{
			if (State == GameState.Victory || State == GameState.Failed)
			{
				return;
			}
			State = (won ? GameState.Victory : GameState.Failed);
			Timer.Running = false;
			if ((bool)Trash)
			{
				Trash.Cancel();
			}
			Shuffle.Cancel();
			Cards.Freeze();
			Collections.CancelReservations();
			if (won && (bool)Tutorial && Tutorial.InLesson)
			{
				Feedback.Play(6);
				Feedback.Celebration();
				Tutorial.CompleteLesson();
				return;
			}
			if (won)
			{
				StartCoroutine(Celebrate());
				return;
			}
			if (Feedback.SoundOn && (bool)Feedback.audio)
			{
				Feedback.audio.PlayFailure();
			}
			UI.ShowResult(false, Collections.Total);
		}

		private IEnumerator Celebrate()
		{
			Feedback.Play(6);
			Feedback.Celebration();
			yield return new WaitForSeconds(0.24f);
			UI.ShowResult(true, Collections.Total);
		}
	}
}
