using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class ComicVictory : MaskableGraphic
	{
		private Vector2 focus;

		private float age;

		private const float Duration = 1.65f;

		private static readonly Color Ink = FindStyle.Hex("252b4b");

		private static readonly Color Gold = FindStyle.Hex("ffda46");

		private static readonly Color Blue = FindStyle.Hex("79e2f2");

		public bool Playing { get; private set; }

		public static ComicVictory Create(RectTransform parent)
		{
			GameObject o = new GameObject("Comic Victory FX", typeof(RectTransform), typeof(CanvasRenderer), typeof(ComicVictory));
			RectTransform r = (RectTransform)o.transform;
			r.SetParent(parent, false);
			r.anchorMin = Vector2.zero;
			r.anchorMax = Vector2.one;
			Vector2 vector2 = (r.offsetMin = (r.offsetMax = Vector2.zero));
			r.pivot = parent.pivot;
			ComicVictory fx = o.GetComponent<ComicVictory>();
			fx.raycastTarget = false;
			o.SetActive(false);
			return fx;
		}

		public void Play(Vector3 world)
		{
			StopAllCoroutines();
			base.gameObject.SetActive(true);
			base.transform.SetAsLastSibling();
			focus = base.rectTransform.InverseTransformPoint(world);
			age = 0f;
			Playing = true;
			SetVerticesDirty();
			StartCoroutine(Animate());
		}

		public void Cancel()
		{
			StopAllCoroutines();
			Playing = false;
			base.gameObject.SetActive(false);
		}

		private IEnumerator Animate()
		{
			for (age = 0f; age < 1.65f; age += Time.unscaledDeltaTime)
			{
				SetVerticesDirty();
				yield return null;
			}
			Playing = false;
			base.gameObject.SetActive(false);
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			float t = age;
			float rays = Fade(0.35f, 1.25f, t);
			if (t < 0.16f)
			{
				Color flash = Color.white;
				flash.a = 0.3f * (1f - t / 0.16f);
				Rect r = base.rectTransform.rect;
				Quad(vh, new Vector2(r.xMin, r.yMin), new Vector2(r.xMin, r.yMax), new Vector2(r.xMax, r.yMax), new Vector2(r.xMax, r.yMin), flash);
			}
			for (int i = 0; i < 28; i++)
			{
				float a = (float)i * 3.14159265f * 2f / 28f + 0.07f;
				float move = Mathf.Clamp01(t / 0.7f);
				float inner = Mathf.Lerp(190f, 510f, move) + (float)(i % 3 * 34);
				float outer = 1250f;
				Color c = ((i % 4 == 0) ? Blue : ((i % 3 == 0) ? Gold : Ink));
				c.a = rays * ((i % 3 == 0) ? 0.54f : 0.32f);
				Triangle(vh, At(a, inner), At(a - 0.012f - (float)(i % 2) * 0.009f, outer), At(a + 0.012f + (float)(i % 2) * 0.009f, outer), c);
			}
			float ring = Mathf.Clamp01(t / 0.65f);
			if (t < 0.65f)
			{
				Ring(vh, focus, Mathf.Lerp(40f, 690f, FindStyle.Ease(ring)), Mathf.Lerp(18f, 4f, ring), Tint(Ink, (1f - ring) * 0.8f));
			}
			float grow = ((t < 0.19f) ? Mathf.Lerp(0.12f, 1.12f, FindStyle.Ease(t / 0.19f)) : Mathf.Lerp(1.12f, 1f, Mathf.Clamp01((t - 0.19f) / 0.24f)));
			float fade = Fade(0.65f, 1.45f, t);
			Burst(vh, focus + new Vector2(12f, -14f), new Vector2(470f, 320f) * grow, 0.08f, Tint(Ink, fade * 0.24f));
			Burst(vh, focus, new Vector2(470f, 320f) * grow, 0.08f, Tint(Ink, fade));
			Burst(vh, focus, new Vector2(457f, 307f) * grow, 0.08f, Tint(Blue, fade));
			Burst(vh, focus, new Vector2(430f, 285f) * grow, -0.03f, Tint(Ink, fade));
			Burst(vh, focus, new Vector2(417f, 272f) * grow, -0.03f, Tint(Gold, fade));
			for (int j = 0; j < 8; j++)
			{
				float delay = (float)(j % 3) * 0.035f;
				float u = Mathf.Clamp01((t - 0.1f - delay) / 0.85f);
				if (!(u <= 0f))
				{
					float a2 = (float)j * 3.14159265f * 2f / 8f + 0.35f;
					Vector2 p = focus + new Vector2(Mathf.Cos(a2) * 500f, Mathf.Sin(a2) * 360f) * Mathf.Lerp(0.45f, 1.12f, FindStyle.Ease(u));
					float size = (float)((j % 2 == 0) ? 36 : 24) * Mathf.Sin(Mathf.Min(1f, u * 2f) * 3.14159265f * 0.5f);
					float alpha = Fade(1f, 1.6f, t);
					Star(vh, p, size + 5f, a2 + u * 0.5f, Tint(Ink, alpha));
					Star(vh, p, size, a2 + u * 0.5f, Tint((j % 2 == 0) ? Gold : Color.white, alpha));
				}
			}
			float dots = Fade(0.7f, 1.25f, t);
			for (int side = -1; side <= 1; side += 2)
			{
				for (int row = 0; row < 5; row++)
				{
					for (int col = 0; col < 6 - row; col++)
					{
						Vector2 p2 = focus + new Vector2(side * (300 + col * 24), 185 + row * 24);
						Disc(vh, p2, 4f, Tint(Ink, dots * 0.28f));
					}
				}
			}
		}

		private Vector2 At(float angle, float radius)
		{
			return focus + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
		}

		private static float Fade(float start, float end, float time)
		{
			return 1f - Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(start, end, time));
		}

		private static Color Tint(Color c, float alpha)
		{
			c.a = alpha;
			return c;
		}

		private static void Triangle(VertexHelper vh, Vector2 a, Vector2 b, Vector2 c, Color tint)
		{
			int i = vh.currentVertCount;
			vh.AddVert(a, tint, Vector2.zero);
			vh.AddVert(b, tint, Vector2.zero);
			vh.AddVert(c, tint, Vector2.zero);
			vh.AddTriangle(i, i + 1, i + 2);
		}

		private static void Quad(VertexHelper vh, Vector2 a, Vector2 b, Vector2 c, Vector2 d, Color tint)
		{
			Triangle(vh, a, b, c, tint);
			Triangle(vh, a, c, d, tint);
		}

		private static void Burst(VertexHelper vh, Vector2 center, Vector2 size, float angle, Color tint)
		{
			for (int i = 0; i < 24; i++)
			{
				float a = (float)i * 3.14159265f * 2f / 24f + angle;
				float b = (float)(i + 1) * 3.14159265f * 2f / 24f + angle;
				Vector2 p = center + Vector2.Scale(new Vector2(Mathf.Cos(a), Mathf.Sin(a)), size) * ((i % 2 == 0) ? 1f : 0.73f);
				Vector2 q = center + Vector2.Scale(new Vector2(Mathf.Cos(b), Mathf.Sin(b)), size) * (((i + 1) % 2 == 0) ? 1f : 0.73f);
				Triangle(vh, center, p, q, tint);
			}
		}

		private static void Star(VertexHelper vh, Vector2 center, float radius, float angle, Color tint)
		{
			for (int i = 0; i < 10; i++)
			{
				float a = (float)i * 3.14159265f / 5f + angle;
				float b = (float)(i + 1) * 3.14159265f / 5f + angle;
				Triangle(vh, center, center + new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * radius * ((i % 2 == 0) ? 1f : 0.44f), center + new Vector2(Mathf.Cos(b), Mathf.Sin(b)) * radius * (((i + 1) % 2 == 0) ? 1f : 0.44f), tint);
			}
		}

		private static void Disc(VertexHelper vh, Vector2 center, float radius, Color tint)
		{
			for (int i = 0; i < 8; i++)
			{
				Triangle(vh, center, center + new Vector2(Mathf.Cos((float)i * 3.14159265f / 4f), Mathf.Sin((float)i * 3.14159265f / 4f)) * radius, center + new Vector2(Mathf.Cos((float)(i + 1) * 3.14159265f / 4f), Mathf.Sin((float)(i + 1) * 3.14159265f / 4f)) * radius, tint);
			}
		}

		private static void Ring(VertexHelper vh, Vector2 center, float radius, float width, Color tint)
		{
			for (int i = 0; i < 48; i++)
			{
				float a = (float)i * 3.14159265f / 24f;
				float b = (float)(i + 1) * 3.14159265f / 24f;
				Vector2 p = new Vector2(Mathf.Cos(a), Mathf.Sin(a));
				Vector2 q = new Vector2(Mathf.Cos(b), Mathf.Sin(b));
				Quad(vh, center + p * (radius - width), center + p * radius, center + q * radius, center + q * (radius - width), tint);
			}
		}
	}
}
