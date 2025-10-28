using System;
using UnityEngine;

public class StoryManager : BaseManager<StoryManager>
{
    private StoryUI _currentUI;
    
    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    public void StartStory(Stories stories, Action onComplete = null)
    {
        Debug.Log($"[Story] 开始剧情：{stories.name}");
        _currentUI = UIManager.Instance.Show("Story").GetComponent<StoryUI>();
        _currentUI.StartStory(stories, onComplete);
    }
    
    public void EndStory()
    {
        UIManager.Instance.Hide("Story");
    }
}
