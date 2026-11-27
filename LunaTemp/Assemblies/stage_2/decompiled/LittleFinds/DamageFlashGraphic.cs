using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class DamageFlashGraphic : MaskableGraphic
	{
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			Rect r = base.rectTransform.rect;
			float inset = Mathf.Min(r.width, r.height) * 0.18f;
			Vector2[] outer = new Vector2[4]
			{
				new Vector2(r.xMin, r.yMin),
				new Vector2(r.xMin, r.yMax),
				new Vector2(r.xMax, r.yMax),
				new Vector2(r.xMax, r.yMin)
			};
			Vector2[] inner = new Vector2[4]
			{
				new Vector2(r.xMin + inset, r.yMin + inset),
				new Vector2(r.xMin + inset, r.yMax - inset),
				new Vector2(r.xMax - inset, r.yMax - inset),
				new Vector2(r.xMax - inset, r.yMin + inset)
			};
			Color clear = color;
			clear.a = 0f;
			for (int k = 0; k < 4; k++)
			{
				vh.AddVert(outer[k], color, Vector2.zero);
			}
			for (int j = 0; j < 4; j++)
			{
				vh.AddVert(inner[j], clear, Vector2.zero);
			}
			for (int i = 0; i < 4; i++)
			{
				int l = (i + 1) % 4;
				vh.AddTriangle(i, l, i + 4);
				vh.AddTriangle(l, l + 4, i + 4);
			}
		}
	}
}
