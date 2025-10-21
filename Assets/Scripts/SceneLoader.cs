using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;

public class SceneLoader : BaseManager<SceneLoader>
{
    private SceneHandler _currentScene;
    public float minLoadingTime = 1f;
    
    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    /// <summary>
    /// 加载新场景（可异步 + 带Loading画面）
    /// </summary>
    public void LoadScene(string sceneName, UnityAction onLoadComplete = null)
    {
        Debug.Log("[Scene] Loading: " + sceneName);
        
        StartCoroutine(LoadSceneAsync(sceneName, onLoadComplete));
    }

    private IEnumerator LoadSceneAsync(string sceneName, UnityAction onLoadComplete)
    {
        UIManager.Instance.Show("Cut");
        //切换场景，隐藏人物，关闭音乐
        CharacterManager.Instance.HideCharacter();
        AudioManager.Instance.StopBGM();
        
        var startTime = Time.time;

        var asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp!.allowSceneActivation = false;

        // 强制等待一定时间（例如用于动画）
        while (!asyncOp.isDone)
        {
            if (asyncOp.progress >= 0.9f && Time.time - startTime >= minLoadingTime)
            {
                asyncOp.allowSceneActivation = true;
            }
            yield return null;
        }
    
        UIManager.Instance.Hide("Cut");
        onLoadComplete?.Invoke();
        
        // 进入场景记录当前场景,不保存主菜单
        if (sceneName != "MainMenu")
        {
            DataManager.Instance.SetCurrentSceneName(sceneName);
        }
    }

    public void SetCurrentSceneHandler(SceneHandler scene)
    {
        _currentScene = scene;
    }

    public SceneHandler GetCurrentSceneHandler()
    {
        return _currentScene;
    }
}