using Managers;
using UnityEngine;

namespace Scenes
{
    public class Level1 : MonoBehaviour
    {
        public Dialogues dialogueStart;
        
        public void Start()
        {
            AudioManager.Instance.PlayBGM("level1");
            DialogueManager.Instance.StartDialogue(dialogueStart, OnComplete);
        }

        private void OnComplete()
        {
            
        }
    }
}