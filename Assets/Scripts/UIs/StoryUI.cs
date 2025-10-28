using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryUI : MonoBehaviour
{
    public Image storyImage;
    public GameObject black;
    public GameObject white;
    public float playSpeed;

    private Queue<Story> _stories;
    private Action _onComplete;
    

    private void Awake()
    {
        _stories = new Queue<Story>();
    }

    public void StartStory(Stories stories, Action onComplete = null)
    {
        _stories.Clear();
        _onComplete = onComplete;

        foreach (var story in stories.storyList)
        {
            _stories.Enqueue(story);
        }

        DisplayNextShot();
    }
    
    private void EndStory()
    {
        StoryManager.Instance.EndStory();
        _onComplete?.Invoke();
        _onComplete = null;
    }
    
    private void DisplayNextShot()
    {
        if (_stories.Count == 0)
        {
            EndStory();
            return;
        }

        var story = _stories.Dequeue();
        RefreshUI(story);
        StartCoroutine(PlayStory());
    }

    private void RefreshUI(Story story)
    {
        black.SetActive(story.needBlackBg);
        white.SetActive(story.needWhiteBg);

        storyImage.sprite = story.shot;
    }

    private IEnumerator PlayStory()
    {
        yield return new WaitForSeconds(playSpeed);
        DisplayNextShot();
    }
}