using System;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class ResultSticker : MaskableGraphic
	{
		public Text heading;

		public Text kicker;

		public bool Won { get; private set; }

		public static ResultSticker Create(Transform parent, Font font)
		{
			GameObject o = new GameObject("Result Sticker", typeof(RectTransform), typeof(CanvasRenderer), typeof(ResultSticker));
			RectTransform r = (RectTransform)o.transform;
			r.SetParent(parent, false);
			Vector2 vector3 = (r.anchorMin = (r.anchorMax = new Vector2(0.5f, 1f)));
			r.pivot = new Vector2(0.5f, 0.5f);
			r.sizeDelta = new Vector2(760f, 360f);
			r.anchoredPosition = new Vector2(0f, -220f);
			ResultSticker card = o.GetComponent<ResultSticker>();
			card.raycastTarget = false;
			card.kicker = Label(r, font, "Result Level", 44, new Vector2(52f, -28f), new Vector2(650f, 70f));
			card.heading = Label(r, font, "Result Word", 104, new Vector2(52f, -100f), new Vector2(656f, 182f));
			return card;
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
			Text text = o.GetComponent<Text>();
			text.font = font;
			text.fontSize = size;
			text.fontStyle = FontStyle.Normal;
			text.color = FindStyle.Ink;
			text.alignment = TextAnchor.MiddleLeft;
			text.raycastTarget = false;
			return text;
		}

		public void Configure(bool won, int level, string word)
		{
			Won = won;
			kicker.text = "LEVEL " + level;
			heading.text = word;
			color = FindStyle.Hex(won ? "ffe15a" : "ffb5c6");
			SetVerticesDirty();
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			Rect r = base.rectTransform.rect;
			Vector2[] shape = new Vector2[5]
			{
				new Vector2(0.025f, 0.04f),
				new Vector2(0f, 0.91f),
				new Vector2(0.9f, 1f),
				new Vector2(1f, 0.78f),
				new Vector2(0.97f, 0f)
			};
			Polygon(vh, r, shape, 1f, new Vector2(10f, -14f), new Color(0.145f, 0.169f, 0.294f, 0.28f));
			Polygon(vh, r, shape, 1f, Vector2.zero, FindStyle.Ink);
			Polygon(vh, r, shape, 0.973f, Vector2.zero, color);
			Color dot = FindStyle.Ink;
			dot.a = 0.13f;
			for (int row = 0; row < 4; row++)
			{
				for (int col = 0; col < 5 - row; col++)
				{
					Circle(vh, new Vector2(r.xMax - 38f - (float)(col * 19), r.yMin + 24f + (float)(row * 19)), 3.5f, dot);
				}
			}
			int i = vh.currentVertCount;
			vh.AddVert(r.min + Vector2.Scale(new Vector2(0.9f, 1f), r.size), FindStyle.Ink, Vector2.zero);
			vh.AddVert(r.min + Vector2.Scale(new Vector2(0.885f, 0.77f), r.size), FindStyle.Ink, Vector2.zero);
			vh.AddVert(r.min + Vector2.Scale(new Vector2(1f, 0.78f), r.size), FindStyle.Ink, Vector2.zero);
			vh.AddTriangle(i, i + 1, i + 2);
		}

		private static void Polygon(VertexHelper vh, Rect r, Vector2[] shape, float scale, Vector2 offset, Color tint)
		{
			int j = vh.currentVertCount;
			vh.AddVert(r.center + offset, tint, Vector2.zero);
			foreach (Vector2 p in shape)
			{
				vh.AddVert(r.center + (r.min + Vector2.Scale(p, r.size) - r.center) * scale + offset, tint, Vector2.zero);
			}
			for (int i = 0; i < shape.Length; i++)
			{
				vh.AddTriangle(j, j + i + 1, j + (i + 1) % shape.Length + 1);
			}
		}

		private static void Circle(VertexHelper vh, Vector2 center, float radius, Color tint)
		{
			int k = vh.currentVertCount;
			vh.AddVert(center, tint, Vector2.zero);
			for (int j = 0; j < 8; j++)
			{
				float a = (float)j * 3.14159265f / 4f;
				vh.AddVert(center + new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * radius, tint, Vector2.zero);
			}
			for (int i = 0; i < 8; i++)
			{
				vh.AddTriangle(k, k + 1 + i, k + 1 + (i + 1) % 8);
			}
		}
	}
}
