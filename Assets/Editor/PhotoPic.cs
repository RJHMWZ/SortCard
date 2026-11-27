using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Unity.EditorCoroutines.Editor;

public class PhotoPic : EditorWindow
{
    string path = "Assets/Resources/Screenshot/";
    string picName = "preview.jpg";
    [MenuItem("Tools/PhotoPic")]

    public static void ShowPic()
    {
        GetWindow<PhotoPic>("PhotoPic");
    }
    private void OnGUI()
    {
        if (GUILayout.Button("截图"))
        {
            // 设置保存路径
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath = path + picName;
            EditorCoroutineUtility.StartCoroutineOwnerless(TakeScreenshot(filePath));
        }
    }
    IEnumerator TakeScreenshot(string filePath)
    {
        if(File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        ScreenCapture.CaptureScreenshot(filePath);
        Debug.Log("Screenshot saved to: " + filePath);
        while (!File.Exists(filePath))
        {
            yield return null;
        }
        AssetDatabase.Refresh();
        if (File.Exists(filePath))
        {
            EditorUtility.DisplayDialog("截图成功", "您已经截图成功到" + filePath, "确定");
        }
    }
}
