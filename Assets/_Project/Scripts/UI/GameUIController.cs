using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace LittleFinds {
public class GameUIController:MonoBehaviour {
 public GameManager game;public Button helpButton;public Text lives,time,message,modalTitle,modalDescription,modalButtonText,soundLabel,hapticLabel;public RectTransform modal;public GameObject overlay;public Button startButton,freshButton;public GameObject settingsRoot;public GameObject resultRoot;public CollectionBox[] resultBoxes;
 CountdownPressure clockPressure;
 int lastLives=-1;int lastSecond=-1;bool help;Coroutine toast;
 Coroutine modalMotion;CanvasGroup modalFade;bool dismissing;
 ResultSticker sticker;CanvasGroup actionFade;Image modalPaper,veil;Color buttonColor,buttonTextColor,veilColor;bool resultMode;
 void Awake(){modalPaper=modal.GetComponent<Image>();veil=overlay.GetComponent<Image>();veilColor=veil.color;buttonColor=startButton.GetComponent<Image>().color;buttonTextColor=modalButtonText.color;}
 void Start(){clockPressure=CountdownPressure.Create(game);soundLabel.text=game.Feedback.SoundOn?"Sound On":"Sound Off";hapticLabel.text=game.Feedback.Haptics?"Touch On":"Touch Off";}
 void Update(){
  if(!game)return;
  SyncLives();
  bool lesson=game.Tutorial&&game.Tutorial.InLesson;
  if(lesson){time.text="TRAINING";time.fontSize=44;}
  int seconds=Mathf.CeilToInt(game.Timer.Remaining);
  if(!lesson&&seconds!=lastSecond){lastSecond=seconds;time.text=(seconds/60).ToString("00")+":"+(seconds%60).ToString("00");}
  freshButton.interactable=game.State==GameState.Playing&&(!game.Tutorial||game.Tutorial.CanShuffle);
 }
 public void SyncLives(){if(lives&&lastLives!=game.Risk.Lives){lastLives=game.Risk.Lives;lives.text=lastLives.ToString();lives.color=lastLives<=2?FindStyle.Hex("dc5076"):FindStyle.Ink;}}
 public void ResetClock(){if(clockPressure)clockPressure.ResetVisual();lastSecond=-1;time.fontSize=game.Tutorial.InLesson?44:78;}
 public void Message(string value){
  string copy=null;
  if(value.StartsWith("Penalty: "))copy=value.Substring(9);
  else if(value.StartsWith("Shuffle cost:"))copy="Fresh Cards: -"+game.Risk.shuffleSeconds.ToString("0")+" seconds";
  else if(value.StartsWith("Pending:"))copy="The last treasure is already on its way.";
  else if(value.Contains("Three little"))copy="Collect an open card first.";
  else if(value.Contains("full"))copy="Collection complete! Try Fresh Cards.";
  else if(value.Contains("queued"))copy="Fresh cards coming right up!";
  if(copy==null)return;
  if(toast!=null)StopCoroutine(toast);
  message.text=copy;toast=StartCoroutine(Toast());
 }
 IEnumerator Toast(){message.gameObject.SetActive(true);yield return new WaitForSeconds(1.2f);message.gameObject.SetActive(false);toast=null;}
 public void CloseModal(){StopModalMotion();overlay.SetActive(false);modal.localScale=Vector3.one;modal.anchoredPosition=Vector2.zero;startButton.interactable=true;if(actionFade)actionFade.alpha=1;if(modalFade)modalFade.alpha=1;if(toast!=null)StopCoroutine(toast);message.gameObject.SetActive(false);toast=null;}
 public void ShowResult(bool won,int count){
  StopModalMotion();if(toast!=null)StopCoroutine(toast);toast=null;message.gameObject.SetActive(false);help=false;resultMode=true;modal.sizeDelta=new Vector2(820,600);modalPaper.enabled=false;
  if(resultRoot)resultRoot.SetActive(false);settingsRoot.SetActive(false);modalDescription.gameObject.SetActive(false);modalTitle.gameObject.SetActive(false);
  if(!sticker)sticker=ResultSticker.Create(modal,game.Feedback.font);sticker.gameObject.SetActive(true);
  string word=won?(game.LevelIndex==4?"ALL CLEAR!":"NAILED IT!"):game.Risk.Lives==0?"OUT OF LIVES!":"TIME'S UP!";
  sticker.Configure(won,game.LevelIndex+1,word);modalTitle.text=word;
  var button=(RectTransform)startButton.transform;button.anchoredPosition=new Vector2(150,-450);button.sizeDelta=new Vector2(520,126);modalButtonText.rectTransform.sizeDelta=new Vector2(512,126);modalButtonText.fontSize=58;
  startButton.GetComponent<Image>().color=FindStyle.Ink;modalButtonText.color=Color.white;
  modalButtonText.text=won?(game.LevelIndex==4?"PLAY AGAIN  >":"NEXT LEVEL  >"):"TRY AGAIN  >";
  veil.color=new Color(.145f,.169f,.294f,.30f);
  if(!actionFade){actionFade=startButton.GetComponent<CanvasGroup>();if(!actionFade)actionFade=startButton.gameObject.AddComponent<CanvasGroup>();}
  OpenModalAnimated();
 }
 public void ShowHelp(){if(game.Tutorial.InLesson)game.Tutorial.ExitLevel();else game.Tutorial.EnterLevel();}
 void ModalMode(){resultMode=false;if(sticker)sticker.gameObject.SetActive(false);modalPaper.enabled=true;modalTitle.gameObject.SetActive(true);veil.color=veilColor;startButton.GetComponent<Image>().color=buttonColor;modalButtonText.color=buttonTextColor;modalButtonText.fontSize=52;startButton.interactable=true;if(actionFade)actionFade.alpha=1;modal.sizeDelta=new Vector2(920,800);modalTitle.rectTransform.anchoredPosition=new Vector2(50,-42);modalTitle.rectTransform.sizeDelta=new Vector2(820,100);modalTitle.fontSize=54;modalDescription.gameObject.SetActive(false);var button=(RectTransform)startButton.transform;button.anchoredPosition=new Vector2(60,-620);button.sizeDelta=new Vector2(800,132);modalButtonText.rectTransform.sizeDelta=new Vector2(792,132);}
 public void ShowSettings(){StopModalMotion();ModalMode();modal.sizeDelta=new Vector2(920,965);((RectTransform)startButton.transform).anchoredPosition=new Vector2(60,-785);helpButton.GetComponentInChildren<Text>().text=game.Tutorial.InLesson?"Exit Tutorial":"How to play";help=true;if(resultRoot)resultRoot.SetActive(false);settingsRoot.SetActive(true);modalTitle.text="Settings";modalDescription.text="";modalButtonText.text="Back to play";OpenModalAnimated();}
 public void StartClicked(){if(dismissing||!overlay.activeSelf)return;game.Feedback.Play(7);bool settings=help;bool victory=game.State==GameState.Victory;StopModalMotion();dismissing=true;modalMotion=StartCoroutine(DismissModal(settings,victory));}
 void StopModalMotion(){if(modalMotion!=null)StopCoroutine(modalMotion);modalMotion=null;dismissing=false;}
 void OpenModalAnimated(){
  if(!modalFade){modalFade=overlay.GetComponent<CanvasGroup>();if(!modalFade)modalFade=overlay.AddComponent<CanvasGroup>();}
  overlay.SetActive(true);modalFade.alpha=0;modalFade.blocksRaycasts=true;if(resultMode){modal.localScale=Vector3.one;modal.anchoredPosition=Vector2.zero;startButton.interactable=false;actionFade.alpha=0;modalMotion=StartCoroutine(RevealSticker());return;}modal.localScale=Vector3.one*.86f;modal.anchoredPosition=new Vector2(0,-42);
  modalMotion=StartCoroutine(RevealModal());
 }
 IEnumerator RevealModal(){
  const float duration=.34f;
  for(float t=0;t<duration;t+=Time.unscaledDeltaTime){float u=t/duration;float spring=1+2.2f*Mathf.Pow(u-1,3)+1.2f*Mathf.Pow(u-1,2);modalFade.alpha=Mathf.SmoothStep(0,1,Mathf.Clamp01(u/.65f));modal.localScale=Vector3.one*Mathf.LerpUnclamped(.86f,1,spring);modal.anchoredPosition=Vector2.LerpUnclamped(new Vector2(0,-42),Vector2.zero,spring);yield return null;}
  modalFade.alpha=1;modal.localScale=Vector3.one;modal.anchoredPosition=Vector2.zero;modalMotion=null;
 }
 IEnumerator RevealSticker(){
  var card=sticker.rectTransform;float angle=sticker.Won?-4:3;
  for(float t=0;t<.48f;t+=Time.unscaledDeltaTime){float u=Mathf.Clamp01(t/.38f);float spring=1+2.2f*Mathf.Pow(u-1,3)+1.2f*Mathf.Pow(u-1,2);modalFade.alpha=Mathf.SmoothStep(0,1,Mathf.Clamp01(t/.16f));
   card.localScale=Vector3.one*Mathf.LerpUnclamped(sticker.Won?1.65f:.7f,1,spring);card.localRotation=Quaternion.Euler(0,0,Mathf.LerpUnclamped(sticker.Won?-18:14,angle,spring));card.anchoredPosition=Vector2.LerpUnclamped(new Vector2(sticker.Won?-60:0,sticker.Won?-140:-300),new Vector2(0,-220),spring);
   actionFade.alpha=Mathf.SmoothStep(0,1,Mathf.Clamp01((t-.18f)/.2f));yield return null;}
  card.localScale=Vector3.one;card.localRotation=Quaternion.Euler(0,0,angle);card.anchoredPosition=new Vector2(0,-220);modalFade.alpha=actionFade.alpha=1;startButton.interactable=true;modalMotion=null;
 }
 IEnumerator DismissModal(bool settings,bool victory){
  Vector3 scale=modal.localScale;Vector2 position=modal.anchoredPosition;float alpha=modalFade?modalFade.alpha:1;
  Vector2 cardFrom=sticker?sticker.rectTransform.anchoredPosition:Vector2.zero;Quaternion turn=sticker?sticker.rectTransform.localRotation:Quaternion.identity;
  float duration=settings?.16f:.24f;
  for(float t=0;t<duration;t+=Time.unscaledDeltaTime){float u=Mathf.SmoothStep(0,1,t/duration);if(modalFade)modalFade.alpha=alpha*(1-u);
   if(resultMode&&sticker){sticker.rectTransform.anchoredPosition=cardFrom+new Vector2(victory?180:-100,130)*u;sticker.rectTransform.localRotation=turn*Quaternion.Euler(0,0,u*(victory?-18:16));sticker.rectTransform.localScale=Vector3.one*(1+.12f*u);actionFade.alpha=1-u;}
   else{modal.localScale=Vector3.Lerp(scale,Vector3.one*.94f,u);modal.anchoredPosition=position+Vector2.down*(u*20);}yield return null;}
  modalMotion=null;CloseModal();help=false;if(!settings){if(victory)game.ContinueCampaign();else game.Begin();}
 }
 public void ToggleSound(){game.Feedback.SoundOn=!game.Feedback.SoundOn;PlayerPrefs.SetInt("LittleFinds.Sound",game.Feedback.SoundOn?1:0);if(!game.Feedback.SoundOn&&game.Feedback.audio)game.Feedback.audio.StopAll();soundLabel.text=game.Feedback.SoundOn?"Sound On":"Sound Off";game.Feedback.Play(7);}
 public void ToggleHaptics(){game.Feedback.Haptics=!game.Feedback.Haptics;PlayerPrefs.SetInt("LittleFinds.Haptics",game.Feedback.Haptics?1:0);hapticLabel.text=game.Feedback.Haptics?"Touch On":"Touch Off";}
}
}
