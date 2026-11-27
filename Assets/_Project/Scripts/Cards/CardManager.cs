using UnityEngine;
using System.Collections.Generic;
namespace LittleFinds {
public class CardManager:MonoBehaviour {
 public CardController[] cards;public Sprite[] art;public GameManager game;
 static readonly string[] titles={"Goldfish","Clownfish","Blue Tang","Pufferfish","Teddy","Robot","Duck","Blocks","Apple","Banana","Cherries","Pear","Daisy","Tulip","Rose","Bluebell","Donut","Cupcake","Ice Cream","Candy","Guitar","Drum","Piano","Note"};
 public int OpenCount {get{int n=0;foreach(var c in cards)if(c.gameObject.activeSelf&&c.IsOpen)n++;return n;}}
 public bool Collecting {get{foreach(var c in cards)if(c.State==CardState.Collecting)return true;return false;}}
 public int BoardCount {get{int n=0;foreach(var c in cards)if(c.gameObject.activeSelf&&c.State!=CardState.Removed&&c.State!=CardState.Collecting)n++;return n;}}
 public FindData Item(int index)=>new FindData(titles[index],index/4,art[index]);
 public void Deal(){
  if(game.Tutorial&&game.Tutorial.InLesson){var lesson=game.Tutorial.BuildDeck();for(int i=0;i<12;i++)cards[i].Bind(game,i,lesson[i]);return;}
  var data=new List<FindData>();var needed=new List<int>();var remaining=new List<int>();
  foreach(int c in game.Collections.Targets)if(game.Collections.Count(c)<4){needed.Add(c);for(int n=game.Collections.Count(c);n<4;n++)remaining.Add(c);}
  for(int i=remaining.Count-1;i>0;i--){int j=Random.Range(0,i+1);int swap=remaining[i];remaining[i]=remaining[j];remaining[j]=swap;}
  int useful=12-game.Level.Distractors;
  // Every unfinished category appears even when later levels have few useful slots.
  foreach(int c in needed){data.Add(Pick(c));remaining.Remove(c);}
  // Only level one starts with all twelve required cards. Every refill keeps its level's ratio.
  foreach(int c in remaining){if(data.Count>=useful)break;data.Add(Pick(c));}
  while(data.Count<useful)data.Add(Pick(needed.Count>0?needed[Random.Range(0,needed.Count)]:game.Collections.Targets[0]));
  var decoys=new List<int>(game.Collections.Distractors);
  foreach(int c in game.Collections.Targets)if(game.Collections.Count(c)>=4)decoys.Add(c);
  for(int n=0;n<game.Level.Distractors;n++)data.Add(Pick(n<2?game.Collections.Distractors[n]:decoys[Random.Range(0,decoys.Count)]));
  for(int i=data.Count-1;i>0;i--){int j=Random.Range(0,i+1);var tmp=data[i];data[i]=data[j];data[j]=tmp;}
  for(int i=0;i<12;i++)cards[i].Bind(game,i,data[i]);
 }
 FindData Pick(int c)=>Item(c*4+Random.Range(0,4));
 public void Freeze(){foreach(var c in cards)c.Freeze();}
 public void RefreshCategories(){foreach(var c in cards)if(c.Data!=null)c.RefreshCategoryVisual();}
}
}
