using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace LittleFinds {
// Screen-space comic inks: outward speed lines, a cutout burst and outlined stars.
// No particle system, shader dependency or falling confetti is needed by the playable.
public class ComicVictory:MaskableGraphic {
 Vector2 focus;float age;const float Duration=1.65f;
 static readonly Color Ink=FindStyle.Hex("252b4b"),Gold=FindStyle.Hex("ffda46"),Blue=FindStyle.Hex("79e2f2");
 public bool Playing {get;private set;}
 public static ComicVictory Create(RectTransform parent){var o=new GameObject("Comic Victory FX",typeof(RectTransform),typeof(CanvasRenderer),typeof(ComicVictory));var r=(RectTransform)o.transform;r.SetParent(parent,false);r.anchorMin=Vector2.zero;r.anchorMax=Vector2.one;r.offsetMin=r.offsetMax=Vector2.zero;r.pivot=parent.pivot;var fx=o.GetComponent<ComicVictory>();fx.raycastTarget=false;o.SetActive(false);return fx;}
 public void Play(Vector3 world){StopAllCoroutines();gameObject.SetActive(true);transform.SetAsLastSibling();focus=rectTransform.InverseTransformPoint(world);age=0;Playing=true;SetVerticesDirty();StartCoroutine(Animate());}
 public void Cancel(){StopAllCoroutines();Playing=false;gameObject.SetActive(false);}
 IEnumerator Animate(){for(age=0;age<Duration;age+=Time.unscaledDeltaTime){SetVerticesDirty();yield return null;}Playing=false;gameObject.SetActive(false);}
 protected override void OnPopulateMesh(VertexHelper vh){
  vh.Clear();float t=age;float rays=Fade(.35f,1.25f,t);
  if(t<.16f){Color flash=Color.white;flash.a=.30f*(1-t/.16f);Rect r=rectTransform.rect;Quad(vh,new Vector2(r.xMin,r.yMin),new Vector2(r.xMin,r.yMax),new Vector2(r.xMax,r.yMax),new Vector2(r.xMax,r.yMin),flash);}
  for(int i=0;i<28;i++){
   float a=i*Mathf.PI*2/28+.07f,move=Mathf.Clamp01(t/.7f);float inner=Mathf.Lerp(190,510,move)+(i%3)*34,outer=1250;Color c=i%4==0?Blue:i%3==0?Gold:Ink;c.a=rays*(i%3==0?.54f:.32f);
   Triangle(vh,At(a,inner),At(a-.012f-(i%2)*.009f,outer),At(a+.012f+(i%2)*.009f,outer),c);
  }
  float ring=Mathf.Clamp01(t/.65f);if(t<.65f)Ring(vh,focus,Mathf.Lerp(40,690,FindStyle.Ease(ring)),Mathf.Lerp(18,4,ring),Tint(Ink,(1-ring)*.8f));
  float grow=t<.19f?Mathf.Lerp(.12f,1.12f,FindStyle.Ease(t/.19f)):Mathf.Lerp(1.12f,1,Mathf.Clamp01((t-.19f)/.24f));float fade=Fade(.65f,1.45f,t);
  Burst(vh,focus+new Vector2(12,-14),new Vector2(470,320)*grow,.08f,Tint(Ink,fade*.24f));
  Burst(vh,focus,new Vector2(470,320)*grow,.08f,Tint(Ink,fade));Burst(vh,focus,new Vector2(457,307)*grow,.08f,Tint(Blue,fade));
  Burst(vh,focus,new Vector2(430,285)*grow,-.03f,Tint(Ink,fade));Burst(vh,focus,new Vector2(417,272)*grow,-.03f,Tint(Gold,fade));
  // A few bold cutout stars shoot away from the stamp, rather than raining like confetti.
  for(int i=0;i<8;i++){
   float delay=(i%3)*.035f,u=Mathf.Clamp01((t-.10f-delay)/.85f);if(u<=0)continue;
   float a=i*Mathf.PI*2/8+.35f;Vector2 p=focus+new Vector2(Mathf.Cos(a)*500,Mathf.Sin(a)*360)*Mathf.Lerp(.45f,1.12f,FindStyle.Ease(u));float size=(i%2==0?36:24)*Mathf.Sin(Mathf.Min(1,u*2)*Mathf.PI*.5f);float alpha=Fade(1,1.6f,t);
   Star(vh,p,size+5,a+u*.5f,Tint(Ink,alpha));Star(vh,p,size,a+u*.5f,Tint(i%2==0?Gold:Color.white,alpha));
  }
  float dots=Fade(.7f,1.25f,t);for(int side=-1;side<=1;side+=2)for(int row=0;row<5;row++)for(int col=0;col<6-row;col++){Vector2 p=focus+new Vector2(side*(300+col*24),185+row*24);Disc(vh,p,4,Tint(Ink,dots*.28f));}
 }
 Vector2 At(float angle,float radius)=>focus+new Vector2(Mathf.Cos(angle),Mathf.Sin(angle))*radius;
 static float Fade(float start,float end,float time)=>1-Mathf.SmoothStep(0,1,Mathf.InverseLerp(start,end,time));
 static Color Tint(Color c,float alpha){c.a=alpha;return c;}
 static void Triangle(VertexHelper vh,Vector2 a,Vector2 b,Vector2 c,Color tint){int n=vh.currentVertCount;vh.AddVert(a,tint,Vector2.zero);vh.AddVert(b,tint,Vector2.zero);vh.AddVert(c,tint,Vector2.zero);vh.AddTriangle(n,n+1,n+2);}
 static void Quad(VertexHelper vh,Vector2 a,Vector2 b,Vector2 c,Vector2 d,Color tint){Triangle(vh,a,b,c,tint);Triangle(vh,a,c,d,tint);}
 static void Burst(VertexHelper vh,Vector2 center,Vector2 size,float angle,Color tint){for(int i=0;i<24;i++){float a=i*Mathf.PI*2/24+angle,b=(i+1)*Mathf.PI*2/24+angle;Vector2 p=center+Vector2.Scale(new Vector2(Mathf.Cos(a),Mathf.Sin(a)),size)*(i%2==0?1:.73f);Vector2 q=center+Vector2.Scale(new Vector2(Mathf.Cos(b),Mathf.Sin(b)),size)*((i+1)%2==0?1:.73f);Triangle(vh,center,p,q,tint);}}
 static void Star(VertexHelper vh,Vector2 center,float radius,float angle,Color tint){for(int i=0;i<10;i++){float a=i*Mathf.PI/5+angle,b=(i+1)*Mathf.PI/5+angle;Triangle(vh,center,center+new Vector2(Mathf.Cos(a),Mathf.Sin(a))*radius*(i%2==0?1:.44f),center+new Vector2(Mathf.Cos(b),Mathf.Sin(b))*radius*((i+1)%2==0?1:.44f),tint);}}
 static void Disc(VertexHelper vh,Vector2 center,float radius,Color tint){for(int i=0;i<8;i++)Triangle(vh,center,center+new Vector2(Mathf.Cos(i*Mathf.PI/4),Mathf.Sin(i*Mathf.PI/4))*radius,center+new Vector2(Mathf.Cos((i+1)*Mathf.PI/4),Mathf.Sin((i+1)*Mathf.PI/4))*radius,tint);}
 static void Ring(VertexHelper vh,Vector2 center,float radius,float width,Color tint){for(int i=0;i<48;i++){float a=i*Mathf.PI/24,b=(i+1)*Mathf.PI/24;Vector2 p=new Vector2(Mathf.Cos(a),Mathf.Sin(a)),q=new Vector2(Mathf.Cos(b),Mathf.Sin(b));Quad(vh,center+p*(radius-width),center+p*radius,center+q*radius,center+q*(radius-width),tint);}}
}
}
