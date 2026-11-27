using UnityEngine;

namespace LittleFinds
{
	public class AudioDirector : MonoBehaviour
	{
		public AudioClip[] clips;

		public AudioClip[] flipVariants;

		public AudioClip[] impactVariants;

		[Range(0f, 1f)]
		public float masterVolume = 0.8f;

		[Range(0f, 1f)]
		public float victoryGain = 0.34f;

		[Range(0f, 1f)]
		public float damageGain = 0.4f;

		[Range(0f, 1f)]
		public float failureGain = 0.45f;

		private readonly AudioSource[] voices = new AudioSource[8];

		private readonly float[] last = new float[11];

		private int flipIndex;

		private int impactIndex;

		private int next;

		public AudioClip damageClip;

		public AudioClip failureClip;

		private AudioSource damageVoice;

		private AudioSource victoryVoice;

		private static readonly float[] gains = new float[11]
		{
			0.6f, 0.9f, 0.5f, 0.65f, 1.2f, 0.95f, 0.34f, 0.9f, 0.75f, 0.7f,
			0.8f
		};

		private static readonly float[] flipGains = new float[3] { 1f, 0.75f, 0.8f };

		private static readonly float[] impactGains = new float[3] { 1.1f, 1.6f, 0.92f };

		private void Awake()
		{
			for (int j = 0; j < voices.Length; j++)
			{
				voices[j] = base.gameObject.AddComponent<AudioSource>();
				voices[j].playOnAwake = false;
			}
			for (int i = 0; i < last.Length; i++)
			{
				last[i] = -10f;
			}
			damageVoice = base.gameObject.AddComponent<AudioSource>();
			damageVoice.playOnAwake = false;
			damageVoice.clip = damageClip;
			victoryVoice = base.gameObject.AddComponent<AudioSource>();
			victoryVoice.playOnAwake = false;
		}

		public void PlayFailure()
		{
			if ((bool)failureClip)
			{
				AudioSource[] array = voices;
				foreach (AudioSource voice in array)
				{
					voice.Stop();
				}
				damageVoice.Stop();
				victoryVoice.Stop();
				victoryVoice.clip = failureClip;
				victoryVoice.pitch = 1f;
				victoryVoice.volume = masterVolume * failureGain;
				victoryVoice.Play();
			}
		}

		public void PlayDamage()
		{
			if ((bool)damageClip)
			{
				damageVoice.volume = masterVolume * damageGain;
				damageVoice.Stop();
				damageVoice.Play();
			}
		}

		public void Play(int cue, int chain = 0)
		{
			if (cue < 0 || cue >= clips.Length || Time.unscaledTime - last[cue] < 0.035f)
			{
				return;
			}
			last[cue] = Time.unscaledTime;
			AudioClip clip = clips[cue];
			float variantGain = 1f;
			if (cue == 0 && flipVariants.Length != 0)
			{
				int index2 = flipIndex++ % flipVariants.Length;
				clip = flipVariants[index2];
				if (index2 < flipGains.Length)
				{
					variantGain = flipGains[index2];
				}
			}
			if (cue == 8 && impactVariants.Length != 0)
			{
				int index = impactIndex++ % impactVariants.Length;
				clip = impactVariants[index];
				if (index < impactGains.Length)
				{
					variantGain = impactGains[index];
				}
			}
			if (!clip)
			{
				return;
			}
			if (cue == 6)
			{
				AudioSource[] array = voices;
				foreach (AudioSource v2 in array)
				{
					v2.Stop();
				}
				damageVoice.Stop();
				victoryVoice.Stop();
				victoryVoice.clip = clip;
				victoryVoice.pitch = 1f;
				victoryVoice.volume = masterVolume * victoryGain;
				victoryVoice.Play();
				return;
			}
			AudioSource voice = null;
			int playing = 0;
			AudioSource[] array2 = voices;
			foreach (AudioSource v in array2)
			{
				if (v.isPlaying)
				{
					playing++;
				}
				else if (!voice)
				{
					voice = v;
				}
			}
			if (!voice)
			{
				voice = voices[next++ % voices.Length];
			}
			voice.Stop();
			voice.clip = clip;
			AudioSource audioSource = voice;
			float pitch;
			switch (cue)
			{
			default:
				pitch = 1f;
				break;
			case 3:
				pitch = Mathf.Min(1.18f, 0.96f + (float)chain * 0.025f);
				break;
			case 8:
				pitch = Mathf.Min(1.22f, 1f + (float)chain * 0.025f);
				break;
			}
			audioSource.pitch = pitch;
			float overlapGain = Mathf.Max(0.8f, 1f / Mathf.Sqrt(Mathf.Max(1f, (float)playing * 0.55f)));
			voice.volume = Mathf.Clamp01(masterVolume * gains[Mathf.Min(cue, gains.Length - 1)] * variantGain * overlapGain);
			voice.Play();
		}

		public void StopAll()
		{
			AudioSource[] array = voices;
			foreach (AudioSource voice in array)
			{
				if ((bool)voice)
				{
					voice.Stop();
				}
			}
			if ((bool)damageVoice)
			{
				damageVoice.Stop();
			}
			if ((bool)victoryVoice)
			{
				victoryVoice.Stop();
			}
		}
	}
}
