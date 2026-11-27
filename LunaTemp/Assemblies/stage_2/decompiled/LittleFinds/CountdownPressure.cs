using System;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class CountdownPressure : MonoBehaviour
	{
		private GameManager game;

		private RectTransform clock;

		private Image paper;

		private Text digits;

		private Vector2 origin;

		private Vector3 scale;

		private Color paperColor;

		private float phase;

		private float flashAge = 1f;

		private float lastRemaining = -1f;

		private int milestoneStage;

		public int UrgencyStage { get; private set; }

		public static CountdownPressure Create(GameManager game)
		{
			CountdownPressure effect = game.UI.time.transform.parent.gameObject.AddComponent<CountdownPressure>();
			effect.game = game;
			effect.clock = (RectTransform)effect.transform;
			effect.clock.anchoredPosition += new Vector2((0.5f - effect.clock.pivot.x) * effect.clock.rect.width, (0.5f - effect.clock.pivot.y) * effect.clock.rect.height);
			effect.clock.pivot = new Vector2(0.5f, 0.5f);
			effect.paper = effect.GetComponent<Image>();
			effect.digits = game.UI.time;
			effect.origin = effect.clock.anchoredPosition;
			effect.scale = effect.clock.localScale;
			effect.paperColor = effect.paper.color;
			return effect;
		}

		public void ResetVisual()
		{
			if ((bool)clock)
			{
				phase = 0f;
				flashAge = 1f;
				lastRemaining = -1f;
				Restore();
			}
		}

		private void Restore()
		{
			UrgencyStage = 0;
			clock.anchoredPosition = origin;
			clock.localScale = scale;
			clock.localRotation = Quaternion.identity;
			paper.color = paperColor;
			digits.color = FindStyle.Ink;
		}

		private void Update()
		{
			if (!game)
			{
				return;
			}
			bool active = game.State == GameState.Playing && game.Timer.Running && !game.UI.overlay.activeSelf && !game.Tutorial.InLesson;
			float seconds = game.Timer.Remaining;
			if (!active || seconds <= 0f)
			{
				if (game.State == GameState.Shuffling && game.Timer.Running && !game.UI.overlay.activeSelf && !game.Tutorial.InLesson)
				{
					Restore();
					flashAge = 1f;
					phase = 0f;
				}
				else
				{
					ResetVisual();
				}
				return;
			}
			bool crossed = lastRemaining > 0f && ((lastRemaining > 30f && seconds <= 30f) || (lastRemaining > 20f && seconds <= 20f) || (lastRemaining > 10f && seconds <= 10f));
			bool finalEntry = lastRemaining > 5f && seconds <= 5f;
			lastRemaining = seconds;
			if (crossed && seconds > 5f)
			{
				flashAge = 0f;
				milestoneStage = ((!(seconds <= 10f)) ? 1 : 2);
			}
			flashAge += Time.unscaledDeltaTime;
			float danger = 0f;
			float beat;
			if (seconds <= 5f)
			{
				UrgencyStage = 3;
				danger = 1f - Mathf.Clamp01(seconds / 5f);
				if (finalEntry)
				{
					phase = 3.14159265f / 2f;
				}
				float rate = Mathf.Lerp(1.5f, 4f, danger);
				phase += Time.unscaledDeltaTime * rate * 3.14159265f * 2f;
				beat = Mathf.Pow(Mathf.Max(0f, Mathf.Sin(phase)), 3f);
			}
			else
			{
				if (!(flashAge < 0.45f))
				{
					Restore();
					return;
				}
				UrgencyStage = milestoneStage;
				beat = Mathf.Sin(flashAge / 0.45f * 3.14159265f);
			}
			Color alarm = FindStyle.Hex((UrgencyStage >= 2) ? "ef3156" : "ed8e21");
			digits.color = Color.Lerp(FindStyle.Ink, alarm, (UrgencyStage == 3) ? (0.7f + beat * 0.3f) : beat);
			paper.color = Color.Lerp(paperColor, FindStyle.Hex((UrgencyStage >= 2) ? "ffb7c6" : "ffe09a"), beat * ((UrgencyStage == 3) ? (0.4f + 0.2f * danger) : 0.55f));
			clock.localScale = scale * (1f + beat * ((UrgencyStage == 3) ? (0.06f + 0.06f * danger) : 0.05f));
			clock.anchoredPosition = origin + new Vector2((UrgencyStage == 3) ? (Mathf.Sin(phase * 2f) * 3f * beat) : 0f, beat * 2f);
			clock.localRotation = Quaternion.Euler(0f, 0f, (UrgencyStage == 3) ? (Mathf.Sin(phase) * 1.8f * beat) : 0f);
		}

		private void OnDisable()
		{
			ResetVisual();
		}
	}
}
