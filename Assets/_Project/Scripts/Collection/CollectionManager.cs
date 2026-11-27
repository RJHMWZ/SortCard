using UnityEngine;
namespace LittleFinds {
public class CollectionManager:MonoBehaviour {
 public RectTransform[] pools;public CollectionBox[] boxes;
 readonly int[] counts=new int[6],reserved=new int[6];
 public int[] Targets {get;private set;}=new[]{0,1,2};
 public int[] Distractors {get;private set;}=new[]{3,4};
 public int Slot(int c)=>System.Array.IndexOf(Targets,c);
 public bool IsTarget(int c)=>Slot(c)>=0;
 public CollectionBox BoxFor(int c)=>boxes[Slot(c)];
 public int Count(int c)=>c>=0&&c<6?counts[c]:0;
 public int Total {get{int total=0;foreach(int c in Targets)total+=counts[c];return total;}}
 public bool IsSatisfiedOrReserved(int c)=>!IsTarget(c)||counts[c]+reserved[c]>=4;
 public bool CanReserve(int c)=>IsTarget(c)&&counts[c]+reserved[c]<4;
 public bool Reserve(int c){if(!CanReserve(c))return false;reserved[c]++;return true;}
 public void Commit(int c,Sprite item){if(!IsTarget(c)||reserved[c]<=0)return;reserved[c]--;counts[c]++;BoxFor(c).Accept(item,counts[c]);}
 public void ResetCollections(){
  var game=GetComponent<GameManager>();var order=new[]{0,1,2,3,4,5};
  if(!game.Tutorial.InLesson){var targets=game.Level.Targets;int index=0;foreach(int c in targets)order[index++]=c;for(int c=0;c<6;c++)if(System.Array.IndexOf(targets,c)<0)order[index++]=c;}
  Targets=new[]{order[0],order[1],order[2]};Distractors=new[]{order[3],order[4]};
  for(int c=0;c<6;c++)counts[c]=reserved[c]=0;
  for(int slot=0;slot<3;slot++){int c=Targets[slot];boxes[slot].Configure(c,game.Cards.art[c*4]);boxes[slot].ResetBox();}
 }
 public void CancelReservations(){for(int c=0;c<6;c++)reserved[c]=0;}
}
}
