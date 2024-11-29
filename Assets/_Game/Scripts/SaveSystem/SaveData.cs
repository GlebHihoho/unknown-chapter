using System.Collections.Generic;
using System;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class SaveData
{

    public enum Type { Normal, Quick, Auto}

    [Serializable]
    public struct InventoryItem
    {
        public int id;
        public int quantity;
    }

    [Serializable]
    public struct MapZones
    {
        public string name;
        public bool locked;
    }

    [Serializable]
    public struct RecordStatus
    {
        public string id;
        public QuestState state;
        public List<int> entriesChanged;
    }

    [Serializable]
    public struct Character
    {
        public int talkIndex;
    }

    [Serializable]
    public class Level
    {
        public Vector3 playerPosition = Vector3.zero;
        public Quaternion playerRotation = Quaternion.identity;

        public List<MapZones> mapZones = new List<MapZones>();

        public string zoneID = string.Empty;

        public List<bool> collectibles = new List<bool>();

        public List<bool> containers = new List<bool> ();

        public List<Character> characters = new List<Character>();
    }


    public class Save
    {
        public Type type = Type.Normal;

        public bool isNewGame = true;

        public int level = 0;
        public List<Level> levels = new List<Level>();

        public string location = string.Empty;

        public string timeStamp = string.Empty;

        public bool mapUnlocked = false;

        public float timeOfDay = 0;

        public double physical = 0;
        public double perception = 0;
        public double intellect = 0;

        public string dialogues = string.Empty;

        public string journalActiveQuest = string.Empty;
        public List<string> journalRecords = new List<string>();
        public List<RecordStatus> recordStatus = new List<RecordStatus>();

        public List<InventoryItem> inventory = new List<InventoryItem>();

        public bool wallOfMistActive = true;
    }

}
