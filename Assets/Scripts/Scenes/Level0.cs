using UnityEngine;

public class Level0 : MonoBehaviour
{
    public Dialogues dialogue;
    
    public void Start()
    {
        DialogueManager.Instance.StartDialogue(dialogue, OnComplete);
    }

    private void OnComplete()
    {
        
    }
}