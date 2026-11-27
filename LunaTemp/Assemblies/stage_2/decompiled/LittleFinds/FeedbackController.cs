using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFinds
{
	public class FeedbackController : MonoBehaviour
	{
		public RectTransform layer;

		public Sprite round;

		public Sprite burst;

		public Sprite ring;

		public Sprite star;

		public RectTransform stage;

		public new AudioDirector audio;

		public Font font;

		public bool SoundOn = true;

		public bool Haptics = true;

		private readonly Image[] sparks = new Image[128];

		private readonly Image[] badges = new Image[12];

		private readonly Text[] floats = new Text[12];

		private readonly Image[] rings = new Image[12];

		private int ringIndex;

		private int particleIndex;

		private int floatIndex;

		private Coroutine recoil;

		private Vector2 recoilOrigin;

		private GameManager game;

		private ComicVictory victoryFX;

		private Coroutine lifeLoss;

		private RectTransform health;

		private Image healthPaper;

		private Image heart;

		private Vector2 healthOrigin;

		private Vector3 healthScale;

		private Color healthColor;

		private Color heartColor;

		private DamageFlashGraphic damageFlash;

		private static readonly string[] comicWords = new string[6] { "POW!", "BAM!", "NICE!", "BOOM!", "WOW!", "SMASH!" };

		private readonly List<CardShatter> shattered = new List<CardShatter>();

		public bool IsShowingLifeLoss => lifeLoss != null;

		public int CollectionPops { get; private set; }

		public bool IsShattering
		{
			get
			{
				shattered.RemoveAll((CardShatter effect) => !effect);
				return shattered.Count > 0;
			}
		}

		public void Shatter(CardController card)
		{
			shattered.RemoveAll((CardShatter effect) => !effect);
			shattered.Add(CardShatter.Spawn(card, layer));
		}

		private void Awake()
		{
			game = GetComponent<GameManager>();
			SoundOn = PlayerPrefs.GetInt("LittleFinds.Sound", 1) == 1;
			Haptics = PlayerPrefs.GetInt("LittleFinds.Haptics", 1) == 1;
			health = (RectTransform)game.UI.lives.transform.parent;
			healthPaper = health.GetComponent<Image>();
			Transform heartTransform = health.Find("Heart");
			if ((bool)heartTransform)
			{
				heart = heartTransform.GetComponent<Image>();
			}
			healthOrigin = health.anchoredPosition;
			healthScale = health.localScale;
			healthColor = healthPaper.color;
			if ((bool)heart)
			{
				heartColor = heart.color;
			}
			GameObject flashObject = new GameObject("Life Loss Red Border", typeof(RectTransform), typeof(CanvasRenderer), typeof(DamageFlashGraphic));
			flashObject.transform.SetParent(layer, false);
			damageFlash = flashObject.GetComponent<DamageFlashGraphic>();
			damageFlash.raycastTarget = false;
			RectTransform flashRect = (RectTransform)flashObject.transform;
			flashRect.anchorMin = Vector2.zero;
			flashRect.anchorMax = Vector2.one;
			Vector2 vector2 = (flashRect.offsetMin = (flashRect.offsetMax = Vector2.zero));
			flashObject.SetActive(false);
			for (int k = 0; k < sparks.Length; k++)
			{
				GameObject o = new GameObject("Impact Confetti", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
				o.transform.SetParent(layer, false);
				sparks[k] = o.GetComponent<Image>();
				sparks[k].sprite = round;
				sparks[k].raycastTarget = false;
				o.SetActive(false);
			}
			for (int j = 0; j < rings.Length; j++)
			{
				rings[j] = EffectImage("Impact Ring", ring);
			}
			for (int i = 0; i < floats.Length; i++)
			{
				GameObject badge = new GameObject("Comic Burst", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
				badge.transform.SetParent(layer, false);
				badges[i] = badge.GetComponent<Image>();
				badges[i].sprite = burst;
				badges[i].raycastTarget = false;
				badges[i].rectTransform.sizeDelta = new Vector2(580f, 270f);
				badge.SetActive(false);
				GameObject o2 = new GameObject("Comic Pop", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text), typeof(Outline));
				o2.transform.SetParent(layer, false);
				Text text = o2.GetComponent<Text>();
				text.font = font;
				text.fontSize = 70;
				text.fontStyle = FontStyle.Normal;
				text.alignment = TextAnchor.MiddleCenter;
				text.raycastTarget = false;
				text.rectTransform.sizeDelta = new Vector2(650f, 160f);
				Outline outline = o2.GetComponent<Outline>();
				outline.effectColor = Color.white;
				outline.effectDistance = new Vector2(2f, -2f);
				floats[i] = text;
				o2.SetActive(false);
			}
		}

		private Image EffectImage(string name, Sprite sprite)
		{
			GameObject o = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
			o.transform.SetParent(layer, false);
			Image i = o.GetComponent<Image>();
			i.sprite = sprite;
			i.raycastTarget = false;
			o.SetActive(false);
			return i;
		}

		public void Play(int index)
		{
			if (Haptics && (index == 0 || index == 8 || index == 5))
			{
				TouchFeedback.Tap();
			}
			if (SoundOn && (bool)audio)
			{
				audio.Play(index, game ? game.Streak : 0);
			}
		}

		public void Penalty(string word, Vector3 origin)
		{
			if (SoundOn && (bool)audio)
			{
				audio.PlayDamage();
			}
			if (Haptics)
			{
				TouchFeedback.Damage();
			}
			if (lifeLoss != null)
			{
				StopCoroutine(lifeLoss);
				ResetLifeLoss();
			}
			game.UI.SyncLives();
			lifeLoss = StartCoroutine(LifeLoss());
			Pulse(health.TransformPoint(health.rect.center), 215f, FindStyle.Hex("ef3156"), 0.5f);
			Burst(NearCard(origin), "-" + 100 + "\n-1 LIFE", false);
			Kick(2.4f);
		}

		private IEnumerator LifeLoss()
		{
			damageFlash.gameObject.SetActive(true);
			Color red = FindStyle.Hex("ef3156");
			for (float t = 0f; t < 0.85f; t += Time.unscaledDeltaTime)
			{
				float u = t / 0.85f;
				float hit = 1f - Mathf.SmoothStep(0f, 1f, u);
				health.localScale = healthScale * (1f + 0.4f * hit);
				health.anchoredPosition = healthOrigin + Vector2.right * (Mathf.Sin(t * 58f) * 14f * hit);
				healthPaper.color = Color.Lerp(healthColor, new Color(1f, 0.28f, 0.36f), hit);
				game.UI.lives.color = Color.Lerp((game.Risk.Lives <= 2) ? FindStyle.Hex("dc5076") : FindStyle.Ink, Color.white, hit);
				if ((bool)heart)
				{
					heart.color = Color.Lerp(heartColor, Color.white, hit);
				}
				red.a = 0.42f * (1f - Mathf.SmoothStep(0f, 0.6f, t));
				damageFlash.color = red;
				yield return null;
			}
			ResetLifeLoss();
			lifeLoss = null;
		}

		private void ResetLifeLoss()
		{
			if ((bool)health)
			{
				health.localScale = healthScale;
				health.anchoredPosition = healthOrigin;
				healthPaper.color = healthColor;
				if ((bool)heart)
				{
					heart.color = heartColor;
				}
				game.UI.lives.color = ((game.Risk.Lives <= 2) ? FindStyle.Hex("dc5076") : FindStyle.Ink);
			}
			if ((bool)damageFlash)
			{
				damageFlash.gameObject.SetActive(false);
			}
		}

		public void Impact(int category, int chain, bool full, Vector3 cardOrigin, int points = 0)
		{
			Play(8);
			Play(3);
			CollectionPops++;
			string word = (full ? "BOX CLEAR!" : ((chain >= 8) ? "ON FIRE!" : comicWords[(Mathf.Max(1, chain) - 1) % comicWords.Length]));
			Vector3 world = NearCard(cardOrigin);
			float power = 0.9f + (float)Mathf.Min(chain, 8) * 0.04f;
			Burst(world, word + "\n" + ((points > 0) ? ("+" + points) : ("x" + chain)), true, power);
			Pulse(cardOrigin, 190 + Mathf.Min(chain, 8) * 10, FindStyle.Colors[category], 0.28f);
			if (chain >= 2)
			{
				Pulse(cardOrigin, 130f, Color.white, 0.18f);
			}
			if (full || chain >= 2)
			{
				Kick(full ? 1.2f : Mathf.Min(0.45f + (float)chain * 0.07f, 1.05f));
			}
		}

		private Vector3 NearCard(Vector3 origin)
		{
			Vector3 local = layer.InverseTransformPoint(origin) + Vector3.up * 24f;
			local.x = Mathf.Clamp(local.x, layer.rect.xMin + 260f, layer.rect.xMax - 260f);
			local.y = Mathf.Clamp(local.y, layer.rect.yMin + 150f, layer.rect.yMax - 150f);
			return layer.TransformPoint(local);
		}

		private void Pulse(Vector3 world, float size, Color color, float duration)
		{
			Image image = rings[ringIndex++ % rings.Length];
			image.gameObject.SetActive(true);
			image.rectTransform.position = world;
			image.rectTransform.sizeDelta = Vector2.one * size;
			StartCoroutine(Ring(image, color, duration));
		}

		private IEnumerator Ring(Image image, Color c, float duration)
		{
			for (float t = 0f; t < duration; t += Time.deltaTime)
			{
				float u = t / duration;
				image.rectTransform.localScale = Vector3.one * Mathf.Lerp(0.4f, 1.6f, FindStyle.Ease(u));
				c.a = (1f - u) * 0.8f;
				image.color = c;
				yield return null;
			}
			image.gameObject.SetActive(false);
		}

		private void Kick(float strength = 1f)
		{
			if ((bool)stage)
			{
				if (recoil != null)
				{
					StopCoroutine(recoil);
					stage.anchoredPosition = recoilOrigin;
				}
				recoilOrigin = stage.anchoredPosition;
				recoil = StartCoroutine(Recoil(strength));
			}
		}

		private IEnumerator Recoil(float strength)
		{
			float duration = 0.16f * strength;
			for (float t = 0f; t < duration; t += Time.deltaTime)
			{
				float u = t / duration;
				stage.anchoredPosition = recoilOrigin + new Vector2(Mathf.Sin(u * 3.14159265f * 4f) * 7f * (1f - u), Mathf.Sin(u * 3.14159265f * 2f) * 4f * (1f - u)) * strength;
				yield return null;
			}
			stage.anchoredPosition = recoilOrigin;
			recoil = null;
		}

		public void Celebration()
		{
			if (!victoryFX)
			{
				victoryFX = ComicVictory.Create(layer);
			}
			victoryFX.Play(game.UI.modal.position + layer.TransformVector(Vector3.up * 80f));
			Kick(1.45f);
		}

		public void Burst(Vector3 world, string text, bool confetti = true, float power = 1f)
		{
			if (confetti)
			{
				StartCoroutine(Particles(world));
			}
			int slot = -1;
			for (int i = 0; i < floats.Length; i++)
			{
				int candidate = floatIndex++ % floats.Length;
				if (!floats[candidate].gameObject.activeSelf)
				{
					slot = candidate;
					break;
				}
			}
			if (slot >= 0)
			{
				StartCoroutine(Float(slot, world, text, power));
			}
			switch (text)
			{
			default:
				if (!(text == "WOW!"))
				{
					break;
				}
				goto case "NICE!";
			case "NICE!":
			case "SWEET!":
			case "ON FIRE!":
				Play(3);
				break;
			}
		}

		private IEnumerator Particles(Vector3 world)
		{
			Image[] batch = new Image[12];
			Vector2[] dirs = new Vector2[12];
			for (int i = 0; i < 12; i++)
			{
				batch[i] = sparks[particleIndex++ % sparks.Length];
				batch[i].sprite = ((i % 4 == 0) ? star : round);
				float a = 3.14159265f * 2f * (float)i / 12f;
				dirs[i] = new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * UnityEngine.Random.Range(65, 145);
				batch[i].rectTransform.sizeDelta = ((i % 3 == 0) ? new Vector2(16f, 16f) : new Vector2(26f, 8f));
				batch[i].rectTransform.localRotation = Quaternion.Euler(0f, 0f, a * 57.29578f);
				batch[i].gameObject.SetActive(true);
			}
			Vector3 local = layer.InverseTransformPoint(world);
			for (float t = 0f; t < 0.42f; t += Time.deltaTime)
			{
				float u = t / 0.42f;
				for (int j = 0; j < 12; j++)
				{
					batch[j].rectTransform.localPosition = local + (Vector3)(dirs[j] * FindStyle.Ease(u)) + Vector3.down * (u * u * 25f);
					batch[j].rectTransform.localScale = Vector3.one * (1f - u * 0.8f);
					Color c = FindStyle.Colors[j % 3];
					c.a = 1f - u;
					batch[j].color = c;
				}
				yield return null;
			}
			Image[] array = batch;
			foreach (Image p in array)
			{
				p.gameObject.SetActive(false);
			}
		}

		private IEnumerator Float(int slot, Vector3 world, string text, float power)
		{
			Text label = floats[slot];
			Image badge = badges[slot];
			bool comic = text.Contains("\n");
			bool loss = text.Contains("-1 LIFE");
			bool special = !text.StartsWith("+") && text != "Whoosh!";
			badge.transform.SetAsLastSibling();
			label.transform.SetAsLastSibling();
			label.gameObject.SetActive(true);
			badge.gameObject.SetActive(special);
			label.text = text;
			label.fontSize = (comic ? 70 : (special ? 86 : 58));
			label.rectTransform.sizeDelta = (comic ? new Vector2(300f, 180f) : new Vector2(650f, 160f));
			badge.rectTransform.sizeDelta = (comic ? new Vector2(320f, 210f) : new Vector2(580f, 270f));
			Vector3 local = layer.InverseTransformPoint(world);
			for (float t = 0f; t < 0.7f; t += Time.deltaTime)
			{
				float u = t / 0.7f;
				float scale = ((u < 0.2f) ? Mathf.Lerp(0.45f, 1.3f, FindStyle.Ease(u / 0.2f)) : Mathf.Lerp(1.3f, 1f, (u - 0.2f) / 0.8f));
				label.rectTransform.localPosition = local + Vector3.up * (comic ? (u * 36f) : (32f + u * 105f));
				badge.rectTransform.localPosition = label.rectTransform.localPosition;
				Quaternion quaternion3 = (label.rectTransform.localRotation = (badge.rectTransform.localRotation = Quaternion.Euler(0f, 0f, comic ? ((slot % 2 == 0) ? (-7) : 7) : 0)));
				Vector3 vector3 = (label.rectTransform.localScale = (badge.rectTransform.localScale = Vector3.one * (scale * (comic ? power : 1f))));
				float alpha = 1f - Mathf.SmoothStep(0f, 1f, Mathf.Clamp01((u - 0.55f) / 0.45f));
				Color ink = (loss ? FindStyle.Hex("d51d43") : FindStyle.Ink);
				ink.a = alpha;
				label.color = ink;
				Color paper = (loss ? FindStyle.Hex("ffb7c6") : ((!comic) ? ((text == "-1 LIFE") ? new Color(1f, 0.66f, 0.74f) : Color.white) : ((slot % 3 == 0) ? FindStyle.Hex("ffe45a") : ((slot % 3 == 1) ? FindStyle.Hex("70dfef") : FindStyle.Hex("ffb96a")))));
				paper.a = alpha;
				badge.color = paper;
				yield return null;
			}
			label.gameObject.SetActive(false);
			badge.gameObject.SetActive(false);
		}

		public void Clear()
		{
			StopAllCoroutines();
			if ((bool)victoryFX)
			{
				victoryFX.Cancel();
			}
			ResetLifeLoss();
			lifeLoss = null;
			foreach (CardShatter effect in shattered)
			{
				if ((bool)effect)
				{
					UnityEngine.Object.Destroy(effect.gameObject);
				}
			}
			shattered.Clear();
			if ((bool)audio)
			{
				audio.StopAll();
			}
			if (recoil != null && (bool)stage)
			{
				stage.anchoredPosition = recoilOrigin;
			}
			recoil = null;
			Image[] array = rings;
			foreach (Image p in array)
			{
				if ((bool)p)
				{
					p.gameObject.SetActive(false);
				}
			}
			Image[] array2 = sparks;
			foreach (Image p2 in array2)
			{
				if ((bool)p2)
				{
					p2.gameObject.SetActive(false);
				}
			}
			Text[] array3 = floats;
			foreach (Text p3 in array3)
			{
				if ((bool)p3)
				{
					p3.gameObject.SetActive(false);
				}
			}
			Image[] array4 = badges;
			foreach (Image p4 in array4)
			{
				if ((bool)p4)
				{
					p4.gameObject.SetActive(false);
				}
			}
		}
	}
}
