using UnityEngine;

namespace LittleFinds
{
	[ExecuteAlways]
	public class MobileGameLayout : MonoBehaviour
	{
		public RectTransform board;

		public RectTransform grid;

		public RectTransform footer;

		public RectTransform hint;

		public RectTransform effects;

		public RectTransform trash;

		public TutorialController tutorial;

		public RectTransform[] slots;

		private Vector2 lastSize;

		private void OnEnable()
		{
			Reflow();
		}

		private void Update()
		{
			if (lastSize != ((RectTransform)base.transform).rect.size)
			{
				Reflow();
			}
		}

		public void Reflow()
		{
			if (!board || !grid || !footer || slots == null || slots.Length != 12)
			{
				return;
			}
			lastSize = ((RectTransform)base.transform).rect.size;
			float footerTop = lastSize.y - 142f;
			footer.anchoredPosition = new Vector2(0f, 0f - footerTop);
			float areaTop = (((bool)tutorial && tutorial.InLesson) ? 858 : 650);
			float maxHeight = footerTop - areaTop - 40f;
			float cardHeight = Mathf.Min((maxHeight - 72f - 40f) / 3f, 284.5903f);
			float cardWidth = cardHeight * 227f / 291f;
			float gridHeight = cardHeight * 3f + 40f;
			float boardHeight = gridHeight + 72f;
			float boardTop = areaTop + Mathf.Max(0f, (footerTop - 40f - areaTop - boardHeight) * 0.5f);
			if ((bool)trash)
			{
				float gapEnd = (((bool)tutorial && tutorial.InLesson) ? (boardTop - 160f) : boardTop);
				trash.anchoredPosition = new Vector2(0f, (0f - (466f + gapEnd)) * 0.5f);
			}
			board.anchoredPosition = new Vector2(0f, 0f - boardTop);
			board.sizeDelta = new Vector2(1020f, boardHeight);
			grid.anchoredPosition = new Vector2(36f, -36f);
			grid.sizeDelta = new Vector2(948f, gridHeight);
			float columnGap = (948f - cardWidth * 4f) / 3f;
			for (int i = 0; i < 12; i++)
			{
				if ((bool)slots[i])
				{
					slots[i].anchoredPosition = new Vector2((float)(i % 4) * (cardWidth + columnGap), (float)(-(i / 4)) * (cardHeight + 20f));
					slots[i].localScale = Vector3.one * (cardHeight / 291f);
				}
			}
			if ((bool)tutorial && tutorial.InLesson && (bool)tutorial.coach)
			{
				tutorial.coach.anchoredPosition = new Vector2(0f, 0f - (boardTop - 160f));
			}
			if ((bool)hint)
			{
				hint.anchoredPosition = new Vector2(0f, 0f - (footerTop - 86f));
			}
			if ((bool)effects)
			{
				effects.sizeDelta = lastSize;
			}
		}
	}
}
