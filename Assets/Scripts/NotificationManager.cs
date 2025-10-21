using System.Collections;
using UnityEngine;
using TMPro;

public class NotificationManager : BaseManager<NotificationManager>
{
    public static NotificationManager Instance;

    [Header("UI Components")]
    public GameObject notificationPanel;
    public TextMeshProUGUI notificationText;
    public CanvasGroup canvasGroup;
    public float displayDuration = 2f;

    private Coroutine currentCoroutine;

    void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    public void ShowNotification(string message)
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ShowNotificationCoroutine(message));
    }

    private IEnumerator ShowNotificationCoroutine(string message)
    {
        notificationText.text = message;
        notificationPanel.SetActive(true);
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(displayDuration);

        float fadeDuration = 0.5f;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        notificationPanel.SetActive(false);
        canvasGroup.alpha = 1f;
    }
}

