using System.Collections;
using UnityEngine;

namespace LittleFinds
{
	public class ShuffleController : MonoBehaviour
	{
		public GameManager game;

		public RectTransform center;

		private bool queued;

		private bool queuedStarting;

		public bool IsQueued => queued;

		private bool Resolving => game.Cards.Collecting || ((bool)game.Trash && game.Trash.IsBusy);

		private void Update()
		{
			if (!TryRefreshEmpty())
			{
				TryQueuedShuffle();
			}
		}

		public bool TryRefreshEmpty()
		{
			if (game.State != GameState.Playing || Resolving || game.Cards.BoardCount != 0 || ((bool)game.Tutorial && !game.Tutorial.CanShuffle))
			{
				return false;
			}
			queued = false;
			StartCoroutine(Run(false, true));
			return true;
		}

		public void Shuffle()
		{
			if ((!game.Tutorial || game.Tutorial.CanShuffle) && game.State == GameState.Playing)
			{
				if (Resolving)
				{
					queued = true;
					game.UI.Message("Shuffle queued");
				}
				else if (!TryRefreshEmpty())
				{
					queued = false;
					StartCoroutine(Run(false));
				}
			}
		}

		public void TryQueuedShuffle()
		{
			if (queued && !queuedStarting && game.State == GameState.Playing && !Resolving)
			{
				queuedStarting = true;
				StartCoroutine(StartQueued());
			}
		}

		private IEnumerator StartQueued()
		{
			yield return null;
			queuedStarting = false;
			if (queued && game.State == GameState.Playing && !Resolving)
			{
				Shuffle();
			}
		}

		public void InitialDeal()
		{
			StartCoroutine(Run(true));
		}

		public void Cancel()
		{
			queued = false;
			queuedStarting = false;
			StopAllCoroutines();
		}

		private IEnumerator Run(bool initial, bool automatic = false)
		{
			if (!initial && !automatic && !game.Risk.PayForShuffle())
			{
				yield break;
			}
			queued = false;
			game.SetState(GameState.Shuffling);
			game.UI.Message("A FRESH LITTLE MIX");
			game.Feedback.Play(4);
			CardController[] cards = game.Cards.cards;
			Vector3[] starts = new Vector3[12];
			if (!initial && !automatic)
			{
				for (int i = 0; i < 12; i++)
				{
					cards[i].PrepareShuffle();
					starts[i] = cards[i].transform.position;
				}
				for (float t = 0f; t < 0.31f; t += Time.deltaTime)
				{
					for (int j = 0; j < 12; j++)
					{
						if (cards[j].gameObject.activeSelf)
						{
							float u = FindStyle.Ease(Mathf.Clamp01((t - (float)j * 0.004f) / 0.26f));
							cards[j].transform.position = Vector3.Lerp(starts[j], center.position, u);
							cards[j].transform.localScale = Vector3.one * Mathf.Lerp(1f, 0.08f, u);
							cards[j].transform.localRotation = Quaternion.Euler(0f, 0f, (float)((j % 2 == 0) ? 45 : (-45)) * u);
							cards[j].group.alpha = 1f - u;
						}
					}
					yield return null;
				}
			}
			game.Cards.Deal();
			for (int m = 0; m < 12; m++)
			{
				starts[m] = cards[m].transform.position;
				cards[m].transform.position = center.position;
				cards[m].transform.localScale = Vector3.one * 0.25f;
				cards[m].group.alpha = 0f;
				cards[m].group.blocksRaycasts = false;
			}
			for (float t2 = 0f; t2 < 0.47f; t2 += Time.deltaTime)
			{
				for (int k = 0; k < 12; k++)
				{
					float u2 = Mathf.Clamp01((t2 - (float)k * 0.012f) / 0.32f);
					float eased = 1f + 2.3f * Mathf.Pow(u2 - 1f, 3f) + 1.3f * Mathf.Pow(u2 - 1f, 2f);
					cards[k].transform.position = Vector3.LerpUnclamped(center.position, cards[k].HomePosition, eased);
					cards[k].transform.localScale = Vector3.one * Mathf.LerpUnclamped(0.25f, 1f, eased);
					cards[k].transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(13f, 0f, u2));
					cards[k].group.alpha = Mathf.Clamp01(u2 * 4f);
				}
				yield return null;
			}
			foreach (CardController c in cards)
			{
				c.transform.position = c.HomePosition;
				c.transform.localScale = Vector3.one;
				c.transform.localRotation = Quaternion.identity;
				c.group.alpha = 1f;
				c.group.blocksRaycasts = true;
			}
			game.SetState(GameState.Playing);
			if ((bool)game.Tutorial)
			{
				game.Tutorial.OnShuffled();
			}
			game.UI.Message("Choose a card and see what you find.");
		}
	}
}
