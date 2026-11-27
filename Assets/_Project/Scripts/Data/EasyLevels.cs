namespace LittleFinds {
public sealed class EasyLevel {
 public readonly string Name;public readonly int[] Targets;public readonly float Seconds,ComboWindow,OpenSeconds,ReturnSeconds;public readonly int Distractors;
 public EasyLevel(string name,int a,int b,int c,float seconds,float comboWindow,int distractors,float openSeconds,float returnSeconds){Name=name;Targets=new[]{a,b,c};Seconds=seconds;ComboWindow=comboWindow;Distractors=distractors;OpenSeconds=openSeconds;ReturnSeconds=returnSeconds;}
}
public static class EasyLevels {
 // Later decks contain more decoys and need more refills, so their tighter rounds get a little extra time.
 public static readonly EasyLevel[] All={
  new EasyLevel("First Finds",0,1,2,35,6,0,4.5f,.28f),
  new EasyLevel("Sweet Picnic",2,4,3,40,5.5f,2,3.8f,.25f),
  new EasyLevel("Toy Band",1,5,0,45,5,4,3.1f,.22f),
  new EasyLevel("Garden Party",3,2,4,50,4.5f,6,2.5f,.19f),
  new EasyLevel("Grand Finale",5,1,4,60,4,8,2f,.16f)
 };
}
}
