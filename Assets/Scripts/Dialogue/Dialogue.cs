using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public Sprite characterSprite;
    [TextArea(3, 10)]
    public string[] sentences;
    public bool hasQuest;
}
