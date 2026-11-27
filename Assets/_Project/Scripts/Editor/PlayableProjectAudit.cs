using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace LittleFinds.Editor {
// Editor-only checks; none of the file or editor APIs enter the playable runtime.
public static class PlayableProjectAudit {
 [MenuItem("Little Finds/Validate Playable Project")]
 public static void Validate(){
  var scenes=EditorBuildSettings.scenes;
  if(scenes.Length!=1||!scenes[0].enabled||scenes[0].path!="Assets/_Project/Scenes/Game.unity")throw new Exception("Build must contain only Game.unity.");
  if(UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline)throw new Exception("Use the built-in renderer for this UI-only playable.");
  foreach(string path in AssetDatabase.GetDependencies(scenes[0].path,true))if(path.StartsWith("Assets/")&&!File.Exists(path))throw new Exception("Missing dependency: "+path);
  foreach(string path in Directory.GetFiles("Assets/_Project/Scripts","*.cs",SearchOption.AllDirectories)){
   if(path.Replace('\\','/').Contains("/Editor/"))continue;
   string code=File.ReadAllText(path);
   foreach(string unsupported in new[]{"UnityEditor","System.IO","System.Reflection","DllImport","AndroidJava","OnAudioFilterRead","AudioClip.Create","SceneManager.LoadScene"})if(code.Contains(unsupported))throw new Exception("Playable compatibility review: "+path+" contains "+unsupported);
  }
  if(!EditorApplication.isPlaying&&!EditorSceneManager.GetActiveScene().isDirty){
   var scene=EditorSceneManager.OpenScene(scenes[0].path);
   foreach(var root in scene.GetRootGameObjects())foreach(var item in root.GetComponentsInChildren<Transform>(true))if(GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(item.gameObject)>0)throw new Exception("Missing script on "+item.name);
  }
  Debug.Log("Playable project audit passed. Run Playworks Code Diagnostics and a Develop HTML build after installing the plugin.");
 }
}
}
