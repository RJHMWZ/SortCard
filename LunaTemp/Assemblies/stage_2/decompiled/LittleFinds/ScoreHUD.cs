using System;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class ScoreHUD : MonoBehaviour
	{
		private GameManager game;

		private Text number;

		private float hitAge = 1f;

		private bool negative;

		public static ScoreHUD Create(GameManager game)
		{
			GameObject o = new GameObject("Score HUD", typeof(RectTransform), typeof(ScoreHUD));
			RectTransform r = (RectTransform)o.transform;
			r.SetParent(game.Feedback.layer.parent, false);
			Vector2 vector3 = (r.anchorMin = (r.anchorMax = new Vector2(0f, 1f)));
			r.pivot = new Vector2(0f, 0.5f);
			r.sizeDelta = new Vector2(340f, 128f);
			r.SetSiblingIndex(game.Feedback.layer.GetSiblingIndex());
			ScoreHUD hud = o.GetComponent<ScoreHUD>();
			hud.game = game;
			hud.number = Label(r, game.Feedback.font, "Score Value", 82, new Vector2(0f, -20f), new Vector2(330f, 88f));
			hud.SetScore(game.Score, 0);
			hud.Align();
			return hud;
		}

		private static Text Label(Transform parent, Font font, string name, int size, Vector2 position, Vector2 dimensions)
		{
			GameObject o = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
			RectTransform r = (RectTransform)o.transform;
			r.SetParent(parent, false);
			Vector2 vector2 = (r.pivot = new Vector2(0f, 1f));
			Vector2 vector5 = (r.anchorMin = (r.anchorMax = vector2));
			r.anchoredPosition = position;
			r.sizeDelta = dimensions;
			Text t = o.GetComponent<Text>();
			t.font = font;
			t.fontSize = size;
			t.fontStyle = FontStyle.Normal;
			t.alignment = TextAnchor.MiddleLeft;
			t.color = FindStyle.Ink;
			t.raycastTarget = false;
			return t;
		}

		public void SetScore(int score, int delta)
		{
			number.text = score.ToString();
			negative = delta < 0;
			hitAge = ((delta == 0) ? 1 : 0);
			Animate(0f);
		}

		private void Align()
		{
			if ((bool)game && (bool)game.Trash)
			{
				((RectTransform)base.transform).anchoredPosition = new Vector2(48f, game.Trash.target.anchoredPosition.y);
			}
		}

		private void LateUpdate()
		{
			Align();
			Animate(Time.unscaledDeltaTime);
		}

		private void Animate(float dt)
		{
			hitAge += dt;
			float hit = 1f - Mathf.Clamp01(hitAge / 0.3f);
			float pop = Mathf.Sin(Mathf.Clamp01(hitAge / 0.3f) * 3.14159265f);
			number.rectTransform.localScale = Vector3.one * (1f + 0.2f * pop);
			number.rectTransform.anchoredPosition = new Vector2(negative ? (Mathf.Sin(hitAge * 48f) * 5f * hit) : 0f, -20f + pop * 3f);
			number.color = Color.Lerp(FindStyle.Ink, FindStyle.Hex(negative ? "ef3156" : "269f89"), hit);
		}
	}
}
