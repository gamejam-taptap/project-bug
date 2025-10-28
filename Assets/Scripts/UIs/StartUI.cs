using UnityEngine;

public class StartUI : MonoBehaviour
{
    public void OnStartClicked()
    {
        AudioManager.Instance.PlaySFX("click-start");
        GameManager.Instance.StartGame();
    }

    public void OnQuitClicked()
    {
        AudioManager.Instance.PlaySFX("click-quit");
        GameManager.Instance.QuitGame();
    }

    public void OnResetClicked()
    {
        AudioManager.Instance.PlaySFX("click-quit");
        GameManager.Instance.ResetGame();
    }
}