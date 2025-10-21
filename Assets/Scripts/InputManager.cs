using UnityEngine.InputSystem;

public class InputManager : BaseManager<InputManager>
{
    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    private void Update()
    {
        // 检测 Esc 键切换暂停状态
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        switch (GameManager.Instance.CurrentState)
        {
            case GameState.Playing:
                GameManager.Instance.PauseGame();
                break;
            case GameState.Paused:
                GameManager.Instance.ResumeGame();
                break;
        }
    }
}
