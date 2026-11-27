using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
namespace LittleFinds {
public class TrashBin : MonoBehaviour {
 public GameManager game;public RectTransform target;public TrashCanGraphic icon;
 CardController source;RectTransform ghost;CanvasGroup ghostFade;Coroutine flight;int pointer;bool over,committed,penalty;Vector3 ghostScale;
 float nextWiggle,wiggleTime=-1,landingTime;
 public bool IsBusy=>source!=null;
 public RectTransform Ghost=>ghost;
 public bool Available=>game&&game.State==GameState.Playing&&!game.UI.overlay.activeSelf&&(!game.Tutorial||game.Tutorial.CanDiscard);
 public static TrashBin Create(GameManager game){
  var root=new GameObject("Trash Bin",typeof(RectTransform),typeof(TrashBin));
  var rect=(RectTransform)root.transform;rect.SetParent(game.Feedback.layer.parent,false);
  rect.anchorMin=rect.anchorMax=new Vector2(.5f,1);rect.pivot=new Vector2(.5f,.5f);rect.sizeDelta=new Vector2(200,152);
  // Keep the bin below modal overlays and above the board background.
  root.transform.SetSiblingIndex(game.Feedback.layer.GetSiblingIndex());
  var bin=root.GetComponent<TrashBin>();bin.game=game;bin.target=rect;
  var symbol=new GameObject("Trash Can",typeof(RectTransform),typeof(CanvasRenderer),typeof(TrashCanGraphic));var sr=(RectTransform)symbol.transform;sr.SetParent(rect,false);sr.sizeDelta=new Vector2(144,144);sr.anchoredPosition=Vector2.zero;
  sr.anchorMin=sr.anchorMax=sr.pivot=new Vector2(.5f,.5f);sr.anchoredPosition=Vector2.zero;
  bin.icon=symbol.GetComponent<TrashCanGraphic>();bin.icon.color=FindStyle.Ink;bin.icon.raycastTarget=false;
  bin.nextWiggle=Time.unscaledTime+3;
  var layout=rect.parent.GetComponent<MobileGameLayout>();layout.trash=rect;layout.Reflow();return bin;
 }
 void OnEnable(){
  // Upgrade bins serialized with the previous panel design as well.
  var panel=GetComponent<Image>();if(panel)panel.enabled=false;var border=GetComponent<Outline>();if(border)border.enabled=false;
  var caption=transform.Find("Trash Label");if(caption)caption.gameObject.SetActive(false);
  if(target){target.anchorMin=target.anchorMax=new Vector2(.5f,1);target.pivot=new Vector2(.5f,.5f);target.sizeDelta=new Vector2(200,152);}
  if(icon){var rect=icon.rectTransform;rect.anchorMin=rect.anchorMax=rect.pivot=new Vector2(.5f,.5f);rect.sizeDelta=new Vector2(144,144);rect.anchoredPosition=Vector2.zero;}
  nextWiggle=Time.unscaledTime+3;
 }
 void Update(){
  if(IsBusy&&(!Available||!source.gameObject.activeInHierarchy))Cancel();
  target.localScale=Vector3.Lerp(target.localScale,Vector3.one*(over?1.07f:1),1-Mathf.Exp(-24*Time.unscaledDeltaTime));
  float dt=Time.unscaledDeltaTime;icon.LidOpen=Mathf.MoveTowards(icon.LidOpen,committed?1:over?.35f:0,dt*(committed?10:7));
  if(!IsBusy)icon.color=Available?FindStyle.Ink:FindStyle.Soft;
  if(IsBusy||!Available){wiggleTime=-1;nextWiggle=Time.unscaledTime+5.5f;}
  else if(Time.unscaledTime>=nextWiggle&&wiggleTime<0){wiggleTime=0;nextWiggle=Time.unscaledTime+5.5f;}
  float angle=0;if(wiggleTime>=0){wiggleTime+=dt;float u=Mathf.Clamp01(wiggleTime/.65f);angle=Mathf.Sin(u*Mathf.PI*5)*7*(1-u);if(u>=1)wiggleTime=-1;}
  icon.rectTransform.localRotation=Quaternion.Euler(0,0,angle);
  if(landingTime>0){landingTime=Mathf.Max(0,landingTime-dt);float bounce=Mathf.Sin((1-landingTime/.25f)*Mathf.PI)*.12f;icon.rectTransform.localScale=new Vector3(1+bounce,1-bounce,1);}else icon.rectTransform.localScale=Vector3.one;
 }
 public bool BeginDrag(CardController card,PointerEventData e){
  if(IsBusy||!Available||!card.CanDragToTrash||(game.Tutorial&&!game.Tutorial.CanDiscardCard(card)))return false;
  source=card;pointer=e.pointerId;source.SetDragging(true);e.eligibleForClick=false;
  var root=new GameObject("Card Drag Shadow",typeof(RectTransform),typeof(CanvasGroup));ghost=(RectTransform)root.transform;ghost.SetParent(game.Feedback.layer,false);
  ghost.anchorMin=ghost.anchorMax=ghost.pivot=new Vector2(.5f,.5f);ghost.sizeDelta=card.visual.rect.size;ghost.localRotation=Quaternion.identity;
  ghostScale=new Vector3(card.transform.lossyScale.x/game.Feedback.layer.lossyScale.x,card.transform.lossyScale.y/game.Feedback.layer.lossyScale.y,1)*.94f;
  ghost.localScale=ghostScale;ghost.position=card.visual.position;
  ghostFade=root.GetComponent<CanvasGroup>();ghostFade.alpha=.58f;ghostFade.blocksRaycasts=false;ghostFade.interactable=false;
  var copy=Instantiate(card.visual.gameObject,ghost,false);var cr=(RectTransform)copy.transform;cr.anchorMin=cr.anchorMax=cr.pivot=new Vector2(.5f,.5f);cr.localPosition=Vector3.zero;cr.localRotation=Quaternion.identity;cr.localScale=Vector3.one;
  foreach(var graphic in copy.GetComponentsInChildren<Graphic>(true))graphic.raycastTarget=false;
  MoveDrag(card,e);return true;
 }
 public void MoveDrag(CardController card,PointerEventData e){
  if(source!=card||pointer!=e.pointerId||committed||flight!=null)return;
  over=RectTransformUtility.RectangleContainsScreenPoint(target,e.position,e.pressEventCamera);
  // Keep the mouth visible beneath the hovering card shadow.
  ghost.localScale=ghostScale*(over?.5f:1);
  if(RectTransformUtility.ScreenPointToWorldPointInRectangle(game.Feedback.layer,e.position,e.pressEventCamera,out var world))ghost.position=world+game.Feedback.layer.TransformVector(Vector3.up*(over?130:85));
  // A hidden card must not reveal its usefulness through the bin's hover color.
  bool known=card.State==CardState.FaceUp;bool harmful=known&&game.WouldDiscardCostLife(card);
  icon.color=over?(harmful?FindStyle.Hex("d51d43"):known?FindStyle.Hex("258c78"):FindStyle.Ink):FindStyle.Ink;
 }
 public void EndDrag(CardController card,PointerEventData e){
  if(source!=card||pointer!=e.pointerId||flight!=null)return;
  e.eligibleForClick=false;
  committed=Available&&RectTransformUtility.RectangleContainsScreenPoint(target,e.position,e.pressEventCamera);
  penalty=committed&&game.WouldDiscardCostLife(card);
  if(committed){wiggleTime=-1;icon.rectTransform.localRotation=Quaternion.identity;}
  flight=StartCoroutine(FlyShadow(committed));
 }
 IEnumerator FlyShadow(bool discard){
  Vector3 start=ghost.position;Vector3 scale=ghost.localScale;float duration=discard?.36f:.18f;
  for(float t=0;t<duration;t+=Time.deltaTime){
   float u=Mathf.SmoothStep(0,1,t/duration);Vector3 end=discard?icon.rectTransform.TransformPoint(new Vector3(0,Mathf.Lerp(.24f,0,u)*icon.rectTransform.rect.height,0)):source.visual.position;
   Vector3 bend=(start+end)*.5f+game.Feedback.layer.TransformVector(Vector3.up*(discard?100:30));
   ghost.position=(1-u)*(1-u)*start+2*(1-u)*u*bend+u*u*end;
   ghost.localScale=scale*Mathf.Lerp(1,discard?.12f:1.06f,u);ghost.localRotation=Quaternion.Euler(0,0,discard?u*28:0);ghostFade.alpha=Mathf.Lerp(.58f,0,discard?Mathf.Clamp01((u-.55f)/.45f):u);yield return null;
  }
  var card=source;bool charge=penalty;
  // Release ownership before resolving damage: losing the last life cancels all interactions.
  Destroy(ghost.gameObject);ghost=null;ghostFade=null;source=null;flight=null;over=committed=penalty=false;card.SetDragging(false);
  if(discard)landingTime=.25f;
  if(discard){card.Hide();game.Discarded(card,charge);}else card.ResetReturnClock();
 }
 public void Cancel(){
  if(flight!=null)StopCoroutine(flight);flight=null;
  if(source)source.SetDragging(false);source=null;if(ghost)Destroy(ghost.gameObject);ghost=null;ghostFade=null;over=committed=penalty=false;
  landingTime=0;wiggleTime=-1;nextWiggle=Time.unscaledTime+3;
  if(target)target.localScale=Vector3.one;if(icon){icon.color=FindStyle.Ink;icon.LidOpen=0;icon.rectTransform.localRotation=Quaternion.identity;icon.rectTransform.localScale=Vector3.one;}
 }
 void OnDisable(){Cancel();}
}
}
