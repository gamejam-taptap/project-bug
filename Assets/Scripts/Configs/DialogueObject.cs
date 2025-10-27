using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues", menuName = "Dialogue")]
public class Dialogues : ScriptableObject
{
    public List<Dialogue> dialogueList;
}

[Serializable]
public class Dialogue
{
    public string speakerName;
    [TextArea(3, 10)]
    public string sentence;
    public Sprite characterAvatar;
    public Sprite image;
}