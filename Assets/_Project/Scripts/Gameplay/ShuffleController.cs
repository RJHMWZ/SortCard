using UnityEngine;
using System.Collections;
namespace LittleFinds {
public class ShuffleController:MonoBehaviour {
 public GameManager game;public RectTransform center;bool queued,queuedStarting;
 public bool IsQueued=>queued;
 // Fragments are independent visuals; they must not block a fresh deal.
 bool Resolving=>game.Cards.Collecting||(game.Trash&&game.Trash.IsBusy);
 void Update(){if(!TryRefreshEmpty())TryQueuedShuffle();}
 public bool TryRefreshEmpty(){if(game.State!=GameState.Playing||Resolving||game.Cards.BoardCount!=0||(game.Tutorial&&!game.Tutorial.CanShuffle))return false;queued=false;StartCoroutine(Run(false,true));return true;}
 public void Shuffle(){if(game.Tutorial&&!game.Tutorial.CanShuffle)return;if(game.State!=GameState.Playing)return;if(Resolving){queued=true;game.UI.Message("Shuffle queued");return;}if(TryRefreshEmpty())return;queued=false;StartCoroutine(Run(false));}
 public void TryQueuedShuffle(){if(queued&&!queuedStarting&&game.State==GameState.Playing&&!Resolving){queuedStarting=true;StartCoroutine(StartQueued());}}
 IEnumerator StartQueued(){yield return null;queuedStarting=false;if(queued&&game.State==GameState.Playing&&!Resolving)Shuffle();}
 public void InitialDeal(){StartCoroutine(Run(true));}
 public void Cancel(){queued=false;queuedStarting=false;StopAllCoroutines();}
 IEnumerator Run(bool initial,bool automatic=false){
  if(!initial&&!automatic&&!game.Risk.PayForShuffle())yield break;
  queued=false;game.SetState(GameState.Shuffling);game.UI.Message("A FRESH LITTLE MIX");game.Feedback.Play(4);
  var cards=game.Cards.cards;Vector3[] starts=new Vector3[12];
  if(!initial&&!automatic){for(int i=0;i<12;i++){cards[i].PrepareShuffle();starts[i]=cards[i].transform.position;}
   for(float t=0;t<.31f;t+=Time.deltaTime){for(int i=0;i<12;i++){if(!cards[i].gameObject.activeSelf)continue;float u=FindStyle.Ease(Mathf.Clamp01((t-i*.004f)/.26f));cards[i].transform.position=Vector3.Lerp(starts[i],center.position,u);cards[i].transform.localScale=Vector3.one*Mathf.Lerp(1,.08f,u);cards[i].transform.localRotation=Quaternion.Euler(0,0,(i%2==0?45:-45)*u);cards[i].group.alpha=1-u;}yield return null;}
  }
  game.Cards.Deal();for(int i=0;i<12;i++){starts[i]=cards[i].transform.position;cards[i].transform.position=center.position;cards[i].transform.localScale=Vector3.one*.25f;cards[i].group.alpha=0;cards[i].group.blocksRaycasts=false;}
  for(float t=0;t<.47f;t+=Time.deltaTime){for(int i=0;i<12;i++){float u=Mathf.Clamp01((t-i*.012f)/.32f);float eased=1+2.3f*Mathf.Pow(u-1,3)+1.3f*Mathf.Pow(u-1,2);cards[i].transform.position=Vector3.LerpUnclamped(center.position,cards[i].HomePosition,eased);cards[i].transform.localScale=Vector3.one*Mathf.LerpUnclamped(.25f,1,eased);cards[i].transform.localRotation=Quaternion.Euler(0,0,Mathf.Lerp(13,0,u));cards[i].group.alpha=Mathf.Clamp01(u*4);}yield return null;}
  for(int i=0;i<cards.Length;i++){var c=cards[i];c.transform.position=c.HomePosition;c.transform.localScale=Vector3.one;c.transform.localRotation=Quaternion.identity;c.group.alpha=1;c.group.blocksRaycasts=true;}game.SetState(GameState.Playing);if(game.Tutorial)game.Tutorial.OnShuffled();game.UI.Message("Choose a card and see what you find.");
 }
}
}
