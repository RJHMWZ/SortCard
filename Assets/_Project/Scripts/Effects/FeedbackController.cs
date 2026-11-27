using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace LittleFinds {
public class FeedbackController : MonoBehaviour {
 public RectTransform layer;
 public Sprite round, burst, ring, star;public RectTransform stage;public AudioDirector audio; public Font font; 
 public bool SoundOn=true,Haptics=true;
 readonly Image[] sparks=new Image[128],badges=new Image[12];
 readonly Text[] floats=new Text[12];
 readonly Image[] rings=new Image[12];int ringIndex;
 int particleIndex,floatIndex; Coroutine recoil;Vector2 recoilOrigin; GameManager game;
 ComicVictory victoryFX;
 Coroutine lifeLoss;RectTransform health;Image healthPaper,heart;Vector2 healthOrigin;Vector3 healthScale;Color healthColor,heartColor;DamageFlashGraphic damageFlash;
 public bool IsShowingLifeLoss=>lifeLoss!=null;
 public int CollectionPops {get;private set;}
 static readonly string[] comicWords={"POW!","BAM!","NICE!","BOOM!","WOW!","SMASH!"};
 readonly System.Collections.Generic.List<CardShatter> shattered=new System.Collections.Generic.List<CardShatter>();
 public bool IsShattering {get{shattered.RemoveAll(effect=>!effect);return shattered.Count>0;}}
 public void Shatter(CardController card){shattered.RemoveAll(effect=>!effect);shattered.Add(CardShatter.Spawn(card,layer));}
 void Awake(){
  game=GetComponent<GameManager>();SoundOn=PlayerPrefs.GetInt("LittleFinds.Sound",1)==1;Haptics=PlayerPrefs.GetInt("LittleFinds.Haptics",1)==1;
  health=(RectTransform)game.UI.lives.transform.parent;healthPaper=health.GetComponent<Image>();
  var heartTransform=health.Find("Heart");if(heartTransform)heart=heartTransform.GetComponent<Image>();
  healthOrigin=health.anchoredPosition;healthScale=health.localScale;healthColor=healthPaper.color;if(heart)heartColor=heart.color;
  var flashObject=new GameObject("Life Loss Red Border",typeof(RectTransform),typeof(CanvasRenderer),typeof(DamageFlashGraphic));flashObject.transform.SetParent(layer,false);
  damageFlash=flashObject.GetComponent<DamageFlashGraphic>();damageFlash.raycastTarget=false;
  var flashRect=(RectTransform)flashObject.transform;flashRect.anchorMin=Vector2.zero;flashRect.anchorMax=Vector2.one;flashRect.offsetMin=flashRect.offsetMax=Vector2.zero;flashObject.SetActive(false);
  for(int i=0;i<sparks.Length;i++){
   var o=new GameObject("Impact Confetti",typeof(RectTransform),typeof(CanvasRenderer),typeof(Image));o.transform.SetParent(layer,false);
   sparks[i]=o.GetComponent<Image>();sparks[i].sprite=round;sparks[i].raycastTarget=false;o.SetActive(false);
  }
  for(int i=0;i<rings.Length;i++)rings[i]=EffectImage("Impact Ring",ring);
  for(int i=0;i<floats.Length;i++){
   var badge=new GameObject("Comic Burst",typeof(RectTransform),typeof(CanvasRenderer),typeof(Image));badge.transform.SetParent(layer,false);
   badges[i]=badge.GetComponent<Image>();badges[i].sprite=burst;badges[i].raycastTarget=false;badges[i].rectTransform.sizeDelta=new Vector2(580,270);badge.SetActive(false);
   var o=new GameObject("Comic Pop",typeof(RectTransform),typeof(CanvasRenderer),typeof(Text),typeof(Outline));o.transform.SetParent(layer,false);
   var text=o.GetComponent<Text>();text.font=font;text.fontSize=70;text.fontStyle=FontStyle.Normal;text.alignment=TextAnchor.MiddleCenter;text.raycastTarget=false;text.rectTransform.sizeDelta=new Vector2(650,160);
   var outline=o.GetComponent<Outline>();outline.effectColor=Color.white;outline.effectDistance=new Vector2(2,-2);
   floats[i]=text;o.SetActive(false);
  }
 }
 Image EffectImage(string name,Sprite sprite){var o=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Image));o.transform.SetParent(layer,false);var i=o.GetComponent<Image>();i.sprite=sprite;i.raycastTarget=false;o.SetActive(false);return i;}
 public void Play(int index){
  if(Haptics&&(index==0||index==8||index==5))TouchFeedback.Tap();
  if(SoundOn&&audio)audio.Play(index,game?game.Streak:0);
 }
 public void Penalty(string word,Vector3 origin){
  if(SoundOn&&audio)audio.PlayDamage();if(Haptics)TouchFeedback.Damage();
  if(lifeLoss!=null){StopCoroutine(lifeLoss);ResetLifeLoss();}
  game.UI.SyncLives();lifeLoss=StartCoroutine(LifeLoss());
  Pulse(health.TransformPoint(health.rect.center),215,FindStyle.Hex("ef3156"),.5f);
  Burst(NearCard(origin),"-"+GameManager.MistakePoints+"\n-1 LIFE",false);Kick(2.4f);
 }
 IEnumerator LifeLoss(){
  damageFlash.gameObject.SetActive(true);var red=FindStyle.Hex("ef3156");
  for(float t=0;t<.85f;t+=Time.unscaledDeltaTime){
   float u=t/.85f,hit=1-Mathf.SmoothStep(0,1,u);
   health.localScale=healthScale*(1+.4f*hit);health.anchoredPosition=healthOrigin+Vector2.right*(Mathf.Sin(t*58)*14*hit);
   healthPaper.color=Color.Lerp(healthColor,new Color(1,.28f,.36f),hit);
   game.UI.lives.color=Color.Lerp(game.Risk.Lives<=2?FindStyle.Hex("dc5076"):FindStyle.Ink,Color.white,hit);
   if(heart)heart.color=Color.Lerp(heartColor,Color.white,hit);
   red.a=.42f*(1-Mathf.SmoothStep(0,.6f,t));damageFlash.color=red;yield return null;
  }
  ResetLifeLoss();lifeLoss=null;
 }
 void ResetLifeLoss(){if(health){health.localScale=healthScale;health.anchoredPosition=healthOrigin;healthPaper.color=healthColor;if(heart)heart.color=heartColor;game.UI.lives.color=game.Risk.Lives<=2?FindStyle.Hex("dc5076"):FindStyle.Ink;}if(damageFlash)damageFlash.gameObject.SetActive(false);}
 public void Impact(int category,int chain,bool full,Vector3 cardOrigin,int points=0){
  Play(8);Play(3);CollectionPops++;
  string word=full?"BOX CLEAR!":chain>=8?"ON FIRE!":comicWords[(Mathf.Max(1,chain)-1)%comicWords.Length];
  Vector3 world=NearCard(cardOrigin);float power=.9f+Mathf.Min(chain,8)*.04f;
  Burst(world,word+"\n"+(points>0?"+"+points:"x"+chain),true,power);
  Pulse(cardOrigin,190+Mathf.Min(chain,8)*10,FindStyle.Colors[category],.28f);
  if(chain>=2)Pulse(cardOrigin,130,Color.white,.18f);
  if(full||chain>=2)Kick(full?1.2f:Mathf.Min(.45f+chain*.07f,1.05f));
 }

 Vector3 NearCard(Vector3 origin){Vector3 local=layer.InverseTransformPoint(origin)+Vector3.up*24;local.x=Mathf.Clamp(local.x,layer.rect.xMin+260,layer.rect.xMax-260);local.y=Mathf.Clamp(local.y,layer.rect.yMin+150,layer.rect.yMax-150);return layer.TransformPoint(local);}

 void Pulse(Vector3 world,float size,Color color,float duration){var image=rings[ringIndex++%rings.Length];image.gameObject.SetActive(true);image.rectTransform.position=world;image.rectTransform.sizeDelta=Vector2.one*size;StartCoroutine(Ring(image,color,duration));}
 IEnumerator Ring(Image image,Color c,float duration){for(float t=0;t<duration;t+=Time.deltaTime){float u=t/duration;image.rectTransform.localScale=Vector3.one*Mathf.Lerp(.4f,1.6f,FindStyle.Ease(u));c.a=(1-u)*.8f;image.color=c;yield return null;}image.gameObject.SetActive(false);}
 void Kick(float strength=1){if(!stage)return;if(recoil!=null){StopCoroutine(recoil);stage.anchoredPosition=recoilOrigin;}recoilOrigin=stage.anchoredPosition;recoil=StartCoroutine(Recoil(strength));}
 IEnumerator Recoil(float strength){float duration=.16f*strength;for(float t=0;t<duration;t+=Time.deltaTime){float u=t/duration;stage.anchoredPosition=recoilOrigin+new Vector2(Mathf.Sin(u*Mathf.PI*4)*7*(1-u),Mathf.Sin(u*Mathf.PI*2)*4*(1-u))*strength;yield return null;}stage.anchoredPosition=recoilOrigin;recoil=null;}
 public void Celebration(){if(!victoryFX)victoryFX=ComicVictory.Create(layer);victoryFX.Play(game.UI.modal.position+layer.TransformVector(Vector3.up*80));Kick(1.45f);}
 public void Burst(Vector3 world,string text,bool confetti=true,float power=1){
  if(confetti)StartCoroutine(Particles(world));int slot=-1;
  for(int i=0;i<floats.Length;i++){int candidate=floatIndex++%floats.Length;if(!floats[candidate].gameObject.activeSelf){slot=candidate;break;}}
  if(slot>=0)StartCoroutine(Float(slot,world,text,power));
  if(text=="NICE!"||text=="SWEET!"||text=="ON FIRE!"||text=="WOW!")Play(3);
 }
 IEnumerator Particles(Vector3 world){
  var batch=new Image[12];var dirs=new Vector2[12];
  for(int i=0;i<12;i++){
   batch[i]=sparks[particleIndex++%sparks.Length];batch[i].sprite=i%4==0?star:round;float a=Mathf.PI*2*i/12;
   dirs[i]=new Vector2(Mathf.Cos(a),Mathf.Sin(a))*Random.Range(65,145);
   batch[i].rectTransform.sizeDelta=i%3==0?new Vector2(16,16):new Vector2(26,8);
   batch[i].rectTransform.localRotation=Quaternion.Euler(0,0,a*Mathf.Rad2Deg);
   batch[i].gameObject.SetActive(true);
  }
  Vector3 local=layer.InverseTransformPoint(world);
  for(float t=0;t<.42f;t+=Time.deltaTime){
   float u=t/.42f;
   for(int i=0;i<12;i++){
    batch[i].rectTransform.localPosition=local+(Vector3)(dirs[i]*FindStyle.Ease(u))+Vector3.down*(u*u*25);
    batch[i].rectTransform.localScale=Vector3.one*(1-u*.8f);
    Color c=FindStyle.Colors[i%3];c.a=1-u;batch[i].color=c;
   }
   yield return null;
  }
  foreach(var p in batch)p.gameObject.SetActive(false);
 }
 IEnumerator Float(int slot,Vector3 world,string text,float power){
  var label=floats[slot];var badge=badges[slot];bool comic=text.Contains("\n");bool loss=text.Contains("-1 LIFE");bool special=!text.StartsWith("+")&&text!="Whoosh!";
  badge.transform.SetAsLastSibling();label.transform.SetAsLastSibling();
  label.gameObject.SetActive(true);badge.gameObject.SetActive(special);label.text=text;label.fontSize=comic?70:special?86:58;
  label.rectTransform.sizeDelta=comic?new Vector2(300,180):new Vector2(650,160);badge.rectTransform.sizeDelta=comic?new Vector2(320,210):new Vector2(580,270);
  Vector3 local=layer.InverseTransformPoint(world);
  for(float t=0;t<.7f;t+=Time.deltaTime){
   float u=t/.7f;float scale=u<.2f?Mathf.Lerp(.45f,1.3f,FindStyle.Ease(u/.2f)):Mathf.Lerp(1.3f,1,(u-.2f)/.8f);
   label.rectTransform.localPosition=local+Vector3.up*(comic?u*36:32+u*105);badge.rectTransform.localPosition=label.rectTransform.localPosition;
   label.rectTransform.localRotation=badge.rectTransform.localRotation=Quaternion.Euler(0,0,comic?(slot%2==0?-7:7):0);
   label.rectTransform.localScale=badge.rectTransform.localScale=Vector3.one*(scale*(comic?power:1));
   float alpha=1-Mathf.SmoothStep(0,1,Mathf.Clamp01((u-.55f)/.45f));
   Color ink=loss?FindStyle.Hex("d51d43"):FindStyle.Ink;ink.a=alpha;label.color=ink;
   Color paper=loss?FindStyle.Hex("ffb7c6"):comic?(slot%3==0?FindStyle.Hex("ffe45a"):slot%3==1?FindStyle.Hex("70dfef"):FindStyle.Hex("ffb96a")):text=="-1 LIFE"?new Color(1,.66f,.74f):Color.white;paper.a=alpha;badge.color=paper;
   yield return null;
  }
  label.gameObject.SetActive(false);badge.gameObject.SetActive(false);
 }
 public void Clear(){StopAllCoroutines();if(victoryFX)victoryFX.Cancel();ResetLifeLoss();lifeLoss=null;foreach(var effect in shattered)if(effect)Destroy(effect.gameObject);shattered.Clear();if(audio)audio.StopAll();if(recoil!=null&&stage)stage.anchoredPosition=recoilOrigin;recoil=null;foreach(var p in rings)if(p)p.gameObject.SetActive(false);foreach(var p in sparks)if(p)p.gameObject.SetActive(false);foreach(var p in floats)if(p)p.gameObject.SetActive(false);foreach(var p in badges)if(p)p.gameObject.SetActive(false);}
}
}
