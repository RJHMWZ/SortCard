using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	[ExecuteAlways]
	public class SafeArea : MonoBehaviour
	{
		public RectTransform content;

		private Rect previous;

		private Vector2 size;

		private RectMask2D landscapeClip;

		private Rect Viewport => new Rect(0f, 0f, Screen.width, Screen.height);

		private void OnEnable()
		{
			Apply();
		}

		private void Update()
		{
			if (previous != Viewport || size != new Vector2(Screen.width, Screen.height))
			{
				Apply();
			}
		}

		public void Apply()
		{
			if (!content || Screen.width == 0 || Screen.height == 0)
			{
				return;
			}
			previous = Viewport;
			size = new Vector2(Screen.width, Screen.height);
			Rect frame = previous;
			bool landscape = Screen.width > Screen.height;
			if (landscape)
			{
				float width = Mathf.Min(frame.width, frame.height * 9f / 16f);
				frame = new Rect(frame.center.x - width * 0.5f, frame.y, width, frame.height);
			}
			RectTransform root = (RectTransform)base.transform;
			root.anchorMin = new Vector2(frame.xMin / (float)Screen.width, frame.yMin / (float)Screen.height);
			root.anchorMax = new Vector2(frame.xMax / (float)Screen.width, frame.yMax / (float)Screen.height);
			Vector2 vector2 = (root.offsetMin = (root.offsetMax = Vector2.zero));
			if (!landscapeClip)
			{
				landscapeClip = GetComponent<RectMask2D>();
			}
			if (landscape && !landscapeClip)
			{
				landscapeClip = base.gameObject.AddComponent<RectMask2D>();
			}
			if ((bool)landscapeClip)
			{
				landscapeClip.enabled = landscape;
			}
			Canvas.ForceUpdateCanvases();
			float scale = root.rect.width / 1080f;
			if (!(scale <= 0f))
			{
				RectTransform rectTransform = content;
				RectTransform rectTransform2 = content;
				Vector2 vector4 = (content.pivot = new Vector2(0.5f, landscape ? 0.5f : 1f));
				vector2 = (rectTransform.anchorMin = (rectTransform2.anchorMax = vector4));
				content.localScale = Vector3.one * scale;
				content.sizeDelta = new Vector2(1020f, root.rect.height / scale - 40f);
				content.anchoredPosition = (landscape ? Vector2.zero : new Vector2(0f, -20f));
				MobileGameLayout layout = content.GetComponent<MobileGameLayout>();
				if ((bool)layout)
				{
					layout.Reflow();
				}
			}
		}
	}
}
