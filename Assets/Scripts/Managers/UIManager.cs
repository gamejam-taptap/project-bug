using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    [Serializable]
    public class UIPage
    {
        public string name;
        public GameObject root;
    }

    [Header("UI Pages")]
    public List<UIPage> pages;

    private readonly Dictionary<string, GameObject> _pageDict = new();

    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    private void Start()
    {
        foreach (var page in pages)
        {
            if (page != null && page.root != null)
            {
                _pageDict[page.name] = page.root;
                page.root.SetActive(false); // 初始化隐藏所有页面
            }
        }
    }

    public GameObject Show(string pageName)
    {
        if (_pageDict.TryGetValue(pageName, out var page))
        {
            Debug.Log($"[UI] {pageName}显示");
            page.SetActive(true);
            return page;
        }

        Debug.LogWarning($"[UI] UI Page not found: {pageName}");
        return null;
    }

    public void Hide(string pageName)
    {
        if (_pageDict.TryGetValue(pageName, out var page))
        {
            Debug.Log($"[UI] {pageName}关闭");
            page.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"[UI] UI Page not found: {pageName}");
        }
    }
}
