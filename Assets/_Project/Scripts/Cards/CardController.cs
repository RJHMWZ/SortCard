using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
namespace LittleFinds {
public class CardController:MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler,
#if !UNITY_LUNA
 IPointerMoveHandler,
#endif
 IPointerDownHandler,IPointerUpHandler,IBeginDragHandler,IDragHandler,IEndDragHandler {
 public RectTransform visual,rotator; public GameObject front,back; public Image illustration,frontPaper,shine; public Text title,subtitle,number;
 public CanvasGroup group; public CardState State {get;private set;}=CardState.FaceDown;
 public FindData Data {get;private set;} public GameManager Game {get;private set;}
 public Vector3 HomePosition=>((RectTransform)home).TransformPoint(((RectTransform)home).rect.center);
 public float FaceUpDuration=>Game.Tutorial&&Game.Tutorial.InLesson?4.5f:Game.Level.OpenSeconds;
 public int Index {get;private set;} public bool IsOpen=>State==CardState.FaceUp||State==CardState.Flipping;
 float age; bool hovered,collectQueued; Vector2 tilt; RectTransform rt; Transform home;
#if UNITY_LUNA
 Camera hoverCamera;
#endif
 public bool IsDragging {get;private set;}
 public bool CanDragToTrash=>Game&&Game.State==GameState.Playing&&!IsDragging&&(State==CardState.FaceDown||State==CardState.FaceUp);
 float ignoreClickUntil;
 void Awake(){rt=(RectTransform)transform;home=rt.parent;}
 public void Bind(GameManager game,int index,FindData data){
  StopAllCoroutines();Game=game;Index=index;Data=data;collectQueued=false;age=0;hovered=false;tilt=Vector2.zero;IsDragging=false;ignoreClickUntil=0;State=CardState.FaceDown;
  if(!rt)rt=(RectTransform)transform;if(!home)home=rt.parent;rt.SetParent(home,false);rt.anchoredPosition=Vector2.zero;rt.localRotation=Quaternion.identity;rt.localScale=Vector3.one;visual.localScale=Vector3.one;visual.localRotation=Quaternion.identity;visual.anchoredPosition=Vector2.zero;
  group.alpha=1;group.blocksRaycasts=true;rotator.localRotation=Quaternion.identity;front.SetActive(false);back.SetActive(true);
  title.text=data.title;subtitle.text="";subtitle.gameObject.SetActive(false);number.text="";illustration.sprite=data.art;
  RefreshCategoryVisual();gameObject.SetActive(true);
 }
 void Update(){
  if(Game==null)return;
#if UNITY_LUNA
  // Luna has no IPointerMoveHandler; keep desktop hover motion through polling.
  if(hovered)UpdateHover(Input.mousePosition,hoverCamera);
#endif
  if(State==CardState.FaceUp&&!IsDragging&&Game.State==GameState.Playing&&(!Game.Tutorial||Game.Tutorial.AllowAutoReturn)){age+=Time.deltaTime;if(age>=FaceUpDuration)Return();}
  if(State==CardState.FaceDown||State==CardState.FaceUp){float k=1-Mathf.Exp(-22*Time.deltaTime);visual.localScale=Vector3.Lerp(visual.localScale,Vector3.one*(hovered?1.045f:1),k);visual.anchoredPosition=Vector2.Lerp(visual.anchoredPosition,new Vector2(0,hovered?12:0),k);visual.localRotation=Quaternion.Slerp(visual.localRotation,Quaternion.Euler(tilt.y,tilt.x,0),k);}
 }
 public void OnPointerClick(PointerEventData e){if(!e.dragging&&Time.unscaledTime>=ignoreClickUntil)Tap();}
 public void Tap(){if(Game==null||Game.State!=GameState.Playing||IsDragging)return;if(Game.Tutorial&&!Game.Tutorial.CanTap(this))return;if(State==CardState.FaceDown){if(Game.Cards.OpenCount>=3){if(Game.Tutorial&&Game.Tutorial.OnLimitReached(this))return;Game.UI.Message("Three little finds at a time. Tap one again to collect.");return;}StartCoroutine(Flip(true));Game.Feedback.Play(0);}else if(State==CardState.Flipping){collectQueued=true;}else if(State==CardState.FaceUp)Game.Collect(this);}
 public void SetDragging(bool value){IsDragging=value;age=0;hovered=false;tilt=Vector2.zero;visual.localScale=Vector3.one;visual.localRotation=Quaternion.identity;visual.anchoredPosition=Vector2.zero;if(!value)ignoreClickUntil=Time.unscaledTime+.1f;}
 public void OnBeginDrag(PointerEventData e){if(Game&&Game.Trash)Game.Trash.BeginDrag(this,e);}
 public void OnDrag(PointerEventData e){if(Game&&Game.Trash)Game.Trash.MoveDrag(this,e);}
 public void OnEndDrag(PointerEventData e){if(Game&&Game.Trash)Game.Trash.EndDrag(this,e);}
 IEnumerator Flip(bool reveal){
  State=reveal?CardState.Flipping:CardState.Returning;collectQueued=false;hovered=false;tilt=Vector2.zero;
  visual.localRotation=Quaternion.identity;visual.localScale=Vector3.one;visual.anchoredPosition=Vector2.zero;
  float duration=reveal?.25f:Game.Tutorial&&Game.Tutorial.InLesson?.28f:Game.Level.ReturnSeconds;
  for(float t=0;t<duration;t+=Time.deltaTime){
   float a=Mathf.Lerp(reveal?0:180,reveal?180:0,Mathf.SmoothStep(0,1,t/duration));
   rotator.localRotation=Quaternion.Euler(0,a,0);front.SetActive(a>=90);back.SetActive(a<90);yield return null;
  }
  rotator.localRotation=Quaternion.Euler(0,reveal?180:0,0);front.SetActive(reveal);back.SetActive(!reveal);
  State=reveal?CardState.FaceUp:CardState.FaceDown;age=0;visual.localScale=Vector3.one*(reveal?1.055f:1);
  
  if(Game.Tutorial){if(reveal)Game.Tutorial.OnRevealed(this);else Game.Tutorial.OnReturned();}
  if(reveal&&collectQueued){collectQueued=false;Game.Collect(this);}
 }
 public void RefreshCategoryVisual(){
  if(Data==null||Game==null)return;
  int c=Data.category;
  subtitle.text="";subtitle.gameObject.SetActive(false);
  number.text="";number.gameObject.SetActive(false);subtitle.color=FindStyle.Ink;
  frontPaper.color=c>=0&&c<FindStyle.Colors.Length?Color.Lerp(Color.white,FindStyle.Colors[c],.10f):Color.white;
 }
 public void ResetReturnClock(){age=0;}
 public void PreviewForTutorial(){if(Game&&Game.Tutorial&&Game.Tutorial.InLesson&&State==CardState.FaceDown)StartCoroutine(Flip(true));}
 public void Shatter(){StopAllCoroutines();State=CardState.Removed;group.blocksRaycasts=false;hovered=false;Game.Feedback.Shatter(this);group.alpha=0;gameObject.SetActive(false);}
 public void Return(bool penalize=true){if(State!=CardState.FaceUp)return;if(Game.State!=GameState.Playing)return;Game.Feedback.Play(9);StartCoroutine(Flip(false));}
 public void BeginCollect(){StopAllCoroutines();State=CardState.Collecting;hovered=false;tilt=Vector2.zero;visual.localRotation=Quaternion.identity;visual.anchoredPosition=Vector2.zero;group.blocksRaycasts=false;StartCoroutine(Fly());}
 IEnumerator Fly(){
  Vector3 start=rt.position;var box=Game.Collections.BoxFor(Data.category);float lane=box.OpenForCard();
  Vector3 end=box.Approach(lane),bend=(start+end)*.5f+Vector3.right*(Index%2==0?70:-70);
  rt.SetParent(Game.Feedback.layer,true);Vector3 flightScale=rt.localScale;
  for(float t=0;t<.30f;t+=Time.deltaTime){float u=Mathf.SmoothStep(0,1,t/.30f);rt.position=(1-u)*(1-u)*start+2*(1-u)*u*bend+u*u*end;rt.localScale=flightScale*Mathf.Lerp(1,.38f,u);rt.localRotation=Quaternion.Euler(0,0,Mathf.Sin(u*Mathf.PI)*8);group.alpha=1;yield return null;}
  rt.position=end;rt.localScale=flightScale*.38f;rt.localRotation=Quaternion.identity;rt.SetParent(box.insertionLayer,true);Vector3 from=rt.position;
  for(float t=0;t<.15f;t+=Time.deltaTime){rt.position=Vector3.Lerp(from,box.Inside(lane),FindStyle.Ease(t/.15f));yield return null;}
  rt.position=box.Inside(lane);
  State=CardState.Removed;group.alpha=0;gameObject.SetActive(false);Game.Collected(this);
 }
 public void Dismiss(){StopAllCoroutines();State=CardState.Removed;group.blocksRaycasts=false;StartCoroutine(FadeExtra());}
 IEnumerator FadeExtra(){for(float t=0;t<.18f;t+=Time.deltaTime){float u=t/.18f;group.alpha=1-u;rt.localScale=Vector3.one*(1-u*.65f);rt.localRotation=Quaternion.Euler(0,0,u*10);yield return null;}gameObject.SetActive(false);}
 public void Freeze(){if(State==CardState.Collecting)group.alpha=0;StopAllCoroutines();group.blocksRaycasts=false;hovered=false;}
 public void Hide(){Freeze();State=CardState.Removed;gameObject.SetActive(false);}
 public void PrepareShuffle(){StopAllCoroutines();hovered=false;State=CardState.Returning;visual.localRotation=Quaternion.identity;visual.localScale=Vector3.one;visual.anchoredPosition=Vector2.zero;group.blocksRaycasts=false;}
 public void ResetHover(){hovered=false;tilt=Vector2.zero;}
 public void OnPointerEnter(PointerEventData e){
#if UNITY_LUNA
  hoverCamera=e.enterEventCamera;
#endif
  if(!IsDragging&&!Application.isMobilePlatform&&(!Game||!Game.Tutorial||Game.Tutorial.CanTap(this)))hovered=true;
 }
 public void OnPointerExit(PointerEventData e){hovered=false;tilt=Vector2.zero;}
 public void OnPointerMove(PointerEventData e){UpdateHover(e.position,e.pressEventCamera);}
 void UpdateHover(Vector2 position,Camera eventCamera){if(IsDragging||Application.isMobilePlatform||(Game&&Game.Tutorial&&!Game.Tutorial.CanTap(this)))return;RectTransformUtility.ScreenPointToLocalPointInRectangle(rt,position,eventCamera,out var p);tilt=new Vector2(p.x/rt.rect.width*17,-p.y/rt.rect.height*13);if(shine)shine.rectTransform.anchoredPosition=new Vector2(p.x*.45f,p.y*.45f);}
 public void OnPointerDown(PointerEventData e){if(IsDragging||(Game&&Game.Tutorial&&!Game.Tutorial.CanTap(this)))return;if(State==CardState.FaceUp||State==CardState.FaceDown)visual.localScale=Vector3.one*.93f;}
 public void OnPointerUp(PointerEventData e){}
}
}
