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

    const string quickSave = "QuickSave";

    public const string quickSaveLabel = "Áûסענ.";
    public const string autoSaveLabel = "Àגעמ";

    string path;

    SaveData.Save save;


    public static event Action OnLoadCompleted;


    public struct Summary
    {
        public string location;
        public DateTime timeStamp;
        public SaveData.Type type;
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


    private void Start()
    {
        GameControls.instance.OnQuicksave += QuickSave;
        GameControls.instance.OnQuickload += QuickLoad;
    }

    private void OnDestroy()
    {
        GameControls.instance.OnQuicksave -= QuickSave;
        GameControls.instance.OnQuickload -= QuickLoad;
    }


    private void QuickSave(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Save(SaveData.Type.Quick);
    }

    private void QuickLoad(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Load(quickSave);
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


    private void Save() => Save(SaveData.Type.Normal);

    private void Save(SaveData.Type type = SaveData.Type.Normal)
    {
        DateTime timestamp = DateTime.Now;

        string saveName;

        if (type == SaveData.Type.Quick) saveName = quickSave;
        else saveName = timestamp.ToString().Replace(":", "_").Replace(".","-");


        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        string fileName = path + Path.DirectorySeparatorChar + saveName + extension;

        save = new();

        save.type = type;

      
        save.timeStamp = timestamp.ToString();

        save.dialogues = saver.RecordData();

        save.inventory.Clear();

        ISaveable[] saveables = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<ISaveable>().ToArray();


        foreach (ISaveable item in saveables)
        {
            item.Save(ref save);
        }

        save.location = Environment.ZoneManager.instance.GetZoneLabel(save.zoneID);


        File.WriteAllText(fileName, JsonUtility.ToJson(save));


        Summary info;
        info.location = save.location;
        info.timeStamp = timestamp;
        info.type = save.type;


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

            save = new();

            save = JsonUtility.FromJson<SaveData.Save>(File.ReadAllText(fileName));

            saver.ApplyData(save.dialogues);


            ISaveable[] saveables = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<ISaveable>().ToArray();

            foreach (ISaveable item in saveables)
            {
                item.Load(save);
            }

        }

        OnLoadCompleted?.Invoke();
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


            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles().OrderBy(f => f.LastWriteTime).ToArray();


            foreach (FileInfo file in files)
            {
                string filename = file.FullName;

                save = JsonUtility.FromJson<SaveData.Save>(File.ReadAllText(filename));

                DateTime timestamp;

                if (DateTime.TryParse(save.timeStamp, out timestamp))
                {
                    Summary summary;

                    string name = Path.GetFileNameWithoutExtension(filename);
                    summary.location = save.location;
                    summary.timeStamp = timestamp;
                    summary.type = save.type;

                    SavesInfo.Add(name, summary);
                }
            }
        }
    }


}
