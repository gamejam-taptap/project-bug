public class DialogueManager : BaseManager<DialogueManager>
{
    private DialogueUI _currentDialogueUI;

    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    public void StartDialogue(Dialogues dialogues, System.Action onComplete = null)
    {
        _currentDialogueUI = UIManager.Instance.Show("Dialogue").GetComponent<DialogueUI>();
        //CharacterManager.Instance.HideCharacter();
        _currentDialogueUI.StartDialogue(dialogues, onComplete);
    }


    public void EndDialogue()
    {
        
        UIManager.Instance.Hide("Dialogue");
        //CharacterManager.Instance.ShowCharacter();
    }
}