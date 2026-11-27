using UnityEngine;
using UnityEngine.UI;
namespace LittleFinds {
[ExecuteAlways] public class SafeArea : MonoBehaviour {
 public RectTransform content;
 Rect previous; Vector2 size;
 RectMask2D landscapeClip;
 void OnEnable() { Apply(); }
 Rect Viewport {
  get {
#if UNITY_LUNA
   return new Rect(0,0,Screen.width,Screen.height);
#else
   return Screen.safeArea;
#endif
  }
 }
 void Update() { if (previous != Viewport || size != new Vector2(Screen.width, Screen.height)) Apply(); }
 public void Apply() {
  if (!content || Screen.width == 0 || Screen.height == 0) return;
  previous = Viewport; size = new Vector2(Screen.width, Screen.height);
  // Wide playable containers keep the same 9:16 game area as a portrait phone.
  // The background is a sibling of this root and still fills the entire canvas.
  Rect frame = previous;
  bool landscape = Screen.width > Screen.height;
  if (landscape) {
   float width = Mathf.Min(frame.width, frame.height * 9f / 16f);
   frame = new Rect(frame.center.x - width * .5f, frame.y, width, frame.height);
  }
  var root = (RectTransform)transform;
  root.anchorMin = new Vector2(frame.xMin / Screen.width, frame.yMin / Screen.height);
  root.anchorMax = new Vector2(frame.xMax / Screen.width, frame.yMax / Screen.height);
  root.offsetMin = root.offsetMax = Vector2.zero;
  if (!landscapeClip) landscapeClip = GetComponent<RectMask2D>();
  if (landscape && !landscapeClip) landscapeClip = gameObject.AddComponent<RectMask2D>();
  if (landscapeClip) landscapeClip.enabled = landscape;
  Canvas.ForceUpdateCanvases();
  float scale = root.rect.width / 1080f;
  if (scale <= 0) return;
  content.anchorMin = content.anchorMax = content.pivot = new Vector2(.5f, landscape ? .5f : 1);
  content.localScale = Vector3.one * scale;
  content.sizeDelta = new Vector2(1020, root.rect.height / scale - 40);
  content.anchoredPosition = landscape ? Vector2.zero : new Vector2(0, -20);
  var layout = content.GetComponent<MobileGameLayout>(); if (layout) layout.Reflow();
 }
}
}
