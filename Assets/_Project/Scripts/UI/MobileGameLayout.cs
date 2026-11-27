using UnityEngine;
namespace LittleFinds {
[ExecuteAlways] public class MobileGameLayout : MonoBehaviour {
 public RectTransform board,grid,footer,hint,effects,trash;public TutorialController tutorial;
 public RectTransform[] slots;
 Vector2 lastSize;
 void OnEnable(){Reflow();}
 void Update(){if(lastSize!=((RectTransform)transform).rect.size)Reflow();}
 public void Reflow(){
  if(!board||!grid||!footer||slots==null||slots.Length!=12)return;
  lastSize=((RectTransform)transform).rect.size;
  float footerTop=lastSize.y-142;
  footer.anchoredPosition=new Vector2(0,-footerTop);
  const float padding=36,gap=20,innerWidth=948;
  float areaTop=tutorial&&tutorial.InLesson?858:650;
  float maxHeight=footerTop-areaTop-40;
  float cardHeight=Mathf.Min((maxHeight-padding*2-gap*2)/3,222*291f/227);
  float cardWidth=cardHeight*227/291;
  float gridHeight=cardHeight*3+gap*2;
  float boardHeight=gridHeight+padding*2;
  float boardTop=areaTop+Mathf.Max(0,(footerTop-40-areaTop-boardHeight)*.5f);
  if(trash){float gapEnd=tutorial&&tutorial.InLesson?boardTop-160:boardTop;trash.anchoredPosition=new Vector2(0,-(466+gapEnd)*.5f);}
  board.anchoredPosition=new Vector2(0,-boardTop);board.sizeDelta=new Vector2(1020,boardHeight);
  grid.anchoredPosition=new Vector2(padding,-padding);grid.sizeDelta=new Vector2(innerWidth,gridHeight);
  float columnGap=(innerWidth-cardWidth*4)/3;
  for(int i=0;i<12;i++){
   if(!slots[i])continue;
   slots[i].anchoredPosition=new Vector2((i%4)*(cardWidth+columnGap),-(i/4)*(cardHeight+gap));
   slots[i].localScale=Vector3.one*(cardHeight/291);
  }
  if(tutorial&&tutorial.InLesson&&tutorial.coach)tutorial.coach.anchoredPosition=new Vector2(0,-(boardTop-160));
  if(hint)hint.anchoredPosition=new Vector2(0,-(footerTop-86));
  if(effects)effects.sizeDelta=lastSize;

 }
}
}
