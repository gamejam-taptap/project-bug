using System.IO;
using UnityEngine;

public static class DataUtility
{
    private static readonly string SavePath = Application.persistentDataPath + "/save.json";
    
    public static void SaveData<T>(T data) where T : class
    {
        try
        {
            // 将数据转换为JSON字符串
            var jsonData = JsonUtility.ToJson(data);
            
            // 写入文件
            File.WriteAllText(SavePath, jsonData);
            
            Debug.Log("[Data] Save Succeed: " + SavePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("[Data] Save Failed: " + e.Message);
        }
    }

    public static T LoadData<T>() where T : class, new()
    {
        if (File.Exists(SavePath))
        {
            try
            {
                var jsonData = File.ReadAllText(SavePath);
                var loadedData = JsonUtility.FromJson<T>(jsonData);
                Debug.Log("[Data] Load Succeed: " + SavePath);
                return loadedData;
            }
            catch (System.Exception e)
            {
                Debug.LogError("[Data] Load Failed: " + e.Message);
                return new T(); // 返回新存档
            }
        }

        Debug.Log("[Data] Load New");
        return new T();
    }

    public static void ClearSave()
    {
        if (!File.Exists(SavePath)) return;
        File.Delete(SavePath);
        Debug.Log("存档已删除");
    }
}