using System;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class ComboHUD : MonoBehaviour
	{
		private GameManager game;

		private Image fill;

		private Image track;

		private Image tip;

		private WarningStripes stripes;

		private float phase;

		private float lostAt = -10f;

		private float barStart = 280f;

		private float barWidth = 700f;

		private bool wasBreaking;

		private Text caption;

		private Text level;

		private RectTransform captionRect;

		private float lastHit = -10f;

		private int previous;

		private int displayedLevel = -1;

		private bool displayedLesson;

		private bool first = true;

		public int UrgencyStage { get; private set; }

		public static ComboHUD Create(GameManager game)
		{
			GameObject root = new GameObject("Combo HUD", typeof(RectTransform), typeof(ComboHUD));
			RectTransform rt = (RectTransform)root.transform;
			rt.SetParent(game.Feedback.layer.parent, false);
			Vector2 vector2 = (rt.pivot = new Vector2(0.5f, 1f));
			Vector2 vector5 = (rt.anchorMin = (rt.anchorMax = vector2));
			rt.anchoredPosition = new Vector2(0f, -396f);
			rt.sizeDelta = new Vector2(980f, 64f);
			root.transform.SetSiblingIndex(game.Feedback.layer.GetSiblingIndex());
			ComboHUD hud = root.GetComponent<ComboHUD>();
			hud.game = game;
			hud.caption = Label(game.Feedback.font, rt, "Combo Label", "COMBO", 52, new Vector2(264f, 104f));
			hud.caption.alignment = TextAnchor.MiddleLeft;
			hud.caption.horizontalOverflow = HorizontalWrapMode.Overflow;
			hud.captionRect = hud.caption.rectTransform;
			RectTransform rectTransform = hud.captionRect;
			RectTransform rectTransform2 = hud.captionRect;
			vector2 = (hud.captionRect.pivot = new Vector2(0f, 0.5f));
			vector5 = (rectTransform.anchorMin = (rectTransform2.anchorMax = vector2));
			hud.captionRect.anchoredPosition = new Vector2(0f, -32f);
			hud.track = Bar(rt, "Combo Track", game.Feedback.round, FindStyle.Hex("d6d9e8"));
			RectTransform bar = hud.track.rectTransform;
			vector2 = (bar.pivot = new Vector2(0f, 0.5f));
			vector5 = (bar.anchorMin = (bar.anchorMax = vector2));
			bar.anchoredPosition = new Vector2(280f, -32f);
			bar.sizeDelta = new Vector2(700f, 26f);
			hud.fill = Bar(bar, "Combo Time", game.Feedback.round, FindStyle.Hex("ffb238"));
			RectTransform fr = hud.fill.rectTransform;
			fr.anchorMin = Vector2.zero;
			fr.anchorMax = Vector2.one;
			vector5 = (fr.offsetMin = (fr.offsetMax = Vector2.zero));
			GameObject pattern = new GameObject("Combo Warning Stripes", typeof(RectTransform), typeof(CanvasRenderer), typeof(WarningStripes));
			RectTransform pr = (RectTransform)pattern.transform;
			pr.SetParent(fr, false);
			pr.anchorMin = Vector2.zero;
			pr.anchorMax = Vector2.one;
			vector5 = (pr.offsetMin = (pr.offsetMax = Vector2.zero));
			hud.stripes = pattern.GetComponent<WarningStripes>();
			hud.stripes.raycastTarget = false;
			pattern.SetActive(false);
			hud.tip = Bar(bar, "Combo Burning Tip", game.Feedback.round, Color.white);
			RectTransform tr = hud.tip.rectTransform;
			vector5 = (tr.anchorMin = (tr.anchorMax = new Vector2(0f, 0.5f)));
			tr.pivot = new Vector2(0.5f, 0.5f);
			tr.sizeDelta = new Vector2(12f, 22f);
			hud.tip.gameObject.SetActive(false);
			hud.level = Label(game.Feedback.font, rt.parent, "Level Indicator", "LEVEL 1", 68, new Vector2(310f, 108f));
			RectTransform lr = hud.level.rectTransform;
			vector2 = (lr.pivot = new Vector2(1f, 1f));
			vector5 = (lr.anchorMin = (lr.anchorMax = vector2));
			lr.anchoredPosition = new Vector2(0f, -12f);
			return hud;
		}

		private static Text Label(Font font, Transform parent, string name, string value, int size, Vector2 dimensions)
		{
			GameObject o = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
			o.transform.SetParent(parent, false);
			Text t = o.GetComponent<Text>();
			t.font = font;
			t.text = value;
			t.fontSize = size;
			t.fontStyle = FontStyle.Normal;
			t.alignment = TextAnchor.MiddleCenter;
			t.color = FindStyle.Ink;
			t.raycastTarget = false;
			t.rectTransform.sizeDelta = dimensions;
			return t;
		}

		private static Image Bar(Transform parent, string name, Sprite sprite, Color color)
		{
			GameObject o = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
			o.transform.SetParent(parent, false);
			Image i = o.GetComponent<Image>();
			i.sprite = sprite;
			i.type = Image.Type.Sliced;
			i.color = color;
			i.raycastTarget = false;
			return i;
		}

		private void Update()
		{
			Render(Time.unscaledDeltaTime);
		}

		public void RefreshNow()
		{
			Render(0f);
		}

		private void Render(float delta)
		{
			bool lesson = (bool)game.Tutorial && game.Tutorial.InLesson;
			if (first || displayedLevel != game.LevelIndex || displayedLesson != lesson)
			{
				level.text = (lesson ? "PRACTICE" : ("LEVEL " + (game.LevelIndex + 1)));
				displayedLevel = game.LevelIndex;
				displayedLesson = lesson;
			}
			int combo = game.Streak;
			if (!first && previous > 0 && combo == 0 && game.State == GameState.Playing && !game.UI.overlay.activeSelf)
			{
				lostAt = Time.unscaledTime;
			}
			bool breaking = combo == 0 && Time.unscaledTime - lostAt < 0.5f;
			if (first || combo != previous || breaking != wasBreaking)
			{
				if (combo > previous)
				{
					lastHit = Time.unscaledTime;
				}
				caption.text = (breaking ? "BREAK!" : ((combo > 0) ? ("COMBO x" + combo) : "COMBO"));
				caption.fontSize = 52 + Mathf.RoundToInt(24f * Mathf.Clamp01((float)(combo - 1) / 11f));
				barStart = Mathf.Max(280f, caption.preferredWidth * 1.25f + 36f);
				barWidth = 980f - barStart;
				captionRect.sizeDelta = new Vector2(Mathf.Max(264f, caption.preferredWidth + 16f), 104f);
				track.rectTransform.sizeDelta = new Vector2(barWidth, 26f);
				previous = combo;
				wasBreaking = breaking;
			}
			first = false;
			float fraction = game.ComboFraction;
			bool active = combo > 0;
			UrgencyStage = (active ? ((fraction <= 0.25f) ? 2 : ((fraction <= 0.5f) ? 1 : 0)) : 0);
			bool ticking = active && game.State == GameState.Playing && !game.UI.overlay.activeSelf;
			float pressure = (active ? Mathf.Clamp01(1f - fraction * 2f) : 0f);
			if (ticking)
			{
				phase += delta * Mathf.Lerp(1.5f, 5f, pressure) * 3.14159265f * 2f;
			}
			float beat = ((ticking && UrgencyStage > 0) ? Mathf.Pow(Mathf.Max(0f, Mathf.Sin(phase)), 3f) : 0f);
			Color normal = FindStyle.Hex((combo >= 8) ? "38aef2" : "31bfa2");
			Color amber = FindStyle.Hex("ffa52e");
			Color red = FindStyle.Hex("ef3156");
			Color activeColor = ((UrgencyStage == 2) ? red : ((UrgencyStage == 1) ? amber : normal));
			fill.color = Color.Lerp(activeColor, Color.white, beat * 0.32f);
			caption.color = (breaking ? red : (active ? activeColor : FindStyle.Soft));
			track.color = Color.Lerp(FindStyle.Hex("d6d9e8"), activeColor, (active && UrgencyStage > 0) ? (0.16f + beat * 0.28f) : 0f);
			fill.rectTransform.anchorMax = new Vector2(fraction, 1f);
			track.rectTransform.localScale = new Vector3(1f, 1f + beat * ((UrgencyStage == 2) ? 0.38f : 0.16f), 1f);
			track.rectTransform.anchoredPosition = new Vector2(barStart + ((ticking && UrgencyStage == 2) ? (Mathf.Sin(phase * 2f) * 2.5f * pressure) : 0f), -32f);
			stripes.gameObject.SetActive(active && UrgencyStage > 0);
			stripes.Phase = phase * 7f;
			stripes.color = new Color(0.145f, 0.169f, 0.294f, 0.18f + beat * 0.14f);
			tip.gameObject.SetActive(active && UrgencyStage > 0);
			tip.rectTransform.anchoredPosition = new Vector2(barWidth * fraction, 0f);
			tip.rectTransform.localScale = Vector3.one * (1f + beat * 0.35f);
			tip.color = Color.Lerp(Color.white, FindStyle.Hex("ffe9b0"), beat);
			float hit = Mathf.Clamp01(1f - (Time.unscaledTime - lastHit) / 0.23f);
			captionRect.localScale = Vector3.one * (1f + hit * 0.18f + beat * 0.055f);
			captionRect.anchoredPosition = new Vector2((ticking && UrgencyStage == 2) ? (Mathf.Sin(phase * 1.5f) * 2f) : 0f, -32f);
		}
	}
}
