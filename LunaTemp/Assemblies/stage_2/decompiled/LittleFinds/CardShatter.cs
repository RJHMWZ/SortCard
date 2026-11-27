using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace LittleFinds
{
	public class CardShatter : MonoBehaviour
	{
		private class Piece
		{
			public RectTransform rect;

			public Vector2 origin;

			public Vector2 velocity;

			public float spin;
		}

		private readonly List<Piece> pieces = new List<Piece>();

		private CanvasGroup fade;

		public static CardShatter Spawn(CardController card, RectTransform layer)
		{
			GameObject root = new GameObject("Broken Card", typeof(RectTransform), typeof(CanvasGroup), typeof(CardShatter));
			RectTransform rootRect = (RectTransform)root.transform;
			rootRect.SetParent(layer, false);
			ResetRect(rootRect);
			rootRect.position = card.frontPaper.rectTransform.TransformPoint(card.frontPaper.rectTransform.rect.center);
			CardShatter effect = root.GetComponent<CardShatter>();
			effect.Build(card, layer);
			effect.StartCoroutine(effect.Animate());
			return effect;
		}

		private void Build(CardController card, RectTransform layer)
		{
			fade = GetComponent<CanvasGroup>();
			fade.blocksRaycasts = false;
			fade.interactable = false;
			layer = (RectTransform)base.transform;
			RectTransform paper = card.frontPaper.rectTransform;
			Rect bounds = paper.rect;
			Image image = card.illustration;
			Rect art = image.rectTransform.rect;
			if (image.preserveAspect && (bool)image.sprite)
			{
				float ratio = image.sprite.rect.width / image.sprite.rect.height;
				Vector2 size = art.size;
				if (size.x / size.y > ratio)
				{
					size.x = size.y * ratio;
				}
				else
				{
					size.y = size.x / ratio;
				}
				art = new Rect(art.center - size * 0.5f, size);
			}
			Vector2 a = paper.InverseTransformPoint(image.rectTransform.TransformPoint(art.min));
			Vector2 b = paper.InverseTransformPoint(image.rectTransform.TransformPoint(art.max));
			art = Rect.MinMaxRect(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y));
			Vector2[,] grid = new Vector2[4, 5];
			for (int y2 = 0; y2 <= 3; y2++)
			{
				for (int x = 0; x <= 4; x++)
				{
					float px = bounds.xMin + bounds.width * (float)x / 4f;
					float py = bounds.yMin + bounds.height * (float)y2 / 3f;
					if (x > 0 && x < 4)
					{
						px += Random.Range(-0.22f, 0.22f) * bounds.width / 4f;
					}
					if (y2 > 0 && y2 < 3)
					{
						py += Random.Range(-0.22f, 0.22f) * bounds.height / 3f;
					}
					grid[y2, x] = new Vector2(px, py);
				}
			}
			for (int y = 0; y < 3; y++)
			{
				for (int x2 = 0; x2 < 4; x2++)
				{
					Vector2 p = grid[y, x2];
					Vector2 q = grid[y, x2 + 1];
					Vector2 r = grid[y + 1, x2 + 1];
					Vector2 s = grid[y + 1, x2];
					if ((x2 + y) % 2 == 0)
					{
						AddPiece(new Vector2[3] { p, q, r }, card, layer, paper, art);
						AddPiece(new Vector2[3] { p, r, s }, card, layer, paper, art);
					}
					else
					{
						AddPiece(new Vector2[3] { p, q, s }, card, layer, paper, art);
						AddPiece(new Vector2[3] { q, r, s }, card, layer, paper, art);
					}
				}
			}
		}

		private void AddPiece(Vector2[] triangle, CardController card, RectTransform layer, RectTransform paper, Rect art)
		{
			Vector2 origin = layer.InverseTransformPoint(paper.TransformPoint((triangle[0] + triangle[1] + triangle[2]) / 3f));
			GameObject obj = new GameObject("Paper Shard", typeof(RectTransform), typeof(CanvasRenderer), typeof(CardShardGraphic), typeof(Outline));
			RectTransform rt = obj.GetComponent<RectTransform>();
			rt.SetParent(base.transform, false);
			ResetRect(rt);
			rt.localPosition = origin;
			obj.GetComponent<CardShardGraphic>().Configure(ToLocal(triangle, layer, paper, origin), null, null, card.frontPaper.color);
			Outline outline = obj.GetComponent<Outline>();
			outline.effectColor = FindStyle.Ink;
			outline.effectDistance = new Vector2(1f, -1f);
			List<Vector2> clipped = new List<Vector2>(triangle);
			clipped = Clip(clipped, 0, art.xMin, true);
			clipped = Clip(clipped, 0, art.xMax, false);
			clipped = Clip(clipped, 1, art.yMin, true);
			clipped = Clip(clipped, 1, art.yMax, false);
			Sprite sprite = card.illustration.sprite;
			if (clipped.Count >= 3 && (bool)sprite)
			{
				Vector4 uv = DataUtility.GetOuterUV(sprite);
				Vector2[] coordinates = new Vector2[clipped.Count];
				for (int i = 0; i < coordinates.Length; i++)
				{
					coordinates[i] = new Vector2(Mathf.Lerp(uv.x, uv.z, (clipped[i].x - art.xMin) / art.width), Mathf.Lerp(uv.y, uv.w, (clipped[i].y - art.yMin) / art.height));
				}
				GameObject picture = new GameObject("Artwork Fragment", typeof(RectTransform), typeof(CanvasRenderer), typeof(CardShardGraphic));
				picture.transform.SetParent(rt, false);
				ResetRect((RectTransform)picture.transform);
				picture.GetComponent<CardShardGraphic>().Configure(ToLocal(clipped.ToArray(), layer, paper, origin), coordinates, sprite.texture, card.illustration.color);
			}
			Vector2 center = layer.InverseTransformPoint(paper.TransformPoint(paper.rect.center));
			Vector2 direction = (origin - center).normalized;
			if (direction.sqrMagnitude < 0.01f)
			{
				direction = Vector2.up;
			}
			pieces.Add(new Piece
			{
				rect = rt,
				origin = origin,
				velocity = direction * Random.Range(130f, 310f) + Vector2.up * Random.Range(60f, 140f),
				spin = Random.Range(-360f, 360f)
			});
		}

		private static void ResetRect(RectTransform rect)
		{
			Vector2 vector2 = (rect.pivot = new Vector2(0.5f, 0.5f));
			Vector2 vector5 = (rect.anchorMin = (rect.anchorMax = vector2));
			rect.sizeDelta = Vector2.zero;
			rect.localScale = Vector3.one;
			rect.localRotation = Quaternion.identity;
			rect.localPosition = Vector3.zero;
		}

		private static Vector2[] ToLocal(Vector2[] polygon, RectTransform layer, RectTransform paper, Vector2 origin)
		{
			Vector2[] result = new Vector2[polygon.Length];
			for (int i = 0; i < result.Length; i++)
			{
				result[i] = (Vector2)layer.InverseTransformPoint(paper.TransformPoint(polygon[i])) - origin;
			}
			return result;
		}

		private static List<Vector2> Clip(List<Vector2> input, int axis, float edge, bool above)
		{
			List<Vector2> output = new List<Vector2>();
			if (input.Count == 0)
			{
				return output;
			}
			Vector2 prev = input[input.Count - 1];
			bool prevInside = (above ? (prev[axis] >= edge) : (prev[axis] <= edge));
			foreach (Vector2 point in input)
			{
				bool inside = (above ? (point[axis] >= edge) : (point[axis] <= edge));
				if (inside != prevInside)
				{
					output.Add(Vector2.Lerp(prev, point, (edge - prev[axis]) / (point[axis] - prev[axis])));
				}
				if (inside)
				{
					output.Add(point);
				}
				prev = point;
				prevInside = inside;
			}
			return output;
		}

		private IEnumerator Animate()
		{
			for (float t = 0f; t < 0.65f; t += Time.deltaTime)
			{
				float u = t / 0.65f;
				fade.alpha = 1f - Mathf.SmoothStep(0.3f, 1f, u);
				foreach (Piece piece in pieces)
				{
					piece.rect.localPosition = piece.origin + piece.velocity * t + Vector2.down * (450f * t * t);
					piece.rect.localRotation = Quaternion.Euler(0f, 0f, piece.spin * t);
					piece.rect.localScale = Vector3.one * Mathf.Lerp(1f, 0.65f, u);
				}
				yield return null;
			}
			Object.Destroy(base.gameObject);
		}
	}
}
