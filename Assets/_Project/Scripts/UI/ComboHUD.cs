using UnityEngine;
using UnityEngine.UI;
namespace LittleFinds {
public class ComboHUD : MonoBehaviour {
 GameManager game;Image fill,track,tip;WarningStripes stripes;float phase,lostAt=-10,barStart=280,barWidth=700;bool wasBreaking;Text caption,level;RectTransform captionRect;float lastHit=-10;int previous;
 int displayedLevel=-1;bool displayedLesson;bool first=true;
 public int UrgencyStage {get;private set;}
 public static ComboHUD Create(GameManager game){
  var root=new GameObject("Combo HUD",typeof(RectTransform),typeof(ComboHUD));var rt=(RectTransform)root.transform;rt.SetParent(game.Feedback.layer.parent,false);
  rt.anchorMin=rt.anchorMax=rt.pivot=new Vector2(.5f,1);rt.anchoredPosition=new Vector2(0,-396);rt.sizeDelta=new Vector2(980,64);root.transform.SetSiblingIndex(game.Feedback.layer.GetSiblingIndex());
  var hud=root.GetComponent<ComboHUD>();hud.game=game;
  hud.caption=Label(game.Feedback.font,rt,"Combo Label","COMBO",52,new Vector2(264,104));hud.caption.alignment=TextAnchor.MiddleLeft;hud.caption.horizontalOverflow=HorizontalWrapMode.Overflow;hud.captionRect=hud.caption.rectTransform;hud.captionRect.anchorMin=hud.captionRect.anchorMax=hud.captionRect.pivot=new Vector2(0,.5f);hud.captionRect.anchoredPosition=new Vector2(0,-32);
  hud.track=Bar(rt,"Combo Track",game.Feedback.round,FindStyle.Hex("d6d9e8"));var bar=hud.track.rectTransform;bar.anchorMin=bar.anchorMax=bar.pivot=new Vector2(0,.5f);bar.anchoredPosition=new Vector2(280,-32);bar.sizeDelta=new Vector2(700,26);
  hud.fill=Bar(bar,"Combo Time",game.Feedback.round,FindStyle.Hex("ffb238"));var fr=hud.fill.rectTransform;fr.anchorMin=Vector2.zero;fr.anchorMax=Vector2.one;fr.offsetMin=fr.offsetMax=Vector2.zero;
  var pattern=new GameObject("Combo Warning Stripes",typeof(RectTransform),typeof(CanvasRenderer),typeof(WarningStripes));var pr=(RectTransform)pattern.transform;pr.SetParent(fr,false);pr.anchorMin=Vector2.zero;pr.anchorMax=Vector2.one;pr.offsetMin=pr.offsetMax=Vector2.zero;hud.stripes=pattern.GetComponent<WarningStripes>();hud.stripes.raycastTarget=false;pattern.SetActive(false);
  hud.tip=Bar(bar,"Combo Burning Tip",game.Feedback.round,Color.white);var tr=hud.tip.rectTransform;tr.anchorMin=tr.anchorMax=new Vector2(0,.5f);tr.pivot=new Vector2(.5f,.5f);tr.sizeDelta=new Vector2(12,22);hud.tip.gameObject.SetActive(false);
  hud.level=Label(game.Feedback.font,rt.parent,"Level Indicator","LEVEL 1",68,new Vector2(310,108));var lr=hud.level.rectTransform;lr.anchorMin=lr.anchorMax=lr.pivot=new Vector2(1,1);lr.anchoredPosition=new Vector2(0,-12);
  return hud;
 }
 static Text Label(Font font,Transform parent,string name,string value,int size,Vector2 dimensions){var o=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Text));o.transform.SetParent(parent,false);var t=o.GetComponent<Text>();t.font=font;t.text=value;t.fontSize=size;t.fontStyle=FontStyle.Normal;t.alignment=TextAnchor.MiddleCenter;t.color=FindStyle.Ink;t.raycastTarget=false;t.rectTransform.sizeDelta=dimensions;return t;}
 static Image Bar(Transform parent,string name,Sprite sprite,Color color){var o=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image));o.transform.SetParent(parent,false);var i=o.GetComponent<Image>();i.sprite=sprite;i.type=Image.Type.Sliced;i.color=color;i.raycastTarget=false;return i;}
 void Update(){Render(Time.unscaledDeltaTime);}
 public void RefreshNow(){Render(0);}
 void Render(float delta){
  bool lesson=game.Tutorial&&game.Tutorial.InLesson;
  if(first||displayedLevel!=game.LevelIndex||displayedLesson!=lesson){level.text=lesson?"PRACTICE":"LEVEL "+(game.LevelIndex+1);displayedLevel=game.LevelIndex;displayedLesson=lesson;}
  int combo=game.Streak;
  if(!first&&previous>0&&combo==0&&game.State==GameState.Playing&&!game.UI.overlay.activeSelf)lostAt=Time.unscaledTime;
  bool breaking=combo==0&&Time.unscaledTime-lostAt<.5f;
  if(first||combo!=previous||breaking!=wasBreaking){if(combo>previous)lastHit=Time.unscaledTime;caption.text=breaking?"BREAK!":combo>0?"COMBO x"+combo:"COMBO";caption.fontSize=52+Mathf.RoundToInt(24*Mathf.Clamp01((combo-1)/11f));
   // Reserve room for the largest hit pulse, so growing lettering never touches the bar.
   barStart=Mathf.Max(280,caption.preferredWidth*1.25f+36);barWidth=980-barStart;
   captionRect.sizeDelta=new Vector2(Mathf.Max(264,caption.preferredWidth+16),104);
   track.rectTransform.sizeDelta=new Vector2(barWidth,26);previous=combo;wasBreaking=breaking;}first=false;
  float fraction=game.ComboFraction;bool active=combo>0;
  UrgencyStage=!active?0:fraction<=.25f?2:fraction<=.5f?1:0;
  bool ticking=active&&game.State==GameState.Playing&&!game.UI.overlay.activeSelf;
  float pressure=active?Mathf.Clamp01(1-fraction*2):0;
  if(ticking)phase+=delta*Mathf.Lerp(1.5f,5,pressure)*Mathf.PI*2;
  float beat=ticking&&UrgencyStage>0?Mathf.Pow(Mathf.Max(0,Mathf.Sin(phase)),3):0;
  Color normal=FindStyle.Hex(combo>=8?"38aef2":"31bfa2"),amber=FindStyle.Hex("ffa52e"),red=FindStyle.Hex("ef3156");
  Color activeColor=UrgencyStage==2?red:UrgencyStage==1?amber:normal;
  fill.color=Color.Lerp(activeColor,Color.white,beat*.32f);
  caption.color=breaking?red:active?activeColor:FindStyle.Soft;
  track.color=Color.Lerp(FindStyle.Hex("d6d9e8"),activeColor,active&&UrgencyStage>0?.16f+beat*.28f:0);
  fill.rectTransform.anchorMax=new Vector2(fraction,1);
  track.rectTransform.localScale=new Vector3(1,1+beat*(UrgencyStage==2?.38f:.16f),1);
  track.rectTransform.anchoredPosition=new Vector2(barStart+(ticking&&UrgencyStage==2?Mathf.Sin(phase*2)*2.5f*pressure:0),-32);
  stripes.gameObject.SetActive(active&&UrgencyStage>0);stripes.Phase=phase*7;stripes.color=new Color(.145f,.169f,.294f,.18f+beat*.14f);
  tip.gameObject.SetActive(active&&UrgencyStage>0);tip.rectTransform.anchoredPosition=new Vector2(barWidth*fraction,0);tip.rectTransform.localScale=Vector3.one*(1+beat*.35f);tip.color=Color.Lerp(Color.white,FindStyle.Hex("ffe9b0"),beat);
  float hit=Mathf.Clamp01(1-(Time.unscaledTime-lastHit)/.23f);captionRect.localScale=Vector3.one*(1+hit*.18f+beat*.055f);
  captionRect.anchoredPosition=new Vector2(ticking&&UrgencyStage==2?Mathf.Sin(phase*1.5f)*2:0,-32);
 }
}
}
