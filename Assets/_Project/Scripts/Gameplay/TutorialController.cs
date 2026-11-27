using UnityEngine;
using UnityEngine.UI;
namespace LittleFinds {
// Keep the existing enum values for serialized scene and external references.
public enum LessonStep { Reveal, Collect, Collections, OpenThree, TryFourth, WaitReturn, OtherReveal, OtherReject, Shuffle, CompleteFish, DecoyReveal, DecoyReject, RiskSummary, FinishCollections, Complete, Discard }
public class TutorialController : MonoBehaviour {
 public bool isTutorialLevel;
 public GameManager game;
 public GameObject coachRoot;
 public RectTransform coach,focus,hand;
 public Text heading;
 public Button nextButton;
 public Text nextLabel;
 public LessonStep Step {get;private set;}
 public bool InLesson=>isTutorialLevel;
 public bool CanShuffle=>!InLesson||Step==LessonStep.Shuffle;
 public bool CanDiscard=>!InLesson||Step==LessonStep.Discard;
 public bool CanDiscardCard(CardController card)=>!InLesson||(Step==LessonStep.Discard&&card.Index==1);
 public bool AllowAutoReturn=>!InLesson;
 RectTransform target;readonly Vector3[] corners=new Vector3[4];
 public void EnterLevel(){isTutorialLevel=true;game.Restart();}
 public void ExitLevel(){isTutorialLevel=false;coachRoot.SetActive(false);focus.gameObject.SetActive(false);hand.gameObject.SetActive(false);RefreshLayout();game.Begin();}
 public void PlayRound(){ExitLevel();}
 void RefreshLayout(){var layout=coachRoot.transform.parent.GetComponent<MobileGameLayout>();if(layout)layout.Reflow();game.UI.ResetClock();}
 public void StartLesson(){
  game.ResetScore();game.Risk.ResetRound();game.Timer.ResetTimer();game.Timer.Running=false;game.UI.CloseModal();game.Collections.ResetCollections();
  coachRoot.SetActive(true);game.UI.helpButton.GetComponentInChildren<Text>().text="Exit Tutorial";
  game.BreakStreak();RefreshLayout();SetStep(LessonStep.Reveal);game.Shuffle.InitialDeal();
 }
 public bool CanTap(CardController card){
  if(!InLesson)return true;
  return card.Index==0&&((Step==LessonStep.Reveal&&card.State==CardState.FaceDown)||(Step==LessonStep.Collect&&card.State==CardState.FaceUp));
 }
 public void OnRevealed(CardController card){if(InLesson&&Step==LessonStep.Reveal&&card.Index==0)SetStep(LessonStep.Collect);}
 public bool OnLimitReached(CardController card){return false;}
 public void OnReturned(){}
 public void OnRejected(CardController card){}
 public void OnCollected(CardController card){if(InLesson&&Step==LessonStep.Collect&&card.Index==0){SetStep(LessonStep.Discard);game.Cards.cards[1].PreviewForTutorial();}}
 public void OnDiscarded(CardController card){if(InLesson&&Step==LessonStep.Discard&&card.Index==1)SetStep(LessonStep.Shuffle);}
 public void OnShuffled(){if(InLesson&&Step==LessonStep.Shuffle)CompleteLesson();}
 public void Continue(){if(InLesson&&Step==LessonStep.Complete)PlayRound();}
 public void CompleteLesson(){SetStep(LessonStep.Complete);PlayerPrefs.SetInt("LittleFinds.TutorialComplete",1);PlayerPrefs.Save();}
 public FindData[] BuildDeck(){
  // One useful Fish, one obvious flower to discard; the rest stay out of the lesson.
  int[] ids={0,12,4,8,1,5,9,2,6,10,3,7};
  var result=new FindData[12];for(int i=0;i<12;i++)result[i]=game.Cards.Item(ids[i]);return result;
 }
 void SetStep(LessonStep step){
  Step=step;foreach(var card in game.Cards.cards)card.ResetHover();
  bool complete=step==LessonStep.Complete;nextButton.gameObject.SetActive(complete);nextLabel.text="Play!";
  if(hand)hand.GetComponentInChildren<Text>().text=step==LessonStep.Discard?"DRAG":"TAP";
  switch(step){
   case LessonStep.Reveal:heading.text="1/4  Tap the card to flip it";break;
   case LessonStep.Collect:heading.text="2/4  Tap again to collect Fish";break;
   case LessonStep.Discard:heading.text="3/4  Drag the flower to Trash";break;
   case LessonStep.Shuffle:heading.text="4/4  Tap Fresh Cards to shuffle";break;
   default:heading.text="Fill all three boxes. Ready?";break;
  }
  heading.rectTransform.sizeDelta=new Vector2(complete?730:964,132);
  heading.alignment=complete?TextAnchor.MiddleLeft:TextAnchor.MiddleCenter;
  if(step!=LessonStep.Reveal)game.Feedback.Play(10);
  RefreshTarget();
 }
 void RefreshTarget(){
  target=null;
  if(Step==LessonStep.Reveal||Step==LessonStep.Collect)target=(RectTransform)game.Cards.cards[0].transform;
  else if(Step==LessonStep.Discard)target=(RectTransform)game.Cards.cards[1].transform;
  else if(Step==LessonStep.Shuffle)target=(RectTransform)game.UI.freshButton.transform;
  // Tutorial decoration must never intercept card or button input in the playable.
  foreach(var graphic in focus.GetComponentsInChildren<Graphic>(true))graphic.raycastTarget=false;
  foreach(var graphic in hand.GetComponentsInChildren<Graphic>(true))graphic.raycastTarget=false;
  focus.gameObject.SetActive(target!=null);hand.gameObject.SetActive(target!=null);
 }
 void LateUpdate(){
  if(!InLesson||!target||!target.gameObject.activeInHierarchy)return;
  target.GetWorldCorners(corners);
  Vector3 min=focus.parent.InverseTransformPoint(corners[0]),max=focus.parent.InverseTransformPoint(corners[2]);
  focus.localPosition=(min+max)*.5f;focus.sizeDelta=new Vector2(max.x-min.x+24,max.y-min.y+24);
  focus.localScale=Vector3.one*(1+Mathf.Sin(Time.unscaledTime*5)*.025f);
  hand.localPosition=new Vector3((min.x+max.x)*.5f,min.y-26+Mathf.Sin(Time.unscaledTime*5)*7,0);
 }
}
}
