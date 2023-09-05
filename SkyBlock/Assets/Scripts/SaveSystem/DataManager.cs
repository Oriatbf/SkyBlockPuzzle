using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int clearStage;
    public List<bool> clearStars1 = new List<bool>();
    public List<bool> clearStars2 = new List<bool>();
    public List<bool> clearStars3 = new List<bool>();
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    string path;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        path = Path.Combine(Application.dataPath, "database.json");
        JsonLoad();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            JsonSave();
        }
    }

    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            StageManager.instance.clearStage = 0;
            StageManager.instance.clearStars1.Add(false);
            StageManager.instance.clearStars2.Add(false);
            StageManager.instance.clearStars3.Add(false);
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                StageManager.instance.clearStage = saveData.clearStage;
                StageManager.instance.clearStars1 = saveData.clearStars1;
                StageManager.instance.clearStars2 = saveData.clearStars2;
                StageManager.instance.clearStars3 = saveData.clearStars3;
            }
        }
    }

    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        saveData.clearStage = StageManager.instance.clearStage;
        saveData.clearStars1 = StageManager.instance.clearStars1;
        saveData.clearStars2 = StageManager.instance.clearStars2;
        saveData.clearStars3 = StageManager.instance.clearStars3;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }
}