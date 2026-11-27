using System;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class TrashCanGraphic : MaskableGraphic
	{
		private float lidOpen;

		public float LidOpen
		{
			get
			{
				return lidOpen;
			}
			set
			{
				float next = Mathf.Clamp01(value);
				if (!(Mathf.Abs(next - lidOpen) < 0.001f))
				{
					lidOpen = next;
					SetVerticesDirty();
				}
			}
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			Rect r = base.rectTransform.rect;
			Quad(vh, r, new Vector2(0.23f, 0.14f), new Vector2(0.17f, 0.72f), new Vector2(0.83f, 0.72f), new Vector2(0.77f, 0.14f), color);
			Color paper = Color.Lerp(Color.white, color, 0.12f);
			Quad(vh, r, new Vector2(0.29f, 0.21f), new Vector2(0.24f, 0.65f), new Vector2(0.76f, 0.65f), new Vector2(0.71f, 0.21f), paper);
			LidBar(vh, r, 0.12f, 0.75f, 0.76f, 0.08f);
			LidBar(vh, r, 0.36f, 0.87f, 0.28f, 0.07f);
			LidBar(vh, r, 0.32f, 0.88f, 0.07f, -0.1f);
			LidBar(vh, r, 0.61f, 0.88f, 0.07f, -0.1f);
			Bar(vh, r, 0.38f, 0.29f, 0.05f, 0.28f, color);
			Bar(vh, r, 0.57f, 0.29f, 0.05f, 0.28f, color);
		}

		private void LidBar(VertexHelper vh, Rect r, float x, float y, float w, float h)
		{
			Quad(vh, r, LidPoint(new Vector2(x, y)), LidPoint(new Vector2(x, y + h)), LidPoint(new Vector2(x + w, y + h)), LidPoint(new Vector2(x + w, y)), color);
		}

		private Vector2 LidPoint(Vector2 p)
		{
			Vector2 hinge = new Vector2(0.84f, 0.76f);
			float angle = (0f - lidOpen) * 45f * (3.14159265f / 180f);
			Vector2 d = p - hinge;
			return hinge + new Vector2(d.x * Mathf.Cos(angle) - d.y * Mathf.Sin(angle), d.x * Mathf.Sin(angle) + d.y * Mathf.Cos(angle)) + Vector2.up * (lidOpen * 0.025f);
		}

		private static void Bar(VertexHelper vh, Rect r, float x, float y, float w, float h, Color c)
		{
			Quad(vh, r, new Vector2(x, y), new Vector2(x, y + h), new Vector2(x + w, y + h), new Vector2(x + w, y), c);
		}

		private static void Quad(VertexHelper vh, Rect r, Vector2 a, Vector2 b, Vector2 c, Vector2 d, Color tint)
		{
			int i = vh.currentVertCount;
			Vector2[] array = new Vector2[4] { a, b, c, d };
			foreach (Vector2 p in array)
			{
				vh.AddVert(r.min + Vector2.Scale(p, r.size), tint, Vector2.zero);
			}
			vh.AddTriangle(i, i + 1, i + 2);
			vh.AddTriangle(i, i + 2, i + 3);
		}
	}
}
