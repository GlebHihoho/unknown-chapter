using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UI.InventoryScripts;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.SaveLoadData
{
    public class SaveManager : MonoBehaviour
    {
        private string savePath;
        private GameData gameData;

        public static SaveManager Instance;

        private void Awake()
        {
            Instance = this;
            savePath = Application.persistentDataPath + "/gameData.json";
            // LoadGameData();
        }

        public void LoadGameData()
        {
            // if (File.Exists(savePath))
            // {
                try
                {
                    // string saveFile = File.ReadAllText(savePath);
                    // gameData = JsonUtility.FromJson<GameData>(saveFile);
                    // // gameData.CharacterState.SetCharacterData(gameData.CharacterStateData);
                    // gameData.Inventory.SetInventoryDate(gameData.InventoryData);
                    

                    string saveFile = File.ReadAllText(savePath);
                    gameData = JsonUtility.FromJson<GameData>(saveFile);
                    
                    
                    string json = File.ReadAllText(Application.dataPath + "/items.json");

                    
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
                string path = Application.dataPath + "/itemsss.json";
                File.WriteAllText(path, JsonConvert.SerializeObject(gameData.Inventory._data._items));
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