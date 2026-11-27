using UnityEngine;
namespace LittleFinds {
public enum MistakeKind { FullCollection, WrongCategory, DiscardedTarget }
public class RoundRisk : MonoBehaviour {
 [Min(1)] public int maximumLives=5;
 [Min(0)] public float shuffleSeconds=4;
 [Range(0,.75f)] public float simultaneousMistakeGrace=.35f;
 public GameManager game;
 public int Lives {get;private set;}=5;
 public int Mistakes {get;private set;}
 float lastDamage=-10;
 public void ResetRound(){Lives=maximumLives;Mistakes=0;lastDamage=-10;}
 public bool Damage(MistakeKind kind){return Damage(kind,game.Trash?game.Trash.target.position:game.Feedback.stage.position);}
 public bool Damage(MistakeKind kind,Vector3 origin){
  if(game.State!=GameState.Playing||(kind!=MistakeKind.DiscardedTarget&&Time.unscaledTime-lastDamage<simultaneousMistakeGrace))return false;
  lastDamage=Time.unscaledTime;Mistakes++;
  bool practice=game.Tutorial&&game.Tutorial.InLesson;
  Lives=Mathf.Max(practice?1:0,Lives-1);game.ChangeScore(-GameManager.MistakePoints);game.BreakStreak();
  game.Feedback.Penalty(kind==MistakeKind.WrongCategory?"WRONG BOX!":"BOX FULL!",origin);
  game.UI.Message(kind==MistakeKind.DiscardedTarget?"Penalty: Still needed! -1 life":kind==MistakeKind.WrongCategory?"Penalty: Not a target category. -1 life":"Penalty: Collection already full. -1 life");
  if(Lives==0)game.Finish(false);
  return true;
 }
 public bool PayForShuffle(){
  if(game.Tutorial&&game.Tutorial.InLesson)return true;
  game.Timer.Advance(shuffleSeconds);
  if(game.State!=GameState.Playing)return false;
  game.UI.Message("Shuffle cost: -"+shuffleSeconds.ToString("0")+"s");return true;
 }
}
}
