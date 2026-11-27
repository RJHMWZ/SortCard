using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class TutorialController : MonoBehaviour
	{
		public bool isTutorialLevel;

		public GameManager game;

		public GameObject coachRoot;

		public RectTransform coach;

		public RectTransform focus;

		public RectTransform hand;

		public Text heading;

		public Button nextButton;

		public Text nextLabel;

		private RectTransform target;

		private readonly Vector3[] corners = new Vector3[4];

		public LessonStep Step { get; private set; }

		public bool InLesson => isTutorialLevel;

		public bool CanShuffle => !InLesson || Step == LessonStep.Shuffle;

		public bool CanDiscard => !InLesson || Step == LessonStep.Discard;

		public bool AllowAutoReturn => !InLesson;

		public bool CanDiscardCard(CardController card)
		{
			return !InLesson || (Step == LessonStep.Discard && card.Index == 1);
		}

		public void EnterLevel()
		{
			isTutorialLevel = true;
			game.Restart();
		}

		public void ExitLevel()
		{
			isTutorialLevel = false;
			coachRoot.SetActive(false);
			focus.gameObject.SetActive(false);
			hand.gameObject.SetActive(false);
			RefreshLayout();
			game.Begin();
		}

		public void PlayRound()
		{
			ExitLevel();
		}

		private void RefreshLayout()
		{
			MobileGameLayout layout = coachRoot.transform.parent.GetComponent<MobileGameLayout>();
			if ((bool)layout)
			{
				layout.Reflow();
			}
			game.UI.ResetClock();
		}

		public void StartLesson()
		{
			game.ResetScore();
			game.Risk.ResetRound();
			game.Timer.ResetTimer();
			game.Timer.Running = false;
			game.UI.CloseModal();
			game.Collections.ResetCollections();
			coachRoot.SetActive(true);
			game.UI.helpButton.GetComponentInChildren<Text>().text = "Exit Tutorial";
			game.BreakStreak();
			RefreshLayout();
			SetStep(LessonStep.Reveal);
			game.Shuffle.InitialDeal();
		}

		public bool CanTap(CardController card)
		{
			if (!InLesson)
			{
				return true;
			}
			return card.Index == 0 && ((Step == LessonStep.Reveal && card.State == CardState.FaceDown) || (Step == LessonStep.Collect && card.State == CardState.FaceUp));
		}

		public void OnRevealed(CardController card)
		{
			if (InLesson && Step == LessonStep.Reveal && card.Index == 0)
			{
				SetStep(LessonStep.Collect);
			}
		}

		public bool OnLimitReached(CardController card)
		{
			return false;
		}

		public void OnReturned()
		{
		}

		public void OnRejected(CardController card)
		{
		}

		public void OnCollected(CardController card)
		{
			if (InLesson && Step == LessonStep.Collect && card.Index == 0)
			{
				SetStep(LessonStep.Discard);
				game.Cards.cards[1].PreviewForTutorial();
			}
		}

		public void OnDiscarded(CardController card)
		{
			if (InLesson && Step == LessonStep.Discard && card.Index == 1)
			{
				SetStep(LessonStep.Shuffle);
			}
		}

		public void OnShuffled()
		{
			if (InLesson && Step == LessonStep.Shuffle)
			{
				CompleteLesson();
			}
		}

		public void Continue()
		{
			if (InLesson && Step == LessonStep.Complete)
			{
				PlayRound();
			}
		}

		public void CompleteLesson()
		{
			SetStep(LessonStep.Complete);
			PlayerPrefs.SetInt("LittleFinds.TutorialComplete", 1);
			PlayerPrefs.Save();
		}

		public FindData[] BuildDeck()
		{
			int[] ids = new int[12]
			{
				0, 12, 4, 8, 1, 5, 9, 2, 6, 10,
				3, 7
			};
			FindData[] result = new FindData[12];
			for (int i = 0; i < 12; i++)
			{
				result[i] = game.Cards.Item(ids[i]);
			}
			return result;
		}

		private void SetStep(LessonStep step)
		{
			Step = step;
			CardController[] cards = game.Cards.cards;
			foreach (CardController card in cards)
			{
				card.ResetHover();
			}
			bool complete = step == LessonStep.Complete;
			nextButton.gameObject.SetActive(complete);
			nextLabel.text = "Play!";
			if ((bool)hand)
			{
				hand.GetComponentInChildren<Text>().text = ((step == LessonStep.Discard) ? "DRAG" : "TAP");
			}
			switch (step)
			{
			case LessonStep.Reveal:
				heading.text = "1/4  Tap the card to flip it";
				break;
			case LessonStep.Collect:
				heading.text = "2/4  Tap again to collect Fish";
				break;
			case LessonStep.Discard:
				heading.text = "3/4  Drag the flower to Trash";
				break;
			case LessonStep.Shuffle:
				heading.text = "4/4  Tap Fresh Cards to shuffle";
				break;
			default:
				heading.text = "Fill all three boxes. Ready?";
				break;
			}
			heading.rectTransform.sizeDelta = new Vector2(complete ? 730 : 964, 132f);
			heading.alignment = (complete ? TextAnchor.MiddleLeft : TextAnchor.MiddleCenter);
			if (step != 0)
			{
				game.Feedback.Play(10);
			}
			RefreshTarget();
		}

		private void RefreshTarget()
		{
			target = null;
			if (Step == LessonStep.Reveal || Step == LessonStep.Collect)
			{
				target = (RectTransform)game.Cards.cards[0].transform;
			}
			else if (Step == LessonStep.Discard)
			{
				target = (RectTransform)game.Cards.cards[1].transform;
			}
			else if (Step == LessonStep.Shuffle)
			{
				target = (RectTransform)game.UI.freshButton.transform;
			}
			Graphic[] componentsInChildren = focus.GetComponentsInChildren<Graphic>(true);
			foreach (Graphic graphic in componentsInChildren)
			{
				graphic.raycastTarget = false;
			}
			Graphic[] componentsInChildren2 = hand.GetComponentsInChildren<Graphic>(true);
			foreach (Graphic graphic2 in componentsInChildren2)
			{
				graphic2.raycastTarget = false;
			}
			focus.gameObject.SetActive(target != null);
			hand.gameObject.SetActive(target != null);
		}

		private void LateUpdate()
		{
			if (InLesson && (bool)target && target.gameObject.activeInHierarchy)
			{
				target.GetWorldCorners(corners);
				Vector3 min = focus.parent.InverseTransformPoint(corners[0]);
				Vector3 max = focus.parent.InverseTransformPoint(corners[2]);
				focus.localPosition = (min + max) * 0.5f;
				focus.sizeDelta = new Vector2(max.x - min.x + 24f, max.y - min.y + 24f);
				focus.localScale = Vector3.one * (1f + Mathf.Sin(Time.unscaledTime * 5f) * 0.025f);
				hand.localPosition = new Vector3((min.x + max.x) * 0.5f, min.y - 26f + Mathf.Sin(Time.unscaledTime * 5f) * 7f, 0f);
			}
		}
	}
}
