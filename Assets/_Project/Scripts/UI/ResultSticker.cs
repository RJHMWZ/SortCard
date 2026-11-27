using UnityEngine;
using UnityEngine.UI;
namespace LittleFinds {
// A cut-paper comic stamp, drawn as native UI rather than a second collection panel.
public class ResultSticker : MaskableGraphic {
 public Text heading,kicker;
 public bool Won {get;private set;}
 public static ResultSticker Create(Transform parent,Font font){
  var o=new GameObject("Result Sticker",typeof(RectTransform),typeof(CanvasRenderer),typeof(ResultSticker));var r=(RectTransform)o.transform;r.SetParent(parent,false);r.anchorMin=r.anchorMax=new Vector2(.5f,1);r.pivot=new Vector2(.5f,.5f);r.sizeDelta=new Vector2(760,360);r.anchoredPosition=new Vector2(0,-220);
  var card=o.GetComponent<ResultSticker>();card.raycastTarget=false;card.kicker=Label(r,font,"Result Level",44,new Vector2(52,-28),new Vector2(650,70));card.heading=Label(r,font,"Result Word",104,new Vector2(52,-100),new Vector2(656,182));return card;
 }
 static Text Label(Transform parent,Font font,string name,int size,Vector2 position,Vector2 dimensions){var o=new GameObject(name,typeof(RectTransform),typeof(CanvasRenderer),typeof(Text));var r=(RectTransform)o.transform;r.SetParent(parent,false);r.anchorMin=r.anchorMax=r.pivot=new Vector2(0,1);r.anchoredPosition=position;r.sizeDelta=dimensions;var text=o.GetComponent<Text>();text.font=font;text.fontSize=size;text.fontStyle=FontStyle.Normal;text.color=FindStyle.Ink;text.alignment=TextAnchor.MiddleLeft;text.raycastTarget=false;return text;}
 public void Configure(bool won,int level,string word){Won=won;kicker.text="LEVEL "+level;heading.text=word;color=FindStyle.Hex(won?"ffe15a":"ffb5c6");SetVerticesDirty();}
 protected override void OnPopulateMesh(VertexHelper vh){
  vh.Clear();Rect r=rectTransform.rect;Vector2[] shape={new Vector2(.025f,.04f),new Vector2(0,.91f),new Vector2(.90f,1),new Vector2(1,.78f),new Vector2(.97f,0)};
  Polygon(vh,r,shape,1,new Vector2(10,-14),new Color(.145f,.169f,.294f,.28f));Polygon(vh,r,shape,1,Vector2.zero,FindStyle.Ink);Polygon(vh,r,shape,.973f,Vector2.zero,color);
  // Small printer dots and a folded corner give the stamp a comic-paper texture.
  Color dot=FindStyle.Ink;dot.a=.13f;
  for(int row=0;row<4;row++)for(int col=0;col<5-row;col++)Circle(vh,new Vector2(r.xMax-38-col*19,r.yMin+24+row*19),3.5f,dot);
  int n=vh.currentVertCount;vh.AddVert(r.min+Vector2.Scale(new Vector2(.90f,1),r.size),FindStyle.Ink,Vector2.zero);vh.AddVert(r.min+Vector2.Scale(new Vector2(.885f,.77f),r.size),FindStyle.Ink,Vector2.zero);vh.AddVert(r.min+Vector2.Scale(new Vector2(1,.78f),r.size),FindStyle.Ink,Vector2.zero);vh.AddTriangle(n,n+1,n+2);
 }
 static void Polygon(VertexHelper vh,Rect r,Vector2[] shape,float scale,Vector2 offset,Color tint){int n=vh.currentVertCount;vh.AddVert(r.center+offset,tint,Vector2.zero);foreach(var p in shape)vh.AddVert(r.center+(r.min+Vector2.Scale(p,r.size)-r.center)*scale+offset,tint,Vector2.zero);for(int i=0;i<shape.Length;i++)vh.AddTriangle(n,n+i+1,n+(i+1)%shape.Length+1);}
 static void Circle(VertexHelper vh,Vector2 center,float radius,Color tint){int n=vh.currentVertCount;vh.AddVert(center,tint,Vector2.zero);for(int i=0;i<8;i++){float a=i*Mathf.PI/4;vh.AddVert(center+new Vector2(Mathf.Cos(a),Mathf.Sin(a))*radius,tint,Vector2.zero);}for(int i=0;i<8;i++)vh.AddTriangle(n,n+1+i,n+1+(i+1)%8);}
}
}
