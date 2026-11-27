using UnityEngine;
using UnityEngine.UI;
namespace LittleFinds {
public class CountdownPressure:MonoBehaviour {
 GameManager game;RectTransform clock;Image paper;Text digits;Vector2 origin;Vector3 scale;Color paperColor;float phase,flashAge=1,lastRemaining=-1;int milestoneStage;
 public int UrgencyStage {get;private set;}
 public static CountdownPressure Create(GameManager game){var effect=game.UI.time.transform.parent.gameObject.AddComponent<CountdownPressure>();effect.game=game;effect.clock=(RectTransform)effect.transform;effect.clock.anchoredPosition+=new Vector2((.5f-effect.clock.pivot.x)*effect.clock.rect.width,(.5f-effect.clock.pivot.y)*effect.clock.rect.height);effect.clock.pivot=new Vector2(.5f,.5f);effect.paper=effect.GetComponent<Image>();effect.digits=game.UI.time;effect.origin=effect.clock.anchoredPosition;effect.scale=effect.clock.localScale;effect.paperColor=effect.paper.color;return effect;}
 public void ResetVisual(){if(!clock)return;phase=0;flashAge=1;lastRemaining=-1;Restore();}
 void Restore(){UrgencyStage=0;clock.anchoredPosition=origin;clock.localScale=scale;clock.localRotation=Quaternion.identity;paper.color=paperColor;digits.color=FindStyle.Ink;}
 void Update(){
  if(!game)return;
  bool active=game.State==GameState.Playing&&game.Timer.Running&&!game.UI.overlay.activeSelf&&!game.Tutorial.InLesson;
  float seconds=game.Timer.Remaining;
  if(!active||seconds<=0){
   if(game.State==GameState.Shuffling&&game.Timer.Running&&!game.UI.overlay.activeSelf&&!game.Tutorial.InLesson){Restore();flashAge=1;phase=0;}
   else ResetVisual();
   return;
  }
  // Each milestone flashes once, even if a shuffle cost skips over the exact second.
  bool crossed=lastRemaining>0&&((lastRemaining>30&&seconds<=30)||(lastRemaining>20&&seconds<=20)||(lastRemaining>10&&seconds<=10));
  bool finalEntry=lastRemaining>5&&seconds<=5;
  lastRemaining=seconds;
  if(crossed&&seconds>5){flashAge=0;milestoneStage=seconds<=10?2:1;}
  flashAge+=Time.unscaledDeltaTime;
  float beat,danger=0;
  if(seconds<=5){
   UrgencyStage=3;danger=1-Mathf.Clamp01(seconds/5);
   if(finalEntry)phase=Mathf.PI*.5f;
   float rate=Mathf.Lerp(1.5f,4f,danger);phase+=Time.unscaledDeltaTime*rate*Mathf.PI*2;
   beat=Mathf.Pow(Mathf.Max(0,Mathf.Sin(phase)),3);
  }else if(flashAge<.45f){UrgencyStage=milestoneStage;beat=Mathf.Sin(flashAge/.45f*Mathf.PI);}
  else{Restore();return;}
  Color alarm=FindStyle.Hex(UrgencyStage>=2?"ef3156":"ed8e21");
  digits.color=Color.Lerp(FindStyle.Ink,alarm,UrgencyStage==3?.7f+beat*.3f:beat);
  paper.color=Color.Lerp(paperColor,FindStyle.Hex(UrgencyStage>=2?"ffb7c6":"ffe09a"),beat*(UrgencyStage==3?.4f+.2f*danger:.55f));
  clock.localScale=scale*(1+beat*(UrgencyStage==3?.06f+.06f*danger:.05f));
  clock.anchoredPosition=origin+new Vector2(UrgencyStage==3?Mathf.Sin(phase*2)*3*beat:0,beat*2);
  clock.localRotation=Quaternion.Euler(0,0,UrgencyStage==3?Mathf.Sin(phase)*1.8f*beat:0);
 }
 void OnDisable(){ResetVisual();}
}
}
