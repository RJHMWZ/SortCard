
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using Newtonsoft.Json.Linq;
using Object = UnityEngine.Object;
using System.Linq;


[Serializable]
public class SpriteData
{
    public int id;
    public string path;
    public int assetbundleId;
    public List<object> data;
}

[Serializable]
public class SpriteRoot
{
    public List<SpriteData> sprites;
}

[Serializable]
public class SpineData
{
    public string __type__;
    public string _name;
    public string _skeletonJson;
}

[Serializable]
public class SpineRoot
{
    public List<SpineData> spines;
}

public class SpriteFrameWrapper
{
    [JsonProperty("__type__")]
    public string Type { get; set; }

    [JsonProperty("content")]
    public SpriteFrame Content { get; set; }    
}

public class SpriteFrame
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("texture")]
    public string Texture { get; set; } = "default_texture";


    [JsonProperty("rect")]
    public List<int> Rect { get; set; }  // [x, y, width, height]

    [JsonProperty("rotated")]
    public int Rotated { get; set; }

    [JsonProperty("offset")]
    public List<float> Offset { get; set; }

    [JsonProperty("originalSize")]
    public List<int> OriginalSize { get; set; }

    [JsonProperty("capInsets")]
    public List<int> CapInsets { get; set; }
}

public struct RectData
{
    public int posx;
    public int posy;
    public int width;
    public int height;
    public float pivotX;
    public float pivotY;
}

public class SpawnTool:EditorWindow
{
    private TextAsset sourceJson;
    private int neededCount = 0;
    private Texture2D[] selectedTextures;

    private SpriteRoot root ;


    private Dictionary<string, Texture2D> textureDic;

    private List<SpriteFrameWrapper> spriteFrames;

    private HashSet<string> textureIds;
    [MenuItem("Tools/SpawnTool")]
    public static void ShowWindow()
    {
        GetWindow<SpawnTool>("SpawnTool");
    }

    private void OnGUI()
    {
        sourceJson = (TextAsset)EditorGUILayout.ObjectField("SourceJson", sourceJson, typeof(TextAsset), false);

        if (GUILayout.Button("检测需要图片数量"))
        {
            root = null;
            if (sourceJson != null)
            {
                try
                {
                    root = JsonConvert.DeserializeObject<SpriteRoot>(sourceJson.text);

                    if (root == null)
                    {
                        Debug.LogError("反序列化结果为 null，非Unity图集");
                    }
                    else
                    {
                        textureIds = new();
                        foreach (var sprite in root.sprites)
                        {
                            object value = sprite.data[2];
                            string key = value.ToString();
                            if (key[0] == '-') textureIds.Add(key);
                        }

                        neededCount = textureIds.Count;
                        var old = selectedTextures;
                        selectedTextures = new Texture2D[neededCount];
                        for (int i = 0; i < neededCount; i++)
                        {
                            if (old != null && i < old.Length)
                                selectedTextures[i] = old[i];
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Debug.Log("UnityJSON 反序列化失败" );
                }
                
                if (root==null||root.sprites.Count==0)
                {
                    spriteFrames = new List<SpriteFrameWrapper>();
                    try
                    {
                        JArray rawArray = JArray.Parse(sourceJson.text);

                        foreach (var item in rawArray)
                        {
                            if (item is JObject obj && obj.ContainsKey("__type__"))
                            {
                                string type = obj["__type__"]?.ToString();
                                if (type == "cc.SpriteFrame")
                                {
                                    var frame = item.ToObject<SpriteFrameWrapper>();
                                    if (!(frame.Content.Rect[0] == 0 && frame.Content.Rect[1]==0)) 
                                        spriteFrames.Add(frame);
                                }
                            }
                        }

                        textureIds = new();
                        foreach (var sprite in spriteFrames)
                        {
                            textureIds.Add(sprite.Content.Texture);
                        }

                        neededCount = textureIds.Count;
                        var old = selectedTextures;
                        selectedTextures = new Texture2D[neededCount];
                        for (int i = 0; i < neededCount; i++)
                        {
                            if (old != null && i < old.Length)
                                selectedTextures[i] = old[i];
                        }
                    }
                    catch (JsonException ex)
                    {
                        Debug.Log("Cocos1解析 JSON 失败 ");
                    }
                    if (spriteFrames.Count == 0)
                    {
                        try
                        {
                            JArray rawArray = JArray.Parse(sourceJson.text);
                            JArray frames = rawArray.Last() as JArray;
                            
                            foreach(var frame in frames)
                            {
                                JArray spriteFrame = frame[0] as JArray;

                                var token = spriteFrame[0];

                                if (token.Type == JTokenType.Object)
                                {
                                    var obj = (JObject)token;
                                    SpriteFrameWrapper sfw = new();
                                    if (obj.ContainsKey("name"))
                                    {
                                        sfw.Content = obj.ToObject<SpriteFrame>();
                                        spriteFrames.Add(sfw);
                                    }
                                }
                            }

                            textureIds = new();
                            foreach (var sprite in spriteFrames)
                            {
                                textureIds.Add(sprite.Content.Texture);
                            }

                            neededCount = textureIds.Count;
                            var old = selectedTextures;
                            selectedTextures = new Texture2D[neededCount];
                            for (int i = 0; i < neededCount; i++)
                            {
                                if (old != null && i < old.Length)
                                    selectedTextures[i] = old[i];
                            }
                        }
                        catch (JsonException ex)
                        {
                            Debug.LogError("Cocos2解析JSON 失败 "+ex.Message);
                        }
                    }
                }
                
            }
        }

        if (selectedTextures != null)
        {
            EditorGUILayout.LabelField("请选择对应图片：");

            for (int i = 0; i < selectedTextures.Length; i++)
            {
                selectedTextures[i] = (Texture2D)EditorGUILayout.ObjectField($"图片 {i }", selectedTextures[i], typeof(Texture2D), false);
            }
        }



        if (GUILayout.Button("样本测试"))
        {
            if(root==null||root.sprites.Count == 0)
            {
                textureDic = new();
                List<Texture2D> textures = selectedTextures.ToList();
                int i = 0;
                foreach(var sprite in spriteFrames)
                {
                    i++;
                    int j = 0;
                    RectData rect = GetRect(sprite.Content);
                    string key = sprite.Content.Texture;
                    if (!textureDic.ContainsKey(key) && textureDic.Count < neededCount)
                    {
                        textureDic.Add(key, null);
                        foreach (var texture in textures)
                        {
                            Texture2D selectedImg = texture;
                            string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedImg));
                            string path = rootPath + "/" + selectedImg.name + ".png";
                            TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;
                            // 设置为可读
                            texImp.isReadable = true;
                            AssetDatabase.ImportAsset(path);

                            // 创建文件夹
                            if (j == 0) AssetDatabase.CreateFolder(rootPath, i.ToString());
                            var width = (int)rect.width;
                            var height = (int)rect.height;
                            Texture2D smallImg = new Texture2D(width, height);
                            var pixelStartX = rect.posx;
                            var pixelEndX = pixelStartX + rect.width;
                            var pixelStartY = texture.height-rect.height-rect.posy;
                            var pixelEndY = pixelStartY + height;

                            for (int x = pixelStartX; x <= pixelEndX; ++x)
                            {
                                for (int y = pixelStartY; y <= pixelEndY; ++y)
                                {
                                    smallImg.SetPixel(x - pixelStartX, y - pixelStartY, selectedImg.GetPixel(x, y));
                                }
                            }
                            string smallImgPath = rootPath + "/" + i.ToString() + "/" + (i * textures.Count + j++).ToString() + ".png";

                            SaveTexture(smallImg, smallImgPath, rect);

                        }

                    }
                }
            }
            else
            {
                textureDic = new();
                List<Texture2D> textures = selectedTextures.ToList();
                int i = 0;
                foreach (var sprite in root.sprites)
                {
                    int j = 0;
                    RectData rect = GetRect(sprite);
                    object value = sprite.data[2];

                    string key = value.ToString();
                    if (!textureDic.ContainsKey(key) && textureDic.Count < neededCount)
                    {
                        i++;
                        textureDic.Add(key, null);
                        foreach (var texture in textures)
                        {
                            Texture2D selectedImg = texture;
                            string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedImg));
                            string path = rootPath + "/" + selectedImg.name + ".png";
                            TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;
                            // 设置为可读
                            texImp.isReadable = true;
                            AssetDatabase.ImportAsset(path);

                            // 创建文件夹
                            if (j++ == 0) AssetDatabase.CreateFolder(rootPath, i.ToString());
                            var width = (int)rect.width;
                            var height = (int)rect.height;
                            Texture2D smallImg = new Texture2D(width, height);
                            var pixelStartX = rect.posx;
                            var pixelEndX = pixelStartX + rect.width;
                            var pixelStartY = rect.posy;
                            var pixelEndY = pixelStartY + height;

                            for (int x = pixelStartX; x <= pixelEndX; ++x)
                            {
                                for (int y = pixelStartY; y <= pixelEndY; ++y)
                                {
                                    smallImg.SetPixel(x - pixelStartX, y - pixelStartY, selectedImg.GetPixel(x, y));
                                }
                            }
                            string smallImgPath = rootPath + "/" + i.ToString() + "/" + sprite.data[0].ToString()+"_" +j.ToString() + ".png";

                            SaveTexture(smallImg, smallImgPath, rect);

                        }

                    }
                }
            }
            
        }
        if (GUILayout.Button("生成纹理"))
        {
            if (root != null && root.sprites.Count > 0)
            {
                int i = 0;
                List<string> keys = textureDic.Keys.ToList();
                for (int t = 0; t < keys.Count; t++)
                {
                    textureDic[keys[t]] = selectedTextures[t];
                }
                string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(textureDic[keys[0]]));
                string folderPath = Path.Combine(rootPath, "atlas"); // 例如 "Assets/MyFolder/atlas"

                if (!AssetDatabase.IsValidFolder(folderPath))
                    AssetDatabase.CreateFolder(rootPath, "atlas");


                foreach (var sprite in root.sprites)
                {
                    i++;
                    RectData rect = GetRect(sprite);
                    if (!textureDic.ContainsKey(sprite.data[2].ToString()))
                        continue;
                    Texture2D texture = textureDic[sprite.data[2].ToString()];
                    Texture2D selectedImg = texture;

                    string path = rootPath + "/" + selectedImg.name + ".png";
                    TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;
                    // 设置为可读
                    texImp.isReadable = true;
                    AssetDatabase.ImportAsset(path);

                    // 创建文件夹

                    var width = (int)rect.width;
                    var height = (int)rect.height;
                    Texture2D smallImg = new Texture2D(width, height);
                    var pixelStartX = rect.posx;
                    var pixelEndX = pixelStartX + rect.width;
                    var pixelStartY = rect.posy;
                    var pixelEndY = pixelStartY + height;

                    for (int x = pixelStartX; x <= pixelEndX; ++x)
                    {
                        for (int y = pixelStartY; y <= pixelEndY; ++y)
                        {
                            smallImg.SetPixel(x - pixelStartX, y - pixelStartY, selectedImg.GetPixel(x, y));
                        }
                    }
                    string smallImgPath = rootPath + "/" + "atlas" + "/" + sprite.data[0].ToString() + ".png";

                    SaveTexture(smallImg, smallImgPath, rect);
                }
            }
            else
            {
                List<string> keys = textureDic.Keys.ToList();
                for (int t = 0; t < keys.Count; t++)
                {
                    textureDic[keys[t]] = selectedTextures[t];
                }
                string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(textureDic[keys[0]]));
                string folderPath = Path.Combine(rootPath, "atlas"); // 例如 "Assets/MyFolder/atlas"

                if (!AssetDatabase.IsValidFolder(folderPath))
                    AssetDatabase.CreateFolder(rootPath, "atlas");
                foreach (var sprite in spriteFrames)
                {
                    RectData rect = GetRect(sprite.Content);
                    if (!textureDic.ContainsKey(sprite.Content.Texture))
                        continue;
                    Texture2D texture = textureDic[sprite.Content.Texture];
                    Texture2D selectedImg = texture;

                    string path = rootPath + "/" + selectedImg.name + ".png";
                    TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;
                    // 设置为可读
                    texImp.isReadable = true;
                    AssetDatabase.ImportAsset(path);

                    // 创建文件夹

                    var width = (int)rect.width;
                    var height = (int)rect.height;
                    Texture2D smallImg = new Texture2D(width, height);
                    var pixelStartX = rect.posx;
                    var pixelEndX = pixelStartX + rect.width;
                    var pixelStartY = texture.height - rect.height - rect.posy;
                    var pixelEndY = pixelStartY + height;

                    for (int x = pixelStartX; x <= pixelEndX; ++x)
                    {
                        for (int y = pixelStartY; y <= pixelEndY; ++y)
                        {
                            smallImg.SetPixel(x - pixelStartX, y - pixelStartY, selectedImg.GetPixel(x, y));
                        }
                    }
                    string smallImgPath = rootPath + "/" + "atlas" + "/" + sprite.Content.Name + ".png";

                    SaveTexture(smallImg, smallImgPath, rect,sprite.Content.Rotated==1);
                    
                }
            }
            
        }
        if (GUILayout.Button("提取SpineJson"))
        {
            string jsonText = sourceJson.text;
            JArray rootArray = JArray.Parse(jsonText);

            foreach (var token in rootArray)
            {
                if (token is JObject obj)
                {
                    string name = obj["_name"]?.ToString();
                    JObject skeletonJson = obj["_skeletonJson"] as JObject;

                    if (string.IsNullOrEmpty(name) || skeletonJson == null)
                    {
                        Debug.LogWarning("跳过无效数据");
                        continue;
                    }

                    string outputJson = JsonConvert.SerializeObject(skeletonJson, Formatting.Indented);
                    string outputPath = Path.Combine("Assets/Resources/", name + ".json");

                    File.WriteAllText(outputPath, outputJson);
                    Debug.Log($"成功导出: {outputPath}");
                }
                else
                {
                    Debug.LogWarning("数组中存在非对象元素，已跳过");
                }
            }
        }
    }

    public static Texture2D RotateTexture(Texture2D original)
    {
        int width = original.width;
        int height = original.height;

        Texture2D rotated;

        rotated = new Texture2D(height, width);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                rotated.SetPixel(height - y - 1, x, original.GetPixel(x, y));
            }
        }

        rotated.Apply();
        return rotated;
    }
    static RectData GetRect(SpriteFrame data)
    {
        RectData rect = new RectData();
        rect.width = data.Rect[data.Rotated==1?3:2];
        rect.height = data.Rect[data.Rotated==1?2:3];
        rect.pivotX = ((float)data.Rect[2] / 2 - data.Offset[0]) / data.Rect[2];
        rect.pivotY = ((float)data.Rect[3] / 2- data.Offset[1]) / data.Rect[3];

        rect.posx = data.Rect[0];
        rect.posy = data.Rect[1];
        return rect;
        
    }

    static RectData GetRect(SpriteData data)
    {
        RectData rect = new RectData();
        int cnt = 0;
        float minU = float.MaxValue;
        float maxU = float.MinValue;
        float minV = float.MaxValue;
        float maxV = float.MinValue;
        foreach (var obj in data.data)
        {
            JArray arr = obj as JArray;
            if (arr != null)
            {
                float[] result = arr.ToObject<float[]>();
                if (cnt == 1)
                {
                    for (int j = 0; j < result.Length; j++)
                    {
                        if (j % 5 == 3)
                        {
                            minU = Math.Min(minU, result[j]);
                            maxU = Math.Max(maxU, result[j]);
                        }
                        if (j % 5 == 4)
                        {
                            minV = Math.Min(minV, result[j]);
                            maxV = Math.Max(maxV, result[j]);
                        }
                    }
                }
                cnt++;
            }
        }

        int width = Convert.ToInt16(data.data[^8]);
        int heigh = Convert.ToInt16(data.data[^7]);
        float pX = (float)Convert.ToDouble(data.data[^2]);
        float pY =(float)Convert.ToDouble(data.data[^1]);
        

        rect.posx = (int)(minU * width);
        rect.posy = (int)(minV * heigh);
        rect.width = (int)((maxU - minU) * width);
        rect.height = (int)((maxV - minV) * heigh);

        rect.pivotX = 0.5f;
        rect.pivotY = -pY/rect.height;
        

        return rect;
    }


    static void SaveTexture(Texture2D smallImg, string smallImgPath,RectData rect,bool rot = false)
    {
        Texture2D img = new Texture2D(smallImg.width, smallImg.height);
        img.SetPixels(smallImg.GetPixels(0), 0);
        smallImg = img;
        if(rot) smallImg =  RotateTexture(smallImg);
        File.WriteAllBytes(smallImgPath, smallImg.EncodeToPNG());
        AssetDatabase.Refresh();
        TextureImporter smallTextureImp = AssetImporter.GetAtPath(smallImgPath) as TextureImporter;
        smallTextureImp.isReadable = true;
        smallTextureImp.alphaIsTransparency = true;
        smallTextureImp.mipmapEnabled = false;
        smallTextureImp.textureType = TextureImporterType.Sprite;
        smallTextureImp.spriteImportMode = SpriteImportMode.Single;
        smallTextureImp.spritePixelsPerUnit = 100; // 可根据项目修改
        smallTextureImp.spritePivot = new Vector2(rect.pivotX,rect.pivotY); // 仅用于 PivotUnitMode.Center
        smallTextureImp.spritePixelsPerUnit = 100; // 控制 Sprite 尺寸单位（可自定义）
        AssetDatabase.ImportAsset(smallImgPath);
    }

}
