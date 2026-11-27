using UnityEngine;
namespace LittleFinds {
public static class TouchFeedback {
 public static void Damage(){Vibrate();}
 public static void Tap(){Vibrate();}
 static void Vibrate(){
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR && !UNITY_WEBGL && !UNITY_LUNA
  Handheld.Vibrate();
#endif
 }
}
}
