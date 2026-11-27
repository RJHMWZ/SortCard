using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class WarningStripes : MaskableGraphic
	{
		private float phase;

		public float Phase
		{
			get
			{
				return phase;
			}
			set
			{
				phase = value;
				SetVerticesDirty();
			}
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			Rect r = base.rectTransform.rect;
			float offset = Mathf.Repeat(phase, 36f);
			for (float x = r.xMin - 72f + offset; x < r.xMax + 36f; x += 36f)
			{
				int i = vh.currentVertCount;
				float left = Mathf.Clamp(x, r.xMin, r.xMax);
				float right = Mathf.Clamp(x + 13f, r.xMin, r.xMax);
				float upperLeft = Mathf.Clamp(x + 16f, r.xMin, r.xMax);
				float upperRight = Mathf.Clamp(x + 29f, r.xMin, r.xMax);
				vh.AddVert(new Vector2(left, r.yMin + 2f), color, Vector2.zero);
				vh.AddVert(new Vector2(upperLeft, r.yMax - 2f), color, Vector2.zero);
				vh.AddVert(new Vector2(upperRight, r.yMax - 2f), color, Vector2.zero);
				vh.AddVert(new Vector2(right, r.yMin + 2f), color, Vector2.zero);
				vh.AddTriangle(i, i + 1, i + 2);
				vh.AddTriangle(i, i + 2, i + 3);
			}
		}
	}
}
