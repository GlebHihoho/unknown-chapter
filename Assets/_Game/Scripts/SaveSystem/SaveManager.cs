using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class SaveManager : MonoBehaviour
{

    public static SaveManager instance;

    PixelCrushers.DialogueSystem.DialogueSystemSaver saver;

    const string savesFolder = "Saves";
    const string extension = ".dat";

    string path;

    SaveData.Save save = new SaveData.Save();


    public struct Summary
    {
        public string location;
        public DateTime timeStamp;
    }

    public Dictionary<string, Summary> SavesInfo { get; private set; } = new Dictionary<string, Summary>();


    public static event Action<string> OnSaveAdded;
    public static event Action<string> OnSaveRemoved;

    public static event Action OnStartingLoad;

    private void Awake()
    {

        if (instance == null) instance = this;

        saver = GetComponent<PixelCrushers.DialogueSystem.DialogueSystemSaver>();

        path = Application.persistentDataPath + Path.DirectorySeparatorChar + savesFolder;

        RefreshSavesInfo();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save("TestSave");
        }

        else if (Input.GetKeyDown(KeyCode.F6))
        {
            Load("TestSave");
        }
    }


    private void OnEnable()
    {
        SaveEntryUI.OnSave += Save;
        SaveEntryUI.OnLoad += Load;
        SaveEntryUI.OnDelete += Delete;
    }

    private void OnDisable()
    {
        SaveEntryUI.OnSave -= Save;
        SaveEntryUI.OnLoad -= Load;
        SaveEntryUI.OnDelete -= Delete;
    }



    private void Save(string saveName)
    {

        if (saveName == "") return;

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        string fileName = path + Path.DirectorySeparatorChar + saveName + extension;


        save.location = "Somewhere";

        DateTime timestamp = DateTime.Now;

        save.timeStamp = timestamp.ToString();

        save.dialogues = saver.RecordData();

        save.inventory.Clear();

        ISaveable[] saveables = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveable>().ToArray();


        foreach (ISaveable item in saveables)
        {
            item.Save(ref save);
        }


        File.WriteAllText(fileName, JsonUtility.ToJson(save));


        Summary info;
        info.location = save.location;
        info.timeStamp = timestamp;


        if (SavesInfo.ContainsKey(saveName)) SavesInfo[saveName] = info;
        else SavesInfo.Add(saveName, info);

        OnSaveAdded?.Invoke(saveName);


        Debug.Log("Saved: " + fileName);
    }


    private void Load(string saveName)
    {
        string fileName = path + Path.DirectorySeparatorChar + saveName + extension;

        if (File.Exists(fileName))
        {

            OnStartingLoad?.Invoke();

            save = JsonUtility.FromJson<SaveData.Save>(File.ReadAllText(fileName));

            saver.ApplyData(save.dialogues);


            ISaveable[] saveables = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveable>().ToArray();

            foreach (ISaveable item in saveables)
            {
                item.Load(save);
            }

        }
    }


    private void Delete(string saveName)
    {
        string fileName = path + Path.DirectorySeparatorChar + saveName + extension;

        if (File.Exists(fileName))
        {
            File.Delete(fileName);

            SavesInfo.Remove(saveName);

            OnSaveRemoved?.Invoke(saveName);
        }
    }


    private void RefreshSavesInfo()
    {
        SavesInfo.Clear();

        if (Directory.Exists(path))
        {

            string[] filenames = Directory.GetFiles(path);

            foreach (string filename in filenames)
            {
                save = JsonUtility.FromJson<SaveData.Save>(File.ReadAllText(filename));

                DateTime timestamp;

                if (DateTime.TryParse(save.timeStamp, out timestamp))
                {
                    Summary info;

                    string name = Path.GetFileNameWithoutExtension(filename);
                    info.location = save.location;
                    info.timeStamp = timestamp;

                    SavesInfo.Add(name, info);
                }
            }
        }
    }


}
