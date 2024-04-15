using UnityEngine;

[CreateAssetMenu(fileName = "Costume", menuName = "Data")]
public class CostumeData : ScriptableObject
{
    public Sprite graphic;
    public int number;
    public string name;
    [TextArea] public string desc;
}
