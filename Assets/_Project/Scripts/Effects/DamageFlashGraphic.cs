using UnityEngine;
using UnityEngine.UI;
namespace LittleFinds {
// A soft red border leaves the cards in the center readable during a hit.
public class DamageFlashGraphic : MaskableGraphic {
 protected override void OnPopulateMesh(VertexHelper vh){
  vh.Clear();Rect r=rectTransform.rect;float inset=Mathf.Min(r.width,r.height)*.18f;
  Vector2[] outer={new Vector2(r.xMin,r.yMin),new Vector2(r.xMin,r.yMax),new Vector2(r.xMax,r.yMax),new Vector2(r.xMax,r.yMin)};
  Vector2[] inner={new Vector2(r.xMin+inset,r.yMin+inset),new Vector2(r.xMin+inset,r.yMax-inset),new Vector2(r.xMax-inset,r.yMax-inset),new Vector2(r.xMax-inset,r.yMin+inset)};
  Color clear=color;clear.a=0;
  for(int i=0;i<4;i++)vh.AddVert(outer[i],color,Vector2.zero);
  for(int i=0;i<4;i++)vh.AddVert(inner[i],clear,Vector2.zero);
  for(int i=0;i<4;i++){int j=(i+1)%4;vh.AddTriangle(i,j,i+4);vh.AddTriangle(j,j+4,i+4);}
 }
}
}
