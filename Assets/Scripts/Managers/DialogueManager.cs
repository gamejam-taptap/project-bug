using System;
using UnityEngine;

public class DialogueManager : BaseManager<DialogueManager>
{
    private DialogueUI _currentDialogueUI;

    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    public void StartDialogue(Dialogues dialogues, Action onComplete = null)
    {
        Debug.Log($"[Dialogue] 开始对话：{dialogues.name}");
        _currentDialogueUI = UIManager.Instance.Show("Dialogue").GetComponent<DialogueUI>();
        _currentDialogueUI.StartDialogue(dialogues, onComplete);
    }


    public void EndDialogue()
    {
        
        UIManager.Instance.Hide("Dialogue");
    }
}