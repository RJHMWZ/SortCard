using UnityEngine;
using System.Collections;
namespace LittleFinds {
public class GameManager:MonoBehaviour {
 public RoundRisk Risk;public TutorialController Tutorial;public CardManager Cards;public CollectionManager Collections;public ShuffleController Shuffle;public GameTimer Timer;public GameUIController UI;public FeedbackController Feedback;
 public TrashBin Trash;
 public int LevelIndex {get;private set;}
 public EasyLevel Level=>EasyLevels.All[LevelIndex];
 public int BestCombo {get;private set;}
 public float ComboRemaining {get;private set;}
 public float ComboFraction=>Mathf.Clamp01(ComboRemaining/Level.ComboWindow);
 public int Score {get;private set;}
 public const int CorrectPoints=100,ComboBonus=25,MistakePoints=100;
 ScoreHUD scoreHUD;
 public void ChangeScore(int delta){Score=Mathf.Max(0,Score+delta);if(scoreHUD)scoreHUD.SetScore(Score,delta);}
 public void ResetScore(){Score=0;if(scoreHUD)scoreHUD.SetScore(0,0);}
 ComboHUD comboHUD;
 public void EnsureTrash(){if(!Trash)Trash=TrashBin.Create(this);}
 public GameState State {get;private set;}=GameState.Ready; public int Streak {get;private set;}
 void Start(){
  Application.runInBackground=true;Application.targetFrameRate=60;
#if !UNITY_LUNA
  Screen.orientation=ScreenOrientation.Portrait;
#endif
  LevelIndex=0;EnsureCombo();
  EnsureTrash();if(!scoreHUD)scoreHUD=ScoreHUD.Create(this);if(UnityEngine.EventSystems.EventSystem.current)UnityEngine.EventSystems.EventSystem.current.pixelDragThreshold=Mathf.Clamp(Screen.width/60,10,24);
  if(!Tutorial.InLesson&&PlayerPrefs.GetInt("LittleFinds.TutorialSeen",PlayerPrefs.GetInt("LittleFinds.TutorialComplete",0))==0){PlayerPrefs.SetInt("LittleFinds.TutorialSeen",1);PlayerPrefs.Save();Tutorial.isTutorialLevel=true;}
  Timer.Expired=()=>Finish(false);Cards.game=this;Shuffle.game=this;
  if(Tutorial.InLesson){Tutorial.StartLesson();}else Begin();
 }
 public void EnsureCombo(){if(!comboHUD)comboHUD=ComboHUD.Create(this);}
 void Update(){if(State==GameState.Playing&&!UI.overlay.activeSelf&&ComboRemaining>0){ComboRemaining=Mathf.Max(0,ComboRemaining-Time.deltaTime);if(ComboRemaining==0)BreakStreak();}}
 public void BreakStreak(){Streak=0;ComboRemaining=0;}
 public void RegisterCombo(){Streak=ComboRemaining>0?Streak+1:1;BestCombo=Mathf.Max(BestCombo,Streak);ComboRemaining=Level.ComboWindow;if(comboHUD)comboHUD.RefreshNow();}
 public void PlayLevel(int index){LevelIndex=Mathf.Clamp(index,0,EasyLevels.All.Length-1);Begin();}
 public void ContinueCampaign(){PlayLevel(LevelIndex==EasyLevels.All.Length-1?0:LevelIndex+1);}
 public void SetState(GameState state){State=state;}
 public void Begin(){StopAllCoroutines();if(Trash)Trash.Cancel();Shuffle.Cancel();Cards.Freeze();Feedback.Clear();ResetScore();Collections.ResetCollections();Risk.ResetRound();BreakStreak();BestCombo=0;UI.CloseModal();UI.ResetClock();Timer.ResetTimer(Tutorial&&Tutorial.InLesson?120:Level.Seconds);Shuffle.InitialDeal();}
 public bool WouldDiscardCostLife(CardController card)=>card.Data!=null&&Collections.IsTarget(card.Data.category)&&Collections.Count(card.Data.category)<4;
 public void Discarded(CardController card,bool costLife){
  if(State!=GameState.Playing)return;
  if(costLife)Risk.Damage(MistakeKind.DiscardedTarget,card.HomePosition);else Feedback.Play(9);
  if(Tutorial)Tutorial.OnDiscarded(card);
  if(State==GameState.Playing&&!Shuffle.TryRefreshEmpty())Shuffle.TryQueuedShuffle();
 }
 public void Collect(CardController card){
  if(State!=GameState.Playing||card.State!=CardState.FaceUp)return;
  int c=card.Data.category;
  if(!Collections.Reserve(c)){
   if(!Collections.IsTarget(c)||Collections.Count(c)>=4){card.Shatter();Risk.Damage(Collections.IsTarget(c)?MistakeKind.FullCollection:MistakeKind.WrongCategory,card.HomePosition);if(Tutorial)Tutorial.OnRejected(card);}
   else {card.Return(false);UI.Message("Pending: The last treasure is already on its way.");}
   return;
  }
  // A correct click owns the combo immediately; flight only delivers the collection.
  RegisterCombo();int points=CorrectPoints+(Streak-1)*ComboBonus;ChangeScore(points);card.BeginCollect();Feedback.Play(1);
  Feedback.Impact(c,Streak,Collections.IsSatisfiedOrReserved(c),card.HomePosition,points);
 }
 public void Collected(CardController card){if(State!=GameState.Playing&&State!=GameState.Shuffling)return;int c=card.Data.category;Collections.Commit(c,card.Data.art);
  if(Collections.Count(c)==4){Cards.RefreshCategories();Feedback.Play(2);}
  if(Tutorial)Tutorial.OnCollected(card);
  if(Collections.Total==12)Finish(true);else if(!Shuffle.TryRefreshEmpty())Shuffle.TryQueuedShuffle();}
 public void Restart(){StopAllCoroutines();if(Trash)Trash.Cancel();Timer.Running=false;Shuffle.Cancel();Cards.Freeze();Collections.CancelReservations();Risk.ResetRound();Streak=0;if(Tutorial&&Tutorial.InLesson){Feedback.Clear();Tutorial.StartLesson();return;}Begin();}
 public void Finish(bool won){if(State==GameState.Victory||State==GameState.Failed)return;State=won?GameState.Victory:GameState.Failed;Timer.Running=false;if(Trash)Trash.Cancel();Shuffle.Cancel();Cards.Freeze();Collections.CancelReservations();if(won&&Tutorial&&Tutorial.InLesson){Feedback.Play(6);Feedback.Celebration();Tutorial.CompleteLesson();}else if(won)StartCoroutine(Celebrate());else {if(Feedback.SoundOn&&Feedback.audio)Feedback.audio.PlayFailure();UI.ShowResult(false,Collections.Total);}}
 IEnumerator Celebrate(){Feedback.Play(6);Feedback.Celebration();yield return new WaitForSeconds(.24f);UI.ShowResult(true,Collections.Total);}
}
}
