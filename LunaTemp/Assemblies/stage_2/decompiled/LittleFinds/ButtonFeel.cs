using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LittleFinds
{
	public class ButtonFeel : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerExitHandler
	{
		private Button button;

		private RectTransform rt;

		private Coroutine bounce;

		private GameManager game;

		private void Awake()
		{
			button = GetComponent<Button>();
			rt = (RectTransform)base.transform;
			game = Object.FindObjectOfType<GameManager>();
		}

		public void OnPointerDown(PointerEventData e)
		{
			if (button.IsInteractable())
			{
				if (bounce != null)
				{
					StopCoroutine(bounce);
				}
				rt.localScale = Vector3.one * 0.94f;
				if ((bool)game)
				{
					game.Feedback.Play(7);
				}
			}
		}

		public void OnPointerUp(PointerEventData e)
		{
			Restore();
		}

		public void OnPointerExit(PointerEventData e)
		{
			Restore();
		}

		private void Restore()
		{
			if ((bool)rt && base.isActiveAndEnabled)
			{
				if (bounce != null)
				{
					StopCoroutine(bounce);
				}
				bounce = StartCoroutine(Spring());
			}
		}

		private IEnumerator Spring()
		{
			Vector3 start = rt.localScale;
			for (float t = 0f; t < 0.18f; t += Time.unscaledDeltaTime)
			{
				float u = t / 0.18f;
				float i = 1f + 2.3f * Mathf.Pow(u - 1f, 3f) + 1.3f * Mathf.Pow(u - 1f, 2f);
				rt.localScale = Vector3.LerpUnclamped(start, Vector3.one, i);
				yield return null;
			}
			rt.localScale = Vector3.one;
			bounce = null;
		}

		private void OnDisable()
		{
			if ((bool)rt)
			{
				rt.localScale = Vector3.one;
			}
			bounce = null;
		}
	}
}
