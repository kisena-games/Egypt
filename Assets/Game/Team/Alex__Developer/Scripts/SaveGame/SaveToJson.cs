using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveToJson : MonoBehaviour
{
    [System.Serializable]
    private class PlayerData
    {
        public int level;

    }
    private string filePath = "playerData.json";
    private PlayerData _playerData = new PlayerData();

    void Awake()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
       
            switch (index)
            {
                case 1: SetLevel(1); break;
                case 2: SetLevel(2); break;
                case 3: SetLevel(4); break;
                case 6: SetLevel(7); break;
                case 9: SetLevel(10); break;
                case 12: SetLevel(13); break;
                case 15: SetLevel(16); break;
                case 18: SetLevel(19); break;
                case 21: SetLevel(22); break;
            }
        if (GetLevel() == 0)
        {
            _playerData.level = 1;
            SavePlayerDataToJson();
        }

    }
    private void SavePlayerDataToJson()
    {
        string jsonData = JsonUtility.ToJson(_playerData);
        File.WriteAllText(Application.dataPath+"/"+filePath, jsonData);
    }
    private void LoadPlayerDataFromJson()
    {
        string jsonData = File.ReadAllText(Application.dataPath + "/" + filePath);
        _playerData=JsonUtility.FromJson<PlayerData>(jsonData);
    }

    public void SetLevel(int level)
    {
        _playerData.level = level;
        SavePlayerDataToJson();
    }

    public int GetLevel()
    {
        LoadPlayerDataFromJson();
        return _playerData.level;
    }
}
