using System;

public class DataManager : BaseManager<DataManager>
{
    [Serializable]
    public class GameData
    {
        public string currentSceneName = "ward";
    }
    
    private GameData _gameData;
    
    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    private void Start()
    {
        _gameData = DataUtility.LoadData<GameData>();
    }

    public void OnReset()
    {
        _gameData = new GameData();
    }

    private void SaveData()
    {
        DataUtility.SaveData(_gameData);
    }

    public string GetCurrentSceneName()
    {
        return _gameData.currentSceneName;
    }

    public void SetCurrentSceneName(string sceneName)
    {
        _gameData.currentSceneName = sceneName;
        SaveData();
    }
}