using UnityEngine;
using UnityEngine.UI;
namespace LittleFinds {
public class WarningStripes:MaskableGraphic {
 float phase;public float Phase {get=>phase;set{phase=value;SetVerticesDirty();}}
 protected override void OnPopulateMesh(VertexHelper vh){vh.Clear();Rect r=rectTransform.rect;float offset=Mathf.Repeat(phase,36);
  for(float x=r.xMin-72+offset;x<r.xMax+36;x+=36){int n=vh.currentVertCount;float left=Mathf.Clamp(x,r.xMin,r.xMax),right=Mathf.Clamp(x+13,r.xMin,r.xMax),upperLeft=Mathf.Clamp(x+16,r.xMin,r.xMax),upperRight=Mathf.Clamp(x+29,r.xMin,r.xMax);vh.AddVert(new Vector2(left,r.yMin+2),color,Vector2.zero);vh.AddVert(new Vector2(upperLeft,r.yMax-2),color,Vector2.zero);vh.AddVert(new Vector2(upperRight,r.yMax-2),color,Vector2.zero);vh.AddVert(new Vector2(right,r.yMin+2),color,Vector2.zero);vh.AddTriangle(n,n+1,n+2);vh.AddTriangle(n,n+2,n+3);}
 }
}
}
