using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class CardShardGraphic : MaskableGraphic
	{
		private Vector2[] points;

		private Vector2[] uvs;

		private Texture texture;

		public override Texture mainTexture => texture ? texture : Texture2D.whiteTexture;

		public void Configure(Vector2[] vertices, Vector2[] coordinates, Texture source, Color tint)
		{
			points = vertices;
			uvs = coordinates;
			texture = source;
			color = tint;
			raycastTarget = false;
			SetAllDirty();
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			if (points != null)
			{
				for (int i = 0; i < points.Length; i++)
				{
					vh.AddVert(points[i], color, (uvs == null) ? Vector2.zero : uvs[i]);
				}
				for (int j = 1; j < points.Length - 1; j++)
				{
					vh.AddTriangle(0, j, j + 1);
				}
			}
		}
	}
}
