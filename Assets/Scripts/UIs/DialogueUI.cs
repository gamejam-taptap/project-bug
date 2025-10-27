using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;
    public Image avatarImage;
    //public Image image;
    public float typingSpeed = 0.05f;

    private Queue<Dialogue> _dialogues;
    private bool _isTyping;
    private Action _onDialogueComplete;

    private void Awake()
    {
        _dialogues = new Queue<Dialogue>();
    }

    public void StartDialogue(Dialogues dialogues, Action onComplete = null)
    {
        _dialogues.Clear();
        _onDialogueComplete = onComplete;

        foreach (var dialogue in dialogues.dialogueList)
        {
            _dialogues.Enqueue(dialogue);
        }

        DisplayNextSentence();
    }
    
    public void DisplayNextSentence()
    {
        if (_isTyping) return;

        if (_dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        var dialogue = _dialogues.Dequeue();
        RefreshUI(dialogue);
        StartCoroutine(TypeSentence(dialogue.sentence));
    }

    private void RefreshUI(Dialogue dialogue)
    {
        dialogueText.text = "";
        nameText.text = dialogue.speakerName;

        if (dialogue.characterAvatar)
        {
            avatarImage.gameObject.SetActive(true);
            avatarImage.sprite = dialogue.characterAvatar;
        }
        else
        {
            avatarImage.gameObject.SetActive(false);
        }

        // if (dialogue.image)
        // {
        //     image.gameObject.SetActive(true);
        //     image.sprite = dialogue.image;
        // }
        // else
        // {
        //     image.gameObject.SetActive(false);
        // }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _isTyping = true;
        dialogueText.text = "";
        foreach (var letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        _isTyping = false;
    }

    private void EndDialogue()
    {
        DialogueManager.Instance.EndDialogue();
        _onDialogueComplete?.Invoke();
        _onDialogueComplete = null;
    }
}