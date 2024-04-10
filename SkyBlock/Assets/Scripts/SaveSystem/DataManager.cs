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
    public static DataManager Inst;

    string path;

    private void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "database.json");
        JsonLoad();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            JsonSave();
        }
    }

    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            StageManager.Inst.clearStage = 0;
            StageManager.Inst.clearStars1.Add(false);
            StageManager.Inst.clearStars2.Add(false);
            StageManager.Inst.clearStars3.Add(false);
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                StageManager.Inst.clearStage = saveData.clearStage;
                StageManager.Inst.clearStars1 = saveData.clearStars1;
                StageManager.Inst.clearStars2 = saveData.clearStars2;
                StageManager.Inst.clearStars3 = saveData.clearStars3;
            }
        }
    }

    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        saveData.clearStage = StageManager.Inst.clearStage;
        saveData.clearStars1 = StageManager.Inst.clearStars1;
        saveData.clearStars2 = StageManager.Inst.clearStars2;
        saveData.clearStars3 = StageManager.Inst.clearStars3;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }

   
}