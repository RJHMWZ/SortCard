using UnityEngine;
namespace LittleFinds {
public class AudioDirector : MonoBehaviour {
 public AudioClip[] clips,flipVariants,impactVariants;
 [Range(0,1)] public float masterVolume=.8f;
 [Range(0,1)] public float victoryGain=.34f,damageGain=.4f,failureGain=.45f;
 readonly AudioSource[] voices=new AudioSource[8];
 readonly float[] last=new float[11];int flipIndex,impactIndex,next;
 public AudioClip damageClip,failureClip;AudioSource damageVoice,victoryVoice;
 // Balance the supplied clips by their audible (non-silent) levels.
 // Flip, collect, full box, combo, shuffle, clear, victory, button, impact, return, tutorial.
 static readonly float[] gains={.6f,.9f,.5f,.65f,1.2f,.95f,.34f,.9f,.75f,.7f,.8f};
 static readonly float[] flipGains={1,.75f,.8f},impactGains={1.1f,1.6f,.92f};
 void Awake(){for(int i=0;i<voices.Length;i++){voices[i]=gameObject.AddComponent<AudioSource>();voices[i].playOnAwake=false;}for(int i=0;i<last.Length;i++)last[i]=-10;
  damageVoice=gameObject.AddComponent<AudioSource>();damageVoice.playOnAwake=false;
  damageVoice.clip=damageClip;
  victoryVoice=gameObject.AddComponent<AudioSource>();victoryVoice.playOnAwake=false;
#if !UNITY_LUNA
  // AudioSource.priority is unavailable in Luna's audio implementation.
  foreach(var voice in voices){voice.spatialBlend=0;voice.priority=100;}
  damageVoice.spatialBlend=0;damageVoice.priority=32;victoryVoice.spatialBlend=0;victoryVoice.priority=16;
#endif
 }
 public void PlayFailure(){if(!failureClip)return;foreach(var voice in voices)voice.Stop();damageVoice.Stop();victoryVoice.Stop();victoryVoice.clip=failureClip;victoryVoice.pitch=1;victoryVoice.volume=masterVolume*failureGain;victoryVoice.Play();}
 public void PlayDamage(){if(!damageClip)return;damageVoice.volume=masterVolume*damageGain;damageVoice.Stop();damageVoice.Play();}
 public void Play(int cue,int chain=0){
  if(cue<0||cue>=clips.Length||Time.unscaledTime-last[cue]<.035f)return;
  last[cue]=Time.unscaledTime;AudioClip clip=clips[cue];float variantGain=1;
  if(cue==0&&flipVariants.Length>0){int index=flipIndex++%flipVariants.Length;clip=flipVariants[index];if(index<flipGains.Length)variantGain=flipGains[index];}
  if(cue==8&&impactVariants.Length>0){int index=impactIndex++%impactVariants.Length;clip=impactVariants[index];if(index<impactGains.Length)variantGain=impactGains[index];}
  if(!clip)return;
  if(cue==6){foreach(var v in voices)v.Stop();damageVoice.Stop();victoryVoice.Stop();victoryVoice.clip=clip;victoryVoice.pitch=1;victoryVoice.volume=masterVolume*victoryGain;victoryVoice.Play();return;}
  AudioSource voice=null;int playing=0;foreach(var v in voices){if(v.isPlaying)playing++;else if(!voice)voice=v;}
  if(!voice)voice=voices[next++%voices.Length];
  voice.Stop();voice.clip=clip;voice.pitch=cue==8?Mathf.Min(1.22f,1+chain*.025f):cue==3?Mathf.Min(1.18f,.96f+chain*.025f):1;
  // Keep busy combos clear: overlapping voices can attenuate a new cue by at most 2 dB.
  float overlapGain=Mathf.Max(.8f,1/Mathf.Sqrt(Mathf.Max(1,playing*.55f)));
  voice.volume=Mathf.Clamp01(masterVolume*gains[Mathf.Min(cue,gains.Length-1)]*variantGain*overlapGain);voice.Play();
 }
 public void StopAll(){foreach(var voice in voices)if(voice)voice.Stop();if(damageVoice)damageVoice.Stop();if(victoryVoice)victoryVoice.Stop();}
}
}
