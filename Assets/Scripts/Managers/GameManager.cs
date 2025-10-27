using Managers;
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
    public GameState CurrentState { get; private set; } = GameState.None;
    
    private bool _hasUsedElevator = false;
    

    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    private void Start()
    {
        Debug.Log("[Game] 进入游戏");
        
        Debug.Log("[Game] 进入主菜单");
        CurrentState = GameState.MainMenu;
        Time.timeScale = 1;
        UIManager.Instance.Show("Start");
    }
    
    public void StartGame()
    {
        Debug.Log("[Game] 开始游戏");
        
        CurrentState = GameState.Playing;
        Time.timeScale = 1;
        UIManager.Instance.Hide("Start");

        LevelManager.Instance.Load("Level0");
    }

    public void ResetGame()
    {
        Debug.Log("[Game] 重置游戏");
        
        DataUtility.ClearSave();
        DataManager.Instance.OnReset();

        // notifacation
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
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
        UIManager.Instance.Show("Start");
    }

    public void ResumeGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        UIManager.Instance.Hide("Start");
    }
}