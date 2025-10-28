using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stories", menuName = "Story")]
public class Stories : ScriptableObject
{
    public List<Story> storyList;
}

[Serializable]
public class Story
{
    public Sprite shot;
    public bool needBlackBg;
    public bool needWhiteBg;
    public string announcement;
}