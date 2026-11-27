using UnityEngine;
using UnityEngine.UI;
namespace LittleFinds {
// A polygon of paper or artwork. Each shard keeps the corresponding original UVs.
public class CardShardGraphic : MaskableGraphic {
 Vector2[] points,uvs;Texture texture;
 public override Texture mainTexture=>texture?texture:Texture2D.whiteTexture;
 public void Configure(Vector2[] vertices,Vector2[] coordinates,Texture source,Color tint){points=vertices;uvs=coordinates;texture=source;color=tint;raycastTarget=false;SetAllDirty();}
 protected override void OnPopulateMesh(VertexHelper vh){
  vh.Clear();if(points==null)return;
  for(int i=0;i<points.Length;i++)vh.AddVert(points[i],color,uvs==null?Vector2.zero:uvs[i]);
  for(int i=1;i<points.Length-1;i++)vh.AddTriangle(0,i,i+1);
 }
}
}
