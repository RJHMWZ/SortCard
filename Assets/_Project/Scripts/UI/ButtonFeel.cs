using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
namespace LittleFinds {
public class ButtonFeel : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler {
 Button button;RectTransform rt;Coroutine bounce;GameManager game;
 void Awake(){button=GetComponent<Button>();rt=(RectTransform)transform;game=FindObjectOfType<GameManager>();}
 public void OnPointerDown(PointerEventData e){if(!button.IsInteractable())return;if(bounce!=null)StopCoroutine(bounce);rt.localScale=Vector3.one*.94f;if(game)game.Feedback.Play(7);}
 public void OnPointerUp(PointerEventData e){Restore();}
 public void OnPointerExit(PointerEventData e){Restore();}
 void Restore(){if(!rt||!isActiveAndEnabled)return;if(bounce!=null)StopCoroutine(bounce);bounce=StartCoroutine(Spring());}
 IEnumerator Spring(){Vector3 start=rt.localScale;for(float t=0;t<.18f;t+=Time.unscaledDeltaTime){float u=t/.18f;float k=1+2.3f*Mathf.Pow(u-1,3)+1.3f*Mathf.Pow(u-1,2);rt.localScale=Vector3.LerpUnclamped(start,Vector3.one,k);yield return null;}rt.localScale=Vector3.one;bounce=null;}
 void OnDisable(){if(rt)rt.localScale=Vector3.one;bounce=null;}
}
}
