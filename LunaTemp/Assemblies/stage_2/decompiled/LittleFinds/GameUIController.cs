using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class GameUIController : MonoBehaviour
	{
		public GameManager game;

		public Button helpButton;

		public Text lives;

		public Text time;

		public Text message;

		public Text modalTitle;

		public Text modalDescription;

		public Text modalButtonText;

		public Text soundLabel;

		public Text hapticLabel;

		public RectTransform modal;

		public GameObject overlay;

		public Button startButton;

		public Button freshButton;

		public GameObject settingsRoot;

		public GameObject resultRoot;

		public CollectionBox[] resultBoxes;

		private CountdownPressure clockPressure;

		private int lastLives = -1;

		private int lastSecond = -1;

		private bool help;

		private Coroutine toast;

		private Coroutine modalMotion;

		private CanvasGroup modalFade;

		private bool dismissing;

		private ResultSticker sticker;

		private CanvasGroup actionFade;

		private Image modalPaper;

		private Image veil;

		private Color buttonColor;

		private Color buttonTextColor;

		private Color veilColor;

		private bool resultMode;

		private void Awake()
		{
			modalPaper = modal.GetComponent<Image>();
			veil = overlay.GetComponent<Image>();
			veilColor = veil.color;
			buttonColor = startButton.GetComponent<Image>().color;
			buttonTextColor = modalButtonText.color;
		}

		private void Start()
		{
			clockPressure = CountdownPressure.Create(game);
			soundLabel.text = (game.Feedback.SoundOn ? "Sound On" : "Sound Off");
			hapticLabel.text = (game.Feedback.Haptics ? "Touch On" : "Touch Off");
		}

		private void Update()
		{
			if ((bool)game)
			{
				SyncLives();
				bool lesson = (bool)game.Tutorial && game.Tutorial.InLesson;
				if (lesson)
				{
					time.text = "TRAINING";
					time.fontSize = 44;
				}
				int seconds = Mathf.CeilToInt(game.Timer.Remaining);
				if (!lesson && seconds != lastSecond)
				{
					lastSecond = seconds;
					time.text = (seconds / 60).ToString("00") + ":" + (seconds % 60).ToString("00");
				}
				freshButton.interactable = game.State == GameState.Playing && (!game.Tutorial || game.Tutorial.CanShuffle);
			}
		}

		public void SyncLives()
		{
			if ((bool)lives && lastLives != game.Risk.Lives)
			{
				lastLives = game.Risk.Lives;
				lives.text = lastLives.ToString();
				lives.color = ((lastLives <= 2) ? FindStyle.Hex("dc5076") : FindStyle.Ink);
			}
		}

		public void ResetClock()
		{
			if ((bool)clockPressure)
			{
				clockPressure.ResetVisual();
			}
			lastSecond = -1;
			time.fontSize = (game.Tutorial.InLesson ? 44 : 78);
		}

		public void Message(string value)
		{
			string copy = null;
			if (value.StartsWith("Penalty: "))
			{
				copy = value.Substring(9);
			}
			else if (value.StartsWith("Shuffle cost:"))
			{
				copy = "Fresh Cards: -" + game.Risk.shuffleSeconds.ToString("0") + " seconds";
			}
			else if (value.StartsWith("Pending:"))
			{
				copy = "The last treasure is already on its way.";
			}
			else if (value.Contains("Three little"))
			{
				copy = "Collect an open card first.";
			}
			else if (value.Contains("full"))
			{
				copy = "Collection complete! Try Fresh Cards.";
			}
			else if (value.Contains("queued"))
			{
				copy = "Fresh cards coming right up!";
			}
			if (copy != null)
			{
				if (toast != null)
				{
					StopCoroutine(toast);
				}
				message.text = copy;
				toast = StartCoroutine(Toast());
			}
		}

		private IEnumerator Toast()
		{
			message.gameObject.SetActive(true);
			yield return new WaitForSeconds(1.2f);
			message.gameObject.SetActive(false);
			toast = null;
		}

		public void CloseModal()
		{
			StopModalMotion();
			overlay.SetActive(false);
			modal.localScale = Vector3.one;
			modal.anchoredPosition = Vector2.zero;
			startButton.interactable = true;
			if ((bool)actionFade)
			{
				actionFade.alpha = 1f;
			}
			if ((bool)modalFade)
			{
				modalFade.alpha = 1f;
			}
			if (toast != null)
			{
				StopCoroutine(toast);
			}
			message.gameObject.SetActive(false);
			toast = null;
		}

		public void ShowResult(bool won, int count)
		{
			StopModalMotion();
			if (toast != null)
			{
				StopCoroutine(toast);
			}
			toast = null;
			message.gameObject.SetActive(false);
			help = false;
			resultMode = true;
			modal.sizeDelta = new Vector2(820f, 600f);
			modalPaper.enabled = false;
			if ((bool)resultRoot)
			{
				resultRoot.SetActive(false);
			}
			settingsRoot.SetActive(false);
			modalDescription.gameObject.SetActive(false);
			modalTitle.gameObject.SetActive(false);
			if (!sticker)
			{
				sticker = ResultSticker.Create(modal, game.Feedback.font);
			}
			sticker.gameObject.SetActive(true);
			string word = ((!won) ? ((game.Risk.Lives == 0) ? "OUT OF LIVES!" : "TIME'S UP!") : ((game.LevelIndex == 4) ? "ALL CLEAR!" : "NAILED IT!"));
			sticker.Configure(won, game.LevelIndex + 1, word);
			modalTitle.text = word;
			RectTransform button = (RectTransform)startButton.transform;
			button.anchoredPosition = new Vector2(150f, -450f);
			button.sizeDelta = new Vector2(520f, 126f);
			modalButtonText.rectTransform.sizeDelta = new Vector2(512f, 126f);
			modalButtonText.fontSize = 58;
			startButton.GetComponent<Image>().color = FindStyle.Ink;
			modalButtonText.color = Color.white;
			modalButtonText.text = ((!won) ? "TRY AGAIN  >" : ((game.LevelIndex == 4) ? "PLAY AGAIN  >" : "NEXT LEVEL  >"));
			veil.color = new Color(0.145f, 0.169f, 0.294f, 0.3f);
			if (!actionFade)
			{
				actionFade = startButton.GetComponent<CanvasGroup>();
				if (!actionFade)
				{
					actionFade = startButton.gameObject.AddComponent<CanvasGroup>();
				}
			}
			OpenModalAnimated();
		}

		public void ShowHelp()
		{
			if (game.Tutorial.InLesson)
			{
				game.Tutorial.ExitLevel();
			}
			else
			{
				game.Tutorial.EnterLevel();
			}
		}

		private void ModalMode()
		{
			resultMode = false;
			if ((bool)sticker)
			{
				sticker.gameObject.SetActive(false);
			}
			modalPaper.enabled = true;
			modalTitle.gameObject.SetActive(true);
			veil.color = veilColor;
			startButton.GetComponent<Image>().color = buttonColor;
			modalButtonText.color = buttonTextColor;
			modalButtonText.fontSize = 52;
			startButton.interactable = true;
			if ((bool)actionFade)
			{
				actionFade.alpha = 1f;
			}
			modal.sizeDelta = new Vector2(920f, 800f);
			modalTitle.rectTransform.anchoredPosition = new Vector2(50f, -42f);
			modalTitle.rectTransform.sizeDelta = new Vector2(820f, 100f);
			modalTitle.fontSize = 54;
			modalDescription.gameObject.SetActive(false);
			RectTransform button = (RectTransform)startButton.transform;
			button.anchoredPosition = new Vector2(60f, -620f);
			button.sizeDelta = new Vector2(800f, 132f);
			modalButtonText.rectTransform.sizeDelta = new Vector2(792f, 132f);
		}

		public void ShowSettings()
		{
			StopModalMotion();
			ModalMode();
			modal.sizeDelta = new Vector2(920f, 965f);
			((RectTransform)startButton.transform).anchoredPosition = new Vector2(60f, -785f);
			helpButton.GetComponentInChildren<Text>().text = (game.Tutorial.InLesson ? "Exit Tutorial" : "How to play");
			help = true;
			if ((bool)resultRoot)
			{
				resultRoot.SetActive(false);
			}
			settingsRoot.SetActive(true);
			modalTitle.text = "Settings";
			modalDescription.text = "";
			modalButtonText.text = "Back to play";
			OpenModalAnimated();
		}

		public void StartClicked()
		{
			if (!dismissing && overlay.activeSelf)
			{
				game.Feedback.Play(7);
				bool settings = help;
				bool victory = game.State == GameState.Victory;
				StopModalMotion();
				dismissing = true;
				modalMotion = StartCoroutine(DismissModal(settings, victory));
			}
		}

		private void StopModalMotion()
		{
			if (modalMotion != null)
			{
				StopCoroutine(modalMotion);
			}
			modalMotion = null;
			dismissing = false;
		}

		private void OpenModalAnimated()
		{
			if (!modalFade)
			{
				modalFade = overlay.GetComponent<CanvasGroup>();
				if (!modalFade)
				{
					modalFade = overlay.AddComponent<CanvasGroup>();
				}
			}
			overlay.SetActive(true);
			modalFade.alpha = 0f;
			modalFade.blocksRaycasts = true;
			if (resultMode)
			{
				modal.localScale = Vector3.one;
				modal.anchoredPosition = Vector2.zero;
				startButton.interactable = false;
				actionFade.alpha = 0f;
				modalMotion = StartCoroutine(RevealSticker());
			}
			else
			{
				modal.localScale = Vector3.one * 0.86f;
				modal.anchoredPosition = new Vector2(0f, -42f);
				modalMotion = StartCoroutine(RevealModal());
			}
		}

		private IEnumerator RevealModal()
		{
			for (float t = 0f; t < 0.34f; t += Time.unscaledDeltaTime)
			{
				float u = t / 0.34f;
				float spring = 1f + 2.2f * Mathf.Pow(u - 1f, 3f) + 1.2f * Mathf.Pow(u - 1f, 2f);
				modalFade.alpha = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(u / 0.65f));
				modal.localScale = Vector3.one * Mathf.LerpUnclamped(0.86f, 1f, spring);
				modal.anchoredPosition = Vector2.LerpUnclamped(new Vector2(0f, -42f), Vector2.zero, spring);
				yield return null;
			}
			modalFade.alpha = 1f;
			modal.localScale = Vector3.one;
			modal.anchoredPosition = Vector2.zero;
			modalMotion = null;
		}

		private IEnumerator RevealSticker()
		{
			RectTransform card = sticker.rectTransform;
			float angle = (sticker.Won ? (-4) : 3);
			for (float t = 0f; t < 0.48f; t += Time.unscaledDeltaTime)
			{
				float u = Mathf.Clamp01(t / 0.38f);
				float spring = 1f + 2.2f * Mathf.Pow(u - 1f, 3f) + 1.2f * Mathf.Pow(u - 1f, 2f);
				modalFade.alpha = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t / 0.16f));
				card.localScale = Vector3.one * Mathf.LerpUnclamped(sticker.Won ? 1.65f : 0.7f, 1f, spring);
				card.localRotation = Quaternion.Euler(0f, 0f, Mathf.LerpUnclamped(sticker.Won ? (-18) : 14, angle, spring));
				card.anchoredPosition = Vector2.LerpUnclamped(new Vector2(sticker.Won ? (-60) : 0, sticker.Won ? (-140) : (-300)), new Vector2(0f, -220f), spring);
				actionFade.alpha = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01((t - 0.18f) / 0.2f));
				yield return null;
			}
			card.localScale = Vector3.one;
			card.localRotation = Quaternion.Euler(0f, 0f, angle);
			card.anchoredPosition = new Vector2(0f, -220f);
			float num3 = (modalFade.alpha = (actionFade.alpha = 1f));
			startButton.interactable = true;
			modalMotion = null;
		}

		private IEnumerator DismissModal(bool settings, bool victory)
		{
			Vector3 scale = modal.localScale;
			Vector2 position = modal.anchoredPosition;
			float alpha = (modalFade ? modalFade.alpha : 1f);
			Vector2 cardFrom = (sticker ? sticker.rectTransform.anchoredPosition : Vector2.zero);
			Quaternion turn = (sticker ? sticker.rectTransform.localRotation : Quaternion.identity);
			float duration = (settings ? 0.16f : 0.24f);
			for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
			{
				float u = Mathf.SmoothStep(0f, 1f, t / duration);
				if ((bool)modalFade)
				{
					modalFade.alpha = alpha * (1f - u);
				}
				if (resultMode && (bool)sticker)
				{
					sticker.rectTransform.anchoredPosition = cardFrom + new Vector2(victory ? 180 : (-100), 130f) * u;
					sticker.rectTransform.localRotation = turn * Quaternion.Euler(0f, 0f, u * (float)(victory ? (-18) : 16));
					sticker.rectTransform.localScale = Vector3.one * (1f + 0.12f * u);
					actionFade.alpha = 1f - u;
				}
				else
				{
					modal.localScale = Vector3.Lerp(scale, Vector3.one * 0.94f, u);
					modal.anchoredPosition = position + Vector2.down * (u * 20f);
				}
				yield return null;
			}
			modalMotion = null;
			CloseModal();
			help = false;
			if (!settings)
			{
				if (victory)
				{
					game.ContinueCampaign();
				}
				else
				{
					game.Begin();
				}
			}
		}

		public void ToggleSound()
		{
			game.Feedback.SoundOn = !game.Feedback.SoundOn;
			PlayerPrefs.SetInt("LittleFinds.Sound", game.Feedback.SoundOn ? 1 : 0);
			if (!game.Feedback.SoundOn && (bool)game.Feedback.audio)
			{
				game.Feedback.audio.StopAll();
			}
			soundLabel.text = (game.Feedback.SoundOn ? "Sound On" : "Sound Off");
			game.Feedback.Play(7);
		}

		public void ToggleHaptics()
		{
			game.Feedback.Haptics = !game.Feedback.Haptics;
			PlayerPrefs.SetInt("LittleFinds.Haptics", game.Feedback.Haptics ? 1 : 0);
			hapticLabel.text = (game.Feedback.Haptics ? "Touch On" : "Touch Off");
		}
	}
}
