using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using System.Collections;
using System.Collections.Generic;
namespace LittleFinds {
// Procedurally cut the visible paper and illustration into 24 irregular triangles.
public class CardShatter : MonoBehaviour {
 class Piece {public RectTransform rect;public Vector2 origin,velocity;public float spin;}
 readonly List<Piece> pieces=new List<Piece>();
 CanvasGroup fade;
 public static CardShatter Spawn(CardController card,RectTransform layer){
  var root=new GameObject("Broken Card",typeof(RectTransform),typeof(CanvasGroup),typeof(CardShatter));
  var rootRect=(RectTransform)root.transform;rootRect.SetParent(layer,false);ResetRect(rootRect);
  rootRect.position=card.frontPaper.rectTransform.TransformPoint(card.frontPaper.rectTransform.rect.center);var effect=root.GetComponent<CardShatter>();
  effect.Build(card,layer);effect.StartCoroutine(effect.Animate());return effect;
 }
 void Build(CardController card,RectTransform layer){
  fade=GetComponent<CanvasGroup>();fade.blocksRaycasts=false;fade.interactable=false;
  // All fragment geometry and motion use this root, whose origin is the card center.
  layer=(RectTransform)transform;
  var paper=card.frontPaper.rectTransform;Rect bounds=paper.rect;
  var image=card.illustration;Rect art=image.rectTransform.rect;
  if(image.preserveAspect&&image.sprite){
   float ratio=image.sprite.rect.width/image.sprite.rect.height;
   Vector2 size=art.size;if(size.x/size.y>ratio)size.x=size.y*ratio;else size.y=size.x/ratio;
   art=new Rect(art.center-size*.5f,size);
  }
  // Express the image's actual drawing rectangle in paper coordinates.
  Vector2 a=paper.InverseTransformPoint(image.rectTransform.TransformPoint(art.min));
  Vector2 b=paper.InverseTransformPoint(image.rectTransform.TransformPoint(art.max));
  art=Rect.MinMaxRect(Mathf.Min(a.x,b.x),Mathf.Min(a.y,b.y),Mathf.Max(a.x,b.x),Mathf.Max(a.y,b.y));
  var grid=new Vector2[4,5];
  for(int y=0;y<=3;y++)for(int x=0;x<=4;x++){
   float px=bounds.xMin+bounds.width*x/4,py=bounds.yMin+bounds.height*y/3;
   if(x>0&&x<4)px+=Random.Range(-.22f,.22f)*bounds.width/4;
   if(y>0&&y<3)py+=Random.Range(-.22f,.22f)*bounds.height/3;
   grid[y,x]=new Vector2(px,py);
  }
  for(int y=0;y<3;y++)for(int x=0;x<4;x++){
   Vector2 p=grid[y,x],q=grid[y,x+1],r=grid[y+1,x+1],s=grid[y+1,x];
   if((x+y)%2==0){AddPiece(new[]{p,q,r},card,layer,paper,art);AddPiece(new[]{p,r,s},card,layer,paper,art);}
   else {AddPiece(new[]{p,q,s},card,layer,paper,art);AddPiece(new[]{q,r,s},card,layer,paper,art);}
  }
 }
 void AddPiece(Vector2[] triangle,CardController card,RectTransform layer,RectTransform paper,Rect art){
  Vector2 origin=layer.InverseTransformPoint(paper.TransformPoint((triangle[0]+triangle[1]+triangle[2])/3));
  var obj=new GameObject("Paper Shard",typeof(RectTransform),typeof(CanvasRenderer),typeof(CardShardGraphic),typeof(Outline));
  var rt=obj.GetComponent<RectTransform>();rt.SetParent(transform,false);ResetRect(rt);rt.localPosition=origin;
  obj.GetComponent<CardShardGraphic>().Configure(ToLocal(triangle,layer,paper,origin),null,null,card.frontPaper.color);
  var outline=obj.GetComponent<Outline>();outline.effectColor=FindStyle.Ink;outline.effectDistance=new Vector2(1,-1);
  var clipped=new List<Vector2>(triangle);
  clipped=Clip(clipped,0,art.xMin,true);clipped=Clip(clipped,0,art.xMax,false);
  clipped=Clip(clipped,1,art.yMin,true);clipped=Clip(clipped,1,art.yMax,false);
  var sprite=card.illustration.sprite;
  if(clipped.Count>=3&&sprite){
   Vector4 uv=DataUtility.GetOuterUV(sprite);var coordinates=new Vector2[clipped.Count];
   for(int i=0;i<coordinates.Length;i++)coordinates[i]=new Vector2(Mathf.Lerp(uv.x,uv.z,(clipped[i].x-art.xMin)/art.width),Mathf.Lerp(uv.y,uv.w,(clipped[i].y-art.yMin)/art.height));
   var picture=new GameObject("Artwork Fragment",typeof(RectTransform),typeof(CanvasRenderer),typeof(CardShardGraphic));picture.transform.SetParent(rt,false);ResetRect((RectTransform)picture.transform);
   picture.GetComponent<CardShardGraphic>().Configure(ToLocal(clipped.ToArray(),layer,paper,origin),coordinates,sprite.texture,card.illustration.color);
  }
  Vector2 center=layer.InverseTransformPoint(paper.TransformPoint(paper.rect.center));
  Vector2 direction=(origin-center).normalized;if(direction.sqrMagnitude<.01f)direction=Vector2.up;
  pieces.Add(new Piece{rect=rt,origin=origin,velocity=direction*Random.Range(130f,310f)+Vector2.up*Random.Range(60f,140f),spin=Random.Range(-360f,360f)});
 }
 static void ResetRect(RectTransform rect){
  rect.anchorMin=rect.anchorMax=rect.pivot=new Vector2(.5f,.5f);rect.sizeDelta=Vector2.zero;
  rect.localScale=Vector3.one;rect.localRotation=Quaternion.identity;rect.localPosition=Vector3.zero;
 }
 static Vector2[] ToLocal(Vector2[] polygon,RectTransform layer,RectTransform paper,Vector2 origin){
  var result=new Vector2[polygon.Length];for(int i=0;i<result.Length;i++)result[i]=(Vector2)layer.InverseTransformPoint(paper.TransformPoint(polygon[i]))-origin;return result;
 }
 static List<Vector2> Clip(List<Vector2> input,int axis,float edge,bool above){
  var output=new List<Vector2>();if(input.Count==0)return output;
  Vector2 prev=input[input.Count-1];bool prevInside=above?prev[axis]>=edge:prev[axis]<=edge;
  foreach(var point in input){
   bool inside=above?point[axis]>=edge:point[axis]<=edge;
   if(inside!=prevInside)output.Add(Vector2.Lerp(prev,point,(edge-prev[axis])/(point[axis]-prev[axis])));
   if(inside)output.Add(point);prev=point;prevInside=inside;
  }
  return output;
 }
 IEnumerator Animate(){
  const float duration=.65f;
  for(float t=0;t<duration;t+=Time.deltaTime){
   float u=t/duration;fade.alpha=1-Mathf.SmoothStep(.3f,1,u);
   foreach(var piece in pieces){piece.rect.localPosition=piece.origin+piece.velocity*t+Vector2.down*(450*t*t);piece.rect.localRotation=Quaternion.Euler(0,0,piece.spin*t);piece.rect.localScale=Vector3.one*Mathf.Lerp(1,.65f,u);}
   yield return null;
  }
  Destroy(gameObject);
 }
}
}
