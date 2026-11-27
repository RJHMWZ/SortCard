using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LittleFinds
{
	public class CardController : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public RectTransform visual;

		public RectTransform rotator;

		public GameObject front;

		public GameObject back;

		public Image illustration;

		public Image frontPaper;

		public Image shine;

		public Text title;

		public Text subtitle;

		public Text number;

		public CanvasGroup group;

		private float age;

		private bool hovered;

		private bool collectQueued;

		private Vector2 tilt;

		private RectTransform rt;

		private Transform home;

		private Camera hoverCamera;

		private float ignoreClickUntil;

		public CardState State { get; private set; } = CardState.FaceDown;


		public FindData Data { get; private set; }

		public GameManager Game { get; private set; }

		public Vector3 HomePosition => ((RectTransform)home).TransformPoint(((RectTransform)home).rect.center);

		public float FaceUpDuration => ((bool)Game.Tutorial && Game.Tutorial.InLesson) ? 4.5f : Game.Level.OpenSeconds;

		public int Index { get; private set; }

		public bool IsOpen => State == CardState.FaceUp || State == CardState.Flipping;

		public bool IsDragging { get; private set; }

		public bool CanDragToTrash => (bool)Game && Game.State == GameState.Playing && !IsDragging && (State == CardState.FaceDown || State == CardState.FaceUp);

		private void Awake()
		{
			rt = (RectTransform)base.transform;
			home = rt.parent;
		}

		public void Bind(GameManager game, int index, FindData data)
		{
			StopAllCoroutines();
			Game = game;
			Index = index;
			Data = data;
			collectQueued = false;
			age = 0f;
			hovered = false;
			tilt = Vector2.zero;
			IsDragging = false;
			ignoreClickUntil = 0f;
			State = CardState.FaceDown;
			if (!rt)
			{
				rt = (RectTransform)base.transform;
			}
			if (!home)
			{
				home = rt.parent;
			}
			rt.SetParent(home, false);
			rt.anchoredPosition = Vector2.zero;
			rt.localRotation = Quaternion.identity;
			rt.localScale = Vector3.one;
			visual.localScale = Vector3.one;
			visual.localRotation = Quaternion.identity;
			visual.anchoredPosition = Vector2.zero;
			group.alpha = 1f;
			group.blocksRaycasts = true;
			rotator.localRotation = Quaternion.identity;
			front.SetActive(false);
			back.SetActive(true);
			title.text = data.title;
			subtitle.text = "";
			subtitle.gameObject.SetActive(false);
			number.text = "";
			illustration.sprite = data.art;
			RefreshCategoryVisual();
			base.gameObject.SetActive(true);
		}

		private void Update()
		{
			if (Game == null)
			{
				return;
			}
			if (hovered)
			{
				UpdateHover(Input.mousePosition, hoverCamera);
			}
			if (State == CardState.FaceUp && !IsDragging && Game.State == GameState.Playing && (!Game.Tutorial || Game.Tutorial.AllowAutoReturn))
			{
				age += Time.deltaTime;
				if (age >= FaceUpDuration)
				{
					Return();
				}
			}
			if (State == CardState.FaceDown || State == CardState.FaceUp)
			{
				float i = 1f - Mathf.Exp(-22f * Time.deltaTime);
				visual.localScale = Vector3.Lerp(visual.localScale, Vector3.one * (hovered ? 1.045f : 1f), i);
				visual.anchoredPosition = Vector2.Lerp(visual.anchoredPosition, new Vector2(0f, hovered ? 12 : 0), i);
				visual.localRotation = Quaternion.Slerp(visual.localRotation, Quaternion.Euler(tilt.y, tilt.x, 0f), i);
			}
		}

		public void OnPointerClick(PointerEventData e)
		{
			if (!e.dragging && Time.unscaledTime >= ignoreClickUntil)
			{
				Tap();
			}
		}

		public void Tap()
		{
			if (Game == null || Game.State != GameState.Playing || IsDragging || ((bool)Game.Tutorial && !Game.Tutorial.CanTap(this)))
			{
				return;
			}
			if (State == CardState.FaceDown)
			{
				if (Game.Cards.OpenCount >= 3)
				{
					if (!Game.Tutorial || !Game.Tutorial.OnLimitReached(this))
					{
						Game.UI.Message("Three little finds at a time. Tap one again to collect.");
					}
				}
				else
				{
					StartCoroutine(Flip(true));
					Game.Feedback.Play(0);
				}
			}
			else if (State == CardState.Flipping)
			{
				collectQueued = true;
			}
			else if (State == CardState.FaceUp)
			{
				Game.Collect(this);
			}
		}

		public void SetDragging(bool value)
		{
			IsDragging = value;
			age = 0f;
			hovered = false;
			tilt = Vector2.zero;
			visual.localScale = Vector3.one;
			visual.localRotation = Quaternion.identity;
			visual.anchoredPosition = Vector2.zero;
			if (!value)
			{
				ignoreClickUntil = Time.unscaledTime + 0.1f;
			}
		}

		public void OnBeginDrag(PointerEventData e)
		{
			if ((bool)Game && (bool)Game.Trash)
			{
				Game.Trash.BeginDrag(this, e);
			}
		}

		public void OnDrag(PointerEventData e)
		{
			if ((bool)Game && (bool)Game.Trash)
			{
				Game.Trash.MoveDrag(this, e);
			}
		}

		public void OnEndDrag(PointerEventData e)
		{
			if ((bool)Game && (bool)Game.Trash)
			{
				Game.Trash.EndDrag(this, e);
			}
		}

		private IEnumerator Flip(bool reveal)
		{
			State = (reveal ? CardState.Flipping : CardState.Returning);
			collectQueued = false;
			hovered = false;
			tilt = Vector2.zero;
			visual.localRotation = Quaternion.identity;
			visual.localScale = Vector3.one;
			visual.anchoredPosition = Vector2.zero;
			float duration = (reveal ? 0.25f : (((bool)Game.Tutorial && Game.Tutorial.InLesson) ? 0.28f : Game.Level.ReturnSeconds));
			for (float t = 0f; t < duration; t += Time.deltaTime)
			{
				float a = Mathf.Lerp((!reveal) ? 180 : 0, reveal ? 180 : 0, Mathf.SmoothStep(0f, 1f, t / duration));
				rotator.localRotation = Quaternion.Euler(0f, a, 0f);
				front.SetActive(a >= 90f);
				back.SetActive(a < 90f);
				yield return null;
			}
			rotator.localRotation = Quaternion.Euler(0f, reveal ? 180 : 0, 0f);
			front.SetActive(reveal);
			back.SetActive(!reveal);
			State = (reveal ? CardState.FaceUp : CardState.FaceDown);
			age = 0f;
			visual.localScale = Vector3.one * (reveal ? 1.055f : 1f);
			if ((bool)Game.Tutorial)
			{
				if (reveal)
				{
					Game.Tutorial.OnRevealed(this);
				}
				else
				{
					Game.Tutorial.OnReturned();
				}
			}
			if (reveal && collectQueued)
			{
				collectQueued = false;
				Game.Collect(this);
			}
		}

		public void RefreshCategoryVisual()
		{
			if (Data != null && !(Game == null))
			{
				int c = Data.category;
				subtitle.text = "";
				subtitle.gameObject.SetActive(false);
				number.text = "";
				number.gameObject.SetActive(false);
				subtitle.color = FindStyle.Ink;
				frontPaper.color = ((c >= 0 && c < FindStyle.Colors.Length) ? Color.Lerp(Color.white, FindStyle.Colors[c], 0.1f) : Color.white);
			}
		}

		public void ResetReturnClock()
		{
			age = 0f;
		}

		public void PreviewForTutorial()
		{
			if ((bool)Game && (bool)Game.Tutorial && Game.Tutorial.InLesson && State == CardState.FaceDown)
			{
				StartCoroutine(Flip(true));
			}
		}

		public void Shatter()
		{
			StopAllCoroutines();
			State = CardState.Removed;
			group.blocksRaycasts = false;
			hovered = false;
			Game.Feedback.Shatter(this);
			group.alpha = 0f;
			base.gameObject.SetActive(false);
		}

		public void Return(bool penalize = true)
		{
			if (State == CardState.FaceUp && Game.State == GameState.Playing)
			{
				Game.Feedback.Play(9);
				StartCoroutine(Flip(false));
			}
		}

		public void BeginCollect()
		{
			StopAllCoroutines();
			State = CardState.Collecting;
			hovered = false;
			tilt = Vector2.zero;
			visual.localRotation = Quaternion.identity;
			visual.anchoredPosition = Vector2.zero;
			group.blocksRaycasts = false;
			StartCoroutine(Fly());
		}

		private IEnumerator Fly()
		{
			Vector3 start = rt.position;
			CollectionBox box = Game.Collections.BoxFor(Data.category);
			float lane = box.OpenForCard();
			Vector3 end = box.Approach(lane);
			Vector3 bend = (start + end) * 0.5f + Vector3.right * ((Index % 2 == 0) ? 70 : (-70));
			rt.SetParent(Game.Feedback.layer, true);
			Vector3 flightScale = rt.localScale;
			for (float t2 = 0f; t2 < 0.3f; t2 += Time.deltaTime)
			{
				float u = Mathf.SmoothStep(0f, 1f, t2 / 0.3f);
				rt.position = (1f - u) * (1f - u) * start + 2f * (1f - u) * u * bend + u * u * end;
				rt.localScale = flightScale * Mathf.Lerp(1f, 0.38f, u);
				rt.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(u * 3.14159265f) * 8f);
				group.alpha = 1f;
				yield return null;
			}
			rt.position = end;
			rt.localScale = flightScale * 0.38f;
			rt.localRotation = Quaternion.identity;
			rt.SetParent(box.insertionLayer, true);
			Vector3 from = rt.position;
			for (float t = 0f; t < 0.15f; t += Time.deltaTime)
			{
				rt.position = Vector3.Lerp(from, box.Inside(lane), FindStyle.Ease(t / 0.15f));
				yield return null;
			}
			rt.position = box.Inside(lane);
			State = CardState.Removed;
			group.alpha = 0f;
			base.gameObject.SetActive(false);
			Game.Collected(this);
		}

		public void Dismiss()
		{
			StopAllCoroutines();
			State = CardState.Removed;
			group.blocksRaycasts = false;
			StartCoroutine(FadeExtra());
		}

		private IEnumerator FadeExtra()
		{
			for (float t = 0f; t < 0.18f; t += Time.deltaTime)
			{
				float u = t / 0.18f;
				group.alpha = 1f - u;
				rt.localScale = Vector3.one * (1f - u * 0.65f);
				rt.localRotation = Quaternion.Euler(0f, 0f, u * 10f);
				yield return null;
			}
			base.gameObject.SetActive(false);
		}

		public void Freeze()
		{
			if (State == CardState.Collecting)
			{
				group.alpha = 0f;
			}
			StopAllCoroutines();
			group.blocksRaycasts = false;
			hovered = false;
		}

		public void Hide()
		{
			Freeze();
			State = CardState.Removed;
			base.gameObject.SetActive(false);
		}

		public void PrepareShuffle()
		{
			StopAllCoroutines();
			hovered = false;
			State = CardState.Returning;
			visual.localRotation = Quaternion.identity;
			visual.localScale = Vector3.one;
			visual.anchoredPosition = Vector2.zero;
			group.blocksRaycasts = false;
		}

		public void ResetHover()
		{
			hovered = false;
			tilt = Vector2.zero;
		}

		public void OnPointerEnter(PointerEventData e)
		{
			hoverCamera = e.enterEventCamera;
			if (!IsDragging && !Application.isMobilePlatform && (!Game || !Game.Tutorial || Game.Tutorial.CanTap(this)))
			{
				hovered = true;
			}
		}

		public void OnPointerExit(PointerEventData e)
		{
			hovered = false;
			tilt = Vector2.zero;
		}

		public void OnPointerMove(PointerEventData e)
		{
			UpdateHover(e.position, e.pressEventCamera);
		}

		private void UpdateHover(Vector2 position, Camera eventCamera)
		{
			if (!IsDragging && !Application.isMobilePlatform && (!Game || !Game.Tutorial || Game.Tutorial.CanTap(this)))
			{
				RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, position, eventCamera, out var p);
				tilt = new Vector2(p.x / rt.rect.width * 17f, (0f - p.y) / rt.rect.height * 13f);
				if ((bool)shine)
				{
					shine.rectTransform.anchoredPosition = new Vector2(p.x * 0.45f, p.y * 0.45f);
				}
			}
		}

		public void OnPointerDown(PointerEventData e)
		{
			if (!IsDragging && (!Game || !Game.Tutorial || Game.Tutorial.CanTap(this)) && (State == CardState.FaceUp || State == CardState.FaceDown))
			{
				visual.localScale = Vector3.one * 0.93f;
			}
		}

		public void OnPointerUp(PointerEventData e)
		{
		}
	}
}
