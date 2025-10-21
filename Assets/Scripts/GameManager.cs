using UnityEngine;

public enum GameState
{
    None,
    MainMenu,
    Playing,
    Paused
}

public class GameManager : BaseManager<GameManager>
{
    public event System.Action OnElevatorUsed;
    
    public GameState CurrentState { get; private set; } = GameState.None;
    public GameObject postProcessingVolume;
    
    private bool _hasUsedElevator = false;
    

    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    private void Start()
    {
        Debug.Log("[Game] 进入游戏");

        SceneLoader.Instance.LoadScene("MainMenu", MainMenu);
    }

    public void MainMenu()
    {
        Debug.Log("[Game] 进入主菜单");

        postProcessingVolume.SetActive(true);
        CurrentState = GameState.MainMenu;
        Time.timeScale = 1;
        UIManager.Instance.Show("Start");
    }
    
    public void StartGame()
    {
        Debug.Log("[Game] 开始游戏");
        
        postProcessingVolume.SetActive(false);
        CurrentState = GameState.Playing;
        Time.timeScale = 1;
        UIManager.Instance.Hide("Start");
        
        // 读取保存的场景名
        var sceneName = DataManager.Instance.GetCurrentSceneName();
        SceneLoader.Instance.LoadScene(sceneName);
        
        // todo fix
        //CheckHaveSyringe();
    }

    public void ResetGame()
    {
        Debug.Log("[Game] 重置游戏");
        
        UIManager.Instance.Hide("Start");
        DataUtility.ClearSave();
        DataManager.Instance.OnReset();
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene("MainMenu", MainMenu);
    }

    public void QuitGame()
    {
        Debug.Log("[Game] 退出游戏");
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PauseGame()
    {
        postProcessingVolume.SetActive(true);
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
        UIManager.Instance.Show("Start");
    }

    public void ResumeGame()
    {
        postProcessingVolume.SetActive(false);
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        UIManager.Instance.Hide("Start");
    }

    public void SwitchCurrentSceneWorld()
    {
        SceneLoader.Instance.GetCurrentSceneHandler()?.SwitchWorldWithEffects();
    }

    public void SetPostProcessingVolume(bool show)
    {
        postProcessingVolume.SetActive(show);
    }
}