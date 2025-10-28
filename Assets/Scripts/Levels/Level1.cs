using Managers;
using UnityEngine;

namespace Scenes
{
    public class Level1 : MonoBehaviour
    {
        public Stories fail;
        
        public void Start()
        {
            AudioManager.Instance.PlayBGM("level1");
            //StoryManager.Instance.StartStory(fail, OnComplete);
            CharacterManager.Instance.ShowPlayer();
        }

        private void OnComplete()
        {
            
        }

        public void onClickDoor()
        {
            StoryManager.Instance.StartStory(fail, OnComplete);
        }
    }
}