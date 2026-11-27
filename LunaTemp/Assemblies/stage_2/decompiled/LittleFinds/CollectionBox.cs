using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class CollectionBox : MonoBehaviour
	{
		public int category;

		public RectTransform mouth;

		public RectTransform insertionLayer;

		public RectTransform storedPoint;

		public Image frontLip;

		public Image progressFill;

		public Image check;

		public Image categoryIcon;

		public Image[] stamps;

		public Text title;

		private Coroutine fillMotion;

		private Coroutine landing;

		private Coroutine mouthMotion;

		private Vector3 restScale = Vector3.one;

		private int waiting;

		private int laneIndex;

		public float Progress => progressFill.rectTransform.anchorMax.x;

		public int StoredCount { get; private set; }

		public Vector3 MouthCenter => mouth.TransformPoint(mouth.rect.center);

		private void Awake()
		{
			restScale = base.transform.localScale;
			LayoutHeader();
		}

		public Vector3 Approach(float lane)
		{
			return mouth.TransformPoint(mouth.rect.center + new Vector2(lane, 64f));
		}

		public Vector3 Inside(float lane)
		{
			return storedPoint.TransformPoint(new Vector3(lane, 0f, 0f));
		}

		public float OpenForCard()
		{
			waiting++;
			float lane = (laneIndex++ % 3 - 1) * 64;
			if (mouthMotion != null)
			{
				StopCoroutine(mouthMotion);
			}
			mouthMotion = StartCoroutine(MouthTo(new Vector3(1.03f, 1.18f, 1f)));
			return lane;
		}

		public void Configure(int id, Sprite icon)
		{
			category = id;
			title.text = FindStyle.Names[id];
			categoryIcon.sprite = icon;
			progressFill.color = FindStyle.Colors[id];
			LayoutHeader();
		}

		public void LayoutHeader()
		{
			if ((bool)title && (bool)categoryIcon)
			{
				float width = ((RectTransform)base.transform).rect.width;
				RectTransform icon = categoryIcon.rectTransform;
				RectTransform label = title.rectTransform;
				float textWidth = Mathf.Min(title.preferredWidth + 14f, Mathf.Max(0f, width - 32f - icon.rect.width - 8f));
				float left = (width - icon.rect.width - 8f - textWidth) * 0.5f;
				icon.anchoredPosition = new Vector2(left, icon.anchoredPosition.y);
				label.anchoredPosition = new Vector2(left + icon.rect.width + 8f, label.anchoredPosition.y);
				label.sizeDelta = new Vector2(textWidth, label.sizeDelta.y);
			}
		}

		public void ResetBox()
		{
			StopAllCoroutines();
			fillMotion = (landing = (mouthMotion = null));
			waiting = (laneIndex = 0);
			StoredCount = 0;
			base.transform.localScale = restScale;
			mouth.localScale = Vector3.one;
			Image[] array = stamps;
			foreach (Image stamp in array)
			{
				stamp.gameObject.SetActive(false);
				stamp.sprite = null;
			}
			SetFill(0f);
			CompleteVisual(false);
		}

		public void Accept(Sprite item, int count)
		{
			StoredCount = count;
			waiting = Mathf.Max(0, waiting - 1);
			if (count > 0 && count <= stamps.Length)
			{
				Image stamp = stamps[count - 1];
				stamp.sprite = item;
				stamp.gameObject.SetActive(true);
			}
			if (fillMotion != null)
			{
				StopCoroutine(fillMotion);
			}
			fillMotion = StartCoroutine(FillTo((float)count / 4f));
			if (landing != null)
			{
				StopCoroutine(landing);
			}
			landing = StartCoroutine(Settle());
			if (waiting == 0)
			{
				if (mouthMotion != null)
				{
					StopCoroutine(mouthMotion);
				}
				mouthMotion = StartCoroutine(MouthTo(Vector3.one));
			}
		}

		public void SummaryFrom(CollectionBox source)
		{
			StopAllCoroutines();
			Configure(source.category, source.categoryIcon.sprite);
			StoredCount = source.StoredCount;
			for (int i = 0; i < stamps.Length; i++)
			{
				stamps[i].sprite = source.stamps[i].sprite;
				stamps[i].gameObject.SetActive(i < StoredCount);
			}
			SetFill((float)StoredCount / 4f);
			CompleteVisual(StoredCount >= 4);
		}

		private void SetFill(float amount)
		{
			progressFill.rectTransform.anchorMax = new Vector2(Mathf.Clamp01(amount), 1f);
		}

		private void CompleteVisual(bool full)
		{
			check.gameObject.SetActive(full);
			frontLip.color = (full ? FindStyle.Hex("fff0bc") : Color.Lerp(Color.white, FindStyle.Colors[category], 0.15f));
			mouth.GetComponent<Image>().color = FindStyle.Hex(full ? "aaa8a0" : "252b4b");
		}

		private IEnumerator FillTo(float target)
		{
			float from = Progress;
			for (float t = 0f; t < 0.19f; t += Time.deltaTime)
			{
				SetFill(Mathf.Lerp(from, target, FindStyle.Ease(t / 0.19f)));
				yield return null;
			}
			SetFill(target);
			CompleteVisual(target >= 1f);
			fillMotion = null;
		}

		private IEnumerator Settle()
		{
			for (float t = 0f; t < 0.24f; t += Time.deltaTime)
			{
				float u = t / 0.24f;
				float squash = Mathf.Sin(u * 3.14159265f * 2f) * (1f - u);
				base.transform.localScale = Vector3.Scale(restScale, new Vector3(1f + 0.025f * squash, 1f - 0.055f * squash, 1f));
				yield return null;
			}
			base.transform.localScale = restScale;
			landing = null;
		}

		private IEnumerator MouthTo(Vector3 target)
		{
			Vector3 from = mouth.localScale;
			for (float t = 0f; t < 0.1f; t += Time.deltaTime)
			{
				mouth.localScale = Vector3.Lerp(from, target, t / 0.1f);
				yield return null;
			}
			mouth.localScale = target;
			mouthMotion = null;
		}
	}
}
