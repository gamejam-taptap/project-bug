using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class LevelManager : BaseManager<LevelManager> 
    {
        [Serializable]
        public class Scene
        {
            public string name;
            public GameObject prefab;
        }

        public GameObject sceneRoot;
        [Header("Scenes")]
        public List<Scene> scenes;

        private readonly Dictionary<string, GameObject> _sceneDict = new();

        private void Awake()
        {
            SetDoNotDestroyOnLoad();
        }

        private void Start()
        {
            foreach (var scene in scenes)
            {
                if (scene != null && scene.prefab != null)
                {
                    _sceneDict[scene.name] = scene.prefab;
                }
            }
        }

        public GameObject Load(string sceneName)
        {
            if (_sceneDict.TryGetValue(sceneName, out var prefab))
            {
                Debug.Log("[Level] 卸载当前");
                if (sceneRoot.transform.childCount > 0)
                {
                    Destroy(sceneRoot.transform.GetChild(0)?.gameObject);
                }
                
                Debug.Log($"[Level] {sceneName}加载");
                var scene = Instantiate(prefab, sceneRoot.transform);
                return scene;
            }

            Debug.LogWarning($"[Level] scene not found: {sceneName}");
            return null;
        }
    }
}