using UnityEngine;
using UnityEngine.UI;
namespace LittleFinds {
public class ScoreHUD:MonoBehaviour {
 GameManager game;Text number;float hitAge=1;bool negative;
 public static ScoreHUD Create(GameManager game){
  var o=new GameObject("Score HUD",typeof(RectTransform),typeof(ScoreHUD));var r=(RectTransform)o.transform;r.SetParent(game.Feedback.layer.parent,false);r.anchorMin=r.anchorMax=new Vector2(0,1);r.pivot=new Vector2(0,.5f);r.sizeDelta=new Vector2(340,128);r.SetSiblingIndex(game.Feedback.layer.GetSiblingIndex());
  var hud=o.GetComponent<ScoreHUD>();hud.game=game;hud.number=Label(r,game.Feedback.font,"Score Value",82,new Vector2(0,-20),new Vector2(330,88));hud.SetScore(game.Score,0);hud.Align();return hud;
 }
 static Text Label(Transform parent,Font font,string name,int size,Vector2 position,Vector2 dimensions){var o=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Text));var r=(RectTransform)o.transform;r.SetParent(parent,false);r.anchorMin=r.anchorMax=r.pivot=new Vector2(0,1);r.anchoredPosition=position;r.sizeDelta=dimensions;var t=o.GetComponent<Text>();t.font=font;t.fontSize=size;t.fontStyle=FontStyle.Normal;t.alignment=TextAnchor.MiddleLeft;t.color=FindStyle.Ink;t.raycastTarget=false;return t;}
 public void SetScore(int score,int delta){number.text=score.ToString();negative=delta<0;hitAge=delta==0?1:0;Animate(0);}
 void Align(){if(game&&game.Trash)((RectTransform)transform).anchoredPosition=new Vector2(48,game.Trash.target.anchoredPosition.y);}
 void LateUpdate(){Align();Animate(Time.unscaledDeltaTime);}
 void Animate(float dt){hitAge+=dt;float hit=1-Mathf.Clamp01(hitAge/.3f);float pop=Mathf.Sin(Mathf.Clamp01(hitAge/.3f)*Mathf.PI);number.rectTransform.localScale=Vector3.one*(1+.2f*pop);number.rectTransform.anchoredPosition=new Vector2(negative?Mathf.Sin(hitAge*48)*5*hit:0,-20+pop*3);number.color=Color.Lerp(FindStyle.Ink,FindStyle.Hex(negative?"ef3156":"269f89"),hit);}
}
}
