using UnityEngine;
namespace LittleFinds {
public enum GameState { Ready, Playing, Shuffling, Victory, Failed }
public enum CardState { FaceDown, Flipping, FaceUp, Collecting, Returning, Removed }
[System.Serializable] public class FindData {
 public string title; public int category; public Sprite art;
 public FindData(string title,int category,Sprite art){this.title=title;this.category=category;this.art=art;}
}
public static class FindStyle {
 public static Color Ink=Hex("252b4b"), Soft=Hex("8b90aa"), Sage=Hex("6170ff");
 public static string[] Names={"Fish","Toys","Fruit","Flowers","Sweets","Music"};
 public static Color[] Colors={Hex("5baeff"),Hex("9780ed"),Hex("ffae35"),Hex("ed7399"),Hex("e78c59"),Hex("38b797")};
 public static Color Hex(string value){ColorUtility.TryParseHtmlString("#"+value,out var c);return c;}
 public static float Ease(float t){return 1-Mathf.Pow(1-t,3);}
}
}
