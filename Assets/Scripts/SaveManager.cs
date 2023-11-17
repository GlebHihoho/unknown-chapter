using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UI.InventoryScripts;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace DefaultNamespace.SaveLoadData
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _fileName;
        [SerializeField] private List<string> _allSaveName;
        [SerializeField] private string _currentSaveFile = "NewGames";

        public List<string> AllSaveName => _allSaveName;
        public string CurrentSaveFile => _currentSaveFile;

        private string savePath;
        private GameData gameData;

        public static SaveManager Instance;

        private void Awake()
        {
            if (_currentSaveFile == "")
            {
                _currentSaveFile = "NewGames";
            }
            Instance = this;
            // savePath = Application.persistentDataPath + "/gameData.json";
            savePath = Application.persistentDataPath + $"/{CurrentSaveFile}.json";
            LoadSaveNames();
            // LoadGameData();
        }

        public void LoadGameData(string saveFile)
        {
            _currentSaveFile = saveFile;
            // if (File.Exists(savePath))
            // {
                try
                {
                    // string saveFile = File.ReadAllText(savePath);
                    // gameData = JsonUtility.FromJson<GameData>(saveFile);
                    // // gameData.CharacterState.SetCharacterData(gameData.CharacterStateData);
                    // gameData.Inventory.SetInventoryDate(gameData.InventoryData);
                    
            
                    // string saveFile = File.ReadAllText(savePath);
                    // gameData = JsonUtility.FromJson<GameData>(saveFile);


                    string json = File.ReadAllText(Application.dataPath + $"/{_currentSaveFile}.json");
            
                    
                    // string saveFile = File.ReadAllText(savePath);
                    gameData = JsonConvert.DeserializeObject<GameData>(json);
            
                    if (gameData.InventoryData != null)
                    {
                        if (gameData.InventoryData.ItemInventory == null)
                        {
                            gameData.InventoryData.ItemInventory = new List<ItemInventory>();
                        }
            
                        gameData.Inventory.SetInventoryDate(gameData.InventoryData);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Error loading game data: " + e.Message);
                    gameData = CreateDefaultGameData();
                }
            // }
            // else
            // {
            //     // gameData = CreateDefaultGameData();
            //     print("Файла нет");
            // }
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadSceneAsync(currentSceneName);
        }

        public void SaveGameData()
        {
            gameData = CreateDefaultGameData();

            try
            {
                // gameData.CharacterStateData = gameData.CharacterState.GetCharacterData();
                gameData.InventoryData = gameData.Inventory.GetInventoryDate();
                string path = Application.dataPath + $"/{_fileName.text}.json";
                File.WriteAllText(path, JsonConvert.SerializeObject(gameData.Inventory._data._items));
                // File.WriteAllText(path, JsonConvert.SerializeObject(gameData.InventoryData.ItemInventory));
                _allSaveName.Add(_fileName.text);
                SaveSaveNames();
                _fileName.text = "";
            }
            catch (Exception e)
            {
                Debug.LogError("Error saving game data: " + e.Message);
            }
        }

        private GameData CreateDefaultGameData()
        {
            return new GameData
            {
                // CharacterState = FindObjectOfType<CharacterState>(),
                Inventory = FindObjectOfType<Inventory>(true),
            };
        }
        
        private void LoadSaveNames()
        {
            // Загрузка списка сохраненных имен из файла
            string saveNamesPath = Application.persistentDataPath + "/saveNames.json";

            if (File.Exists(saveNamesPath))
            {
                string saveNamesJson = File.ReadAllText(saveNamesPath);
                _allSaveName = JsonConvert.DeserializeObject<List<string>>(saveNamesJson);
            }
        }

        private void SaveSaveNames()
        {
            // Сохранение списка сохраненных имен в файл
            string saveNamesPath = Application.persistentDataPath + "/saveNames.json";
            File.WriteAllText(saveNamesPath, JsonConvert.SerializeObject(_allSaveName));
        }

        // private void OnApplicationQuit()
        // {
        //     SaveGameData();
        // }
        //
        // private bool isApplicationPaused = false;
        //
        // private void OnApplicationPause(bool pauseStatus)
        // {
        //     if (pauseStatus && !isApplicationPaused)
        //     {
        //         SaveGameData();
        //         isApplicationPaused = true;
        //     }
        //     else if (!pauseStatus && isApplicationPaused)
        //     {
        //         isApplicationPaused = false;
        //     }
        // }
    }
}

[Serializable]
public class GameData
{
    // public CharacterState CharacterState;
    // public CharacterStateData CharacterStateData;
    public InventoryData InventoryData;
    public Inventory Inventory;
}

// [Serializable]
// public class CharacterStateData
// {
//     public int CurrentHealth;
//     public int MaxHealth;
//     public int Damage;
//     public int Armor;
//     public int AmmoCount;
// }

[Serializable]
public class InventoryData
{
    public List<ItemInventory> ItemInventory;
    public List<Item> ItemsDB;
}