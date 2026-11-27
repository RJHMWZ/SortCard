using UnityEngine;
using UnityEngine.UI;
namespace LittleFinds {
// Native UI geometry stays sharp at every mobile resolution.
public class TrashCanGraphic : MaskableGraphic {
 float lidOpen;
 public float LidOpen {get=>lidOpen;set{float next=Mathf.Clamp01(value);if(Mathf.Abs(next-lidOpen)<.001f)return;lidOpen=next;SetVerticesDirty();}}
 protected override void OnPopulateMesh(VertexHelper vh){
  vh.Clear();Rect r=rectTransform.rect;
  Quad(vh,r,new Vector2(.23f,.14f),new Vector2(.17f,.72f),new Vector2(.83f,.72f),new Vector2(.77f,.14f),color);
  Color paper=Color.Lerp(Color.white,color,.12f);
  Quad(vh,r,new Vector2(.29f,.21f),new Vector2(.24f,.65f),new Vector2(.76f,.65f),new Vector2(.71f,.21f),paper);
  // The lid and handle share a hinge at the right edge; the body stays still.
  LidBar(vh,r,.12f,.75f,.76f,.08f);LidBar(vh,r,.36f,.87f,.28f,.07f);
  LidBar(vh,r,.32f,.88f,.07f,-.1f);LidBar(vh,r,.61f,.88f,.07f,-.1f);
  Bar(vh,r,.38f,.29f,.05f,.28f,color);Bar(vh,r,.57f,.29f,.05f,.28f,color);
 }
 void LidBar(VertexHelper vh,Rect r,float x,float y,float w,float h){Quad(vh,r,LidPoint(new Vector2(x,y)),LidPoint(new Vector2(x,y+h)),LidPoint(new Vector2(x+w,y+h)),LidPoint(new Vector2(x+w,y)),color);}
 Vector2 LidPoint(Vector2 p){Vector2 hinge=new Vector2(.84f,.76f);float angle=-lidOpen*45*Mathf.Deg2Rad;Vector2 d=p-hinge;return hinge+new Vector2(d.x*Mathf.Cos(angle)-d.y*Mathf.Sin(angle),d.x*Mathf.Sin(angle)+d.y*Mathf.Cos(angle))+Vector2.up*(lidOpen*.025f);}
 static void Bar(VertexHelper vh,Rect r,float x,float y,float w,float h,Color c){Quad(vh,r,new Vector2(x,y),new Vector2(x,y+h),new Vector2(x+w,y+h),new Vector2(x+w,y),c);}
 static void Quad(VertexHelper vh,Rect r,Vector2 a,Vector2 b,Vector2 c,Vector2 d,Color tint){int n=vh.currentVertCount;foreach(var p in new[]{a,b,c,d})vh.AddVert(r.min+Vector2.Scale(p,r.size),tint,Vector2.zero);vh.AddTriangle(n,n+1,n+2);vh.AddTriangle(n,n+2,n+3);}
}
}
