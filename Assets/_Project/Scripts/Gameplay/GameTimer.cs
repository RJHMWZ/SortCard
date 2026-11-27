using UnityEngine;
namespace LittleFinds {
public class GameTimer:MonoBehaviour {
 public float Remaining {get;private set;}=120; public bool Running;
 public System.Action Expired;
 public void ResetTimer(float seconds=120){Remaining=seconds;Running=true;}
 public void Advance(float seconds){if(!Running)return;Remaining=Mathf.Max(0,Remaining-seconds);if(Remaining==0){Running=false;Expired?.Invoke();}}
 void Update(){Advance(Time.deltaTime);}
}
}
