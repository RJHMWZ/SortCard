using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LittleFinds
{
	public class TrashBin : MonoBehaviour
	{
		public GameManager game;

		public RectTransform target;

		public TrashCanGraphic icon;

		private CardController source;

		private RectTransform ghost;

		private CanvasGroup ghostFade;

		private Coroutine flight;

		private int pointer;

		private bool over;

		private bool committed;

		private bool penalty;

		private Vector3 ghostScale;

		private float nextWiggle;

		private float wiggleTime = -1f;

		private float landingTime;

		public bool IsBusy => source != null;

		public RectTransform Ghost => ghost;

		public bool Available => (bool)game && game.State == GameState.Playing && !game.UI.overlay.activeSelf && (!game.Tutorial || game.Tutorial.CanDiscard);

		public static TrashBin Create(GameManager game)
		{
			GameObject root = new GameObject("Trash Bin", typeof(RectTransform), typeof(TrashBin));
			RectTransform rect = (RectTransform)root.transform;
			rect.SetParent(game.Feedback.layer.parent, false);
			Vector2 vector3 = (rect.anchorMin = (rect.anchorMax = new Vector2(0.5f, 1f)));
			rect.pivot = new Vector2(0.5f, 0.5f);
			rect.sizeDelta = new Vector2(200f, 152f);
			root.transform.SetSiblingIndex(game.Feedback.layer.GetSiblingIndex());
			TrashBin bin = root.GetComponent<TrashBin>();
			bin.game = game;
			bin.target = rect;
			GameObject symbol = new GameObject("Trash Can", typeof(RectTransform), typeof(CanvasRenderer), typeof(TrashCanGraphic));
			RectTransform sr = (RectTransform)symbol.transform;
			sr.SetParent(rect, false);
			sr.sizeDelta = new Vector2(144f, 144f);
			sr.anchoredPosition = Vector2.zero;
			Vector2 vector5 = (sr.pivot = new Vector2(0.5f, 0.5f));
			vector3 = (sr.anchorMin = (sr.anchorMax = vector5));
			sr.anchoredPosition = Vector2.zero;
			bin.icon = symbol.GetComponent<TrashCanGraphic>();
			bin.icon.color = FindStyle.Ink;
			bin.icon.raycastTarget = false;
			bin.nextWiggle = Time.unscaledTime + 3f;
			MobileGameLayout layout = rect.parent.GetComponent<MobileGameLayout>();
			layout.trash = rect;
			layout.Reflow();
			return bin;
		}

		private void OnEnable()
		{
			Image panel = GetComponent<Image>();
			if ((bool)panel)
			{
				panel.enabled = false;
			}
			Outline border = GetComponent<Outline>();
			if ((bool)border)
			{
				border.enabled = false;
			}
			Transform caption = base.transform.Find("Trash Label");
			if ((bool)caption)
			{
				caption.gameObject.SetActive(false);
			}
			if ((bool)target)
			{
				Vector2 vector3 = (target.anchorMin = (target.anchorMax = new Vector2(0.5f, 1f)));
				target.pivot = new Vector2(0.5f, 0.5f);
				target.sizeDelta = new Vector2(200f, 152f);
			}
			if ((bool)icon)
			{
				RectTransform rect = icon.rectTransform;
				Vector2 vector5 = (rect.pivot = new Vector2(0.5f, 0.5f));
				Vector2 vector3 = (rect.anchorMin = (rect.anchorMax = vector5));
				rect.sizeDelta = new Vector2(144f, 144f);
				rect.anchoredPosition = Vector2.zero;
			}
			nextWiggle = Time.unscaledTime + 3f;
		}

		private void Update()
		{
			if (IsBusy && (!Available || !source.gameObject.activeInHierarchy))
			{
				Cancel();
			}
			target.localScale = Vector3.Lerp(target.localScale, Vector3.one * (over ? 1.07f : 1f), 1f - Mathf.Exp(-24f * Time.unscaledDeltaTime));
			float dt = Time.unscaledDeltaTime;
			icon.LidOpen = Mathf.MoveTowards(icon.LidOpen, committed ? 1f : (over ? 0.35f : 0f), dt * (float)(committed ? 10 : 7));
			if (!IsBusy)
			{
				icon.color = (Available ? FindStyle.Ink : FindStyle.Soft);
			}
			if (IsBusy || !Available)
			{
				wiggleTime = -1f;
				nextWiggle = Time.unscaledTime + 5.5f;
			}
			else if (Time.unscaledTime >= nextWiggle && wiggleTime < 0f)
			{
				wiggleTime = 0f;
				nextWiggle = Time.unscaledTime + 5.5f;
			}
			float angle = 0f;
			if (wiggleTime >= 0f)
			{
				wiggleTime += dt;
				float u = Mathf.Clamp01(wiggleTime / 0.65f);
				angle = Mathf.Sin(u * 3.14159265f * 5f) * 7f * (1f - u);
				if (u >= 1f)
				{
					wiggleTime = -1f;
				}
			}
			icon.rectTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
			if (landingTime > 0f)
			{
				landingTime = Mathf.Max(0f, landingTime - dt);
				float bounce = Mathf.Sin((1f - landingTime / 0.25f) * 3.14159265f) * 0.12f;
				icon.rectTransform.localScale = new Vector3(1f + bounce, 1f - bounce, 1f);
			}
			else
			{
				icon.rectTransform.localScale = Vector3.one;
			}
		}

		public bool BeginDrag(CardController card, PointerEventData e)
		{
			if (IsBusy || !Available || !card.CanDragToTrash || ((bool)game.Tutorial && !game.Tutorial.CanDiscardCard(card)))
			{
				return false;
			}
			source = card;
			pointer = e.pointerId;
			source.SetDragging(true);
			e.eligibleForClick = false;
			GameObject root = new GameObject("Card Drag Shadow", typeof(RectTransform), typeof(CanvasGroup));
			ghost = (RectTransform)root.transform;
			ghost.SetParent(game.Feedback.layer, false);
			RectTransform rectTransform = ghost;
			RectTransform rectTransform2 = ghost;
			Vector2 vector2 = (ghost.pivot = new Vector2(0.5f, 0.5f));
			Vector2 vector5 = (rectTransform.anchorMin = (rectTransform2.anchorMax = vector2));
			ghost.sizeDelta = card.visual.rect.size;
			ghost.localRotation = Quaternion.identity;
			ghostScale = new Vector3(card.transform.lossyScale.x / game.Feedback.layer.lossyScale.x, card.transform.lossyScale.y / game.Feedback.layer.lossyScale.y, 1f) * 0.94f;
			ghost.localScale = ghostScale;
			ghost.position = card.visual.position;
			ghostFade = root.GetComponent<CanvasGroup>();
			ghostFade.alpha = 0.58f;
			ghostFade.blocksRaycasts = false;
			ghostFade.interactable = false;
			GameObject copy = UnityEngine.Object.Instantiate(card.visual.gameObject, ghost, false);
			RectTransform cr = (RectTransform)copy.transform;
			vector2 = (cr.pivot = new Vector2(0.5f, 0.5f));
			vector5 = (cr.anchorMin = (cr.anchorMax = vector2));
			cr.localPosition = Vector3.zero;
			cr.localRotation = Quaternion.identity;
			cr.localScale = Vector3.one;
			Graphic[] componentsInChildren = copy.GetComponentsInChildren<Graphic>(true);
			foreach (Graphic graphic in componentsInChildren)
			{
				graphic.raycastTarget = false;
			}
			MoveDrag(card, e);
			return true;
		}

		public void MoveDrag(CardController card, PointerEventData e)
		{
			if (!(source != card) && pointer == e.pointerId && !committed && flight == null)
			{
				over = RectTransformUtility.RectangleContainsScreenPoint(target, e.position, e.pressEventCamera);
				ghost.localScale = ghostScale * (over ? 0.5f : 1f);
				if (RectTransformUtility.ScreenPointToWorldPointInRectangle(game.Feedback.layer, e.position, e.pressEventCamera, out var world))
				{
					ghost.position = world + game.Feedback.layer.TransformVector(Vector3.up * (over ? 130 : 85));
				}
				bool known = card.State == CardState.FaceUp;
				bool harmful = known && game.WouldDiscardCostLife(card);
				icon.color = ((!over) ? FindStyle.Ink : (harmful ? FindStyle.Hex("d51d43") : (known ? FindStyle.Hex("258c78") : FindStyle.Ink)));
			}
		}

		public void EndDrag(CardController card, PointerEventData e)
		{
			if (!(source != card) && pointer == e.pointerId && flight == null)
			{
				e.eligibleForClick = false;
				committed = Available && RectTransformUtility.RectangleContainsScreenPoint(target, e.position, e.pressEventCamera);
				penalty = committed && game.WouldDiscardCostLife(card);
				if (committed)
				{
					wiggleTime = -1f;
					icon.rectTransform.localRotation = Quaternion.identity;
				}
				flight = StartCoroutine(FlyShadow(committed));
			}
		}

		private IEnumerator FlyShadow(bool discard)
		{
			Vector3 start = ghost.position;
			Vector3 scale = ghost.localScale;
			float duration = (discard ? 0.36f : 0.18f);
			for (float t = 0f; t < duration; t += Time.deltaTime)
			{
				float u = Mathf.SmoothStep(0f, 1f, t / duration);
				Vector3 end = (discard ? icon.rectTransform.TransformPoint(new Vector3(0f, Mathf.Lerp(0.24f, 0f, u) * icon.rectTransform.rect.height, 0f)) : source.visual.position);
				Vector3 bend = (start + end) * 0.5f + game.Feedback.layer.TransformVector(Vector3.up * (discard ? 100 : 30));
				ghost.position = (1f - u) * (1f - u) * start + 2f * (1f - u) * u * bend + u * u * end;
				ghost.localScale = scale * Mathf.Lerp(1f, discard ? 0.12f : 1.06f, u);
				ghost.localRotation = Quaternion.Euler(0f, 0f, discard ? (u * 28f) : 0f);
				ghostFade.alpha = Mathf.Lerp(0.58f, 0f, discard ? Mathf.Clamp01((u - 0.55f) / 0.45f) : u);
				yield return null;
			}
			CardController card = source;
			bool charge = penalty;
			UnityEngine.Object.Destroy(ghost.gameObject);
			ghost = null;
			ghostFade = null;
			source = null;
			flight = null;
			over = (committed = (penalty = false));
			card.SetDragging(false);
			if (discard)
			{
				landingTime = 0.25f;
			}
			if (discard)
			{
				card.Hide();
				game.Discarded(card, charge);
			}
			else
			{
				card.ResetReturnClock();
			}
		}

		public void Cancel()
		{
			if (flight != null)
			{
				StopCoroutine(flight);
			}
			flight = null;
			if ((bool)source)
			{
				source.SetDragging(false);
			}
			source = null;
			if ((bool)ghost)
			{
				UnityEngine.Object.Destroy(ghost.gameObject);
			}
			ghost = null;
			ghostFade = null;
			over = (committed = (penalty = false));
			landingTime = 0f;
			wiggleTime = -1f;
			nextWiggle = Time.unscaledTime + 3f;
			if ((bool)target)
			{
				target.localScale = Vector3.one;
			}
			if ((bool)icon)
			{
				icon.color = FindStyle.Ink;
				icon.LidOpen = 0f;
				icon.rectTransform.localRotation = Quaternion.identity;
				icon.rectTransform.localScale = Vector3.one;
			}
		}

		private void OnDisable()
		{
			Cancel();
		}
	}
}
