using UnityEngine;

public class StartUI : MonoBehaviour
{
    public void OnStartClicked()
    {
        GameManager.Instance.StartGame();
    }

    public void OnQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }

    public void OnResetClicked()
    {
        GameManager.Instance.ResetGame();
    }
}