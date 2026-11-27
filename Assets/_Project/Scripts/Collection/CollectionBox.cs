using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace LittleFinds {
// The slot, front lip and clipping layer give collected cards a visible destination.
public class CollectionBox : MonoBehaviour {
 public int category;
 public RectTransform mouth,insertionLayer,storedPoint;
 public Image frontLip,progressFill,check,categoryIcon;
 public Image[] stamps;
 public Text title;
 Coroutine fillMotion,landing,mouthMotion;
 Vector3 restScale=Vector3.one;
 int waiting,laneIndex;
 public float Progress=>progressFill.rectTransform.anchorMax.x;
 public int StoredCount {get;private set;}
 void Awake(){restScale=transform.localScale;LayoutHeader();}
 public Vector3 Approach(float lane){return mouth.TransformPoint(mouth.rect.center+new Vector2(lane,64));}
 public Vector3 Inside(float lane){return storedPoint.TransformPoint(new Vector3(lane,0,0));}
 public Vector3 MouthCenter=>mouth.TransformPoint(mouth.rect.center);
 public float OpenForCard(){
  waiting++;float lane=(laneIndex++%3-1)*64;
  if(mouthMotion!=null)StopCoroutine(mouthMotion);mouthMotion=StartCoroutine(MouthTo(new Vector3(1.03f,1.18f,1)));
  return lane;
 }
 public void Configure(int id,Sprite icon){category=id;title.text=FindStyle.Names[id];categoryIcon.sprite=icon;progressFill.color=FindStyle.Colors[id];LayoutHeader();}
 public void LayoutHeader(){
  if(!title||!categoryIcon)return;
  const float gap=8,padding=16;
  float width=((RectTransform)transform).rect.width;
  var icon=categoryIcon.rectTransform;var label=title.rectTransform;
  float textWidth=Mathf.Min(title.preferredWidth+14,Mathf.Max(0,width-padding*2-icon.rect.width-gap));
  float left=(width-icon.rect.width-gap-textWidth)*.5f;
  icon.anchoredPosition=new Vector2(left,icon.anchoredPosition.y);
  label.anchoredPosition=new Vector2(left+icon.rect.width+gap,label.anchoredPosition.y);
  label.sizeDelta=new Vector2(textWidth,label.sizeDelta.y);
 }
 public void ResetBox(){
  StopAllCoroutines();fillMotion=landing=mouthMotion=null;waiting=laneIndex=0;StoredCount=0;
  transform.localScale=restScale;mouth.localScale=Vector3.one;
  foreach(var stamp in stamps){stamp.gameObject.SetActive(false);stamp.sprite=null;}
  SetFill(0);CompleteVisual(false);
 }
 public void Accept(Sprite item,int count){
  StoredCount=count;waiting=Mathf.Max(0,waiting-1);
  if(count>0&&count<=stamps.Length){var stamp=stamps[count-1];stamp.sprite=item;stamp.gameObject.SetActive(true);}
  if(fillMotion!=null)StopCoroutine(fillMotion);fillMotion=StartCoroutine(FillTo(count/4f));
  if(landing!=null)StopCoroutine(landing);landing=StartCoroutine(Settle());
  if(waiting==0){if(mouthMotion!=null)StopCoroutine(mouthMotion);mouthMotion=StartCoroutine(MouthTo(Vector3.one));}
 }
 public void SummaryFrom(CollectionBox source){
  StopAllCoroutines();Configure(source.category,source.categoryIcon.sprite);StoredCount=source.StoredCount;
  for(int i=0;i<stamps.Length;i++){stamps[i].sprite=source.stamps[i].sprite;stamps[i].gameObject.SetActive(i<StoredCount);}
  SetFill(StoredCount/4f);CompleteVisual(StoredCount>=4);
 }
 void SetFill(float amount){progressFill.rectTransform.anchorMax=new Vector2(Mathf.Clamp01(amount),1);}
 void CompleteVisual(bool full){check.gameObject.SetActive(full);frontLip.color=full?FindStyle.Hex("fff0bc"):Color.Lerp(Color.white,FindStyle.Colors[category],.15f);mouth.GetComponent<Image>().color=FindStyle.Hex(full?"aaa8a0":"252b4b");}
 IEnumerator FillTo(float target){float from=Progress;for(float t=0;t<.19f;t+=Time.deltaTime){SetFill(Mathf.Lerp(from,target,FindStyle.Ease(t/.19f)));yield return null;}SetFill(target);CompleteVisual(target>=1);fillMotion=null;}
 IEnumerator Settle(){
  for(float t=0;t<.24f;t+=Time.deltaTime){float u=t/.24f;float squash=Mathf.Sin(u*Mathf.PI*2)*(1-u);transform.localScale=Vector3.Scale(restScale,new Vector3(1+.025f*squash,1-.055f*squash,1));yield return null;}
  transform.localScale=restScale;landing=null;
 }
 IEnumerator MouthTo(Vector3 target){Vector3 from=mouth.localScale;for(float t=0;t<.10f;t+=Time.deltaTime){mouth.localScale=Vector3.Lerp(from,target,t/.10f);yield return null;}mouth.localScale=target;mouthMotion=null;}
}
}
