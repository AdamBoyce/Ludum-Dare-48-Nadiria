using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public SaveData Data;

    public string SavePath => $"{Application.persistentDataPath}/Nadiria.ndsv";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public bool HasSavedGame() => File.Exists(SavePath);
    private void Save() => File.WriteAllText(SavePath, JsonUtility.ToJson(Data));

    public void Continue()
    {
        string json = File.ReadAllText(SavePath);
        Data = JsonUtility.FromJson<SaveData>(json);
    }

    public void NewGame()
    {
        Data = new SaveData();
        Save();
    }

    public void Checkpoint(int index)
    {
        Data.ActiveCheckpoint = index;
        Save();
    }
    
    public void GetTreasureOne()  
    {
        Data.TreasureOne = true;
        Save();
    }    

    public void GetTreasureTwo()  
    {
        Data.TreasureTwo = true;
        Save();
    }    

    public void GetTreasureThree()
    {
        Data.TreasureThree = true;
        Save();
    }   

    public void GetTreasureFour()
    {
        Data.TreasureFour = true;
        Save();
    }

    public void OnDefeated()
    {
        ++Data.Deaths;
        Save();
    }
}